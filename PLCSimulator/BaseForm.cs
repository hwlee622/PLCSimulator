using System;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class BaseForm : Form
    {
        private UserControl_Data m_dataAreaUserControl;
        private UserControl_Contact m_rAreaUserControl;
        private UserControl_Contact m_xAreaUserControl;
        private UserControl_Contact m_yAreaUserControl;
        private UserControl_Favorites m_favoriteUserControl;

        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            ProfileRecipe.Instance.Load();
            using (ProfileDlg dlg = new ProfileDlg())
            {
                if (dlg.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
            }

            PLCSimulator.Instance.Start();

            IniHandler ihandler = new IniHandler("Setting/Config.ini");
            string sMewtocolPort = ihandler.GetProfilesString("PanasonicPLC", "Port");
            string sUpperLinkPort = ihandler.GetProfilesString("OmronPLC", "Port");

            Text = $"{Text} {ProfileRecipe.Instance.ProfileInfo.Protocol} : {ProfileRecipe.Instance.ProfileInfo.Port}";
            notifyIcon_system.Text = Text;

            m_dataAreaUserControl = new UserControl_Data(DataManager.DataCode);
            panel_tab.Controls.Add(m_dataAreaUserControl);
            m_dataAreaUserControl.Dock = DockStyle.Fill;

            m_rAreaUserControl = new UserControl_Contact(DataManager.RAreaCode);
            panel_tab.Controls.Add(m_rAreaUserControl);
            m_rAreaUserControl.Dock = DockStyle.Fill;

            m_yAreaUserControl = new UserControl_Contact(DataManager.YAreaCode);
            panel_tab.Controls.Add(m_yAreaUserControl);
            m_yAreaUserControl.Dock = DockStyle.Fill;

            m_xAreaUserControl = new UserControl_Contact(DataManager.XAreaCode);
            panel_tab.Controls.Add(m_xAreaUserControl);
            m_xAreaUserControl.Dock = DockStyle.Fill;

            m_favoriteUserControl = new UserControl_Favorites();
            panel_tab.Controls.Add(m_favoriteUserControl);
            m_favoriteUserControl.Dock = DockStyle.Fill;

            foreach (Control control in panel_tab.Controls)
                control.Hide();
            m_dataAreaUserControl.Show();
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PLCSimulator.Instance.Stop();
            ProfileRecipe.Instance.Save();
        }

        private void notifyIcon_system_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void 화면열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Data_Click(object sender, EventArgs e)
        {
            if (m_dataAreaUserControl.Visible)
                return;

            HidePanel();
            m_dataAreaUserControl.Show();
        }

        private void button_contactR_Click(object sender, EventArgs e)
        {
            if (m_rAreaUserControl.Visible)
                return;

            HidePanel();
            m_rAreaUserControl.Show();
        }

        private void button_contactX_Click(object sender, EventArgs e)
        {
            if (m_yAreaUserControl.Visible)
                return;

            HidePanel();
            m_yAreaUserControl.Show();
        }

        private void button_contactY_Click(object sender, EventArgs e)
        {
            if (m_xAreaUserControl.Visible)
                return;

            HidePanel();
            m_xAreaUserControl.Show();
        }

        private void button_Favorite_Click(object sender, EventArgs e)
        {
            if (m_favoriteUserControl.Visible)
                return;

            HidePanel();
            m_favoriteUserControl.Show();
        }

        private void HidePanel()
        {
            foreach (Control control in panel_tab.Controls)
                control.Hide();
        }
    }
}