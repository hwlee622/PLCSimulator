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
            this.button_NextSync = new System.Windows.Forms.Button();
            this.panel_control = new System.Windows.Forms.Panel();
            this.button_PrevSync = new System.Windows.Forms.Button();
            this.panel_prev = new System.Windows.Forms.Panel();
            this.dataGridView_Prev = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_prevSyncName = new System.Windows.Forms.TextBox();
            this.label_prev_connected = new System.Windows.Forms.Label();
            this.label_input = new System.Windows.Forms.Label();
            this.panel_next = new System.Windows.Forms.Panel();
            this.label_next_connected = new System.Windows.Forms.Label();
            this.dataGridView_Next = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_nextSyncName = new System.Windows.Forms.TextBox();
            this.label_output = new System.Windows.Forms.Label();
            this.timer_gui_update = new System.Windows.Forms.Timer(this.components);
            this.panel_control.SuspendLayout();
            this.panel_prev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Prev)).BeginInit();
            this.panel_next.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Next)).BeginInit();
            this.SuspendLayout();
            // 
            // button_NextSync
            // 
            this.button_NextSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_NextSync.Location = new System.Drawing.Point(274, 3);
            this.button_NextSync.Name = "button_NextSync";
            this.button_NextSync.Size = new System.Drawing.Size(84, 23);
            this.button_NextSync.TabIndex = 5;
            this.button_NextSync.Text = "Connect";
            this.button_NextSync.UseVisualStyleBackColor = true;
            this.button_NextSync.Click += new System.EventHandler(this.button_NextSync_Click);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.button_PrevSync);
            this.panel_control.Controls.Add(this.button_NextSync);
            this.panel_control.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_control.Location = new System.Drawing.Point(0, 0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(361, 32);
            this.panel_control.TabIndex = 6;
            // 
            // button_PrevSync
            // 
            this.button_PrevSync.Location = new System.Drawing.Point(3, 3);
            this.button_PrevSync.Name = "button_PrevSync";
            this.button_PrevSync.Size = new System.Drawing.Size(84, 23);
            this.button_PrevSync.TabIndex = 6;
            this.button_PrevSync.Text = "Connect";
            this.button_PrevSync.UseVisualStyleBackColor = true;
            this.button_PrevSync.Click += new System.EventHandler(this.button_PrevSync_Click);
            // 
            // panel_prev
            // 
            this.panel_prev.Controls.Add(this.dataGridView_Prev);
            this.panel_prev.Controls.Add(this.textBox_prevSyncName);
            this.panel_prev.Controls.Add(this.label_prev_connected);
            this.panel_prev.Controls.Add(this.label_input);
            this.panel_prev.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_prev.Location = new System.Drawing.Point(0, 32);
            this.panel_prev.Name = "panel_prev";
            this.panel_prev.Size = new System.Drawing.Size(180, 297);
            this.panel_prev.TabIndex = 7;
            // 
            // dataGridView_Prev
            // 
            this.dataGridView_Prev.AllowUserToAddRows = false;
            this.dataGridView_Prev.AllowUserToDeleteRows = false;
            this.dataGridView_Prev.AllowUserToResizeRows = false;
            this.dataGridView_Prev.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Prev.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView_Prev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Prev.Location = new System.Drawing.Point(0, 41);
            this.dataGridView_Prev.MultiSelect = false;
            this.dataGridView_Prev.Name = "dataGridView_Prev";
            this.dataGridView_Prev.RowHeadersVisible = false;
            this.dataGridView_Prev.RowTemplate.Height = 23;
            this.dataGridView_Prev.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Prev.Size = new System.Drawing.Size(180, 256);
            this.dataGridView_Prev.TabIndex = 8;
            this.dataGridView_Prev.VirtualMode = true;
            this.dataGridView_Prev.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Prev_CellValueNeeded);
            this.dataGridView_Prev.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Prev_CellValuePushed);
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
            // textBox_prevSyncName
            // 
            this.textBox_prevSyncName.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_prevSyncName.Location = new System.Drawing.Point(0, 20);
            this.textBox_prevSyncName.Name = "textBox_prevSyncName";
            this.textBox_prevSyncName.Size = new System.Drawing.Size(180, 21);
            this.textBox_prevSyncName.TabIndex = 6;
            // 
            // label_prev_connected
            // 
            this.label_prev_connected.BackColor = System.Drawing.Color.Tomato;
            this.label_prev_connected.Location = new System.Drawing.Point(4, 1);
            this.label_prev_connected.Name = "label_prev_connected";
            this.label_prev_connected.Size = new System.Drawing.Size(9, 9);
            this.label_prev_connected.TabIndex = 8;
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
            // panel_next
            // 
            this.panel_next.Controls.Add(this.label_next_connected);
            this.panel_next.Controls.Add(this.dataGridView_Next);
            this.panel_next.Controls.Add(this.textBox_nextSyncName);
            this.panel_next.Controls.Add(this.label_output);
            this.panel_next.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_next.Location = new System.Drawing.Point(181, 32);
            this.panel_next.Name = "panel_next";
            this.panel_next.Size = new System.Drawing.Size(180, 297);
            this.panel_next.TabIndex = 8;
            // 
            // label_next_connected
            // 
            this.label_next_connected.BackColor = System.Drawing.Color.Tomato;
            this.label_next_connected.Location = new System.Drawing.Point(4, 1);
            this.label_next_connected.Name = "label_next_connected";
            this.label_next_connected.Size = new System.Drawing.Size(9, 9);
            this.label_next_connected.TabIndex = 9;
            // 
            // dataGridView_Next
            // 
            this.dataGridView_Next.AllowUserToAddRows = false;
            this.dataGridView_Next.AllowUserToDeleteRows = false;
            this.dataGridView_Next.AllowUserToResizeRows = false;
            this.dataGridView_Next.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Next.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView_Next.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Next.Location = new System.Drawing.Point(0, 41);
            this.dataGridView_Next.MultiSelect = false;
            this.dataGridView_Next.Name = "dataGridView_Next";
            this.dataGridView_Next.RowHeadersVisible = false;
            this.dataGridView_Next.RowTemplate.Height = 23;
            this.dataGridView_Next.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Next.Size = new System.Drawing.Size(180, 256);
            this.dataGridView_Next.TabIndex = 7;
            this.dataGridView_Next.VirtualMode = true;
            this.dataGridView_Next.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Next_CellValueNeeded);
            this.dataGridView_Next.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Next_CellValuePushed);
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
            // textBox_nextSyncName
            // 
            this.textBox_nextSyncName.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_nextSyncName.Location = new System.Drawing.Point(0, 20);
            this.textBox_nextSyncName.Name = "textBox_nextSyncName";
            this.textBox_nextSyncName.Size = new System.Drawing.Size(180, 21);
            this.textBox_nextSyncName.TabIndex = 6;
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
            // UserControl_Sync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.panel_next);
            this.Controls.Add(this.panel_prev);
            this.Controls.Add(this.panel_control);
            this.Name = "UserControl_Sync";
            this.Size = new System.Drawing.Size(361, 329);
            this.Load += new System.EventHandler(this.UserControl_Sync_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControl_Sync_VisibleChanged);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.UserControl_Sync_Layout);
            this.panel_control.ResumeLayout(false);
            this.panel_prev.ResumeLayout(false);
            this.panel_prev.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Prev)).EndInit();
            this.panel_next.ResumeLayout(false);
            this.panel_next.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Next)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_NextSync;
        private System.Windows.Forms.Panel panel_control;
        private System.Windows.Forms.Panel panel_prev;
        private System.Windows.Forms.Panel panel_next;
        private System.Windows.Forms.Label label_input;
        private System.Windows.Forms.Label label_output;
        private System.Windows.Forms.TextBox textBox_prevSyncName;
        private System.Windows.Forms.TextBox textBox_nextSyncName;
        private System.Windows.Forms.DataGridView dataGridView_Prev;
        private System.Windows.Forms.DataGridView dataGridView_Next;
        private System.Windows.Forms.Timer timer_gui_update;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label_prev_connected;
        private System.Windows.Forms.Label label_next_connected;
        private System.Windows.Forms.Button button_PrevSync;
    }
}
