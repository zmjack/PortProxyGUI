namespace PortProxyGUI
{
    partial class SetProxy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetProxy));
            this.label_ListenOn = new System.Windows.Forms.Label();
            this.textBox_ListenOn = new System.Windows.Forms.TextBox();
            this.label_ConnectTo = new System.Windows.Forms.Label();
            this.textBox_ConnectTo = new System.Windows.Forms.TextBox();
            this.textBox_ConnectPort = new System.Windows.Forms.TextBox();
            this.label_ConnectPort = new System.Windows.Forms.Label();
            this.button_Set = new System.Windows.Forms.Button();
            this.label_Type = new System.Windows.Forms.Label();
            this.label_ListenPort = new System.Windows.Forms.Label();
            this.textBox_ListenPort = new System.Windows.Forms.TextBox();
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.label_Comment = new System.Windows.Forms.Label();
            this.textBox_Comment = new System.Windows.Forms.TextBox();
            this.label_Group = new System.Windows.Forms.Label();
            this.comboBox_Group = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label_ListenOn
            // 
            resources.ApplyResources(this.label_ListenOn, "label_ListenOn");
            this.label_ListenOn.Name = "label_ListenOn";
            // 
            // textBox_ListenOn
            // 
            resources.ApplyResources(this.textBox_ListenOn, "textBox_ListenOn");
            this.textBox_ListenOn.Name = "textBox_ListenOn";
            // 
            // label_ConnectTo
            // 
            resources.ApplyResources(this.label_ConnectTo, "label_ConnectTo");
            this.label_ConnectTo.Name = "label_ConnectTo";
            // 
            // textBox_ConnectTo
            // 
            resources.ApplyResources(this.textBox_ConnectTo, "textBox_ConnectTo");
            this.textBox_ConnectTo.Name = "textBox_ConnectTo";
            // 
            // textBox_ConnectPort
            // 
            resources.ApplyResources(this.textBox_ConnectPort, "textBox_ConnectPort");
            this.textBox_ConnectPort.Name = "textBox_ConnectPort";
            // 
            // label_ConnectPort
            // 
            resources.ApplyResources(this.label_ConnectPort, "label_ConnectPort");
            this.label_ConnectPort.Name = "label_ConnectPort";
            // 
            // button_Set
            // 
            resources.ApplyResources(this.button_Set, "button_Set");
            this.button_Set.Name = "button_Set";
            this.button_Set.UseVisualStyleBackColor = true;
            this.button_Set.Click += new System.EventHandler(this.button_Set_Click);
            // 
            // label_Type
            // 
            resources.ApplyResources(this.label_Type, "label_Type");
            this.label_Type.Name = "label_Type";
            // 
            // label_ListenPort
            // 
            resources.ApplyResources(this.label_ListenPort, "label_ListenPort");
            this.label_ListenPort.Name = "label_ListenPort";
            // 
            // textBox_ListenPort
            // 
            resources.ApplyResources(this.textBox_ListenPort, "textBox_ListenPort");
            this.textBox_ListenPort.Name = "textBox_ListenPort";
            // 
            // comboBox_Type
            // 
            resources.ApplyResources(this.comboBox_Type, "comboBox_Type");
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.Items.AddRange(new object[] {
            resources.GetString("comboBox_Type.Items"),
            resources.GetString("comboBox_Type.Items1"),
            resources.GetString("comboBox_Type.Items2"),
            resources.GetString("comboBox_Type.Items3"),
            resources.GetString("comboBox_Type.Items4")});
            this.comboBox_Type.Name = "comboBox_Type";
            // 
            // label_Comment
            // 
            resources.ApplyResources(this.label_Comment, "label_Comment");
            this.label_Comment.Name = "label_Comment";
            // 
            // textBox_Comment
            // 
            resources.ApplyResources(this.textBox_Comment, "textBox_Comment");
            this.textBox_Comment.Name = "textBox_Comment";
            // 
            // label_Group
            // 
            resources.ApplyResources(this.label_Group, "label_Group");
            this.label_Group.Name = "label_Group";
            // 
            // comboBox_Group
            // 
            resources.ApplyResources(this.comboBox_Group, "comboBox_Group");
            this.comboBox_Group.FormattingEnabled = true;
            this.comboBox_Group.Name = "comboBox_Group";
            // 
            // SetProxy
            // 
            this.AcceptButton = this.button_Set;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_Group);
            this.Controls.Add(this.label_Group);
            this.Controls.Add(this.textBox_Comment);
            this.Controls.Add(this.label_Comment);
            this.Controls.Add(this.comboBox_Type);
            this.Controls.Add(this.textBox_ListenPort);
            this.Controls.Add(this.label_ListenPort);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.button_Set);
            this.Controls.Add(this.label_ConnectPort);
            this.Controls.Add(this.textBox_ConnectPort);
            this.Controls.Add(this.textBox_ConnectTo);
            this.Controls.Add(this.label_ConnectTo);
            this.Controls.Add(this.textBox_ListenOn);
            this.Controls.Add(this.label_ListenOn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetProxy";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetProxyForm_FormClosing);
            this.Load += new System.EventHandler(this.SetProxyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ListenOn;
        private System.Windows.Forms.TextBox textBox_ListenOn;
        private System.Windows.Forms.Label label_ConnectTo;
        private System.Windows.Forms.TextBox textBox_ConnectTo;
        private System.Windows.Forms.TextBox textBox_ConnectPort;
        private System.Windows.Forms.Label label_ConnectPort;
        private System.Windows.Forms.Button button_Set;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.Label label_ListenPort;
        private System.Windows.Forms.TextBox textBox_ListenPort;
        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.Label label_Comment;
        private System.Windows.Forms.TextBox textBox_Comment;
        private System.Windows.Forms.Label label_Group;
        private System.Windows.Forms.ComboBox comboBox_Group;
    }
}