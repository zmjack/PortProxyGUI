using PortProxyGUI.Data;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PortProxyGUI;

static class Program
{
    private static string GetPath(params string[] pathes)
    {
        if (!pathes.Any()) return string.Empty;

#if NET6_0_OR_GREATER || NET451_OR_GREATER
        return Path.Combine(pathes);
#else
        return pathes.Aggregate(Path.Combine);
#endif
    }

    public static ApplicationDbScope Database { get; } = ApplicationDbScope.FromFile(GetPath(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "PortProxyGUI",
        "config.db"
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
