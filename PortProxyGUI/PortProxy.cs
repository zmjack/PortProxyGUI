using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PortProxyGUI
{
    public class PortProxy
    {
        public string Type { get; set; }

        public string ListenOn { get; set; }
        public string ListenPort { get; set; }

        public string ConnectTo { get; set; }
        public string ConnectPort { get; set; }

    }

}
