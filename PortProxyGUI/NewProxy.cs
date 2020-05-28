using NStandard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PortProxyGUI
{
    public partial class NewProxy : Form
    {
        public readonly PortProxyGUI PortProxyGUI;

        public NewProxy(PortProxyGUI portProxyGUI)
        {
            PortProxyGUI = portProxyGUI;

            InitializeComponent();
        }

        private void AddPortProxy(string type, string listenOn, string listenPort, string connectTo, string connectPort)
        {
            var output = CmdRunner.Execute($"netsh interface portproxy add {type} listenaddress={listenOn} listenport={listenPort} connectaddress={connectTo} connectport={connectPort}");
            Invoke((Action)(() => PortProxyGUI.RefreshProxyList()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var type = comboBox_type.Text;
            var listenOn = textBox_listenOn.Text;
            var connectTo = textBox_connectTo.Text;
            var listenPort = textBox_listenPort.Text;
            var connectPort = textBox_connectPort.Text;

            if (!comboBox_type.Items.Contains(type))
            {
                MessageBox.Show($"The type is invalid.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!int.TryParse(listenPort, out var _listenPort) || _listenPort < 0 || _listenPort > 65535)
            {
                MessageBox.Show($"The listen port is invalid.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!int.TryParse(connectPort, out var _connectPort) || _connectPort < 0 || _connectPort > 65535)
            {
                MessageBox.Show($"The connect port is invalid.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var listenOn_any = listenOn == "*";
            var listenOn_ipv4 = listenOn.IsMatch(new Regex(@"^(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])(?:\.(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])){3}$"));
            var listenOn_ipv6 = listenOn.IsMatch(new Regex(@"^[\dABCDEF]{2}(?::(?:[\dABCDEF]{2})){5}$"));
            var connectTo_ipv4 = connectTo.IsMatch(new Regex(@"^(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])(?:\.(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])){3}$"));
            var connectTo_ipv6 = connectTo.IsMatch(new Regex(@"^[\dABCDEF]{2}(?::(?:[\dABCDEF]{2})){5}$"));

            if (!listenOn_any && !listenOn_ipv4 && !listenOn_ipv6)
            {
                MessageBox.Show($"The address which is connect to is neither IPv4 nor IPv6.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!connectTo_ipv4 && !connectTo_ipv6)
            {
                MessageBox.Show($"The address which is connect to is neither IPv4 nor IPv6.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (listenOn_any && connectTo_ipv4 && !type.EndsWith("v4"))
            {
                MessageBox.Show($"The type must be v4tov4 or v6tov4.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (listenOn_any && connectTo_ipv6 && !type.EndsWith("v6"))
            {
                MessageBox.Show($"The type must be v4tov6 or v6tov6.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (listenOn_ipv4 && connectTo_ipv4) type = comboBox_type.Text = "v4tov4";
                else if (listenOn_ipv4 && connectTo_ipv6) type = comboBox_type.Text = "v4tov6";
                else if (listenOn_ipv6 && connectTo_ipv4) type = comboBox_type.Text = "v6tov4";
                else if (listenOn_ipv6 && connectTo_ipv6) type = comboBox_type.Text = "v6tov6";
            }

            AddPortProxy(type, listenOn, listenPort, connectTo, connectPort);
        }

        private void NewProxy_Load(object sender, EventArgs e)
        {

        }

        private void NewProxy_FormClosing(object sender, FormClosingEventArgs e)
        {
            PortProxyGUI.NewProxyForm = null;
        }

    }
}
