using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace PortProxyGUI
{
    public static class PingCheckerUtil
    {
        // Adapted from https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.ping.send
        public static bool GetPingResult(string ipAddress, int timeout, out IPStatus responseStatus, out IPAddress responseIpAddress, out long responseTime)
        {
            //Defaults
            responseIpAddress = null;
            responseTime = 0;
            responseStatus = IPStatus.Unknown;
            try
            {
                //Sending 32bytes
                byte[] buffer = Encoding.ASCII.GetBytes("12345678901234567890123456789012");
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions(64, true);
                PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);
                responseIpAddress = reply.Address;
                responseTime = reply.RoundtripTime;
                responseStatus = reply.Status;
                return (reply.Status == IPStatus.Success) ? true : false;
            }
            catch { return false; }
        }
    }
}
