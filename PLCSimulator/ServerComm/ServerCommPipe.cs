using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace CommInterface
{
    public class ServerCommPipe : ServerComm
    {
        private string m_pipeName;

        private NamedPipeServerStream m_pipeServer;

        private ConcurrentQueue<byte[]> m_sendQueue = new ConcurrentQueue<byte[]>();
        private ConcurrentQueue<byte[]> m_recvQueue = new ConcurrentQueue<byte[]>();

        public ServerCommPipe(string pipeName)
        {
            m_pipeName = pipeName;
        }

        public override void Start()
        {
            Stop();
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            m_pipeServer?.Close();
        }

        public override bool IsConnected()
        {
            bool isConnected = m_pipeServer != null && m_pipeServer.IsConnected;
            return isConnected;
        }

        public override void SendMessage(byte[] message)
        {
            if (IsConnected())
                m_sendQueue.Enqueue(message);
        }

        List<byte> m_buffer = new List<byte>();

        protected override async Task BufferTask(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (m_recvQueue.Count == 0)
                        await Task.Delay(20);
                    else if (m_recvQueue.TryDequeue(out var message))
                    {
                        m_buffer.AddRange(message);
                        ParseMessage(ref m_buffer);
                    }
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }
            }
        }

        protected override async Task ConnectTask(CancellationToken token)
        {
            Task cancel = Task.Delay(Timeout.Infinite, token);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(20);
                    bool isConnected = IsConnected();
                    UpdateConnectState(isConnected);
                    if (token.IsCancellationRequested)
                        continue;
                    else if (!isConnected)
                    {
                        m_pipeServer?.Dispose();

                        m_pipeServer = new NamedPipeServerStream(m_pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 4096, 4096);
                        Task connect = m_pipeServer.WaitForConnectionAsync();
                        await Task.WhenAny(connect, cancel);
                        if (token.IsCancellationRequested)
                            continue;

                        await connect;
                        m_connectExceptionDict.Clear();
                    }
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }
            }
        }

        protected override async Task SendTask(CancellationToken token)
        {
            Task cancel = Task.Delay(Timeout.Infinite, token);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (!m_sendQueue.TryDequeue(out var message) || !IsConnected())
                        await Task.Delay(20);
                    else
                    {
                        Task send = WriteAsync(message);
                        await Task.WhenAny(send, cancel);
                        if (token.IsCancellationRequested)
                            continue;
                        await send;
                    }
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }
            }
        }

        private async Task WriteAsync(byte[] message)
        {
            OnSendLog?.Invoke(message);
            await m_pipeServer.WriteAsync(message, 0, message.Length);
        }

        protected override async Task RecvTask(CancellationToken token)
        {
            Task cancel = Task.Delay(Timeout.Infinite, token);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (!IsConnected())
                        await Task.Delay(20);
                    else
                    {
                        Task read = ReadAsync();
                        await Task.WhenAny(read, cancel);
                        if (token.IsCancellationRequested)
                            continue;
                        await read;
                    }
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }
            }
        }

        private async Task ReadAsync()
        {
            var buffer = new byte[m_pipeServer.InBufferSize];
            int bufferLength = await m_pipeServer.ReadAsync(buffer, 0, buffer.Length);
            if (bufferLength > 0)
            {
                var message = new byte[bufferLength];
                Array.Copy(buffer, message, bufferLength);
                OnReceiveLog?.Invoke(message);
                m_recvQueue.Enqueue(message);
            }
        }
    }
}
