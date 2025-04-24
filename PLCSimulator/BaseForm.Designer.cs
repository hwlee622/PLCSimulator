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
            this.button_Data = new System.Windows.Forms.Button();
            this.button_contactR = new System.Windows.Forms.Button();
            this.button_contactX = new System.Windows.Forms.Button();
            this.button_contactY = new System.Windows.Forms.Button();
            this.panel_button = new System.Windows.Forms.Panel();
            this.button_Favorite = new System.Windows.Forms.Button();
            this.panel_tab = new System.Windows.Forms.Panel();
            this.contextMenuStrip_system.SuspendLayout();
            this.panel_button.SuspendLayout();
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
            // button_Data
            // 
            this.button_Data.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Data.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Data.Font = new System.Drawing.Font("Verdana", 12F);
            this.button_Data.ForeColor = System.Drawing.Color.White;
            this.button_Data.Location = new System.Drawing.Point(12, 11);
            this.button_Data.Name = "button_Data";
            this.button_Data.Size = new System.Drawing.Size(69, 41);
            this.button_Data.TabIndex = 0;
            this.button_Data.TabStop = false;
            this.button_Data.Text = "DATA";
            this.button_Data.UseVisualStyleBackColor = false;
            this.button_Data.Click += new System.EventHandler(this.button_Data_Click);
            // 
            // button_contactR
            // 
            this.button_contactR.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_contactR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_contactR.Font = new System.Drawing.Font("Verdana", 12F);
            this.button_contactR.ForeColor = System.Drawing.Color.White;
            this.button_contactR.Location = new System.Drawing.Point(87, 11);
            this.button_contactR.Name = "button_contactR";
            this.button_contactR.Size = new System.Drawing.Size(69, 41);
            this.button_contactR.TabIndex = 1;
            this.button_contactR.TabStop = false;
            this.button_contactR.Text = "R";
            this.button_contactR.UseVisualStyleBackColor = false;
            this.button_contactR.Click += new System.EventHandler(this.button_contactR_Click);
            // 
            // button_contactX
            // 
            this.button_contactX.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_contactX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_contactX.Font = new System.Drawing.Font("Verdana", 12F);
            this.button_contactX.ForeColor = System.Drawing.Color.White;
            this.button_contactX.Location = new System.Drawing.Point(162, 11);
            this.button_contactX.Name = "button_contactX";
            this.button_contactX.Size = new System.Drawing.Size(69, 41);
            this.button_contactX.TabIndex = 2;
            this.button_contactX.TabStop = false;
            this.button_contactX.Text = "X";
            this.button_contactX.UseVisualStyleBackColor = false;
            this.button_contactX.Click += new System.EventHandler(this.button_contactX_Click);
            // 
            // button_contactY
            // 
            this.button_contactY.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_contactY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_contactY.Font = new System.Drawing.Font("Verdana", 12F);
            this.button_contactY.ForeColor = System.Drawing.Color.White;
            this.button_contactY.Location = new System.Drawing.Point(237, 11);
            this.button_contactY.Name = "button_contactY";
            this.button_contactY.Size = new System.Drawing.Size(69, 41);
            this.button_contactY.TabIndex = 3;
            this.button_contactY.TabStop = false;
            this.button_contactY.Text = "Y";
            this.button_contactY.UseVisualStyleBackColor = false;
            this.button_contactY.Click += new System.EventHandler(this.button_contactY_Click);
            // 
            // panel_button
            // 
            this.panel_button.Controls.Add(this.button_Favorite);
            this.panel_button.Controls.Add(this.button_contactX);
            this.panel_button.Controls.Add(this.button_contactY);
            this.panel_button.Controls.Add(this.button_Data);
            this.panel_button.Controls.Add(this.button_contactR);
            this.panel_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_button.Location = new System.Drawing.Point(0, 0);
            this.panel_button.Name = "panel_button";
            this.panel_button.Size = new System.Drawing.Size(393, 58);
            this.panel_button.TabIndex = 0;
            // 
            // button_Favorite
            // 
            this.button_Favorite.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Favorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Favorite.Font = new System.Drawing.Font("Segoe MDL2 Assets", 16F);
            this.button_Favorite.ForeColor = System.Drawing.Color.White;
            this.button_Favorite.Location = new System.Drawing.Point(312, 11);
            this.button_Favorite.Name = "button_Favorite";
            this.button_Favorite.Size = new System.Drawing.Size(69, 41);
            this.button_Favorite.TabIndex = 4;
            this.button_Favorite.TabStop = false;
            this.button_Favorite.Text = "";
            this.button_Favorite.UseVisualStyleBackColor = false;
            this.button_Favorite.Click += new System.EventHandler(this.button_Favorite_Click);
            // 
            // panel_tab
            // 
            this.panel_tab.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_tab.Location = new System.Drawing.Point(0, 58);
            this.panel_tab.Name = "panel_tab";
            this.panel_tab.Size = new System.Drawing.Size(393, 403);
            this.panel_tab.TabIndex = 1;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(393, 461);
            this.Controls.Add(this.panel_tab);
            this.Controls.Add(this.panel_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(409, 500);
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLCSimulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseForm_FormClosing);
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.contextMenuStrip_system.ResumeLayout(false);
            this.panel_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon_system;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_system;
        private System.Windows.Forms.ToolStripMenuItem 화면열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.Button button_Data;
        private System.Windows.Forms.Button button_contactR;
        private System.Windows.Forms.Button button_contactX;
        private System.Windows.Forms.Button button_contactY;
        private System.Windows.Forms.Panel panel_button;
        private System.Windows.Forms.Panel panel_tab;
        private System.Windows.Forms.Button button_Favorite;
    }
}

