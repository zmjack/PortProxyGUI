using System;

namespace PortProxyGUI.Data
{
    public class Rule : IEquatable<Rule>
    {
        public string Id { get; set; }

        public string Type { get; set; }
        public string ListenOn { get; set; }
        public int ListenPort { get; set; }
        public string ConnectTo { get; set; }
        public int ConnectPort { get; set; }
        public string Comment { get; set; }
        public string Group { get; set; }

        public bool Equals(Rule other)
        {
            return Id == other.Id
                && Type == other.Type
                && ListenOn == other.ListenOn
                && ListenPort == other.ListenPort
                && ConnectTo == other.ConnectTo
                && ConnectPort == other.ConnectPort
                && Comment == other.Comment
                && Group == other.Group;
        }

        public bool EqualsWithKeys(Rule other)
        {
            return Type == other.Type
                && ListenOn == other.ListenOn
                && ListenPort == other.ListenPort;
        }

        public static int ParsePort(string portString)
        {
            if (int.TryParse(portString, out var port) && 0 < port && port < 65536) return port;
            else throw new NotSupportedException($"Invalid port string. ({portString})");
        }
    }
}
