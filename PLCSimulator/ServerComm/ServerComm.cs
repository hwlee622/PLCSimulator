using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace YJComm
{
    public abstract class ServerComm
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

        #endregion Properties

        protected Dictionary<Type, bool> m_connectExceptionDict = new Dictionary<Type, bool>();

        protected CancellationTokenSource m_cts = new CancellationTokenSource();

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

        public abstract void SendMessage(byte[] message);

        protected abstract Task ConnectTask(CancellationToken token);

        protected abstract Task SendTask(CancellationToken token);

        protected abstract Task RecvTask(CancellationToken token);

        protected abstract Task BufferTask(CancellationToken token);

        protected void ParseMessage(ref List<byte> buffer)
        {
            int stxLength = STX != null ? STX.Length : 0;
            int etxLength = ETX != null ? ETX.Length : 0;

            int stxIndex = FindByteIndex(buffer, STX);
            stxIndex = stxIndex >= 0 ? stxIndex : 0;
            int etxIndex = FindByteIndex(buffer, ETX);
            etxIndex = ETX != null ? etxIndex : buffer.Count - etxLength;

            while (etxIndex != -1)
            {
                int length = etxIndex + etxLength - stxIndex;
                var message = buffer.Skip(stxIndex).Take(length).ToArray();
                buffer = buffer.Skip(etxIndex + etxLength).ToList();

                stxIndex = FindByteIndex(buffer, STX);
                stxIndex = stxIndex >= 0 ? stxIndex : 0;
                etxIndex = FindByteIndex(buffer, ETX);
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