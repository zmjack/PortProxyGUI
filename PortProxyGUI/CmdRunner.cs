using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PortProxyGUI
{
    public static class CmdRunner
    {
        public static string Execute(string cmd)
        {
            var proc = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
            });
            proc.Start();

            proc.StandardInput.WriteLine($"{cmd} & exit");
            var output = proc.StandardOutput.ReadToEnd();

            return output;
        }
    }
}
