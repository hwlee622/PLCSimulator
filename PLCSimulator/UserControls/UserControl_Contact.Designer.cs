namespace PLCSimulator
{
    partial class UserControl_Contact
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
            this.panel_Contact = new System.Windows.Forms.Panel();
            this.dataGridView_Contact = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer_gui_update = new System.Windows.Forms.Timer(this.components);
            this.label_search = new System.Windows.Forms.Label();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.panel_control = new System.Windows.Forms.Panel();
            this.panel_Contact.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Contact)).BeginInit();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Contact
            // 
            this.panel_Contact.Controls.Add(this.dataGridView_Contact);
            this.panel_Contact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Contact.Location = new System.Drawing.Point(0, 58);
            this.panel_Contact.Name = "panel_Contact";
            this.panel_Contact.Size = new System.Drawing.Size(361, 178);
            this.panel_Contact.TabIndex = 4;
            // 
            // dataGridView_Contact
            // 
            this.dataGridView_Contact.AllowUserToAddRows = false;
            this.dataGridView_Contact.AllowUserToDeleteRows = false;
            this.dataGridView_Contact.AllowUserToResizeColumns = false;
            this.dataGridView_Contact.AllowUserToResizeRows = false;
            this.dataGridView_Contact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Contact.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView_Contact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Contact.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Contact.Name = "dataGridView_Contact";
            this.dataGridView_Contact.RowHeadersVisible = false;
            this.dataGridView_Contact.RowTemplate.Height = 23;
            this.dataGridView_Contact.Size = new System.Drawing.Size(361, 178);
            this.dataGridView_Contact.TabIndex = 0;
            this.dataGridView_Contact.VirtualMode = true;
            this.dataGridView_Contact.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Contact_CellValueNeeded);
            this.dataGridView_Contact.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Contact_CellValuePushed);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Address";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 78;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Description";
            this.Column3.Name = "Column3";
            // 
            // timer_gui_update
            // 
            this.timer_gui_update.Tick += new System.EventHandler(this.timer_gui_update_Tick);
            // 
            // label_search
            // 
            this.label_search.AutoSize = true;
            this.label_search.ForeColor = System.Drawing.Color.White;
            this.label_search.Location = new System.Drawing.Point(3, 34);
            this.label_search.Name = "label_search";
            this.label_search.Size = new System.Drawing.Size(45, 12);
            this.label_search.TabIndex = 0;
            this.label_search.Text = "Search";
            // 
            // textBox_search
            // 
            this.textBox_search.Location = new System.Drawing.Point(54, 31);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(100, 21);
            this.textBox_search.TabIndex = 1;
            this.textBox_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_search_KeyDown);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.label_search);
            this.panel_control.Controls.Add(this.textBox_search);
            this.panel_control.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_control.Location = new System.Drawing.Point(0, 0);
            this.panel_control.Margin = new System.Windows.Forms.Padding(0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(361, 58);
            this.panel_control.TabIndex = 0;
            // 
            // UserControl_Contact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.panel_Contact);
            this.Controls.Add(this.panel_control);
            this.Name = "UserControl_Contact";
            this.Size = new System.Drawing.Size(361, 236);
            this.Load += new System.EventHandler(this.UserControl_Contact_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControl_Contact_VisibleChanged);
            this.panel_Contact.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Contact)).EndInit();
            this.panel_control.ResumeLayout(false);
            this.panel_control.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Contact;
        private System.Windows.Forms.DataGridView dataGridView_Contact;
        private System.Windows.Forms.Timer timer_gui_update;
        private System.Windows.Forms.Label label_search;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Panel panel_control;
    }
}
