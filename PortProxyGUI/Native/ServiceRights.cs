using System;

namespace PortProxyGUI.Native
{
    [Flags]
    public enum ServiceRights : uint
    {
        SERVICE_PAUSE_CONTINUE = 0x0040,
    }
}
