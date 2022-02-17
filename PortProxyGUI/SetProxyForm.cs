using NStandard;
using PortProxyGUI.Data;
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
        private ListViewItem _listViewItem;
        private Rule _itemRule;

        public SetProxyForm(PortProxyGUI parent)
        {
            ParentWindow = parent;
            InitializeComponent();
            AutoTypeString = comboBox_Type.Text = comboBox_Type.Items.OfType<string>().First();
        }

        public void UseNormalMode()
        {
            _updateMode = false;
            _listViewItem = null;
            _itemRule = null;

            comboBox_Type.Text = AutoTypeString;
            comboBox_Group.Text = "";

            textBox_ListenOn.Text = "*";
            textBox_ListenPort.Text = "";
            textBox_ConnectTo.Text = "";
            textBox_ConnectPort.Text = "";
            textBox_Note.Text = "";
        }

        public void UseUpdateMode(ListViewItem item, Rule rule)
        {
            _updateMode = true;
            _listViewItem = item;

            _itemRule = rule;

            comboBox_Type.Text = rule.Type;
            comboBox_Group.Text = rule.Group;

            textBox_ListenOn.Text = rule.ListenOn;
            textBox_ListenPort.Text = rule.ListenPort.ToString();
            textBox_ConnectTo.Text = rule.ConnectTo;
            textBox_ConnectPort.Text = rule.ConnectPort.ToString();
            textBox_Note.Text = rule.Note;
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

        private void button_Set_Click(object sender, EventArgs e)
        {
            int listenPort, connectPort;

            try
            {
                listenPort = Rule.ParsePort(textBox_ListenPort.Text);
                connectPort = Rule.ParsePort(textBox_ConnectPort.Text);
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message, "Invalid port", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var rule = new Rule
            {
                Type = comboBox_Type.Text.Trim(),
                ListenOn = textBox_ListenOn.Text.Trim(),
                ListenPort = listenPort,
                ConnectTo = textBox_ConnectTo.Text.Trim(),
                ConnectPort = connectPort,
                Note = textBox_Note.Text.Trim(),
                Group = comboBox_Group.Text.Trim(),
            };

            if (rule.Type == AutoTypeString) rule.Type = GetPassType(rule.ListenOn, rule.ConnectTo);

            if (!new[] { "v4tov4", "v4tov6", "v6tov4", "v6tov6" }.Contains(rule.Type))
            {
                MessageBox.Show($"Unknow type for ({rule.ListenOn} -> {rule.ConnectTo}).", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_updateMode)
            {
                var oldRule = Program.SqliteDbScope.GetRule(_itemRule.Type, _itemRule.ListenOn, _itemRule.ListenPort);
                CmdUtil.DeleteProxy(oldRule);
                Program.SqliteDbScope.Remove(oldRule);

                CmdUtil.AddOrUpdateProxy(rule);
                Program.SqliteDbScope.Add(rule);

                _listViewItem.ImageIndex = 1;
                var subItems = _listViewItem.SubItems;
                subItems[1].Text = rule.Type;
                subItems[2].Text = rule.ListenOn;
                subItems[3].Text = rule.ListenPort.ToString();
                subItems[4].Text = rule.ConnectTo;
                subItems[5].Text = rule.ConnectPort.ToString();
                subItems[6].Text = rule.Note;

                if (rule.Group == null) _listViewItem.Group = null;
                else
                {
                    var listView = ParentWindow.listViewProxies;
                    var group = listView.Groups.OfType<ListViewGroup>().FirstOrDefault(x => x.Name == rule.Group);
                    if (group == null)
                    {
                        group = new ListViewGroup(rule.Group);
                        listView.Groups.Add(group);
                    }

                    _listViewItem.Group = group;
                }
            }
            else
            {
                CmdUtil.AddOrUpdateProxy(rule);
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
