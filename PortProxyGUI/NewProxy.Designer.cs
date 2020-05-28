namespace PortProxyGUI
{
    partial class NewProxy
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_listenOn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_connectTo = new System.Windows.Forms.TextBox();
            this.textBox_connectPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_listenPort = new System.Windows.Forms.TextBox();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Listen on";
            // 
            // textBox_listenOn
            // 
            this.textBox_listenOn.Enabled = false;
            this.textBox_listenOn.Location = new System.Drawing.Point(92, 12);
            this.textBox_listenOn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_listenOn.Name = "textBox_listenOn";
            this.textBox_listenOn.Size = new System.Drawing.Size(120, 23);
            this.textBox_listenOn.TabIndex = 1;
            this.textBox_listenOn.Text = "*";
            this.textBox_listenOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Connect to";
            // 
            // textBox_connectTo
            // 
            this.textBox_connectTo.Location = new System.Drawing.Point(92, 46);
            this.textBox_connectTo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_connectTo.Name = "textBox_connectTo";
            this.textBox_connectTo.Size = new System.Drawing.Size(120, 23);
            this.textBox_connectTo.TabIndex = 3;
            this.textBox_connectTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_connectPort
            // 
            this.textBox_connectPort.Location = new System.Drawing.Point(260, 46);
            this.textBox_connectPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_connectPort.Name = "textBox_connectPort";
            this.textBox_connectPort.Size = new System.Drawing.Size(66, 23);
            this.textBox_connectPort.TabIndex = 4;
            this.textBox_connectPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Port";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 88);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 93);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Port";
            // 
            // textBox_listenPort
            // 
            this.textBox_listenPort.Location = new System.Drawing.Point(260, 12);
            this.textBox_listenPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_listenPort.Name = "textBox_listenPort";
            this.textBox_listenPort.Size = new System.Drawing.Size(66, 23);
            this.textBox_listenPort.TabIndex = 2;
            this.textBox_listenPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_type
            // 
            this.comboBox_type.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "v4tov4",
            "v4tov6",
            "v6tov4",
            "v6tov6"});
            this.comboBox_type.Location = new System.Drawing.Point(91, 90);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(121, 25);
            this.comboBox_type.TabIndex = 5;
            // 
            // NewProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 128);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.textBox_listenPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_connectPort);
            this.Controls.Add(this.textBox_connectTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_listenOn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProxy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewProxy";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewProxy_FormClosing);
            this.Load += new System.EventHandler(this.NewProxy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_listenOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_connectTo;
        private System.Windows.Forms.TextBox textBox_connectPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_listenPort;
        private System.Windows.Forms.ComboBox comboBox_type;
    }
}