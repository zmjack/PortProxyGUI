using PortProxyGUI._extern.NStandard;
using System;
using System.Linq;
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

        private bool IsIPv4(string ip)
        {
            if (ip == "localhost" || ip == "*") return true;
            else return ip.IsMatch(new Regex(@"^(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])(?:\.(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])){3}$"));
        }
        private bool IsIPv6(string ip)
        {
            if (ip == "localhost" || ip == "*") return true;
            else return ip.IsMatch(new Regex(@"^[\dABCDEF]{2}(?::(?:[\dABCDEF]{2})){5}$"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var type = comboBox_type.Text.Trim();
            var listenOn = textBox_listenOn.Text.Trim().ToLower();
            var connectTo = textBox_connectTo.Text.Trim().ToLower();
            var listenPort = textBox_listenPort.Text.Trim();
            var connectPort = textBox_connectPort.Text.Trim();

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

            if (string.IsNullOrEmpty(type))
            {
                if (IsIPv4(listenOn) && IsIPv4(connectTo)) type = comboBox_type.Text = "v4tov4";
                else if (IsIPv4(listenOn) && IsIPv6(connectTo)) type = comboBox_type.Text = "v4tov6";
                else if (IsIPv6(listenOn) && IsIPv4(connectTo)) type = comboBox_type.Text = "v6tov4";
                else if (IsIPv6(listenOn) && IsIPv6(connectTo)) type = comboBox_type.Text = "v6tov6";
                else
                {
                    MessageBox.Show($"The address which is connect to is neither IPv4 nor IPv6.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (new[] { "v4tov4", "v4tov6", "v6tov4", "v6tov6" }.Contains(type))
            {
                bool invalid = false;
                switch (type)
                {
                    case "v4tov4": if (!IsIPv4(listenOn) || !IsIPv4(connectTo)) invalid = true; break;
                    case "v4tov6": if (!IsIPv4(listenOn) || !IsIPv6(connectTo)) invalid = true; break;
                    case "v6tov4": if (!IsIPv6(listenOn) || !IsIPv4(connectTo)) invalid = true; break;
                    case "v6tov6": if (!IsIPv6(listenOn) || !IsIPv6(connectTo)) invalid = true; break;
                }
                if (invalid)
                {
                    MessageBox.Show($"The type ({type}) is invalid for ({listenOn} -> {connectTo}).", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                MessageBox.Show($"Unknow type ({type}).", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
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
