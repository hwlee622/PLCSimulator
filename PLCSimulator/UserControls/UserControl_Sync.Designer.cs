namespace PLCSimulator
{
    partial class UserControl_Sync
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
            this.button_Reconnect = new System.Windows.Forms.Button();
            this.panel_control = new System.Windows.Forms.Panel();
            this.panel_input = new System.Windows.Forms.Panel();
            this.dataGridView_Input = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_myPlc = new System.Windows.Forms.TextBox();
            this.label_input = new System.Windows.Forms.Label();
            this.panel_output = new System.Windows.Forms.Panel();
            this.dataGridView_Output = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_syncPlc = new System.Windows.Forms.TextBox();
            this.label_output = new System.Windows.Forms.Label();
            this.timer_gui_update = new System.Windows.Forms.Timer(this.components);
            this.label_input_connected = new System.Windows.Forms.Label();
            this.label_output_connected = new System.Windows.Forms.Label();
            this.panel_control.SuspendLayout();
            this.panel_input.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Input)).BeginInit();
            this.panel_output.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Output)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Reconnect
            // 
            this.button_Reconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Reconnect.Location = new System.Drawing.Point(283, 3);
            this.button_Reconnect.Name = "button_Reconnect";
            this.button_Reconnect.Size = new System.Drawing.Size(75, 23);
            this.button_Reconnect.TabIndex = 5;
            this.button_Reconnect.Text = "Reconnect";
            this.button_Reconnect.UseVisualStyleBackColor = true;
            this.button_Reconnect.Click += new System.EventHandler(this.button_Reconnect_Click);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.button_Reconnect);
            this.panel_control.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_control.Location = new System.Drawing.Point(0, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(361, 32);
            this.panel_control.TabIndex = 6;
            // 
            // panel_input
            // 
            this.panel_input.Controls.Add(this.dataGridView_Input);
            this.panel_input.Controls.Add(this.textBox_myPlc);
            this.panel_input.Controls.Add(this.label_input_connected);
            this.panel_input.Controls.Add(this.label_input);
            this.panel_input.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_input.Location = new System.Drawing.Point(0, 32);
            this.panel_input.Name = "panel_input";
            this.panel_input.Size = new System.Drawing.Size(180, 297);
            this.panel_input.TabIndex = 7;
            // 
            // dataGridView_Input
            // 
            this.dataGridView_Input.AllowUserToAddRows = false;
            this.dataGridView_Input.AllowUserToDeleteRows = false;
            this.dataGridView_Input.AllowUserToResizeRows = false;
            this.dataGridView_Input.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Input.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView_Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Input.Location = new System.Drawing.Point(0, 41);
            this.dataGridView_Input.MultiSelect = false;
            this.dataGridView_Input.Name = "dataGridView_Input";
            this.dataGridView_Input.RowHeadersVisible = false;
            this.dataGridView_Input.RowTemplate.Height = 23;
            this.dataGridView_Input.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Input.Size = new System.Drawing.Size(180, 256);
            this.dataGridView_Input.TabIndex = 8;
            this.dataGridView_Input.VirtualMode = true;
            this.dataGridView_Input.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Input_CellValueNeeded);
            this.dataGridView_Input.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Input_CellValuePushed);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Address";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 78;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Description";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // textBox_myPlc
            // 
            this.textBox_myPlc.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_myPlc.Location = new System.Drawing.Point(0, 20);
            this.textBox_myPlc.Name = "textBox_myPlc";
            this.textBox_myPlc.Size = new System.Drawing.Size(180, 21);
            this.textBox_myPlc.TabIndex = 6;
            // 
            // label_input
            // 
            this.label_input.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_input.ForeColor = System.Drawing.Color.White;
            this.label_input.Location = new System.Drawing.Point(0, 0);
            this.label_input.Name = "label_input";
            this.label_input.Size = new System.Drawing.Size(180, 20);
            this.label_input.TabIndex = 1;
            this.label_input.Text = "    Prev Sync Name";
            // 
            // panel_output
            // 
            this.panel_output.Controls.Add(this.label_output_connected);
            this.panel_output.Controls.Add(this.dataGridView_Output);
            this.panel_output.Controls.Add(this.textBox_syncPlc);
            this.panel_output.Controls.Add(this.label_output);
            this.panel_output.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_output.Location = new System.Drawing.Point(181, 32);
            this.panel_output.Name = "panel_output";
            this.panel_output.Size = new System.Drawing.Size(180, 297);
            this.panel_output.TabIndex = 8;
            // 
            // dataGridView_Output
            // 
            this.dataGridView_Output.AllowUserToAddRows = false;
            this.dataGridView_Output.AllowUserToDeleteRows = false;
            this.dataGridView_Output.AllowUserToResizeRows = false;
            this.dataGridView_Output.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Output.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Output.Location = new System.Drawing.Point(0, 41);
            this.dataGridView_Output.MultiSelect = false;
            this.dataGridView_Output.Name = "dataGridView_Output";
            this.dataGridView_Output.RowHeadersVisible = false;
            this.dataGridView_Output.RowTemplate.Height = 23;
            this.dataGridView_Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Output.Size = new System.Drawing.Size(180, 256);
            this.dataGridView_Output.TabIndex = 7;
            this.dataGridView_Output.VirtualMode = true;
            this.dataGridView_Output.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Output_CellValueNeeded);
            this.dataGridView_Output.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Output_CellValuePushed);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Address";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 78;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // textBox_syncPlc
            // 
            this.textBox_syncPlc.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_syncPlc.Location = new System.Drawing.Point(0, 20);
            this.textBox_syncPlc.Name = "textBox_syncPlc";
            this.textBox_syncPlc.Size = new System.Drawing.Size(180, 21);
            this.textBox_syncPlc.TabIndex = 6;
            // 
            // label_output
            // 
            this.label_output.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_output.ForeColor = System.Drawing.Color.White;
            this.label_output.Location = new System.Drawing.Point(0, 0);
            this.label_output.Name = "label_output";
            this.label_output.Size = new System.Drawing.Size(180, 20);
            this.label_output.TabIndex = 2;
            this.label_output.Text = "    Next Sync Name";
            // 
            // timer_gui_update
            // 
            this.timer_gui_update.Tick += new System.EventHandler(this.timer_gui_update_Tick);
            // 
            // label_input_connected
            // 
            this.label_input_connected.BackColor = System.Drawing.Color.Tomato;
            this.label_input_connected.Location = new System.Drawing.Point(4, 1);
            this.label_input_connected.Name = "label_input_connected";
            this.label_input_connected.Size = new System.Drawing.Size(9, 9);
            this.label_input_connected.TabIndex = 8;
            // 
            // label_output_connected
            // 
            this.label_output_connected.BackColor = System.Drawing.Color.Tomato;
            this.label_output_connected.Location = new System.Drawing.Point(4, 1);
            this.label_output_connected.Name = "label_output_connected";
            this.label_output_connected.Size = new System.Drawing.Size(9, 9);
            this.label_output_connected.TabIndex = 9;
            // 
            // UserControl_Sync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.panel_output);
            this.Controls.Add(this.panel_input);
            this.Controls.Add(this.panel_control);
            this.Name = "UserControl_Sync";
            this.Size = new System.Drawing.Size(361, 329);
            this.Load += new System.EventHandler(this.UserControl_Sync_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControl_Sync_VisibleChanged);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.UserControl_Sync_Layout);
            this.panel_control.ResumeLayout(false);
            this.panel_input.ResumeLayout(false);
            this.panel_input.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Input)).EndInit();
            this.panel_output.ResumeLayout(false);
            this.panel_output.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Output)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Reconnect;
        private System.Windows.Forms.Panel panel_control;
        private System.Windows.Forms.Panel panel_input;
        private System.Windows.Forms.Panel panel_output;
        private System.Windows.Forms.Label label_input;
        private System.Windows.Forms.Label label_output;
        private System.Windows.Forms.TextBox textBox_myPlc;
        private System.Windows.Forms.TextBox textBox_syncPlc;
        private System.Windows.Forms.DataGridView dataGridView_Input;
        private System.Windows.Forms.DataGridView dataGridView_Output;
        private System.Windows.Forms.Timer timer_gui_update;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label_input_connected;
        private System.Windows.Forms.Label label_output_connected;
    }
}
