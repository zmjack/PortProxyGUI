using System;
using System.Runtime.InteropServices;

namespace PortProxyGUI.Native
{
    internal class NativeMethods
    {
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr OpenSCManager(string machineName, string databaseName, uint dwAccess);

        [DllImport("advapi32.dll", EntryPoint = "OpenServiceW", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceRights dwDesiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "QueryServiceStatus", CharSet = CharSet.Auto)]
        internal static extern bool QueryServiceStatus(IntPtr hService, ref ServiceStatus dwServiceStatus);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ControlService(IntPtr hService, ServiceControls dwControl, ref ServiceStatus lpServiceStatus);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseServiceHandle(IntPtr hSCObject);

        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        internal static extern uint DnsFlushResolverCache();

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StartService(IntPtr hService, int dwNumServiceArgs, string[] lpServiceArgVectors);

    }
}
