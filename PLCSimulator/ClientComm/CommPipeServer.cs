using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace CommInterface
{
    public class CommPipeServer : Comm
    {
        private string m_pipeName;

        private NamedPipeServerStream m_pipeServer;

        #region Constructors

        public CommPipeServer(string pipeName)
        {
            m_pipeName = pipeName;
        }

        #endregion Constructors

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

        protected override async Task WriteAsync(byte[] message)
        {
            OnSendLog?.Invoke(message);
            await m_pipeServer.WriteAsync(message, 0, message.Length);
        }

        protected override async Task ReadAsync()
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