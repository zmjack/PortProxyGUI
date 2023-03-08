using Microsoft.Win32;
using PortProxyGUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortProxyGUI
{
    public static class PortProxyUtil
    {
        private static InvalidOperationException InvalidPortProxyType(string type) => new($"Invalid port proxy type ({type}).");
        private static readonly string[] ProxyTypes = new[] { "v4tov4", "v4tov6", "v6tov4", "v6tov6" };

        public static Rule[] GetProxies()
        {
            var ruleList = new List<Rule>();
            foreach (var type in ProxyTypes)
            {
                var keyName = $@"SYSTEM\ControlSet001\Services\PortProxy\{type}\tcp";
                var key = Registry.LocalMachine.OpenSubKey(keyName);

                if (key is not null)
                {
                    foreach (var name in key.GetValueNames())
                    {
                        var listenParts = name.Split('/');
                        var listenOn = listenParts[0];
                        if (!int.TryParse(listenParts[1], out var listenPort)) continue;

                        var connectParts = key.GetValue(name).ToString().Split('/');
                        var connectTo = connectParts[0];
                        if (!int.TryParse(connectParts[1], out var connectPort)) continue;

                        ruleList.Add(new Rule
                        {
                            Type = type,
                            ListenOn = listenOn,
                            ListenPort = listenPort,
                            ConnectTo = connectTo,
                            ConnectPort = connectPort,
                        });
                    }
                }
            }
            return ruleList.ToArray();
        }

        public static void AddOrUpdateProxy(Rule rule)
        {
            if (!ProxyTypes.Contains(rule.Type)) throw InvalidPortProxyType(rule.Type);

            var keyName = $@"SYSTEM\ControlSet001\Services\PortProxy\{rule.Type}\tcp";
            var key = Registry.LocalMachine.OpenSubKey(keyName, true);
            var valueName = $"{rule.ListenOn}/{rule.ListenPort}";
            var value = $"{rule.ConnectTo}/{rule.ConnectPort}";

            key.SetValue(valueName, value);
        }

        public static void DeleteProxy(Rule rule)
        {
            if (!ProxyTypes.Contains(rule.Type)) throw InvalidPortProxyType(rule.Type);

            var keyName = $@"SYSTEM\ControlSet001\Services\PortProxy\{rule.Type}\tcp";
            var key = Registry.LocalMachine.OpenSubKey(keyName, true);
            var valueName = $"{rule.ListenOn}/{rule.ListenPort}";

            key.DeleteValue(valueName);
        }
    }
}
