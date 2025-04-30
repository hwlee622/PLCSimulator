using System;
using System.Drawing;
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
        private UserControl_Macro m_macroUserControl;

        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
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

            m_macroUserControl = new UserControl_Macro(PLCSimulator.Instance.MacroManager);
            panel_tab.Controls.Add(m_macroUserControl);
            m_macroUserControl.Dock = DockStyle.Fill;

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
            if (m_dataAreaUserControl.Visible && panel_tab.Controls[0] == m_dataAreaUserControl)
                return;

            HidePanel();

            m_dataAreaUserControl.BringToFront();
            m_dataAreaUserControl.Show();
            SetSelectedButtonColor(sender, e);
        }

        private void button_contactR_Click(object sender, EventArgs e)
        {
            if (m_rAreaUserControl.Visible && panel_tab.Controls[0] == m_rAreaUserControl)
                return;

            HidePanel();

            m_rAreaUserControl.BringToFront();
            m_rAreaUserControl.Show();
            SetSelectedButtonColor(sender, e);
        }

        private void button_contactX_Click(object sender, EventArgs e)
        {
            if (m_xAreaUserControl.Visible && panel_tab.Controls[0] == m_xAreaUserControl)
                return;

            HidePanel();

            m_xAreaUserControl.BringToFront();
            m_xAreaUserControl.Show();
            SetSelectedButtonColor(sender, e);
        }

        private void button_contactY_Click(object sender, EventArgs e)
        {
            if (m_yAreaUserControl.Visible && panel_tab.Controls[0] == m_yAreaUserControl)
                return;

            HidePanel();

            m_yAreaUserControl.BringToFront();
            m_yAreaUserControl.Show();
            SetSelectedButtonColor(sender, e);
        }

        private void button_Favorite_Click(object sender, EventArgs e)
        {
            if (m_favoriteUserControl.Visible && panel_tab.Controls[0] == m_favoriteUserControl)
                return;

            HidePanel();

            m_favoriteUserControl.BringToFront();
            m_favoriteUserControl.Show();
            SetSelectedButtonColor(sender, e);
        }

        private void button_Macro_Click(object sender, EventArgs e)
        {
            if (m_macroUserControl.Visible)
                m_macroUserControl.Hide();
            else
            {
                m_macroUserControl.BringToFront();
                m_macroUserControl.Show();
            }
        }

        private void HidePanel()
        {
            foreach (Control control in panel_tab.Controls)
                control.Hide();
        }

        private void SetSelectedButtonColor(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                ResetButtonColor();
                Button button = (Button)sender;
                button.BackColor = SystemColors.ControlLight;
            }
        }

        private void ResetButtonColor()
        {
            foreach (Control control in panel_Menu.Controls)
            {
                if (control is Button)
                    control.BackColor = SystemColors.ButtonShadow;
            }
        }
    }
}