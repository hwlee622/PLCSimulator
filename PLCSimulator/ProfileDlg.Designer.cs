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
            // button_Start
            // 
            this.button_Start.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Start.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Start.ForeColor = System.Drawing.Color.White;
            this.button_Start.Location = new System.Drawing.Point(12, 193);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(69, 41);
            this.button_Start.TabIndex = 6;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = false;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Cancel.ForeColor = System.Drawing.Color.White;
            this.button_Cancel.Location = new System.Drawing.Point(97, 193);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(69, 41);
            this.button_Cancel.TabIndex = 7;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
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
            this.textBox_Port.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Port_KeyDown);
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
    }
}