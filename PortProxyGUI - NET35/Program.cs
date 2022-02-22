using PortProxyGUI.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PortProxyGUI
{
    static class Program
    {
        public static readonly ApplicationDbScope SqliteDbScope = ApplicationDbScope.UseDefault();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SqliteDbScope.Migrate();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PortProxyGUI());
        }
    }
}
