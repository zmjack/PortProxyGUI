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
        public SetProxyForm SetProxyForm;
        public About AboutForm;
        private ListViewColumnSorter lvwColumnSorter;

        public PortProxyGUI()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
            listView1.ListViewItemSorter = lvwColumnSorter;
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
                var listenOn = subItems[1].Text;
                var listenPort = subItems[2].Text;
                var output = CmdRunner.Execute($"netsh interface portproxy delete {type} listenaddress={listenOn} listenport={listenPort}");
            }
            RefreshProxyList();
        }

        private void SetProxyForUpdate(SetProxyForm form)
        {
            var item = listView1.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            {
                var subItems = item.SubItems.OfType<ListViewSubItem>().ToArray();
                var type = subItems[0].Text;
                var listenOn = subItems[1].Text;
                var listenPort = subItems[2].Text;
                var connectTo = subItems[3].Text;
                var connectPort = subItems[4].Text;
                form.UseUpdateMode(type, listenOn, listenPort, connectTo, connectPort);
            }
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
                    case string s when s == toolStripMenuItem_New.Text:
                        if (SetProxyForm == null) SetProxyForm = new SetProxyForm(this);
                        SetProxyForm.UseNormalMode();
                        SetProxyForm.Show();
                        break;

                    case string s when s == toolStripMenuItem_Modify.Text:
                        if (SetProxyForm == null) SetProxyForm = new SetProxyForm(this);
                        SetProxyForUpdate(SetProxyForm);
                        SetProxyForm.Show();
                        break;

                    case string s when s == toolStripMenuItem_Refresh.Text: RefreshProxyList(); break;
                    case string s when s == toolStripMenuItem_Delete.Text: DeleteSelectedProxies(); break;

                    case string s when s == toolStripMenuItem_About.Text:
                        if (AboutForm == null)
                        {
                            AboutForm = new About(this);
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
                var selectAny = e.Button == MouseButtons.Right && _sender.SelectedItems.OfType<ListViewItem>().Any();
                toolStripMenuItem_Delete.Enabled = selectAny;
                toolStripMenuItem_Modify.Enabled = selectAny;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (sender is ListView _sender)
            {
                var selectAny = _sender.SelectedItems.OfType<ListViewItem>().Any();
                if (selectAny)
                {
                    if (SetProxyForm == null) SetProxyForm = new SetProxyForm(this);
                    SetProxyForUpdate(SetProxyForm);
                    SetProxyForm.Show();
                }
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            listView1.Sort();
        }
    }
}
