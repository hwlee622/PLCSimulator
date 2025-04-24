using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            comboBox_Protocol.SelectedIndex = (int)ProfileRecipe.Instance.ProfileInfo.Protocol;
            textBox_Port.Text = ProfileRecipe.Instance.ProfileInfo.Port.ToString();
        }

        private void button_Data_Click(object sender, EventArgs e)
        {
            if (Enum.TryParse(comboBox_Protocol.SelectedItem.ToString(), true, out Protocol protocol))
                ProfileRecipe.Instance.ProfileInfo.Protocol = protocol;
            if (int.TryParse(textBox_Port.Text, out int port))
                ProfileRecipe.Instance.ProfileInfo.Port = port;
            ProfileRecipe.Instance.Save();

            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
