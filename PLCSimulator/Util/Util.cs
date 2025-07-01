using System;
using System.Text;

namespace PLCSimulator
{
    public class Util
    {
        public static bool ValidWordAddress(string text, out string code, out int index)
        {
            code = string.Empty;
            index = -1;
            text = text.ToUpper().Trim();
            foreach (var key in DataManager.Instance.WordDataDict.Keys)
            {
                if (text.StartsWith(key))
                {
                    code = key;
                    return DataManager.Instance.WordDataDict[key].ValidateAddress(text.Substring(key.Length), out index);
                }
            }
            return false;
        }

        public static bool ValidBitAddress(string text, out string code, out int index)
        {
            code = string.Empty;
            index = -1;
            text = text.ToUpper().Trim();
            foreach (var key in DataManager.Instance.BitDataDict.Keys)
            {
                if (text.StartsWith(key))
                {
                    code = key;
                    return DataManager.Instance.BitDataDict[key].ValidateAddress(text.Substring(key.Length), out index);
                }
            }
            return false;
        }

        public static ushort[] UShortParseWordData(string text, WordDataType type)
        {
            ushort[] data;
            switch (type)
            {
                default:
                case WordDataType.ASCII:
                    data = new ushort[1];
                    if (text.Length % 2 != 0)
                        text += '\0';
                    data[0] = (ushort)(text[0] << 8 | text[1]);
                    break;
                case WordDataType.Short:
                    data = new ushort[1];
                    short.TryParse(text, out short shortValue);
                    data[0] = (ushort)shortValue;
                    break;
                case WordDataType.Int:
                    data = new ushort[2];
                    int.TryParse(text, out int intValue);
                    data[0] = (ushort)(intValue & 0xFFFF);
                    data[1] = (ushort)((intValue >> 16) & 0x0FFFF);
                    break;
                case WordDataType.Hex:
                    data = new ushort[1];
                    if (text.Length % 4 != 0)
                        text += "\0\0\0\0";
                    text = text.Substring(0, 4);
                    data[0] = Convert.ToUInt16(text, 16);
                    break;
            }
            return data;
        }

        public static string StringParseWordData(ushort[] data, WordDataType type)
        {
            ushort[] tempData = new ushort[2];
            if (data != null && data.Length > 0)
                tempData[0] = data[0];
            if (data != null && data.Length > 1)
                tempData[1] = data[1];

            switch (type)
            {
                default:
                case WordDataType.ASCII:
                    byte[] bitData = BitConverter.GetBytes(tempData[0]);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bitData);
                    return Encoding.ASCII.GetString(bitData);
                case WordDataType.Short:
                    return ((short)tempData[0]).ToString();
                case WordDataType.Int:
                    return ((tempData[1] << 16) | tempData[0]).ToString();
                case WordDataType.Hex:
                    return $"{data[0]:X2}";
            }
        }
    }
}