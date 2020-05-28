using NStandard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace PortProxyGUI
{
    public partial class PortProxyGUI : Form
    {
        public NewProxy NewProxyForm;
        public About AboutForm;

        public PortProxyGUI()
        {
            InitializeComponent();
        }

        private void PortProxyGUI_Load(object sender, EventArgs e)
        {
            RefreshProxyList();
        }

        private void DeleteSelectedProxies()
        {
            var items = listView1.SelectedItems.OfType<ListViewItem>();
            foreach (var item in items)
            {
                var subItems = item.SubItems.OfType<ListViewSubItem>().ToArray();
                var type = subItems[0].Text;
                var listenPort = subItems[1].Text;
                var output = CmdRunner.Execute($"netsh interface portproxy delete {type} listenport={listenPort}");
            }

            RefreshProxyList();
        }

        public void RefreshProxyList()
        {
            var output = CmdRunner.Execute("netsh interface portproxy show all");
            var types = new[] { ("ipv4", "ipv4"), ("ipv4", "ipv6"), ("ipv6", "ipv4"), ("ipv6", "ipv6") };

            var proxies = types.SelectMany(type =>
            {
                var from = type.Item1;
                var to = type.Item2;

                var typeProxies = output
                    .Project(new Regex($@"{from}:[^\n]+?{to}:\r\n\r\n.+?\r\n--------------- ----------  --------------- ----------\r\n(.+?)\r\n\r\n", RegexOptions.Singleline))
                    ?.Split(Environment.NewLine)
                    .Select(line =>
                    {
                        var parts = line.Resolve(new Regex(@"^([^\s]+)\s+([^\s]+)\s+([^\s]+)\s+([^\s]+)$"));
                        return new PortProxy
                        {
                            Type = (from, to) switch
                            {
                                ("ipv4", "ipv4") => "v4tov4",
                                ("ipv4", "ipv6") => "v4tov6",
                                ("ipv6", "ipv4") => "v6tov4",
                                ("ipv6", "ipv6") => "v6tov6",
                                _ => throw new NotSupportedException(),
                            },
                            ListenOn = parts[1].FirstOrDefault(),
                            ListenPort = parts[2].FirstOrDefault(),
                            ConnectTo = parts[3].FirstOrDefault(),
                            ConnectPort = parts[4].FirstOrDefault(),
                        };
                    });
                return typeProxies ?? new PortProxy[0];
            });

            listView1.Items.Clear();
            foreach (var proxy in proxies)
            {
                listView1.Items.Add(new ListViewItem(proxy.Type).Then(vitem =>
                {
                    vitem.SubItems.AddRange(new[] { proxy.ListenPort, proxy.ConnectTo, proxy.ConnectPort });
                }));
            }
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is ContextMenuStrip _sender)
            {
                var selected = _sender.Items.OfType<ToolStripItem>().Where(x => x.Selected).FirstOrDefault();
                if (selected is null || !selected.Enabled) return;

                switch (selected.Text)
                {
                    case "New":
                        if (NewProxyForm == null)
                        {
                            NewProxyForm = new NewProxy(this);
                            NewProxyForm.Show();
                        }
                        else NewProxyForm.Show();
                        break;

                    case "Refresh": RefreshProxyList(); break;
                    case "Delete": DeleteSelectedProxies(); break;

                    case "About":
                        if (AboutForm == null)
                        {
                            AboutForm = new About();
                            AboutForm.Show();
                        }
                        else AboutForm.Show();
                        break;
                }
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is ListView _sender)
            {
                if (e.Button == MouseButtons.Right && _sender.SelectedItems.OfType<ListViewItem>().Any())
                    toolStripMenuItem2.Enabled = true;
                else toolStripMenuItem2.Enabled = false;
            }
        }
    }
}
