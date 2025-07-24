using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class BaseForm : Form
    {
        private Dictionary<Button, UserControl> m_tabDict = new Dictionary<Button, UserControl>();

        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (ProfileDlg dlg = new ProfileDlg())
                {
                    if (dlg.ShowDialog() == DialogResult.Cancel)
                    {
                        Application.Exit();
                        return;
                    }
                }

                AppInstance.Instance.Start();

                IniHandler ihandler = new IniHandler("Setting/Config.ini");
                string sMewtocolPort = ihandler.GetProfilesString("PanasonicPLC", "Port");
                string sUpperLinkPort = ihandler.GetProfilesString("OmronPLC", "Port");

                Text = $"{Text} {ProfileRecipe.Instance.ProfileInfo.Protocol} : {ProfileRecipe.Instance.ProfileInfo.Port}";
                notifyIcon_system.Text = Text;

                AddTab(button_Favorite, new UserControl_Favorites());
                AddTab(button_Macro, new UserControl_Macro(AppInstance.Instance.MacroManager));
                AddTab(button_Sync, new UserControl_Sync(AppInstance.Instance.SyncManager));

                AddWordDataControl();
                AddBitDataControl();

                button_Tab_Click(button_Favorite, null);
            }
            catch
            {
            }
        }

        private void AddWordDataControl()
        {
            foreach (var key in DataManager.Instance.WordDataDict.Keys)
            {
                var btn = GetMenuButton(key);
                AddTab(btn, new UserControl_Word(key));
            }
        }

        private void AddBitDataControl()
        {
            foreach (var key in DataManager.Instance.BitDataDict.Keys)
            {
                var btn = GetMenuButton(key);
                AddTab(btn, new UserControl_Bit(key));
            }
        }

        private Button GetMenuButton(string code)
        {
            var btn = new UntabButton();
            btn.BackColor = SystemColors.ButtonShadow;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Verdata", 12F);
            btn.ForeColor = Color.White;
            btn.Size = new Size(69, 41);
            btn.TabStop = false;
            btn.Text = code;
            btn.UseVisualStyleBackColor = false;
            flowLayoutPanel_menu.Controls.Add(btn);
            return btn;
        }

        private void AddTab(Button button, UserControl tabControl)
        {
            m_tabDict[button] = tabControl;
            tabControl.Dock = DockStyle.Fill;
            panel_tab.Controls.Add(tabControl);
            button.Click += button_Tab_Click;
        }

        public void button_Tab_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null || !m_tabDict.ContainsKey(button))
                return;
            var tab = m_tabDict[button];
            if (tab == null || panel_tab.Controls[0] == tab)
                return;

            foreach (var item in m_tabDict)
            {
                item.Key.BackColor = SystemColors.ButtonShadow;
                item.Value.Hide();
            }

            button.BackColor = SystemColors.ControlLight;
            tab.BringToFront();
            tab.Show();
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppInstance.Instance.Stop();
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
    }
}