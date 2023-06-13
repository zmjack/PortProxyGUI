namespace PortProxyGUI
{
    partial class PortProxyGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortProxyGUI));
            listViewProxies = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            columnHeader4 = new System.Windows.Forms.ColumnHeader();
            columnHeader5 = new System.Windows.Forms.ColumnHeader();
            columnHeader6 = new System.Windows.Forms.ColumnHeader();
            columnHeader7 = new System.Windows.Forms.ColumnHeader();
            contextMenuStrip_RightClick = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem_Enable = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Disable = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_FlushDnsCache = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem_New = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Modify = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem_More = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Import = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem_Export = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem_ResetWindowSize = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            imageListProxies = new System.Windows.Forms.ImageList(components);
            saveFileDialog_Export = new System.Windows.Forms.SaveFileDialog();
            openFileDialog_Import = new System.Windows.Forms.OpenFileDialog();
            statusStrip_Footer = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel_ServiceNotRunning = new System.Windows.Forms.ToolStripStatusLabel();
            contextMenuStrip_RightClick.SuspendLayout();
            statusStrip_Footer.SuspendLayout();
            SuspendLayout();
            // 
            // listViewProxies
            // 
            listViewProxies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            listViewProxies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7 });
            listViewProxies.ContextMenuStrip = contextMenuStrip_RightClick;
            resources.ApplyResources(listViewProxies, "listViewProxies");
            listViewProxies.FullRowSelect = true;
            listViewProxies.Name = "listViewProxies";
            listViewProxies.SmallImageList = imageListProxies;
            listViewProxies.UseCompatibleStateImageBehavior = false;
            listViewProxies.View = System.Windows.Forms.View.Details;
            listViewProxies.ColumnClick += listView1_ColumnClick;
            listViewProxies.ColumnWidthChanged += listViewProxies_ColumnWidthChanged;
            listViewProxies.DoubleClick += listView1_DoubleClick;
            listViewProxies.KeyUp += listViewProxies_KeyUp;
            listViewProxies.MouseUp += listView1_MouseUp;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            columnHeader4.Tag = "";
            resources.ApplyResources(columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(columnHeader5, "columnHeader5");
            // 
            // columnHeader6
            // 
            columnHeader6.Tag = "";
            resources.ApplyResources(columnHeader6, "columnHeader6");
            // 
            // columnHeader7
            // 
            resources.ApplyResources(columnHeader7, "columnHeader7");
            // 
            // contextMenuStrip_RightClick
            // 
            contextMenuStrip_RightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_Enable, toolStripMenuItem_Disable, toolStripSeparator3, toolStripMenuItem_Refresh, toolStripMenuItem_FlushDnsCache, toolStripSeparator2, toolStripMenuItem_New, toolStripMenuItem_Modify, toolStripMenuItem_Delete, toolStripSeparator1, toolStripMenuItem_More, toolStripSeparator4, toolStripMenuItem_About });
            contextMenuStrip_RightClick.Name = "contextMenuStrip1";
            resources.ApplyResources(contextMenuStrip_RightClick, "contextMenuStrip_RightClick");
            contextMenuStrip_RightClick.MouseClick += contextMenuStrip_RightClick_MouseClick;
            // 
            // toolStripMenuItem_Enable
            // 
            toolStripMenuItem_Enable.Name = "toolStripMenuItem_Enable";
            resources.ApplyResources(toolStripMenuItem_Enable, "toolStripMenuItem_Enable");
            // 
            // toolStripMenuItem_Disable
            // 
            toolStripMenuItem_Disable.Name = "toolStripMenuItem_Disable";
            resources.ApplyResources(toolStripMenuItem_Disable, "toolStripMenuItem_Disable");
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripMenuItem_Refresh
            // 
            toolStripMenuItem_Refresh.Name = "toolStripMenuItem_Refresh";
            resources.ApplyResources(toolStripMenuItem_Refresh, "toolStripMenuItem_Refresh");
            // 
            // toolStripMenuItem_FlushDnsCache
            // 
            toolStripMenuItem_FlushDnsCache.Name = "toolStripMenuItem_FlushDnsCache";
            resources.ApplyResources(toolStripMenuItem_FlushDnsCache, "toolStripMenuItem_FlushDnsCache");
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripMenuItem_New
            // 
            toolStripMenuItem_New.Name = "toolStripMenuItem_New";
            resources.ApplyResources(toolStripMenuItem_New, "toolStripMenuItem_New");
            // 
            // toolStripMenuItem_Modify
            // 
            toolStripMenuItem_Modify.Name = "toolStripMenuItem_Modify";
            resources.ApplyResources(toolStripMenuItem_Modify, "toolStripMenuItem_Modify");
            // 
            // toolStripMenuItem_Delete
            // 
            toolStripMenuItem_Delete.Name = "toolStripMenuItem_Delete";
            resources.ApplyResources(toolStripMenuItem_Delete, "toolStripMenuItem_Delete");
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem_More
            // 
            toolStripMenuItem_More.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem_Import, toolStripMenuItem_Export, toolStripSeparator5, toolStripMenuItem_ResetWindowSize });
            toolStripMenuItem_More.Name = "toolStripMenuItem_More";
            resources.ApplyResources(toolStripMenuItem_More, "toolStripMenuItem_More");
            // 
            // toolStripMenuItem_Import
            // 
            toolStripMenuItem_Import.Name = "toolStripMenuItem_Import";
            resources.ApplyResources(toolStripMenuItem_Import, "toolStripMenuItem_Import");
            toolStripMenuItem_Import.Click += toolStripMenuItem_Import_Click;
            // 
            // toolStripMenuItem_Export
            // 
            toolStripMenuItem_Export.Name = "toolStripMenuItem_Export";
            resources.ApplyResources(toolStripMenuItem_Export, "toolStripMenuItem_Export");
            toolStripMenuItem_Export.Click += toolStripMenuItem_Export_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripMenuItem_ResetWindowSize
            // 
            toolStripMenuItem_ResetWindowSize.Name = "toolStripMenuItem_ResetWindowSize";
            resources.ApplyResources(toolStripMenuItem_ResetWindowSize, "toolStripMenuItem_ResetWindowSize");
            toolStripMenuItem_ResetWindowSize.Click += toolStripMenuItem_ResetWindowSize_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripMenuItem_About
            // 
            toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            resources.ApplyResources(toolStripMenuItem_About, "toolStripMenuItem_About");
            // 
            // imageListProxies
            // 
            imageListProxies.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageListProxies.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageListProxies.ImageStream");
            imageListProxies.TransparentColor = System.Drawing.Color.Transparent;
            imageListProxies.Images.SetKeyName(0, "disable.png");
            imageListProxies.Images.SetKeyName(1, "enable.png");
            // 
            // saveFileDialog_Export
            // 
            resources.ApplyResources(saveFileDialog_Export, "saveFileDialog_Export");
            // 
            // openFileDialog_Import
            // 
            openFileDialog_Import.FileName = "openFileDialog1";
            resources.ApplyResources(openFileDialog_Import, "openFileDialog_Import");
            // 
            // statusStrip_Footer
            // 
            statusStrip_Footer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel_Status, toolStripStatusLabel_ServiceNotRunning });
            resources.ApplyResources(statusStrip_Footer, "statusStrip_Footer");
            statusStrip_Footer.Name = "statusStrip_Footer";
            // 
            // toolStripStatusLabel_Status
            // 
            toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            resources.ApplyResources(toolStripStatusLabel_Status, "toolStripStatusLabel_Status");
            // 
            // toolStripStatusLabel_ServiceNotRunning
            // 
            toolStripStatusLabel_ServiceNotRunning.IsLink = true;
            toolStripStatusLabel_ServiceNotRunning.LinkColor = System.Drawing.Color.Red;
            toolStripStatusLabel_ServiceNotRunning.Name = "toolStripStatusLabel_ServiceNotRunning";
            resources.ApplyResources(toolStripStatusLabel_ServiceNotRunning, "toolStripStatusLabel_ServiceNotRunning");
            toolStripStatusLabel_ServiceNotRunning.Click += toolStripStatusLabel_ServiceNotRunning_Click;
            // 
            // PortProxyGUI
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(statusStrip_Footer);
            Controls.Add(listViewProxies);
            Name = "PortProxyGUI";
            FormClosing += PortProxyGUI_FormClosing;
            Load += PortProxyGUI_Load;
            Shown += PortProxyGUI_Shown;
            Resize += PortProxyGUI_Resize;
            contextMenuStrip_RightClick.ResumeLayout(false);
            statusStrip_Footer.ResumeLayout(false);
            statusStrip_Footer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_RightClick;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_New;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Delete;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Refresh;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_About;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Modify;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageListProxies;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Enable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Disable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        internal System.Windows.Forms.ListView listViewProxies;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FlushDnsCache;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_More;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Export;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Import;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_Export;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Import;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ResetWindowSize;
        private System.Windows.Forms.StatusStrip statusStrip_Footer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ServiceNotRunning;
    }
}

