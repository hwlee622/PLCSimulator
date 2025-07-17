using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace CommInterface
{
    public class CommPipeClient : Comm
    {
        private string m_pipeName;

        private NamedPipeClientStream m_pipeClient;

        public CommPipeClient(string pipeName)
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

            m_pipeClient?.Close();
        }

        public override bool IsConnected()
        {
            bool isConnected = m_pipeClient != null && m_pipeClient.IsConnected;
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
                        m_pipeClient?.Dispose();

                        m_pipeClient = new NamedPipeClientStream(".", m_pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
                        Task connect = m_pipeClient.ConnectAsync();
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
            await m_pipeClient.WriteAsync(message, 0, message.Length);
        }

        protected override async Task ReadAsync()
        {
            var buffer = new byte[m_pipeClient.InBufferSize];
            int bufferLength = await m_pipeClient.ReadAsync(buffer, 0, buffer.Length);
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