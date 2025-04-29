using System.Collections.Generic;
using System.Xml.Serialization;

namespace PLCSimulator
{
    [XmlRoot("Profile")]
    public class Profile
    {
        public Protocol Protocol = Protocol.Mewtocol;
        public int Port;

        public List<string> FavoriteAddress = new List<string>();
        public List<Description> DescriptionList = new List<Description>();
        public MacroInfo[] MacroInfoArray = new MacroInfo[10];

        public Profile()
        {
            for (int i = 0; i < MacroInfoArray.Length; i++)
                MacroInfoArray[i] = new MacroInfo();
        }
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
        public ushort Value = 0;
    }
}