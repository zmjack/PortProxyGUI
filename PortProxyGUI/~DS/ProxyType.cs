using System;

namespace PortProxyGUI
{
    public class ProxyType
    {
        public ProxyType(string from, string to)
        {
            From = from;
            To = to;
        }

        public string From { get; set; }
        public string To { get; set; }
        public string Type
        {
            get
            {
                if (From == "ipv4" && To == "ipv4") return "v4tov4";
                if (From == "ipv4" && To == "ipv6") return "v4tov6";
                if (From == "ipv6" && To == "ipv4") return "v6tov4";
                if (From == "ipv6" && To == "ipv6") return "v6tov6";
                throw new NotSupportedException();
            }
        }

    }
}
