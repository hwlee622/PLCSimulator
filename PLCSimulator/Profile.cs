using System.Collections.Generic;
using System.Xml.Serialization;

namespace PLCSimulator
{
    [XmlRoot("Profile")]
    public class Profile
    {
        public Protocol Protocol = Protocol.MewtocolUdp;
        public int Port;
        public string PortName;

        public List<string> FavoriteAddress = new List<string>();
        public List<Description> DescriptionList = new List<Description>();
        public MacroInfo[] MacroInfoArray = new MacroInfo[10];
        public SyncManagerInfo SyncManagerInfo = new SyncManagerInfo();

        public Profile()
        {
            for (int i = 0; i < MacroInfoArray.Length; i++)
                MacroInfoArray[i] = new MacroInfo();
        }
    }

    public class SyncManagerInfo
    {
        public List<string> PrevSyncAddress = new List<string>();
        public List<string> NextSyncAddress = new List<string>();
    }

    public class Description
    {
        [XmlAttribute]
        public string Key;

        [XmlAttribute]
        public string Value;
    }

    public class MacroInfo
    {
        public List<MacroContext> MacroContextList = new List<MacroContext>();
    }

    public class MacroContext
    {
        [XmlAttribute]
        public MacroType MacroType = MacroType.Delay;

        [XmlAttribute]
        public string Address = string.Empty;

        [XmlAttribute]
        public string Value = "0";
    }
}