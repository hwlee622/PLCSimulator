﻿namespace PLCSimulator
{
    partial class UserControl_Word
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
            this.dataGridView_Word = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.label_search = new System.Windows.Forms.Label();
            this.timer_gui_update = new System.Windows.Forms.Timer(this.components);
            this.radioButton_int = new System.Windows.Forms.RadioButton();
            this.radioButton_short = new System.Windows.Forms.RadioButton();
            this.radioButton_ASCII = new System.Windows.Forms.RadioButton();
            this.radioButton_hex = new System.Windows.Forms.RadioButton();
            this.panel_control = new System.Windows.Forms.Panel();
            this.panel_Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Word)).BeginInit();
            this.panel_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Data
            // 
            this.panel_Data.Controls.Add(this.dataGridView_Word);
            this.panel_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Data.Location = new System.Drawing.Point(0, 58);
            this.panel_Data.Name = "panel_Data";
            this.panel_Data.Size = new System.Drawing.Size(361, 271);
            this.panel_Data.TabIndex = 3;
            // 
            // dataGridView_Word
            // 
            this.dataGridView_Word.AllowUserToAddRows = false;
            this.dataGridView_Word.AllowUserToDeleteRows = false;
            this.dataGridView_Word.AllowUserToResizeRows = false;
            this.dataGridView_Word.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Word.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView_Word.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Word.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Word.MultiSelect = false;
            this.dataGridView_Word.Name = "dataGridView_Word";
            this.dataGridView_Word.RowHeadersVisible = false;
            this.dataGridView_Word.RowTemplate.Height = 23;
            this.dataGridView_Word.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Word.Size = new System.Drawing.Size(361, 271);
            this.dataGridView_Word.TabIndex = 0;
            this.dataGridView_Word.VirtualMode = true;
            this.dataGridView_Word.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Word_CellValueNeeded);
            this.dataGridView_Word.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView_Data_CellValuePushed);
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
            // textBox_search
            // 
            this.textBox_search.Location = new System.Drawing.Point(54, 31);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(100, 21);
            this.textBox_search.TabIndex = 5;
            this.textBox_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_search_KeyDown);
            // 
            // label_search
            // 
            this.label_search.AutoSize = true;
            this.label_search.ForeColor = System.Drawing.Color.White;
            this.label_search.Location = new System.Drawing.Point(3, 34);
            this.label_search.Name = "label_search";
            this.label_search.Size = new System.Drawing.Size(45, 12);
            this.label_search.TabIndex = 4;
            this.label_search.Text = "Search";
            // 
            // timer_gui_update
            // 
            this.timer_gui_update.Tick += new System.EventHandler(this.timer_gui_update_Tick);
            // 
            // radioButton_int
            // 
            this.radioButton_int.AutoSize = true;
            this.radioButton_int.ForeColor = System.Drawing.Color.White;
            this.radioButton_int.Location = new System.Drawing.Point(121, 3);
            this.radioButton_int.Name = "radioButton_int";
            this.radioButton_int.Size = new System.Drawing.Size(36, 16);
            this.radioButton_int.TabIndex = 2;
            this.radioButton_int.Text = "Int";
            this.radioButton_int.UseVisualStyleBackColor = true;
            this.radioButton_int.CheckedChanged += new System.EventHandler(this.radioButton_format_CheckedChanged);
            // 
            // radioButton_short
            // 
            this.radioButton_short.AutoSize = true;
            this.radioButton_short.ForeColor = System.Drawing.Color.White;
            this.radioButton_short.Location = new System.Drawing.Point(63, 3);
            this.radioButton_short.Name = "radioButton_short";
            this.radioButton_short.Size = new System.Drawing.Size(52, 16);
            this.radioButton_short.TabIndex = 1;
            this.radioButton_short.Text = "Short";
            this.radioButton_short.UseVisualStyleBackColor = true;
            this.radioButton_short.CheckedChanged += new System.EventHandler(this.radioButton_format_CheckedChanged);
            // 
            // radioButton_ASCII
            // 
            this.radioButton_ASCII.AutoSize = true;
            this.radioButton_ASCII.ForeColor = System.Drawing.Color.White;
            this.radioButton_ASCII.Location = new System.Drawing.Point(3, 3);
            this.radioButton_ASCII.Name = "radioButton_ASCII";
            this.radioButton_ASCII.Size = new System.Drawing.Size(54, 16);
            this.radioButton_ASCII.TabIndex = 0;
            this.radioButton_ASCII.Text = "ASCII";
            this.radioButton_ASCII.UseVisualStyleBackColor = true;
            this.radioButton_ASCII.CheckedChanged += new System.EventHandler(this.radioButton_format_CheckedChanged);
            // 
            // radioButton_hex
            // 
            this.radioButton_hex.AutoSize = true;
            this.radioButton_hex.ForeColor = System.Drawing.Color.White;
            this.radioButton_hex.Location = new System.Drawing.Point(163, 3);
            this.radioButton_hex.Name = "radioButton_hex";
            this.radioButton_hex.Size = new System.Drawing.Size(45, 16);
            this.radioButton_hex.TabIndex = 3;
            this.radioButton_hex.Text = "Hex";
            this.radioButton_hex.UseVisualStyleBackColor = true;
            this.radioButton_hex.CheckedChanged += new System.EventHandler(this.radioButton_format_CheckedChanged);
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.radioButton_hex);
            this.panel_control.Controls.Add(this.textBox_search);
            this.panel_control.Controls.Add(this.radioButton_int);
            this.panel_control.Controls.Add(this.label_search);
            this.panel_control.Controls.Add(this.radioButton_short);
            this.panel_control.Controls.Add(this.radioButton_ASCII);
            this.panel_control.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_control.Location = new System.Drawing.Point(0, 0);
            this.panel_control.Margin = new System.Windows.Forms.Padding(0);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(361, 58);
            this.panel_control.TabIndex = 0;
            // 
            // UserControl_Word
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.panel_Data);
            this.Controls.Add(this.panel_control);
            this.Name = "UserControl_Word";
            this.Size = new System.Drawing.Size(361, 329);
            this.Load += new System.EventHandler(this.UserControlData_Load);
            this.VisibleChanged += new System.EventHandler(this.UserControlData_VisibleChanged);
            this.panel_Data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Word)).EndInit();
            this.panel_control.ResumeLayout(false);
            this.panel_control.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Data;
        private System.Windows.Forms.DataGridView dataGridView_Word;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.Label label_search;
        private System.Windows.Forms.Timer timer_gui_update;
        private System.Windows.Forms.RadioButton radioButton_int;
        private System.Windows.Forms.RadioButton radioButton_short;
        private System.Windows.Forms.RadioButton radioButton_ASCII;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.RadioButton radioButton_hex;
        private System.Windows.Forms.Panel panel_control;
    }
}
