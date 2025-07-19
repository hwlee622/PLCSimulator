using CommInterface;
using System;
using System.Text;

namespace PLCSimulator
{
    public class MewtocolServer
    {
        public const string DT = "DT";
        public const string R = "R";
        public const string X = "X";
        public const string Y = "Y";

        private ServerComm m_comm;

        public MewtocolServer(int port) : this()
        {
            m_comm = new ServerCommUdp(port);
            m_comm.SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x0D }));
            m_comm.OnError += ex => LogWriter.Instance.LogError(ex);
            m_comm.OnReceiveMessage += MessageHandler;
        }

        public MewtocolServer(string portName) : this()
        {
            m_comm = new ServerCommSerial(portName);
            m_comm.SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x0D }));
            m_comm.OnError += ex => LogWriter.Instance.LogError(ex);
            m_comm.OnReceiveMessage += MessageHandler;
        }

        public MewtocolServer()
        {
            DataManager.Instance.BitDataDict.Add(R, new DataManager.BitData(1600, true));
            DataManager.Instance.BitDataDict.Add(X, new DataManager.BitData(1600, true));
            DataManager.Instance.BitDataDict.Add(Y, new DataManager.BitData(1600, true));
            DataManager.Instance.WordDataDict.Add(DT, new DataManager.WordData(50000));
        }

        public void Start()
        {
            m_comm.Start();
        }

        public void Stop()
        {
            m_comm.Stop();
        }

        private void MessageHandler(byte[] recvBytes)
        {
            try
            {
                string command = Encoding.ASCII.GetString(recvBytes);
                string reply = string.Empty;

                if (CheckBCCError(recvBytes))
                    reply = ReplyError(command, 40);
                else if (CheckCommandError(recvBytes))
                    reply = ReplyError(command, 41);
                else
                    reply = CommandHandler(command);

                byte[] sendBytes = Encoding.ASCII.GetBytes(reply);
                m_comm.SendMessage(sendBytes);
            }
            catch (Exception ex)
            {
                LogWriter.Instance.LogError(ex);
            }
        }

        private string CommandHandler(string command)
        {
            string reply = string.Empty;
            string code = command.Substring(4, 3);

            switch (code)
            {
                default:
                    reply = ReplyError(command, 41);
                    break;

                case "RCS":
                    reply = ReadContactAreaSingle(command);
                    break;

                case "RCP":
                    reply = ReadContactAreaMulti(command);
                    break;

                case "RCC":
                    reply = ReadContactAreaBlock(command);
                    break;

                case "WCS":
                    reply = WriteContactAreaSingle(command);
                    break;

                case "WCP":
                    reply = WriteContactAreaMulti(command);
                    break;

                case "WCC":
                    reply = WriteContactAreaBlock(command);
                    break;

                case "RDD":
                    reply = ReadDataArea(command);
                    break;

                case "WDD":
                    reply = WriteDataArea(command);
                    break;
            }

            return reply;
        }

        private string ReadContactAreaSingle(string command)
        {
            if (command.Length != 15)
                return ReplyError(command, 41);
            else if (!DataManager.Instance.BitDataDict.TryGetValue($"{command[7]}", out var bitData) || !int.TryParse(command.Substring(8, 3), out int address) ||
                     !TryParseHexToInt(command.Substring(11, 1), out int hex))
                return ReplyError(command, 61);
            else
            {
                int singleData = bitData.GetData(address * 16 + hex, 1)[0] ? 1 : 0;

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$RC");
                sb.Append(singleData);

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string ReadContactAreaMulti(string command)
        {
            if (command.Length < 16 || ((command.Length - 11) % 5) != 0)
                return ReplyError(command, 41);
            else if (!int.TryParse(command.Substring(7, 1), out int size) || size < 1 || size > 8)
                return ReplyError(command, 41);
            else
            {
                int length = Math.Min((command.Length - 11) / 5, size);
                int[] multiData = new int[length];

                for (int i = 0; i < length; i++)
                {
                    int index = 8 + i * 5;
                    if (!DataManager.Instance.BitDataDict.TryGetValue($"{command[index]}", out var bitData) || !int.TryParse(command.Substring(index + 1, 3), out int address) ||
                        !TryParseHexToInt(command.Substring(index + 4, 1), out int hex))
                        return ReplyError(command, 61);

                    int singleData = bitData.GetData(address * 16 + hex, 1)[0] ? 1 : 0;
                    multiData[i] = singleData;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$RC");
                for (int i = 0; i < length; i++)
                    sb.Append(multiData[i]);

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string ReadContactAreaBlock(string command)
        {
            if (command.Length != 19)
                return ReplyError(command, 41);
            else if (!DataManager.Instance.BitDataDict.TryGetValue($"{command[7]}", out var bitData) || !int.TryParse(command.Substring(8, 4), out int startAddress) || !int.TryParse(command.Substring(12, 4), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= bitData.DataLength || endAddress >= bitData.DataLength)
                return ReplyError(command, 61);
            else
            {
                int length = endAddress - startAddress + 1;
                bool[] bitBlockData = bitData.GetData(startAddress * 16, length * 16);
                ushort[] blockData = new ushort[length];
                for (int i = 0; i < length; i++)
                {
                    bool[] data = bitData.GetData((startAddress + i) * 16, 16);
                    for (int j = 0; j < 16; j++)
                    {
                        int value = data[j] ? 1 : 0;
                        blockData[i] = (ushort)(blockData[i] | value << j);
                    }
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$RC");
                for (int i = 0; i < length; i++)
                {
                    byte[] bits = BitConverter.GetBytes(blockData[i]);
                    if (!BitConverter.IsLittleEndian)
                        Array.Reverse(bits);
                    sb.Append($"{bits[0]:X2}{bits[1]:X2}");
                }

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string WriteContactAreaSingle(string command)
        {
            if (command.Length != 16)
                return ReplyError(command, 41);
            else if (!DataManager.Instance.BitDataDict.TryGetValue($"{command[7]}", out var bitData) || !int.TryParse(command.Substring(8, 3), out int address) ||
                     !TryParseHexToInt(command.Substring(11, 1), out int hex))
                return ReplyError(command, 61);
            else
            {
                bool value = command[12] > '0';
                bitData.SetData(address * 16 + hex, new bool[] { value });

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$WC");

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string WriteContactAreaMulti(string command)
        {
            if (command.Length < 17 || ((command.Length - 11) % 6) != 0)
                return ReplyError(command, 41);
            else if (!int.TryParse(command.Substring(7, 1), out int size) || size < 1 || size > 8)
                return ReplyError(command, 41);
            else
            {
                int length = Math.Min((command.Length - 11) / 6, size);
                for (int i = 0; i < length; i++)
                {
                    int index = 8 + i * 6;
                    if (!DataManager.Instance.BitDataDict.TryGetValue($"{command[7]}", out var bitData) || !int.TryParse(command.Substring(index + 1, 3), out int address) ||
                        !TryParseHexToInt(command.Substring(index + 4, 1), out int hex))
                        return ReplyError(command, 61);

                    bool value = command[index + 5] > '0';
                    bitData.SetData(address * 16 + hex, new bool[] { value });
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$WC");

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string WriteContactAreaBlock(string command)
        {
            if (command.Length < 23 || ((command.Length - 19) % 4) != 0)
                return ReplyError(command, 41);
            else if (!DataManager.Instance.BitDataDict.TryGetValue($"{command[7]}", out var bitData) || !int.TryParse(command.Substring(8, 4), out int startAddress) || !int.TryParse(command.Substring(12, 4), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= bitData.DataLength || endAddress >= bitData.DataLength)
                return ReplyError(command, 61);
            else
            {
                int length = endAddress - startAddress + 1;
                bool[] data = new bool[length * 16];
                for (int i = 0; i < length; i++)
                {
                    int index = 16 + i * 4;
                    string sValue = $"{command.Substring(index + 2, 2)}{command.Substring(index, 2)}";
                    ushort value = Convert.ToUInt16(sValue, 16);
                    for (int j = 0; j < 16; j++)
                        data[i * 16 + j] = (value >> j & 1) == 1;
                }
                bitData.SetData(startAddress * 16, data);

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$WC");

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string ReadDataArea(string command)
        {
            if (command.Length != 20)
                return ReplyError(command, 41);
            else if (!DataManager.Instance.WordDataDict.TryGetValue(DT, out var wordData) ||
                     !int.TryParse(command.Substring(7, 5), out int startAddress) || !int.TryParse(command.Substring(12, 5), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= wordData.DataLength || endAddress >= wordData.DataLength)
                return ReplyError(command, 61);
            else
            {
                int length = endAddress - startAddress + 1;
                var data = wordData.GetData(startAddress, length);

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$RD");
                for (int i = 0; i < length; i++)
                {
                    byte[] bitData = BitConverter.GetBytes(data[i]);
                    if (!BitConverter.IsLittleEndian)
                        Array.Reverse(bitData);
                    sb.Append($"{bitData[0]:X2}{bitData[1]:X2}");
                }

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string WriteDataArea(string command)
        {
            if (command.Length < 24 || ((command.Length - 20) % 4) != 0)
                return ReplyError(command, 41);
            else if (!DataManager.Instance.WordDataDict.TryGetValue(DT, out var wordData) ||
                     !int.TryParse(command.Substring(7, 5), out int startAddress) || !int.TryParse(command.Substring(12, 5), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= wordData.DataLength || endAddress > wordData.DataLength)
                return ReplyError(command, 61);
            else
            {
                int length = Math.Min((command.Length - 20) / 4, endAddress - startAddress + 1);
                ushort[] value = new ushort[length];
                for (int i = 0; i < length; i++)
                {
                    int index = 17 + i * 4;
                    string sValue = $"{command.Substring(index + 2, 2)}{command.Substring(index, 2)}";
                    ushort singleValue = Convert.ToUInt16(sValue, 16);
                    value[i] = singleValue;
                }
                wordData.SetData(startAddress, value);

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$WD");

                string reply = sb.ToString();
                reply = GetBCCMessage(reply);
                return reply;
            }
        }

        private string ReplyError(string command, int errorCode)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(command) || command.Length < 3)
                sb.Append("<00");
            else
                sb.Append(command.Substring(0, 3));

            sb.Append($"!{errorCode / 10}{errorCode % 10}");

            string reply = sb.ToString();
            reply = GetBCCMessage(reply);

            return reply;
        }

        private string GetBCCMessage(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            byte bcc = 0;
            foreach (byte b in bytes)
                bcc ^= b;

            return $"{message}{bcc:X2}{(char)0x0D}";
        }

        private bool CheckBCCError(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 3)
                return true;

            string reply = Encoding.ASCII.GetString(bytes);
            byte bcc = 0;
            for (int i = 0; i < bytes.Length - 3; i++)
                bcc ^= bytes[i];

            if (!string.Equals(reply.Substring(reply.Length - 3, 2), $"{bcc:X2}"))
                return true;
            else
                return false;
        }

        private bool CheckCommandError(byte[] recvBytes)
        {
            if (recvBytes == null || recvBytes.Length < 9 || recvBytes[3] != (byte)'#')
                return true;
            else
                return false;
        }

        private bool TryParseHexToInt(string hex, out int value)
        {
            value = 0;
            try
            {
                value = Convert.ToInt16(hex, 16);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}