using System.Runtime.InteropServices;
using System.Text;

namespace PLCSimulator
{
    public class IniHandler
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string filePath;

        public IniHandler(string path)
        {
            filePath = path;
        }

        ~IniHandler()
        {
        }

        public string GetProfilesString(string section, string key)
        {
            string data;
            StringBuilder temp = new StringBuilder(255);
            int ret = GetPrivateProfileString(section, key, "", temp, 255, filePath);

            data = temp.ToString();
            return data;
        }

        public void WriteProfiles(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, filePath);
        }
    }
}