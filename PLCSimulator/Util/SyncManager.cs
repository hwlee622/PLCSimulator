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

        #region InputComm
        public class InputComm : IDisposable
        {
            private string[] m_address;
            private string m_pipeName;

            private Comm m_comm;
            private CancellationTokenSource m_cts = new CancellationTokenSource();

            public InputComm(string[] address, string pipeName)
            {
                m_address = address;
                m_pipeName = pipeName;

                m_comm = new CommPipeServer(m_pipeName);
                m_comm.SetSTX(Encoding.ASCII.GetBytes(new char[] { (char)0x02 }));
                m_comm.SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x03 }));
                m_comm.OnReceiveMessage += OnRecevieMessage;
                m_comm.Start();
                Task.Run(() => SendMessage(m_cts.Token));
            }

            public void Dispose()
            {
                m_cts.Cancel();
                if (m_comm != null)
                {
                    m_comm.OnReceiveMessage -= OnRecevieMessage;
                    m_comm.Stop();
                }
                m_comm = null;
            }

            private void OnRecevieMessage(byte[] bytes)
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

                        var message = $"{(char)0x02}{(char)0x03}";
                        var bytes = Encoding.ASCII.GetBytes(message);
                        m_comm.SendMessage(bytes);
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region OutputComm
        private class OutputComm : IDisposable
        {
            private string[] m_address;
            private string m_pipeName;

            private Comm m_comm;
            private CancellationTokenSource m_cts = new CancellationTokenSource();

            public OutputComm(string[] address, string pipeName)
            {
                m_address = address;
                m_pipeName = pipeName;

                m_comm = new CommPipeClient(m_pipeName);
                m_comm.SetSTX(Encoding.ASCII.GetBytes(new char[] { (char)0x02 }));
                m_comm.SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x03 }));
                m_comm.Start();
                Task.Run(() => SendMessage(m_cts.Token));
            }

            public void Dispose()
            {
                m_cts.Cancel();
                if (m_comm != null)
                {
                    m_comm.Stop();
                }
                m_comm = null;
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
        #endregion

        private OutputComm m_outputComm;
        private InputComm m_inputComm;

        public void ReConnect()
        {
            DisconnectAll();
            ConnectAll();
        }

        private void DisconnectAll()
        {
            m_outputComm?.Dispose();
            m_inputComm?.Dispose();
        }

        private void ConnectAll()
        {
            var info = ProfileRecipe.Instance.ProfileInfo.SyncMasterInfo;
            m_outputComm = new OutputComm(info.OutputAddress.ToArray(), info.SyncPlc);
            m_inputComm = new InputComm(info.InputAddress.ToArray(), info.MyPlc);
        }
    }
}