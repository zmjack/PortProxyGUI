using PortProxyGUI.Data;
using System;
using System.IO;
using System.Windows.Forms;

namespace PortProxyGUI
{
    static class Program
    {
        public static readonly ApplicationDbScope Database = ApplicationDbScope.FromFile(
            Path.Combine(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "PortProxyGUI"
                ), "config.db"
            ));

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

#if NET6_0_OR_GREATER
            ApplicationConfiguration.Initialize();
#elif NETCOREAPP3_1_OR_GREATER
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#endif

            Application.Run(new PortProxyGUI());
        }
    }
}
