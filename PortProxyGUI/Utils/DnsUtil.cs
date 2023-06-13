using PortProxyGUI.Native;
using System;

namespace PortProxyGUI.Utils
{
    internal class DnsUtil
    {
        public static void FlushCache()
        {
            var status = NativeMethods.DnsFlushResolverCache();
            if (status == 0) throw new InvalidOperationException("Flush DNS Cache failed.");
        }

    }
}
