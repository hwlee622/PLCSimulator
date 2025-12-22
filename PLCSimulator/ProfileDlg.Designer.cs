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
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_Port = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label_Protocol = new System.Windows.Forms.Label();
            this.label_Profile = new System.Windows.Forms.Label();
            this.comboBox_Profile = new System.Windows.Forms.ComboBox();
            this.textBox_MaxBitData = new System.Windows.Forms.TextBox();
            this.label_MaxBitData = new System.Windows.Forms.Label();
            this.textBox_MaxWordData = new System.Windows.Forms.TextBox();
            this.label_MaxWordData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_Protocol
            // 
            this.comboBox_Protocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Protocol.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Protocol.FormattingEnabled = true;
            this.comboBox_Protocol.Location = new System.Drawing.Point(12, 175);
            this.comboBox_Protocol.Name = "comboBox_Protocol";
            this.comboBox_Protocol.Size = new System.Drawing.Size(204, 26);
            this.comboBox_Protocol.TabIndex = 0;
            // 
            // button_Start
            // 
            this.button_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Start.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Start.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Start.ForeColor = System.Drawing.Color.White;
            this.button_Start.Location = new System.Drawing.Point(12, 258);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(94, 41);
            this.button_Start.TabIndex = 6;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = false;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Cancel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Cancel.ForeColor = System.Drawing.Color.White;
            this.button_Cancel.Location = new System.Drawing.Point(122, 258);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(94, 41);
            this.button_Cancel.TabIndex = 7;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label_Port
            // 
            this.label_Port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Port.AutoSize = true;
            this.label_Port.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Port.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Port.ForeColor = System.Drawing.Color.White;
            this.label_Port.Location = new System.Drawing.Point(12, 204);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(46, 18);
            this.label_Port.TabIndex = 8;
            this.label_Port.Text = "Port";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_Port.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Port.Location = new System.Drawing.Point(12, 225);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(204, 27);
            this.textBox_Port.TabIndex = 9;
            this.textBox_Port.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Port_KeyDown);
            // 
            // label_Protocol
            // 
            this.label_Protocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Protocol.AutoSize = true;
            this.label_Protocol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Protocol.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Protocol.ForeColor = System.Drawing.Color.White;
            this.label_Protocol.Location = new System.Drawing.Point(12, 154);
            this.label_Protocol.Name = "label_Protocol";
            this.label_Protocol.Size = new System.Drawing.Size(81, 18);
            this.label_Protocol.TabIndex = 10;
            this.label_Protocol.Text = "Protocol";
            // 
            // label_Profile
            // 
            this.label_Profile.AutoSize = true;
            this.label_Profile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_Profile.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Profile.ForeColor = System.Drawing.Color.White;
            this.label_Profile.Location = new System.Drawing.Point(12, 9);
            this.label_Profile.Name = "label_Profile";
            this.label_Profile.Size = new System.Drawing.Size(65, 18);
            this.label_Profile.TabIndex = 12;
            this.label_Profile.Text = "Profile";
            // 
            // comboBox_Profile
            // 
            this.comboBox_Profile.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Profile.FormattingEnabled = true;
            this.comboBox_Profile.Location = new System.Drawing.Point(12, 30);
            this.comboBox_Profile.Name = "comboBox_Profile";
            this.comboBox_Profile.Size = new System.Drawing.Size(204, 26);
            this.comboBox_Profile.TabIndex = 11;
            this.comboBox_Profile.SelectedIndexChanged += new System.EventHandler(this.comboBox_Profile_SelectedIndexChanged);
            // 
            // textBox_MaxBitData
            // 
            this.textBox_MaxBitData.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MaxBitData.Location = new System.Drawing.Point(128, 62);
            this.textBox_MaxBitData.Name = "textBox_MaxBitData";
            this.textBox_MaxBitData.Size = new System.Drawing.Size(88, 27);
            this.textBox_MaxBitData.TabIndex = 14;
            // 
            // label_MaxBitData
            // 
            this.label_MaxBitData.AutoSize = true;
            this.label_MaxBitData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_MaxBitData.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_MaxBitData.ForeColor = System.Drawing.Color.White;
            this.label_MaxBitData.Location = new System.Drawing.Point(12, 65);
            this.label_MaxBitData.Name = "label_MaxBitData";
            this.label_MaxBitData.Size = new System.Drawing.Size(68, 18);
            this.label_MaxBitData.TabIndex = 13;
            this.label_MaxBitData.Text = "MaxBit";
            // 
            // textBox_MaxWordData
            // 
            this.textBox_MaxWordData.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MaxWordData.Location = new System.Drawing.Point(128, 95);
            this.textBox_MaxWordData.Name = "textBox_MaxWordData";
            this.textBox_MaxWordData.Size = new System.Drawing.Size(88, 27);
            this.textBox_MaxWordData.TabIndex = 16;
            // 
            // label_MaxWordData
            // 
            this.label_MaxWordData.AutoSize = true;
            this.label_MaxWordData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_MaxWordData.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_MaxWordData.ForeColor = System.Drawing.Color.White;
            this.label_MaxWordData.Location = new System.Drawing.Point(12, 98);
            this.label_MaxWordData.Name = "label_MaxWordData";
            this.label_MaxWordData.Size = new System.Drawing.Size(93, 18);
            this.label_MaxWordData.TabIndex = 15;
            this.label_MaxWordData.Text = "MaxWord";
            // 
            // ProfileDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(228, 311);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_MaxWordData);
            this.Controls.Add(this.label_MaxWordData);
            this.Controls.Add(this.textBox_MaxBitData);
            this.Controls.Add(this.label_MaxBitData);
            this.Controls.Add(this.label_Profile);
            this.Controls.Add(this.comboBox_Profile);
            this.Controls.Add(this.label_Protocol);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Start);
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
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label_Protocol;
        private System.Windows.Forms.Label label_Profile;
        private System.Windows.Forms.ComboBox comboBox_Profile;
        private System.Windows.Forms.TextBox textBox_MaxBitData;
        private System.Windows.Forms.Label label_MaxBitData;
        private System.Windows.Forms.TextBox textBox_MaxWordData;
        private System.Windows.Forms.Label label_MaxWordData;
    }
}