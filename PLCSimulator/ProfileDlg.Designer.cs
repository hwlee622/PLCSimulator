namespace PLCSimulator
{
    partial class ProfileDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_Protocol = new System.Windows.Forms.ComboBox();
            this.button_Data = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label_Port = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label_Protocol = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_Protocol
            // 
            this.comboBox_Protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Protocol.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Protocol.FormattingEnabled = true;
            this.comboBox_Protocol.Location = new System.Drawing.Point(12, 30);
            this.comboBox_Protocol.Name = "comboBox_Protocol";
            this.comboBox_Protocol.Size = new System.Drawing.Size(154, 26);
            this.comboBox_Protocol.TabIndex = 0;
            // 
            // button_Data
            // 
            this.button_Data.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Data.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Data.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Data.ForeColor = System.Drawing.Color.White;
            this.button_Data.Location = new System.Drawing.Point(12, 193);
            this.button_Data.Name = "button_Data";
            this.button_Data.Size = new System.Drawing.Size(69, 41);
            this.button_Data.TabIndex = 6;
            this.button_Data.Text = "Start";
            this.button_Data.UseVisualStyleBackColor = false;
            this.button_Data.Click += new System.EventHandler(this.button_Data_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(97, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 41);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Port.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Port.ForeColor = System.Drawing.Color.White;
            this.label_Port.Location = new System.Drawing.Point(12, 70);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(46, 18);
            this.label_Port.TabIndex = 8;
            this.label_Port.Text = "Port";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Port.Location = new System.Drawing.Point(12, 91);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(154, 27);
            this.textBox_Port.TabIndex = 9;
            // 
            // label_Protocol
            // 
            this.label_Protocol.AutoSize = true;
            this.label_Protocol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Protocol.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Protocol.ForeColor = System.Drawing.Color.White;
            this.label_Protocol.Location = new System.Drawing.Point(12, 9);
            this.label_Protocol.Name = "label_Protocol";
            this.label_Protocol.Size = new System.Drawing.Size(81, 18);
            this.label_Protocol.TabIndex = 10;
            this.label_Protocol.Text = "Protocol";
            // 
            // ProfileDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(178, 246);
            this.ControlBox = false;
            this.Controls.Add(this.label_Protocol);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_Data);
            this.Controls.Add(this.comboBox_Protocol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProfileDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProfileSetting";
            this.Load += new System.EventHandler(this.ProfileDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Protocol;
        private System.Windows.Forms.Button button_Data;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label_Protocol;
    }
}