using CommInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
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
            Task.Run(() => SendSyncMessage());
        }

        #endregion Singleton

        private Comm[] m_inputComm = new Comm[10];
        private Comm[] m_outputComm = new Comm[10];

        public void ReConnect()
        {
            DisconnectAll();
            ConnectAll();
        }

        private void DisconnectAll()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_inputComm[i] != null)
                {
                    m_inputComm[i].Stop();
                    foreach (Action<byte[]> action in m_inputComm[i].OnReceiveMessage.GetInvocationList())
                        m_inputComm[i].OnReceiveMessage -= action;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (m_outputComm[i] != null)
                    m_outputComm[i].Stop();
            }
        }

        private void ConnectAll()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int inputIndex = i;
                    m_inputComm[i] = new CommPipeServer($"{ProfileRecipe.Instance.ProfileInfo.MyName}_{i:D2}");
                    m_inputComm[i].Start();
                    m_inputComm[i].OnReceiveMessage += OnRecevieMessage;
                    m_inputComm[i].SetSTX(Encoding.ASCII.GetBytes(new char[] { (char)0x02 }));
                    m_inputComm[i].SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x03 }));

                    void OnRecevieMessage(byte[] message)
                    {
                        try
                        {
                            var sValue = Encoding.ASCII.GetString(message).Trim(new char[] { (char)0x02, (char)0x03 });
                            var sAddress = ProfileRecipe.Instance.ProfileInfo.OutputSyncInfos[inputIndex].InputAddress;

                            if (Util.ValidWordAddress(sAddress, out var code, out var index))
                            {
                                ushort.TryParse(sValue, out var value);
                                DataManager.Instance.WordDataDict[code].SetData(index, new ushort[] { value });
                            }
                            else if (Util.ValidBitAddress(sAddress, out code, out index))
                            {
                                int.TryParse(sValue, out var value);
                                DataManager.Instance.BitDataDict[code].SetData(index, new bool[] { value > 0 });
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    m_outputComm[i] = new CommPipeClient($"{ProfileRecipe.Instance.ProfileInfo.OutputSyncInfos[i].OutputName}_{i:D2}");
                    m_outputComm[i].Start();
                    m_outputComm[i].SetSTX(Encoding.ASCII.GetBytes(new char[] { (char)0x02 }));
                    m_outputComm[i].SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x03 }));
                }
                catch { }
            }
        }

        private async Task SendSyncMessage()
        {
            while (true)
            {
                await Task.Delay(20);

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        var sAddress = ProfileRecipe.Instance.ProfileInfo.OutputSyncInfos[i].OutputAddress;
                        string message = string.Empty;
                        if (Util.ValidWordAddress(sAddress, out var code, out var index))
                        {
                            var value = DataManager.Instance.WordDataDict[code].GetData(index, 1)[0];
                            message = value.ToString();
                        }
                        else if (Util.ValidBitAddress(sAddress, out code, out index))
                        {
                            var value = DataManager.Instance.BitDataDict[code].GetData(index, 1)[0];
                            message = value ? "1" : "0";
                        }
                        else
                        {
                            continue;
                        }

                        message = $"{(char)0x02}{message}{(char)0x03}";
                        var bytes = Encoding.ASCII.GetBytes(message);
                        m_outputComm[i]?.SendMessage(bytes);
                    }
                    catch { }
                }

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        var message = $"{(char)0x02}PING{(char)0x03}";
                        var bytes = Encoding.ASCII.GetBytes(message);
                        m_inputComm[i]?.SendMessage(bytes);
                    }
                    catch { }
                }
            }
        }
    }
}