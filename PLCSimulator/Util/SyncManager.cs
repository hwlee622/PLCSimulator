using CommInterface;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public class SyncManager
    {
        #region Singleton

        public static SyncManager Instance
        { get { return InstanceHolder.Instance; } }

        private static class InstanceHolder
        {
            public static SyncManager Instance = new SyncManager();
        }

        private SyncManager()
        {
        }

        #endregion Singleton

        #region SyncComm

        public class SyncComm : IDisposable
        {
            private string[] m_address;
            private string m_pipeName;
            private ushort[] m_preData;

            private Comm m_comm;
            private CancellationTokenSource m_cts = new CancellationTokenSource();

            public SyncComm(string[] address, string pipeName, bool server)
            {
                m_address = address;
                m_pipeName = pipeName;
                m_preData = new ushort[m_address.Length];

                if (server)
                    m_comm = new CommPipeServer(m_pipeName);
                else
                    m_comm = new CommPipeClient(m_pipeName);
                m_comm.SetSTX(Encoding.ASCII.GetBytes(new char[] { (char)0x02 }));
                m_comm.SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x03 }));
                m_comm.OnReceiveMessage += OnReceiveMessage;
                m_comm.Start();
                Task.Run(() => SendMessage(m_cts.Token));
            }

            public void Dispose()
            {
                m_cts.Cancel();
                if (m_comm != null)
                {
                    m_comm.OnReceiveMessage -= OnReceiveMessage;
                    m_comm.Stop();
                }
                m_comm = null;
            }

            public bool IsConnected()
            {
                return m_comm != null && m_comm.IsConnected();
            }

            private void OnReceiveMessage(byte[] bytes)
            {
                try
                {
                    var message = Encoding.ASCII.GetString(bytes).Trim(new char[] { (char)0x02, (char)0x03 });
                    var length = message.Length / 4;
                    length = Math.Min(length, m_address.Length);
                    for (int i = 0; i < length; i++)
                    {
                        var address = m_address[i];
                        var value = Convert.ToUInt16(message.Substring(i * 4, 4), 16);
                        if (m_preData[i] == value)
                            continue;

                        if (string.IsNullOrEmpty(address))
                            continue;

                        if (Util.ValidWordAddress(address, out var code, out var index))
                        {
                            DataManager.Instance.WordDataDict[code].SetData(index, new ushort[] { value });
                        }
                        else if (Util.ValidBitAddress(address, out code, out index))
                        {
                            DataManager.Instance.BitDataDict[code].SetData(index, new bool[] { value > 0 });
                        }
                        m_preData[i] = value;
                    }
                }
                catch { }
            }

            private async Task SendMessage(CancellationToken token)
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(20);
                        if (token.IsCancellationRequested)
                            continue;

                        var sb = new StringBuilder();
                        foreach (var address in m_address)
                        {
                            if (string.IsNullOrEmpty(address))
                                continue;

                            if (Util.ValidWordAddress(address, out var code, out var index))
                            {
                                var data = DataManager.Instance.WordDataDict[code].GetData(index, 1)[0];
                                var byteData = BitConverter.GetBytes(data);
                                if (!BitConverter.IsLittleEndian)
                                    Array.Reverse(byteData);
                                sb.Append($"{byteData[1]:X2}{byteData[0]:X2}");
                            }
                            else if (Util.ValidBitAddress(address, out code, out index))
                            {
                                var data = DataManager.Instance.BitDataDict[code].GetData(index, 1)[0];
                                var byteData = BitConverter.GetBytes(data);
                                if (!BitConverter.IsLittleEndian)
                                    Array.Reverse(byteData);
                                sb.Append($"00{byteData[0]:X2}");
                            }
                        }

                        var message = $"{(char)0x02}{sb.ToString()}{(char)0x03}";
                        var bytes = Encoding.ASCII.GetBytes(message);
                        m_comm.SendMessage(bytes);
                    }
                    catch { }
                }
            }
        }

        #endregion SyncComm

        private SyncComm m_nextSyncComm;
        private SyncComm m_prevSyncComm;

        public void ReConnect(string prevSyncName, string nextSyncName)
        {
            DisconnectAll();
            ConnectAll(prevSyncName, nextSyncName);
        }

        private void DisconnectAll()
        {
            m_nextSyncComm?.Dispose();
            m_prevSyncComm?.Dispose();
            m_nextSyncComm = null;
            m_prevSyncComm = null;
        }

        private void ConnectAll(string prevSyncName, string nextSyncName)
        {
            var info = ProfileRecipe.Instance.ProfileInfo.SyncManagerInfo;
            if (!string.IsNullOrEmpty(prevSyncName))
                m_prevSyncComm = new SyncComm(info.PrevSyncAddress.ToArray(), prevSyncName, true);
            if (!string.IsNullOrEmpty(nextSyncName))
                m_nextSyncComm = new SyncComm(info.NextSyncAddress.ToArray(), nextSyncName, false);
        }

        public void DisConnectPrevSync()
        {
            m_prevSyncComm?.Dispose();
            m_prevSyncComm = null;
        }

        public void DisconnectNextSync()
        {
            m_nextSyncComm?.Dispose();
            m_nextSyncComm = null;
        }

        public void ConnectPrevSync(string prevSyncName)
        {
            var info = ProfileRecipe.Instance.ProfileInfo.SyncManagerInfo;
            if (!string.IsNullOrEmpty(prevSyncName))
                m_prevSyncComm = new SyncComm(info.PrevSyncAddress.ToArray(), prevSyncName, true);
        }

        public void ConnectNextSync(string nextSyncName)
        {
            var info = ProfileRecipe.Instance.ProfileInfo.SyncManagerInfo;
            if (!string.IsNullOrEmpty(nextSyncName))
                m_nextSyncComm = new SyncComm(info.NextSyncAddress.ToArray(), nextSyncName, false);
        }

        public bool IsPrevConnected()
        {
            return m_prevSyncComm != null && m_prevSyncComm.IsConnected();
        }

        public bool IsNextConnected()
        {
            return m_nextSyncComm != null && m_nextSyncComm.IsConnected();
        }

        public bool IsPrevOpened()
        {
            return m_prevSyncComm != null;
        }

        public bool IsNextOpened()
        {
            return m_nextSyncComm != null;
        }
    }
}