namespace PortProxyGUI
{
    partial class SetProxyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetProxyForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_listenOn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_connectTo = new System.Windows.Forms.TextBox();
            this.textBox_connectPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_submit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_listenPort = new System.Windows.Forms.TextBox();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox_listenOn
            // 
            resources.ApplyResources(this.textBox_listenOn, "textBox_listenOn");
            this.textBox_listenOn.Name = "textBox_listenOn";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBox_connectTo
            // 
            resources.ApplyResources(this.textBox_connectTo, "textBox_connectTo");
            this.textBox_connectTo.Name = "textBox_connectTo";
            // 
            // textBox_connectPort
            // 
            resources.ApplyResources(this.textBox_connectPort, "textBox_connectPort");
            this.textBox_connectPort.Name = "textBox_connectPort";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button_submit
            // 
            resources.ApplyResources(this.button_submit, "button_submit");
            this.button_submit.Name = "button_submit";
            this.button_submit.UseVisualStyleBackColor = true;
            this.button_submit.Click += new System.EventHandler(this.button_submit_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBox_listenPort
            // 
            resources.ApplyResources(this.textBox_listenPort, "textBox_listenPort");
            this.textBox_listenPort.Name = "textBox_listenPort";
            // 
            // comboBox_type
            // 
            resources.ApplyResources(this.comboBox_type, "comboBox_type");
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            resources.GetString("comboBox_type.Items"),
            resources.GetString("comboBox_type.Items1"),
            resources.GetString("comboBox_type.Items2"),
            resources.GetString("comboBox_type.Items3"),
            resources.GetString("comboBox_type.Items4")});
            this.comboBox_type.Name = "comboBox_type";
            // 
            // SetProxyForm
            // 
            this.AcceptButton = this.button_submit;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.textBox_listenPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_submit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_connectPort);
            this.Controls.Add(this.textBox_connectTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_listenOn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetProxyForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetProxyForm_FormClosing);
            this.Load += new System.EventHandler(this.SetProxyForm_Load);
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
        private System.Windows.Forms.Button button_submit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_listenPort;
        private System.Windows.Forms.ComboBox comboBox_type;
    }
}