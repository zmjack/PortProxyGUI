using PortProxyGUI._extern.NStandard;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PortProxyGUI
{
    public partial class SetProxyForm : Form
    {
        public readonly PortProxyGUI PortProxyGUI;
        public bool UpdateMode { get; private set; }
        private string AutoTypeString { get; }

        public SetProxyForm(PortProxyGUI portProxyGUI)
        {
            PortProxyGUI = portProxyGUI;
            InitializeComponent();
            AutoTypeString = comboBox_type.Text = comboBox_type.Items.OfType<string>().First();
        }

        public void UseNormalMode()
        {
            comboBox_type.Enabled = true;
            textBox_listenOn.Enabled = true;
            textBox_listenPort.Enabled = true;
            comboBox_type.Text = AutoTypeString;
            textBox_listenOn.Text = "*";
            textBox_listenPort.Text = "";
            textBox_connectTo.Text = "";
            textBox_connectPort.Text = "";
            UpdateMode = false;
        }

        public void UseUpdateMode(string type, string listenOn, string listenPort, string connectTo, string connectPort)
        {
            comboBox_type.Enabled = false;
            textBox_listenOn.Enabled = false;
            textBox_listenPort.Enabled = false;
            comboBox_type.Text = type;
            textBox_listenOn.Text = listenOn;
            textBox_listenPort.Text = listenPort;
            textBox_connectTo.Text = connectTo;
            textBox_connectPort.Text = connectPort;
            UpdateMode = true;
        }

        private void SetPortProxy(string type, string action, string listenOn, string listenPort, string connectTo, string connectPort)
        {
            var output = CmdRunner.Execute($"netsh interface portproxy {action} {type} listenaddress={listenOn} listenport={listenPort} connectaddress={connectTo} connectport={connectPort}");
            Invoke((Action)(() => PortProxyGUI.RefreshProxyList()));
        }

        private bool IsIPv4(string ip)
        {
            return ip.IsMatch(new Regex(@"^(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])(?:\.(?:\d{1,2}|1\d{2}|2[0-4]\d|25[0-5])){3}$"));
        }
        private bool IsIPv6(string ip)
        {
            return ip.IsMatch(new Regex(@"^[\dABCDEF]{2}(?::(?:[\dABCDEF]{2})){5}$"));
        }

        private string GetPassType(string listenOn, string connectTo)
        {
            var from = IsIPv6(listenOn) ? "v6" : "v4";
            var to = IsIPv6(connectTo) ? "v6" : "v4";
            return $"{from}to{to}";
        }

        private void button_submit_Click(object sender, EventArgs e)
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

            if (type == AutoTypeString) type = GetPassType(listenOn, connectTo);

            if (!new[] { "v4tov4", "v4tov6", "v6tov4", "v6tov6" }.Contains(type))
            {
                MessageBox.Show($"Unknow type for ({listenOn} -> {connectTo}).", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SetPortProxy(type, UpdateMode ? "set" : "add", listenOn, listenPort, connectTo, connectPort);
            Close();
        }

        private void SetProxyForm_Load(object sender, EventArgs e)
        {
            Top = PortProxyGUI.Top + (PortProxyGUI.Height - Height) / 2;
            Left = PortProxyGUI.Left + (PortProxyGUI.Width - Width) / 2;
        }

        private void SetProxyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PortProxyGUI.SetProxyForm = null;
        }

    }
}
