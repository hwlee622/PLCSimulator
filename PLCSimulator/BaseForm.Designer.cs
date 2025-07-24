namespace PLCSimulator
{
    partial class BaseForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.notifyIcon_system = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_system = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.화면열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_Favorite = new PLCSimulator.UntabButton();
            this.panel_SubMenu = new System.Windows.Forms.Panel();
            this.button_Sync = new PLCSimulator.UntabButton();
            this.button_Macro = new PLCSimulator.UntabButton();
            this.flowLayoutPanel_menu = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_tab = new System.Windows.Forms.DoubleBufferedPanel();
            this.contextMenuStrip_system.SuspendLayout();
            this.panel_SubMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon_system
            // 
            this.notifyIcon_system.ContextMenuStrip = this.contextMenuStrip_system;
            this.notifyIcon_system.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_system.Icon")));
            this.notifyIcon_system.Text = "PLCSimulator";
            this.notifyIcon_system.Visible = true;
            this.notifyIcon_system.DoubleClick += new System.EventHandler(this.notifyIcon_system_DoubleClick);
            // 
            // contextMenuStrip_system
            // 
            this.contextMenuStrip_system.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.화면열기ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.contextMenuStrip_system.Name = "contextMenuStrip_system";
            this.contextMenuStrip_system.Size = new System.Drawing.Size(99, 48);
            // 
            // 화면열기ToolStripMenuItem
            // 
            this.화면열기ToolStripMenuItem.Name = "화면열기ToolStripMenuItem";
            this.화면열기ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.화면열기ToolStripMenuItem.Text = "열기";
            this.화면열기ToolStripMenuItem.Click += new System.EventHandler(this.화면열기ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // button_Favorite
            // 
            this.button_Favorite.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Favorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Favorite.Font = new System.Drawing.Font("Segoe MDL2 Assets", 16F);
            this.button_Favorite.ForeColor = System.Drawing.Color.White;
            this.button_Favorite.Location = new System.Drawing.Point(12, 8);
            this.button_Favorite.Name = "button_Favorite";
            this.button_Favorite.Size = new System.Drawing.Size(69, 41);
            this.button_Favorite.TabIndex = 4;
            this.button_Favorite.TabStop = false;
            this.button_Favorite.Text = "";
            this.button_Favorite.UseVisualStyleBackColor = false;
            // 
            // panel_SubMenu
            // 
            this.panel_SubMenu.Controls.Add(this.button_Sync);
            this.panel_SubMenu.Controls.Add(this.button_Favorite);
            this.panel_SubMenu.Controls.Add(this.button_Macro);
            this.panel_SubMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_SubMenu.Location = new System.Drawing.Point(0, 403);
            this.panel_SubMenu.Name = "panel_SubMenu";
            this.panel_SubMenu.Size = new System.Drawing.Size(393, 58);
            this.panel_SubMenu.TabIndex = 2;
            // 
            // button_Sync
            // 
            this.button_Sync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Sync.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Sync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Sync.Font = new System.Drawing.Font("Verdana", 12F);
            this.button_Sync.ForeColor = System.Drawing.Color.White;
            this.button_Sync.Location = new System.Drawing.Point(237, 8);
            this.button_Sync.Name = "button_Sync";
            this.button_Sync.Size = new System.Drawing.Size(69, 41);
            this.button_Sync.TabIndex = 5;
            this.button_Sync.TabStop = false;
            this.button_Sync.Text = "Sync";
            this.button_Sync.UseVisualStyleBackColor = false;
            // 
            // button_Macro
            // 
            this.button_Macro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Macro.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Macro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Macro.Font = new System.Drawing.Font("Verdana", 12F);
            this.button_Macro.ForeColor = System.Drawing.Color.White;
            this.button_Macro.Location = new System.Drawing.Point(312, 8);
            this.button_Macro.Name = "button_Macro";
            this.button_Macro.Size = new System.Drawing.Size(69, 41);
            this.button_Macro.TabIndex = 4;
            this.button_Macro.TabStop = false;
            this.button_Macro.Text = "Macro";
            this.button_Macro.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel_menu
            // 
            this.flowLayoutPanel_menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_menu.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_menu.Name = "flowLayoutPanel_menu";
            this.flowLayoutPanel_menu.Padding = new System.Windows.Forms.Padding(6);
            this.flowLayoutPanel_menu.Size = new System.Drawing.Size(393, 58);
            this.flowLayoutPanel_menu.TabIndex = 3;
            // 
            // panel_tab
            // 
            this.panel_tab.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_tab.Location = new System.Drawing.Point(0, 58);
            this.panel_tab.Name = "panel_tab";
            this.panel_tab.Size = new System.Drawing.Size(393, 345);
            this.panel_tab.TabIndex = 1;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(393, 461);
            this.Controls.Add(this.panel_tab);
            this.Controls.Add(this.flowLayoutPanel_menu);
            this.Controls.Add(this.panel_SubMenu);
            this.MinimumSize = new System.Drawing.Size(409, 500);
            this.Name = "BaseForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLCSimulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseForm_FormClosing);
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.contextMenuStrip_system.ResumeLayout(false);
            this.panel_SubMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon_system;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_system;
        private System.Windows.Forms.ToolStripMenuItem 화면열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_SubMenu;
        private System.Windows.Forms.DoubleBufferedPanel panel_tab;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_menu;
        private UntabButton button_Favorite;
        private UntabButton button_Macro;
        private UntabButton button_Sync;
    }
}

