using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace YJComm
{
    public class ServerCommUdp : ServerComm
    {
        private class UdpPacket
        {
            public UdpPacket(IPEndPoint endPoint, byte[] data)
            {
                EndPoint = endPoint;
                Data = data;
            }

            public IPEndPoint EndPoint { get; set; }
            public byte[] Data { get; set; }
        }

        private int m_port;

        private UdpClient m_udpClient;

        private ConcurrentQueue<UdpPacket> m_sendQueue = new ConcurrentQueue<UdpPacket>();
        private ConcurrentQueue<UdpPacket> m_recvQueue = new ConcurrentQueue<UdpPacket>();
        private Dictionary<IPEndPoint, List<byte>> m_bufferDict = new Dictionary<IPEndPoint, List<byte>>();
        private IPEndPoint m_currentEndPoint = null;

        public ServerCommUdp(int port)
        {
            m_port = port;
        }

        public override void Start()
        {
            Stop();
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
            m_udpClient?.Close();
        }

        public override bool IsConnected()
        {
            bool isConnected = m_udpClient != null && m_udpClient.Client != null;
            return isConnected;
        }

        public override void SendMessage(byte[] message)
        {
            if (IsConnected())
                m_sendQueue.Enqueue(new UdpPacket(m_currentEndPoint, message));
        }

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
                        if (!m_bufferDict.TryGetValue(message.EndPoint, out var buffer))
                            buffer = new List<byte>();

                        m_currentEndPoint = message.EndPoint;
                        buffer.AddRange(message.Data);
                        ParseMessage(ref buffer);
                        m_bufferDict[message.EndPoint] = buffer;

                        var keys = m_bufferDict.Where(x => x.Value.Count == 0).Select(x => x.Key).ToList();
                        foreach (var key in keys)
                            m_bufferDict.Remove(key);
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
                        m_udpClient = new UdpClient(m_port);
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
                    if (m_sendQueue.Count == 0)
                        await Task.Delay(20);
                    else if (m_sendQueue.TryDequeue(out var message) && IsConnected())
                    {
                        Task write = WriteAsync(message);
                        await Task.WhenAny(write, cancel);
                        if (token.IsCancellationRequested)
                            break;

                        write.GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }
            }
        }

        private async Task WriteAsync(UdpPacket message)
        {
            OnSendLog?.Invoke(message.Data);
            m_udpClient.Client.SendTimeout = WriteTimeout;
            await m_udpClient.SendAsync(message.Data, message.Data.Length, message.EndPoint);
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
                            break;

                        read.GetAwaiter().GetResult();
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
            var recvResult = await m_udpClient.ReceiveAsync();
            if (recvResult != null && recvResult.Buffer.Length > 0)
            {
                var clientIp = recvResult.RemoteEndPoint;
                var message = new byte[recvResult.Buffer.Length];
                Array.Copy(recvResult.Buffer, message, recvResult.Buffer.Length);
                OnReceiveLog?.Invoke(message);
                m_recvQueue.Enqueue(new UdpPacket(clientIp, message));
            }
        }
    }
}