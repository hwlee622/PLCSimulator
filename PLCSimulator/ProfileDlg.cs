using System;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class ProfileDlg : Form
    {
        public ProfileDlg()
        {
            InitializeComponent();

            comboBox_Protocol.DataSource = Enum.GetValues(typeof(Protocol));
        }

        private void ProfileDlg_Load(object sender, EventArgs e)
        {
            ProfileRecipe.Instance.Load(string.Empty);
            ProfileRecipe.Instance.Save();

            var profileList = ProfileRecipe.Instance.GetProflieList();
            foreach (var profile in profileList)
                comboBox_Profile.Items.Add(profile);
            if (comboBox_Profile.Items.Count > 0)
                comboBox_Profile.SelectedIndex = 0;
        }

        private void comboBox_Profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProfileRecipe.Instance.Load(comboBox_Profile.SelectedItem.ToString());
            comboBox_Protocol.SelectedIndex = (int)ProfileRecipe.Instance.ProfileInfo.Protocol;
            if (ProfileRecipe.Instance.ProfileInfo.Protocol.ToString().Contains("Serial"))
                textBox_Port.Text = ProfileRecipe.Instance.ProfileInfo.PortName;
            else
                textBox_Port.Text = ProfileRecipe.Instance.ProfileInfo.Port.ToString();
            textBox_MaxBitData.Text = ProfileRecipe.Instance.ProfileInfo.MaxBitData.ToString();
            textBox_MaxWordData.Text = ProfileRecipe.Instance.ProfileInfo.MaxWordData.ToString();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            SelectProfile();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox_Port_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectProfile();
            }
        }

        private void SelectProfile()
        {
            string profileName = comboBox_Profile.Text;
            ProfileRecipe.Instance.Load(profileName);

            if (Enum.TryParse(comboBox_Protocol.SelectedItem.ToString(), true, out Protocol protocol))
                ProfileRecipe.Instance.ProfileInfo.Protocol = protocol;
            if (int.TryParse(textBox_Port.Text, out int port))
                ProfileRecipe.Instance.ProfileInfo.Port = port;
            ProfileRecipe.Instance.ProfileInfo.PortName = textBox_Port.Text;
            if (int.TryParse(textBox_MaxBitData.Text, out var maxBitData) && maxBitData > 0)
                ProfileRecipe.Instance.ProfileInfo.MaxBitData = maxBitData;
            if (int.TryParse(textBox_MaxWordData.Text, out var maxWordData) && maxWordData > 0)
                ProfileRecipe.Instance.ProfileInfo.MaxWordData = maxWordData;
            ProfileRecipe.Instance.Save();

            DialogResult = DialogResult.OK;
        }
    }
}