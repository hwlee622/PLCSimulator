using System;
using System.Drawing;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class BaseForm : Form
    {
        private UserControl_Favorites m_favoriteUserControl;
        private UserControl_Macro m_macroUserControl;
        private UserControl_Sync m_syncUserControl;

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

            m_favoriteUserControl = new UserControl_Favorites();
            panel_tab.Controls.Add(m_favoriteUserControl);
            m_favoriteUserControl.Dock = DockStyle.Fill;

            m_macroUserControl = new UserControl_Macro(PLCSimulator.Instance.MacroManager);
            panel_tab.Controls.Add(m_macroUserControl);
            m_macroUserControl.Dock = DockStyle.Fill;

            m_syncUserControl = new UserControl_Sync(PLCSimulator.Instance.SyncManager);
            panel_tab.Controls.Add(m_syncUserControl);
            m_syncUserControl.Dock = DockStyle.Fill;

            AddWordDataControl();
            AddBitDataControl();

            foreach (Control control in panel_tab.Controls)
                control.Hide();
            m_favoriteUserControl.Show();
        }

        private void AddBitDataControl()
        {
            foreach (var key in DataManager.Instance.BitDataDict.Keys)
            {
                var uc = new UserControl_Bit(key);
                panel_tab.Controls.Add(uc);
                uc.Dock = DockStyle.Fill;

                var btn = GetMenuButton(uc, key);
                flowLayoutPanel_menu.Controls.Add(btn);
            }
        }

        private void AddWordDataControl()
        {
            foreach (var key in DataManager.Instance.WordDataDict.Keys)
            {
                var uc = new UserControl_Word(key);
                panel_tab.Controls.Add(uc);
                uc.Dock = DockStyle.Fill;

                var btn = GetMenuButton(uc, key);
                flowLayoutPanel_menu.Controls.Add(btn);
            }
        }

        private Button GetMenuButton(UserControl uc, string code)
        {
            var btn = new Button();
            btn.BackColor = SystemColors.ButtonShadow;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Verdata", 12F);
            btn.ForeColor = Color.White;
            btn.Size = new Size(69, 41);
            btn.TabStop = false;
            btn.Text = code;
            btn.UseVisualStyleBackColor = false;
            btn.Click += Btn_Click;

            void Btn_Click(object sender, EventArgs e)
            {
                if (uc.Visible && panel_tab.Controls[0] == uc)
                    return;

                HidePanel();

                uc.BringToFront();
                uc.Show();
                SetSelectedButtonColor(sender, e);
            }

            return btn;
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
                HideAdditionalControl();
            else
            {
                m_macroUserControl.BringToFront();
                m_macroUserControl.Show();
            }
        }

        private void button_Sync_Click(object sender, EventArgs e)
        {
            if (m_syncUserControl.Visible)
                HideAdditionalControl();
            else
            {
                m_syncUserControl.BringToFront();
                m_syncUserControl.Show();
            }
        }

        private void HideAdditionalControl()
        {
            m_macroUserControl.Hide();
            m_syncUserControl.Hide();
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
            foreach (Control control in flowLayoutPanel_menu.Controls)
                if (control is Button)
                    control.BackColor = SystemColors.ButtonShadow;
            foreach (Control control in panel_SubMenu.Controls)
                if (control is Button)
                    control.BackColor = SystemColors.ButtonShadow;
        }
    }
}