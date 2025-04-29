using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PLCSimulator
{
    public class ProfileRecipe
    {
        #region Singleton

        public static ProfileRecipe Instance
        { get { return InstanceHolder.Instance; } }

        private static class InstanceHolder
        {
            public static ProfileRecipe Instance = new ProfileRecipe();
        }

        private ProfileRecipe()
        { }

        #endregion Singleton

        public Profile ProfileInfo = new Profile();

        public void Load()
        {
            try
            {
                if (!File.Exists("Profile.xml"))
                    return;

                using (FileStream fs = new FileStream("Profile.xml", FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Profile));
                    ProfileInfo = xs.Deserialize(fs) as Profile;
                }
            }
            catch
            {
            }
        }

        public void Save()
        {
            try
            {
                using (FileStream fs = new FileStream("Profile.xml", FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Profile));
                    xs.Serialize(fs, ProfileInfo);
                }
            }
            catch
            {
            }
        }

        public string GetDescription(string address)
        {
            var desc = ProfileInfo.DescriptionList.Find(x => x.Key == address);
            if (desc != null)
                return desc.Value;
            else
                return string.Empty;
        }

        public void SetDescription(string address, string description)
        {
            var desc = ProfileInfo.DescriptionList.Find(x => x.Key == address);
            if (desc != null)
                desc.Value = description;
            else
                ProfileInfo.DescriptionList.Add(new Description() { Key = address, Value = description });
        }

        public MacroInfo[] GetMacroInfoArray()
        {
            return ProfileInfo.MacroInfoArray;
        }
    }
}