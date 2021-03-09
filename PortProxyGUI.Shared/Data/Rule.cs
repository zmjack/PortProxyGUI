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
        public long ConnectPort { get; set; }

        public bool Equals(Rule other)
        {
            return Id == other.Id
                && Type == other.Type
                && ListenOn == other.ListenOn
                && ListenPort == other.ListenPort
                && ConnectTo == other.ConnectTo
                && ConnectPort == other.ConnectPort;
        }

        public bool EqualsWithKeys(Rule other)
        {
            return Type == other.Type
                && ListenOn == other.ListenOn
                && ListenPort == other.ListenPort;
        }

    }
}
