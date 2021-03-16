using NStandard;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PortProxyGUI
{
    public partial class SetProxyForm : Form
    {
        public readonly PortProxyGUI ParentWindow;
        private string AutoTypeString { get; }

        private bool _updateMode;
        private ListViewItem _updateLiveViewItem;
        private string _oldType;
        private string _oldListenOn;
        private int _oldListenPort;

        public SetProxyForm(PortProxyGUI parent)
        {
            ParentWindow = parent;
            InitializeComponent();
            AutoTypeString = comboBox_type.Text = comboBox_type.Items.OfType<string>().First();
        }

        public void UseNormalMode()
        {
            _updateMode = false;
            _updateLiveViewItem = null;
            _oldType = null;
            _oldListenOn = null;
            _oldListenPort = 0;

            comboBox_type.Text = AutoTypeString;
            textBox_listenOn.Text = "*";
            textBox_listenPort.Text = "";
            textBox_connectTo.Text = "";
            textBox_connectPort.Text = "";
        }

        public void UseUpdateMode(ListViewItem item, string type, string listenOn, int listenPort, string connectTo, string connectPort)
        {
            _updateMode = true;
            _updateLiveViewItem = item;
            _oldType = type;
            _oldListenOn = listenOn.Trim().ToLower();
            _oldListenPort = listenPort;

            comboBox_type.Text = type;
            textBox_listenOn.Text = listenOn.ToString();
            textBox_listenPort.Text = listenPort.ToString();
            textBox_connectTo.Text = connectTo;
            textBox_connectPort.Text = connectPort;
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

            if (_updateMode)
            {
                var rule = Program.SqliteDbScope.GetRule(_oldType, _oldListenOn, _oldListenPort);
                CmdUtil.DeleteProxy(_oldType, _oldListenOn, _oldListenPort);
                Program.SqliteDbScope.Remove(rule);

                rule.Type = type;
                rule.ListenOn = listenOn;
                rule.ListenPort = _listenPort;
                rule.ConnectTo = connectTo;
                rule.ConnectPort = _connectPort;

                CmdUtil.AddProxy("add", type, listenOn, _listenPort, connectTo, _connectPort);
                Program.SqliteDbScope.Add(rule);

                _updateLiveViewItem.ImageIndex = 1;
                var subItems = _updateLiveViewItem.SubItems;
                subItems[1].Text = type;
                subItems[2].Text = listenOn;
                subItems[3].Text = _listenPort.ToString();
                subItems[4].Text = connectTo;
                subItems[5].Text = _connectPort.ToString();
            }
            else
            {
                CmdUtil.AddProxy("add", type, listenOn, _listenPort, connectTo, _connectPort);
                ParentWindow.RefreshProxyList();
            }

            Close();
        }

        private void SetProxyForm_Load(object sender, EventArgs e)
        {
            Top = ParentWindow.Top + (ParentWindow.Height - Height) / 2;
            Left = ParentWindow.Left + (ParentWindow.Width - Width) / 2;
        }

        private void SetProxyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParentWindow.SetProxyForm = null;
        }

    }
}
