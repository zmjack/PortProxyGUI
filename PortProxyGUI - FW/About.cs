using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PortProxyGUI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (sender is LinkLabel _sender)
            {
                Process.Start("explorer", _sender.Text);
            }
        }
    }
}
