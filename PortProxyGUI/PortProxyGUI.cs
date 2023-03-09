﻿using NStandard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace PortProxyGUI
{
    public partial class PortProxyGUI : Form
    {
        public SetProxy SetProxyForm;
        public About AboutForm;
        private ListViewColumnSorter lvwColumnSorter;
        private List<KeyValuePair<string, IPStatus>> hostStatus { get; set; } = new List<KeyValuePair<string, IPStatus>>();
        public PortProxyGUI()
        {
            InitializeComponent();
            Font = Util.UiFont;

            lvwColumnSorter = new ListViewColumnSorter();
            listViewProxies.ListViewItemSorter = lvwColumnSorter;
        }

        private void PortProxyGUI_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Port Proxy GUI v{0}", Program.AppVersion);
            this.Size = new System.Drawing.Size(797, 456);
        }

        private void PortProxyGUI_Shown(object sender, EventArgs e)
        {
            RefreshProxyList();
        }

        private Data.Rule ParseRule(ListViewItem item)
        {
            var subItems = item.SubItems.OfType<ListViewSubItem>().ToArray();
            int listenPort, connectPort;

            listenPort = Data.Rule.ParsePort(subItems[3].Text);
            connectPort = Data.Rule.ParsePort(subItems[5].Text);

            var rule = new Data.Rule
            {
                Id = item.Tag?.ToString(),
                Type = subItems[1].Text.Trim(),
                ListenOn = subItems[2].Text.Trim(),
                ListenPort = listenPort,
                ConnectTo = subItems[4].Text.Trim(),
                ConnectPort = connectPort,
                Comment = subItems[7].Text.Trim(),
                Group = item.Group?.Header.Trim(),
            };
            return rule;
        }

        private void EnableSelectedProxies()
        {
            var items = listViewProxies.SelectedItems.OfType<ListViewItem>();
            foreach (var item in items)
            {
                item.ImageIndex = 1;

                try
                {
                    var rule = ParseRule(item);
                    CmdUtil.AddOrUpdateProxy(rule);
                }
                catch (NotSupportedException ex)
                {
                    MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        private void DisableSelectedProxies()
        {
            var items = listViewProxies.SelectedItems.OfType<ListViewItem>();
            foreach (var item in items)
            {
                item.ImageIndex = 0;

                try
                {
                    var rule = ParseRule(item);
                    CmdUtil.DeleteProxy(rule);
                }
                catch (NotSupportedException ex)
                {
                    MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        private void DeleteSelectedProxies()
        {
            var items = listViewProxies.SelectedItems.OfType<ListViewItem>();
            if (items == null) return;
            DisableSelectedProxies();
            Program.SqliteDbScope.RemoveRange(items.Select(x => new Data.Rule { Id = x.Tag?.ToString() }));
            foreach (var item in items) listViewProxies.Items.Remove(item);
        }

        private void SetProxyForUpdate(SetProxy form)
        {
            var item = listViewProxies.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
            try
            {
                var rule = ParseRule(item);
                form.UseUpdateMode(item, rule);
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void InitProxyGroups(Data.Rule[] rules)
        {
            listViewProxies.Groups.Clear();
            var groups = (
                from g in rules.GroupBy(x => x.Group)
                let name = g.Key
                where !name.IsNullOrWhiteSpace()
                orderby name
                select new ListViewGroup(name)
            ).ToArray();
            listViewProxies.Groups.AddRange(groups);
        }

        private void InitProxyItems(Data.Rule[] rules, Data.Rule[] proxies)
        {
            listViewProxies.Items.Clear();
            foreach (var rule in rules)
            {
                var imageIndex = proxies.Any(p => p.EqualsWithKeys(rule)) ? 1 : 0;
                var group = listViewProxies.Groups.OfType<ListViewGroup>().FirstOrDefault(x => x.Header == rule.Group);

                var item = new ListViewItem();
                UpdateListViewItem(item, rule, imageIndex);
                listViewProxies.Items.Add(item);
            }
        }

        public void UpdateListViewItem(ListViewItem item, Data.Rule rule, int imageIndex, string pingStatus = "Not checked")
        {
            item.ImageIndex = imageIndex;
            item.Tag = rule.Id;
            item.SubItems.Clear();
            item.SubItems.AddRange(new[]
            {
                new ListViewSubItem(item, rule.Type),
                new ListViewSubItem(item, rule.ListenOn),
                new ListViewSubItem(item, rule.ListenPort.ToString()) { Tag = "Number" },
                new ListViewSubItem(item, rule.ConnectTo),
                new ListViewSubItem(item, rule.ConnectPort.ToString ()) { Tag = "Number" },
                new ListViewSubItem(item, pingStatus ?? string.Empty)
                {
                    Tag ="Connect To Ping Status",
                    ForeColor = pingStatus.Equals("Success") ?  Color.Green
                             :  pingStatus.Equals("Pending") ? Color.DarkGray
                             :  Color.MediumVioletRed
                },
                new ListViewSubItem(item, rule.Comment ?? string.Empty)
            });

            if (rule.Group.IsNullOrWhiteSpace()) item.Group = null;
            else
            {
                var group = listViewProxies.Groups.OfType<ListViewGroup>().FirstOrDefault(x => x.Header == rule.Group);
                if (group == null)
                {
                    group = new ListViewGroup(rule.Group);
                    listViewProxies.Groups.Add(group);
                }
                item.Group = group;
            }
        }

        public void RefreshProxyList()
        {
            try
            {

                var proxies = CmdUtil.GetProxies();
                var rules = Program.SqliteDbScope.Rules.ToArray();
                foreach (var proxy in proxies)
                {
                    var matchedRule = rules.FirstOrDefault(r => r.EqualsWithKeys(proxy));
                    proxy.Id = matchedRule?.Id;
                }

                var pendingAdds = proxies.Where(x => x.Valid && x.Id == null);
                var pendingUpdates =
                    from proxy in proxies
                    let exsist = rules.FirstOrDefault(r => r.Id == proxy.Id)
                    where exsist is not null
                    where proxy.Valid && proxy.Id is not null
                    select proxy;

                Program.SqliteDbScope.AddRange(pendingAdds);
                Program.SqliteDbScope.UpdateRange(pendingUpdates);

                rules = Program.SqliteDbScope.Rules.ToArray();
                InitProxyGroups(rules);
                InitProxyItems(rules, proxies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Refreshing");
            }
        }

        public void FlushDnsCache()
        {
            try
            {
                CmdUtil.FlushDNSCache();
                RefreshProxyList();
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is ContextMenuStrip _sender)
            {
                var selected = _sender.Items.OfType<ToolStripMenuItem>().Where(x => x.Selected).FirstOrDefault();
                if (selected is null || !selected.Enabled) return;

                switch (selected)
                {
                    case ToolStripMenuItem item when item == toolStripMenuItem_Enable: EnableSelectedProxies(); break;
                    case ToolStripMenuItem item when item == toolStripMenuItem_Disable: DisableSelectedProxies(); break;

                    case ToolStripMenuItem item when item == toolStripMenuItem_New:
                        if (SetProxyForm == null) SetProxyForm = new SetProxy(this);
                        SetProxyForm.UseNormalMode();
                        SetProxyForm.ShowDialog();
                        break;

                    case ToolStripMenuItem item when item == toolStripMenuItem_Modify:
                        if (SetProxyForm == null) SetProxyForm = new SetProxy(this);
                        SetProxyForUpdate(SetProxyForm);
                        SetProxyForm.ShowDialog();
                        break;

                    case ToolStripMenuItem item when item == toolStripMenuItem_Refresh:
                        RefreshProxyList();
                        break;
                    case ToolStripMenuItem item when item == toolStripMenuItem_RefreshPingStatus:
                        RefreshConnectHostPingStatus();
                        break;
                    case ToolStripMenuItem item when item == toolStripMenuItem_FlushDnsCache:
                        FlushDnsCache();
                        break;

                    case ToolStripMenuItem item when item == toolStripMenuItem_Delete: DeleteSelectedProxies(); break;

                    case ToolStripMenuItem item when item == toolStripMenuItem_About:
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
                toolStripMenuItem_Enable.Enabled = e.Button == MouseButtons.Right && _sender.SelectedItems.OfType<ListViewItem>().Any(x => x.ImageIndex == 0);
                toolStripMenuItem_Disable.Enabled = e.Button == MouseButtons.Right && _sender.SelectedItems.OfType<ListViewItem>().Any(x => x.ImageIndex == 1);

                toolStripMenuItem_Delete.Enabled = e.Button == MouseButtons.Right && _sender.SelectedItems.OfType<ListViewItem>().Any();
                toolStripMenuItem_Modify.Enabled = e.Button == MouseButtons.Right && _sender.SelectedItems.OfType<ListViewItem>().Count() == 1;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (sender is ListView _sender)
            {
                var selectAny = _sender.SelectedItems.OfType<ListViewItem>().Any();
                if (selectAny)
                {
                    if (SetProxyForm == null) SetProxyForm = new SetProxy(this);
                    SetProxyForUpdate(SetProxyForm);
                    SetProxyForm.ShowDialog();
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
            listViewProxies.Sort();
        }

        private void listViewProxies_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is ListView)
            {
                if (e.KeyCode == Keys.Delete) DeleteSelectedProxies();
            }
        }

        private void RefreshConnectHostPingStatus()
        {
            try
            {
                var items = listViewProxies.Items.OfType<ListViewItem>();
                hostStatus = new List<KeyValuePair<string, IPStatus>>();
                foreach (var item in items)
                {
                    try
                    {
                        //Custom Color for cells
                        item.UseItemStyleForSubItems = false;
                        //Proceed
                        Data.Rule rule = ParseRule(item);
                        //Check if host already pinged host
                        var alreadyChkdHost = hostStatus.FirstOrDefault(x => x.Key.Equals(rule.ConnectTo, StringComparison.OrdinalIgnoreCase));
                        if (!string.IsNullOrEmpty(alreadyChkdHost.Key))
                        {
                            //Skip Checking Status since already checked
                            UpdateListViewItem(item, rule, item.ImageIndex, alreadyChkdHost.Value.ToString());
                        }
                        else
                        {
                            //If not yet checked
                            PingCheckerUtil.GetPingResult(rule.ConnectTo, 2, out IPStatus ipStatus, out _, out _);
                            hostStatus.Add(new KeyValuePair<string, IPStatus>(rule.ConnectTo, ipStatus));
                            UpdateListViewItem(item, rule, item.ImageIndex, ipStatus.ToString());
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}
