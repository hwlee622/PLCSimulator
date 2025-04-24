using System;
using System.Text;
using YJComm;

namespace PLCSimulator
{
    public class UpperLinkServer
    {
        private ServerComm m_comm;

        public UpperLinkServer(int port)
        {
            m_comm = new ServerCommUdp(port);
            m_comm.SetSTX(Encoding.ASCII.GetBytes(new char[] { '@' }));
            m_comm.SetETX(Encoding.ASCII.GetBytes(new char[] { '*', (char)0x0D }));
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

                if (CheckFCSError(recvBytes))
                    reply = ReplyError(command, 40);
                else if (CheckCommandError(recvBytes))
                    reply = ReplyError(command, 14);
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
            string code = command.Substring(3, 2);

            switch (code)
            {
                default:
                    reply = ReplyError(command, 14);
                    break;

                case "RD":
                    reply = ReadDataArea(command);
                    break;

                case "WD":
                    reply = WriteDataArea(command);
                    break;
            }

            return reply;
        }

        private string ReadDataArea(string command)
        {
            if (command.Length != 17)
                return ReplyError(command, 14);
            else if (!int.TryParse(command.Substring(5, 4), out int startAddress) || !int.TryParse(command.Substring(9, 4), out int length))
                return ReplyError(command, 14);
            else if (startAddress >= DataManager.MaxDataAreaAddress || startAddress + length - 1 >= DataManager.MaxDataAreaAddress)
                return ReplyError(command, 15);
            else
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return ReplyError(command, 14);

                var data = dataArea.GetData(startAddress, length);

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 5));
                sb.Append("00");
                for (int i = 0; i < length; i++)
                {
                    byte[] bitData = BitConverter.GetBytes(data[i]);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bitData);
                    sb.Append($"{bitData[0]:X2}{bitData[1]:X2}");
                }

                string reply = sb.ToString();
                reply = GetFCSMessage(reply);
                return reply;
            }
        }

        private string WriteDataArea(string command)
        {
            if (command.Length < 17 || ((command.Length - 13) % 4) != 0)
                return ReplyError(command, 14);
            else if (!int.TryParse(command.Substring(5, 4), out int startAddress))
                return ReplyError(command, 14);
            else if (startAddress >= DataManager.MaxDataAreaAddress)
                return ReplyError(command, 15);
            else
            {
                int length = (command.Length - 13) / 4;
                if (startAddress + length - 1 >= DataManager.MaxDataAreaAddress)
                    return ReplyError(command, 15);

                ushort[] value = new ushort[length];
                for (int i = 0; i < length; i++)
                {
                    int index = 9 + i * 4;
                    string sValue = $"{command.Substring(index, 4)}";
                    ushort singleValue = Convert.ToUInt16(sValue, 16);
                    value[i] = singleValue;
                }

                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return ReplyError(command, 14);

                dataArea.SetData(startAddress, value);

                StringBuilder sb = new StringBuilder();
                sb.Append(command.Substring(0, 5));
                sb.Append("00");

                string reply = sb.ToString();
                reply = GetFCSMessage(reply);
                return reply;
            }
        }

        private string ReplyError(string command, int errorCode)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(command) || command.Length < 3)
                sb.Append("@00");
            else
                sb.Append(command.Substring(0, 3));

            if (string.IsNullOrEmpty(command) || command.Length < 5)
                sb.Append("IC");
            else
                sb.Append(command.Substring(3, 2)).Append($"{errorCode / 10}{errorCode % 10}");

            string reply = sb.ToString();
            reply = GetFCSMessage(reply);

            return reply;
        }

        private string GetFCSMessage(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            byte fcs = 0;
            foreach (byte b in bytes)
                fcs ^= b;

            return $"{message}{fcs:X2}*{(char)0x0D}";
        }

        private bool CheckFCSError(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 3)
                return true;

            string reply = Encoding.ASCII.GetString(bytes);
            byte fcs = 0;
            for (int i = 0; i < bytes.Length - 4; i++)
                fcs ^= bytes[i];

            if (!string.Equals(reply.Substring(reply.Length - 4, 2), $"{fcs:X2}"))
                return true;
            else
                return false;
        }

        private bool CheckCommandError(byte[] recvBytes)
        {
            if (recvBytes == null || recvBytes.Length < 9)
                return true;
            else
                return false;
        }
    }
}