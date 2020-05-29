using PortProxyGUI._extern.NStandard;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
                var listenOn= subItems[1].Text;
                var listenPort = subItems[2].Text;
                var output = CmdRunner.Execute($"netsh interface portproxy delete {type} listenaddress={listenOn} listenport={listenPort}");
            }

            RefreshProxyList();
        }

        public void RefreshProxyList()
        {
            var output = CmdRunner.Execute("netsh interface portproxy show all");
            var types = new[]
            {
                new ProxyType("ipv4", "ipv4"),
                new ProxyType("ipv4", "ipv6"),
                new ProxyType("ipv6", "ipv4"),
                new ProxyType("ipv6", "ipv6"),
            };

            var proxies = types.SelectMany(type =>
            {
                var typeProxies = output
                    .Project(new Regex($@"{type.From}:[^\n]+?{type.To}:\r\n\r\n.+?\r\n--------------- ----------  --------------- ----------\r\n(.+?)\r\n\r\n", RegexOptions.Singleline))
                    ?.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .Select(line =>
                    {
                        var parts = line.Resolve(new Regex(@"^([^\s]+)\s+([^\s]+)\s+([^\s]+)\s+([^\s]+)$"));
                        return new PortProxy
                        {
                            Type = type.Type,
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
                    vitem.SubItems.AddRange(new[] { proxy.ListenOn, proxy.ListenPort, proxy.ConnectTo, proxy.ConnectPort });
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
                    case string s when s == toolStripMenuItem1.Text:
                        if (NewProxyForm == null)
                        {
                            NewProxyForm = new NewProxy(this);
                            NewProxyForm.Show();
                        }
                        else NewProxyForm.Show();
                        break;

                    case string s when s == toolStripMenuItem2.Text: DeleteSelectedProxies(); break;
                    case string s when s == toolStripMenuItem3.Text: RefreshProxyList(); break;

                    case string s when s == toolStripMenuItem4.Text:
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
