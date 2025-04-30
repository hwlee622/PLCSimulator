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
        private string m_currentProfile = "Profile.prof";

        public List<string> GetProflieList()
        {
            List<string> profileList = new List<string>();
            try
            {
                DirectoryInfo di = new DirectoryInfo(".");
                var fileList = di.GetFiles("*.prof");
                foreach (var file in fileList)
                    profileList.Add(file.Name.Replace(".prof", ""));
            }
            catch
            {
            }
            return profileList;
        }

        public void Load(string profileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(profileName))
                    m_currentProfile = $"{profileName}.prof";
                if (!File.Exists(m_currentProfile))
                    return;

                using (FileStream fs = new FileStream(m_currentProfile, FileMode.Open))
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
                using (FileStream fs = new FileStream(m_currentProfile, FileMode.Create))
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