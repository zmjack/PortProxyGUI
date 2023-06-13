using System;

namespace PortProxyGUI.Native
{
    [Flags]
    internal enum ServiceControls : uint
    {
        SERVICE_CONTROL_PARAMCHANGE = 0x00000006,
    }
}
