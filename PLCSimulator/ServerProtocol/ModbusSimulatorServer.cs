using EasyModbus;
using System;
using System.Net;
using System.Threading;

namespace PLCSimulator
{
    public class ModbusSimulatorServer
    {
        public const string COIL = "COIL";
        public const string HOLDING_REGISTER = "HR";

        private ModbusServer m_server;

        public ModbusSimulatorServer(int port)
        {
            m_server = new ModbusServer()
            {
                Port = port,
                LocalIPAddress = IPAddress.Any
            };
            m_server.Listen();

            DataManager.Instance.BitDataDict.Add(COIL, new DataManager.BitData(10000));
            DataManager.Instance.WordDataDict.Add(HOLDING_REGISTER, new DataManager.WordData(10000));

            var thread = new Thread(SyncData);
            thread.IsBackground = true;
            thread.Start();

        }

        private void SyncData()
        {
            bool[] prevCoils = new bool[DataManager.Instance.BitDataDict[COIL].DataLength];
            short[] prevHRegister = new short[DataManager.Instance.WordDataDict[HOLDING_REGISTER].DataLength];
            bool[] prevBits = new bool[DataManager.Instance.BitDataDict[COIL].DataLength];
            ushort[] prevWords = new ushort[DataManager.Instance.WordDataDict[HOLDING_REGISTER].DataLength];
            while (true)
            {
                try
                {
                    Thread.Sleep(10);

                    for (int i = 1; i < prevCoils.Length; i++)
                    {
                        var coil = m_server.coils[i];
                        if (prevCoils[i] != coil)
                            DataManager.Instance.BitDataDict[COIL].SetData(i - 1, new bool[] { coil });
                        prevCoils[i] = coil;
                    }

                    for (int i = 1; i < prevHRegister.Length; i++)
                    {
                        var register = m_server.holdingRegisters[i];
                        if (prevHRegister[i] != register)
                            DataManager.Instance.WordDataDict[HOLDING_REGISTER].SetData(i - 1, new ushort[] { (ushort)register });
                        prevHRegister[i] = register;
                    }

                    for (int i = 0; i < prevBits.Length - 1; i++)
                    {
                        var bit = DataManager.Instance.BitDataDict[COIL].GetData(i, 1)[0];
                        if (prevBits[i] != bit)
                            m_server.coils[i + 1] = bit;
                        prevBits[i] = bit;
                    }

                    for (int i = 0; i < prevWords.Length - 1; i++)
                    {
                        var word = DataManager.Instance.WordDataDict[HOLDING_REGISTER].GetData(i, 1)[0];
                        if (prevWords[i] != word)
                            m_server.holdingRegisters[i + 1] = (short)word;
                        prevWords[i] = word;
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.Instance.LogError(ex);
                }
            }
        }
    }
}
