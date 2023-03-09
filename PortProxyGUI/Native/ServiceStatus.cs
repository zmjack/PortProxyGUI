using System.Runtime.InteropServices;

namespace PortProxyGUI.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public uint dwServiceType;
        public uint dwCurrentState;
        public uint dwControlsAccepted;
        public uint dwWin32ExitCode;
        public uint dwServiceSpecificExitCode;
        public uint dwCheckPoint;
        public uint dwWaitHint;
    }
}
