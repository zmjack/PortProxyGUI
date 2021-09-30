using NStandard;
using PortProxyGUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PortProxyGUI
{
    public static class CmdUtil
    {
        private static Regex GetRegex(string fromType, string toType)
        {
            return new Regex($@"{fromType}[^:]*:[^\n]+?{toType}[^:]*:\r\n\r\n.+?\r\n--------------- ----------  --------------- ----------\r\n(.+?)\r\n\r\n", RegexOptions.Singleline);
        }

        private static readonly Dictionary<string, Regex> RegexList = new Dictionary<string, Regex>()
        {
            ["ipv4 to ipv4"] = GetRegex("ipv4", "ipv4"),
            ["ipv4 to ipv6"] = GetRegex("ipv4", "ipv6"),
            ["ipv6 to ipv4"] = GetRegex("ipv6", "ipv4"),
            ["ipv6 to ipv6"] = GetRegex("ipv6", "ipv6"),
        };

        public static Rule[] GetProxies()
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
                var regex = RegexList[$"{type.From} to {type.To}"];
                var typeProxies = output.ExtractFirst(regex)
                    ?.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .Select(line =>
                    {
                        var parts = line.Resolve(new Regex(@"^([^\s]+)\s+([^\s]+)\s+([^\s]+)\s+([^\s]+)$"));
                        return new Rule
                        {
                            Type = type.Type,
                            ListenOn = parts[1].First(),
                            ListenPort = int.Parse(parts[2].First()),
                            ConnectTo = parts[3].First(),
                            ConnectPort = int.Parse(parts[4].First()),
                        };
                    });
                return typeProxies ?? new Rule[0];
            });
            return proxies.ToArray();
        }

        public static void AddProxy(string action, string type, string listenOn, int listenPort, string connectTo, int connectPort)
        {
            CmdRunner.Execute($"netsh interface portproxy {action} {type} listenaddress={listenOn} listenport={listenPort} connectaddress={connectTo} connectport={connectPort}");
        }

        public static void DeleteProxy(string type, string listenOn, int listenPort)
        {
            CmdRunner.Execute($"netsh interface portproxy delete {type} listenaddress={listenOn} listenport={listenPort}");
        }

    }
}
