using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PortProxyGUI
{
    public partial class About : Form
    {
        public readonly PortProxyGUI PortProxyGUI;

        public About(PortProxyGUI portProxyGUI)
        {
            PortProxyGUI = portProxyGUI;

            InitializeComponent();
            Font = Util.UiFont;

            label_version.Text = this.Text = string.Format("Port Proxy GUI v{0}", Program.AppVersion);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (sender is LinkLabel _sender)
            {
                Process.Start("explorer", _sender.Text);
            }
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            PortProxyGUI.AboutForm = null;
        }
    }
}
