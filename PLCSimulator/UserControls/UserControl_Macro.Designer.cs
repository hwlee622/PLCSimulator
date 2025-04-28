namespace PLCSimulator
{
    partial class UserControl_Macro
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_Data = new System.Windows.Forms.Panel();
            this.dataGridView_Macro = new System.Windows.Forms.DataGridView();
            this.timer_gui_update = new System.Windows.Forms.Timer(this.components);
            this.panel_control = new System.Windows.Forms.Panel();
            this.label_Step = new System.Windows.Forms.Label();
            this.button_RemoveMacro = new System.Windows.Forms.Button();
            this.button_AddMacro = new System.Windows.Forms.Button();
            this.button_RunMacro = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Macro)).BeginInit();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Data
            // 
            this.panel_Data.Controls.Add(this.dataGridView_Macro);
            this.panel_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Data.Location = new System.Drawing.Point(0, 58);
            this.panel_Data.Name = "panel_Data";
            this.panel_Data.Size = new System.Drawing.Size(361, 271);
            this.panel_Data.TabIndex = 16;
            // 
            // dataGridView_Macro
            // 
            this.dataGridView_Macro.AllowUserToAddRows = false;
            this.dataGridView_Macro.AllowUserToDeleteRows = false;
            this.dataGridView_Macro.AllowUserToResizeColumns = false;
            this.dataGridView_Macro.AllowUserToResizeRows = false;
            this.dataGridView_Macro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Macro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView_Macro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Macro.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Macro.Name = "dataGridView_Macro";
            this.dataGridView_Macro.RowHeadersVisible = false;
            this.dataGridView_Macro.RowTemplate.Height = 23;
            this.dataGridView_Macro.Size = new System.Drawing.Size(361, 271);
            this.dataGridView_Macro.TabIndex = 0;
            this.dataGridView_Macro.VirtualMode = true;
            this.dataGridView_Macro.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Macro_CellValueNeeded);
            this.dataGridView_Macro.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Macro_CellValuePushed);
            // 
            // timer_gui_update
            // 
            this.timer_gui_update.Tick += new System.EventHandler(this.timer_gui_update_Tick);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.label_Step);
            this.panel_control.Controls.Add(this.button_RemoveMacro);
            this.panel_control.Controls.Add(this.button_AddMacro);
            this.panel_control.Controls.Add(this.button_RunMacro);
            this.panel_control.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_control.Location = new System.Drawing.Point(0, 0);
            this.panel_control.Margin = new System.Windows.Forms.Padding(0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(361, 58);
            this.panel_control.TabIndex = 17;
            // 
            // label_Step
            // 
            this.label_Step.AutoSize = true;
            this.label_Step.ForeColor = System.Drawing.Color.White;
            this.label_Step.Location = new System.Drawing.Point(3, 8);
            this.label_Step.Name = "label_Step";
            this.label_Step.Size = new System.Drawing.Size(30, 12);
            this.label_Step.TabIndex = 7;
            this.label_Step.Text = "Step";
            // 
            // button_RemoveMacro
            // 
            this.button_RemoveMacro.Location = new System.Drawing.Point(84, 29);
            this.button_RemoveMacro.Name = "button_RemoveMacro";
            this.button_RemoveMacro.Size = new System.Drawing.Size(75, 23);
            this.button_RemoveMacro.TabIndex = 6;
            this.button_RemoveMacro.Text = "Remove";
            this.button_RemoveMacro.UseVisualStyleBackColor = true;
            this.button_RemoveMacro.Click += new System.EventHandler(this.button_RemoveMacro_Click);
            // 
            // button_AddMacro
            // 
            this.button_AddMacro.Location = new System.Drawing.Point(3, 29);
            this.button_AddMacro.Name = "button_AddMacro";
            this.button_AddMacro.Size = new System.Drawing.Size(75, 23);
            this.button_AddMacro.TabIndex = 5;
            this.button_AddMacro.Text = "Add";
            this.button_AddMacro.UseVisualStyleBackColor = true;
            this.button_AddMacro.Click += new System.EventHandler(this.button_AddMacro_Click);
            // 
            // button_RunMacro
            // 
            this.button_RunMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RunMacro.Location = new System.Drawing.Point(283, 29);
            this.button_RunMacro.Name = "button_RunMacro";
            this.button_RunMacro.Size = new System.Drawing.Size(75, 23);
            this.button_RunMacro.TabIndex = 4;
            this.button_RunMacro.Text = "Start";
            this.button_RunMacro.UseVisualStyleBackColor = true;
            this.button_RunMacro.Click += new System.EventHandler(this.button_RunMacro_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Type";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Address";
            this.Column2.Name = "Column2";
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Value";
            this.Column3.Name = "Column3";
            // 
            // UserControl_Macro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.panel_Data);
            this.Controls.Add(this.panel_control);
            this.Name = "UserControl_Macro";
            this.Size = new System.Drawing.Size(361, 329);
            this.Load += new System.EventHandler(this.UserControl_Macro_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControl_Macro_VisibleChanged);
            this.panel_Data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Macro)).EndInit();
            this.panel_control.ResumeLayout(false);
            this.panel_control.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Data;
        private System.Windows.Forms.DataGridView dataGridView_Macro;
        private System.Windows.Forms.Timer timer_gui_update;
        private System.Windows.Forms.Panel panel_control;
        private System.Windows.Forms.Button button_RemoveMacro;
        private System.Windows.Forms.Button button_AddMacro;
        private System.Windows.Forms.Button button_RunMacro;
        private System.Windows.Forms.Label label_Step;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
