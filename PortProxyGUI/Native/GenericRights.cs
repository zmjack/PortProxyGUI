using System;

namespace PortProxyGUI.Native
{
    [Flags]
    internal enum GenericRights : uint
    {
        GENERIC_READ = 0x80000000,
        GENERIC_WRITE = 0x40000000,
        GENERIC_EXECUTE = 0x20000000,
        GENERIC_ALL = 0x10000000,
    }
}
