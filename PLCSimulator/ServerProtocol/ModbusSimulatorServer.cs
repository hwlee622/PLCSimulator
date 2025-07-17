using EasyModbus;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public class ModbusSimulatorServer
    {
        public const string COIL = "COIL";
        private const string DISCRETE_INPUT = "INPUT";
        private const string INPUT_REGISTER = "IR";
        public const string HOLDING_REGISTER = "HR";

        private ModbusServer m_server;

        public ModbusSimulatorServer(int port, bool udpFlag)
        {
            m_server = new ModbusServer()
            {
                UDPFlag = udpFlag,
                Port = port,
                LocalIPAddress = IPAddress.Any
            };
            m_server.Listen();

            DataManager.Instance.BitDataDict.Add(COIL, new DataManager.BitData(10000));
            DataManager.Instance.BitDataDict.Add(DISCRETE_INPUT, new DataManager.BitData(10000));
            DataManager.Instance.WordDataDict.Add(INPUT_REGISTER, new DataManager.WordData(10000));
            DataManager.Instance.WordDataDict.Add(HOLDING_REGISTER, new DataManager.WordData(10000));

            Task.Run(() => SyncData());
        }

        private async Task SyncData()
        {
            var coilData = DataManager.Instance.BitDataDict[COIL];
            var inputData = DataManager.Instance.BitDataDict[DISCRETE_INPUT];
            var inputRegisterData = DataManager.Instance.WordDataDict[INPUT_REGISTER];
            var holdingRegisterData = DataManager.Instance.WordDataDict[HOLDING_REGISTER];

            bool[] preCoils = new bool[coilData.DataLength];
            bool[] preInputs = new bool[inputData.DataLength];
            ushort[] preInputRegisters = new ushort[inputRegisterData.DataLength];
            ushort[] preHoldingRegisters = new ushort[holdingRegisterData.DataLength];
            while (true)
            {
                try
                {
                    await Task.Delay(10);

                    for (int i = 0; i < preCoils.Length - 1; i++)
                    {
                        var coil = m_server.coils[i + 1];
                        var bit = coilData.GetData(i, 1)[0];
                        bool value;
                        if (coil == bit)
                            value = coil;
                        else
                            value = coil == preCoils[i] ? bit : coil;

                        coilData.SetData(i, new bool[] { value });
                        m_server.coils[i + 1] = value;
                        preCoils[i] = value;
                    }

                    for (int i = 0; i < preInputs.Length - 1; i++)
                    {
                        var input = m_server.discreteInputs[i + 1];
                        var bit = inputData.GetData(i, 1)[0];
                        bool value;
                        if (input == bit)
                            value = input;
                        else
                            value = input == preInputs[i] ? bit : input;

                        inputData.SetData(i, new bool[] { value });
                        m_server.discreteInputs[i + 1] = value;
                        preInputs[i] = value;
                    }

                    for (int i = 0; i < preInputRegisters.Length - 1; i++)
                    {
                        var register = (ushort)m_server.inputRegisters[i + 1];
                        var word = inputRegisterData.GetData(i, 1)[0];
                        ushort value;
                        if (register == word)
                            value = register;
                        else
                            value = register == preInputRegisters[i] ? word : register;

                        inputRegisterData.SetData(i, new ushort[] { value });
                        m_server.inputRegisters[i + 1] = (short)value;
                        preInputRegisters[i] = value;
                    }

                    for (int i = 0; i < preHoldingRegisters.Length - 1; i++)
                    {
                        var register = (ushort)m_server.holdingRegisters[i + 1];
                        var word = holdingRegisterData.GetData(i, 1)[0];
                        ushort value;
                        if (register == word)
                            value = register;
                        else
                            value = register == preHoldingRegisters[i] ? word : register;

                        holdingRegisterData.SetData(i, new ushort[] { value });
                        m_server.holdingRegisters[i + 1] = (short)value;
                        preHoldingRegisters[i] = value;
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
