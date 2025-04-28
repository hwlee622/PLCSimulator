using System;
using System.Collections.Generic;

namespace PLCSimulator
{
    public class Util
    {
        public static bool IsDTAddress(string text, out int address)
        {
            address = -1;
            text = text.ToUpper();
            if (text.Length >= 3 && text.StartsWith("DT") && int.TryParse(text.Substring(2), out address))
                return true;
            return false;
        }

        public static bool IsContactAddress(string text, out string contactCode, out int address, out int hex)
        {
            contactCode = string.Empty;
            address = -1;
            hex = -1;
            HashSet<string> contactCodeSet = new HashSet<string>() { DataManager.RAreaCode, DataManager.YAreaCode, DataManager.XAreaCode };
            if (text.Length >= 3 && contactCodeSet.Contains(text.Substring(0, 1)) && int.TryParse(text.Substring(1, text.Length - 2), out address) && TryParseHexToInt(text.Substring(text.Length - 1, 1), out hex))
            {
                contactCode = text.Substring(0, 1);
                return true;
            }
            return false;
        }

        private static bool TryParseHexToInt(string hex, out int value)
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