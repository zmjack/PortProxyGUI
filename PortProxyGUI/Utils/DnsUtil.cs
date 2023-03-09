using System;
using System.Runtime.InteropServices;

namespace PortProxyGUI.Utils
{
    internal class DnsUtil
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        static extern uint DnsFlushResolverCache();

        public static void FlushCache()
        {
            var status = DnsFlushResolverCache();
            if (status == 0) throw new InvalidOperationException("Flush DNS Cache failed.");
        }

    }
}
