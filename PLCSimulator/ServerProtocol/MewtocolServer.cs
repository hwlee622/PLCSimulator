using System;
using System.Globalization;
using System.Text;
using CommInterface;

namespace PLCSimulator
{
    public class MewtocolServer
    {
        private ServerComm m_comm;

        public MewtocolServer(int port)
        {
            m_comm = new ServerCommUdp(port);
            m_comm.SetETX(Encoding.ASCII.GetBytes(new char[] { (char)0x0D }));
            m_comm.OnError += ex => LogWriter.Instance.LogError(ex);
            m_comm.OnReceiveMessage += MessageHandler;
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
            else if (!DataManager.Instance.PlcArea.TryGetValue($"{command[7]}", out var contactArea) || !int.TryParse(command.Substring(8, 3), out int address) ||
                     !int.TryParse(command.Substring(11, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hex))
                return ReplyError(command, 61);
            else
            {
                int singleData = contactArea.GetData(address, 1)[0];
                singleData = (singleData >> hex) & 1;

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
                    if (!DataManager.Instance.PlcArea.TryGetValue($"{command[index]}", out var contactArea) || !int.TryParse(command.Substring(index + 1, 3), out int address) ||
                        !int.TryParse(command.Substring(index + 4, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hex))
                        return ReplyError(command, 61);

                    int singleData = contactArea.GetData(address, 1)[0];
                    singleData = (singleData >> hex) & 1;
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
            else if (!DataManager.Instance.PlcArea.TryGetValue($"{command[7]}", out var contactArea) || !int.TryParse(command.Substring(8, 4), out int startAddress) || !int.TryParse(command.Substring(12, 4), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= DataManager.MaxContactAddress || endAddress >= DataManager.MaxContactAddress)
                return ReplyError(command, 61);
            else
            {
                int length = endAddress - startAddress + 1;
                ushort[] blockData = contactArea.GetData(startAddress, length);

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 3));
                sb.Append("$RC");
                for (int i = 0; i < length; i++)
                {
                    byte[] bitData = BitConverter.GetBytes(blockData[i]);
                    if (!BitConverter.IsLittleEndian)
                        Array.Reverse(bitData);
                    sb.Append($"{bitData[0]:X2}{bitData[1]:X2}");
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
            else if (!DataManager.Instance.PlcArea.TryGetValue($"{command[7]}", out var contactArea) || !int.TryParse(command.Substring(8, 3), out int address) || !int.TryParse(command.Substring(11, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hex))
                return ReplyError(command, 61);
            else
            {
                int value = command[12] > '0' ? 1 : 0;
                int mask = 1 << hex;
                var singleData = contactArea.GetData(address, 1);
                singleData[0] = value > 0 ? (ushort)(singleData[0] | mask) : (ushort)(singleData[0] & ~mask);
                contactArea.SetData(address, singleData);

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
                    if (!DataManager.Instance.PlcArea.TryGetValue($"{command[7]}", out var contactArea) || !int.TryParse(command.Substring(index + 1, 3), out int address) ||
                        !int.TryParse(command.Substring(index + 4, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hex))
                        return ReplyError(command, 61);

                    int value = command[index + 5] > '0' ? 1 : 0;
                    int mask = 1 << hex;
                    var singleData = contactArea.GetData(address, 1);
                    singleData[0] = value > 0 ? (ushort)(singleData[0] | mask) : (ushort)(singleData[0] & ~mask);
                    contactArea.SetData(address, singleData);
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
            else if (!DataManager.Instance.PlcArea.TryGetValue($"{command[7]}", out var contactArea) || !int.TryParse(command.Substring(8, 4), out int startAddress) || !int.TryParse(command.Substring(12, 4), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= DataManager.MaxContactAddress || endAddress >= DataManager.MaxContactAddress)
                return ReplyError(command, 61);
            else
            {
                int length = endAddress - startAddress + 1;
                var blockData = contactArea.GetData(startAddress, length);
                for (int i = 0; i < length; i++)
                {
                    int index = 16 + i * 4;
                    string sValue = $"{command.Substring(index + 2, 2)}{command.Substring(index, 2)}";
                    ushort value = Convert.ToUInt16(sValue, 16);
                    blockData[i] = value;
                }
                contactArea.SetData(startAddress, blockData);

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
            else if (!int.TryParse(command.Substring(7, 5), out int startAddress) || !int.TryParse(command.Substring(12, 5), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= DataManager.MaxDataAreaAddress || endAddress >= DataManager.MaxDataAreaAddress)
                return ReplyError(command, 61);
            else
            {
                int length = endAddress - startAddress + 1;
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return ReplyError(command, 41);

                var data = dataArea.GetData(startAddress, length);

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
            else if (!int.TryParse(command.Substring(7, 5), out int startAddress) || !int.TryParse(command.Substring(12, 5), out int endAddress))
                return ReplyError(command, 41);
            else if (endAddress < startAddress || startAddress >= DataManager.MaxDataAreaAddress || endAddress > DataManager.MaxDataAreaAddress)
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

                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return ReplyError(command, 41);

                dataArea.SetData(startAddress, value);

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
    }
}