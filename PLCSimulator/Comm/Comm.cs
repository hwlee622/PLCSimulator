using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace YJComm
{
    // Comm 1.0.3
    public abstract class Comm
    {
        public Action<bool> OnConnectedOrDisconnected;
        public Action<byte[]> OnSendLog;
        public Action<byte[]> OnReceiveLog;
        public Action<Exception> OnError;
        public Action<byte[]> OnReceiveMessage;

        public int WriteTimeout = Timeout.Infinite;

        // 무한 대기 하다 송신 데이터가 있을 시 송신 이벤트를 발생 하는 방식이라 필요 없음
        public int ReadTimeout = Timeout.Infinite;

        #region Properties

        private object m_stxLock = new object();
        private bool m_stxSetted = false;
        private byte[] STX;
        private object m_etxLock = new object();
        private bool m_etxSetted = false;
        private byte[] ETX;

        protected ConcurrentQueue<byte[]> m_sendQueue = new ConcurrentQueue<byte[]>();
        protected ConcurrentQueue<byte[]> m_recvQueue = new ConcurrentQueue<byte[]>();

        protected Dictionary<Type, bool> m_connectExceptionDict = new Dictionary<Type, bool>();

        protected CancellationTokenSource m_cts = new CancellationTokenSource();

        #endregion Properties

        public virtual void Start()
        {
            m_cts = new CancellationTokenSource();
            Task.Run(() => BufferTask(m_cts.Token));
            Task.Run(() => ConnectTask(m_cts.Token));
            Task.Run(() => SendTask(m_cts.Token));
            Task.Run(() => RecvTask(m_cts.Token));
        }

        public virtual void Stop()
        {
            m_cts.Cancel();
            m_connectExceptionDict.Clear();
        }

        public abstract bool IsConnected();

        public void SetSTX(byte[] stx)
        {
            lock (m_stxLock)
            {
                if (!m_stxSetted)
                {
                    m_stxSetted = true;
                    STX = stx;
                }
            }
        }

        public void SetETX(byte[] etx)
        {
            lock (m_etxLock)
            {
                if (!m_etxSetted)
                {
                    m_etxSetted = true;
                    ETX = etx;
                }
            }
        }

        public void SendMessage(byte[] message)
        {
            if (IsConnected())
                m_sendQueue.Enqueue(message);
        }

        protected abstract Task ConnectTask(CancellationToken token);

        private async Task SendTask(CancellationToken token)
        {
            Task cancel = Task.Delay(Timeout.Infinite, token);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (m_sendQueue.Count == 0)
                        await Task.Delay(20);
                    else if (m_sendQueue.TryDequeue(out var message) && IsConnected())
                        await Task.WhenAny(WriteAsync(message), cancel);
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }
            }
        }

        protected abstract Task WriteAsync(byte[] message);

        private async Task RecvTask(CancellationToken token)
        {
            Task cancel = Task.Delay(Timeout.Infinite, token);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (!IsConnected())
                        await Task.Delay(20);
                    else
                        await Task.WhenAny(ReadAsync(), cancel);
                }
                catch (Exception ex)
                {
                    ReportError(ex);
                }
            }
        }

        protected abstract Task ReadAsync();

        protected async Task BufferTask(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(20, token);
                    while (m_recvQueue.TryDequeue(out byte[] message))
                        EnqueueReceiveMessage(message);
                }
                catch (OperationCanceledException ex) { }
                catch (Exception ex)
                {
                    OnError?.Invoke(ex);
                }
            }
        }

        private List<byte> m_buffer = new List<byte>();

        private void EnqueueReceiveMessage(byte[] buffer)
        {
            m_buffer.AddRange(buffer);
            int stxLength = STX != null ? STX.Length : 0;
            int etxLength = ETX != null ? ETX.Length : 0;

            int stxIndex = FindByteIndex(m_buffer, STX);
            stxIndex = stxIndex >= 0 ? stxIndex : 0;
            int etxIndex = FindByteIndex(m_buffer, ETX);
            etxIndex = ETX != null ? etxIndex : m_buffer.Count - etxLength;

            while (etxIndex != -1)
            {
                int length = etxIndex + etxLength - stxIndex;
                var message = m_buffer.Skip(stxIndex).Take(length).ToArray();
                m_buffer = m_buffer.Skip(etxIndex + etxLength).ToList();

                stxIndex = FindByteIndex(m_buffer, STX);
                stxIndex = stxIndex >= 0 ? stxIndex : 0;
                etxIndex = FindByteIndex(m_buffer, ETX);
                OnReceiveMessage?.Invoke(message);
            }
        }

        private int FindByteIndex(List<byte> buffer, byte[] pattern)
        {
            if (pattern != null)
            {
                for (int i = 0; i < buffer.Count; i++)
                {
                    if (buffer.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                        return i;
                }
            }
            return -1;
        }

        private bool m_isConnected;

        protected void UpdateConnectState(bool isConnected)
        {
            if (m_isConnected != isConnected)
                OnConnectedOrDisconnected?.Invoke(isConnected);
            m_isConnected = isConnected;
        }

        protected void ReportError(Exception ex)
        {
            var exType = ex.GetType();
            if (!m_connectExceptionDict.ContainsKey(exType))
            {
                OnError?.Invoke(ex);
                m_connectExceptionDict[exType] = true;
            }
        }
    }
}