# PortProxyGUI

A manager for netsh interface portproxy, which is to evaluate TCP/IP port redirect on windows.

![UI](https://raw.githubusercontent.com/zmjack/PortProxyGUI/master/docs/ui.png)

<br/>

> ![Note]
>
> The software does not configure the firewall.
>
> If necessary, manually configure the firewall.

<br/>

## Runtimes

### .NET

| Target framework                                             | Link                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| ![Static Badge](https://img.shields.io/badge/.NET-8.0-8A2BE2) | [Download .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) |
| ![Static Badge](https://img.shields.io/badge/.NET-6.0-8A2BE2) | [Download .NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) |

### .NET Framework

| Icon | Denote                                                       |
| ---- | ------------------------------------------------------------ |
| ✔️    | OS versions on which is **installed by default**.            |
| ➕    | OS versions on which doesn't come installed but **can be installed**. |

| Target framework                                             | Windows                    | Windows Server                    | Link                                                         |
| ------------------------------------------------------------ | -------------------------- | --------------------------------- | ------------------------------------------------------------ |
| ![Static Badge](https://img.shields.io/badge/.NET Framework-4.5.1-blue) | ✔️ **8.1 +**<br />➕ Vista + | ✔️ **2012 R2 +**<br />➕ 2008 SP2 + | [Download](https://dotnet.microsoft.com/download/dotnet-framework/net451) |
| ![Static Badge](https://img.shields.io/badge/.NET Framework-3.5-blue) | ✔️ **7 +**<br />➕ Vista     | ✔️ **2008 R2 SP1 +**<br />➕ 2003 + | [Download](https://dotnet.microsoft.com/download/dotnet-framework/net35-sp1) |

> ![Note]
>
> If you're using Windows 8, Windows Server 2008 R2 SP1, or greater.
>
> we recommend [installing .NET Framework 3.5 through the control panel](https://learn.microsoft.com/dotnet/framework/install/dotnet-35-windows-10?WT.mc_id=dotnet-35129-website).

<br/>

## Upgrade

- **v1.4.2**
  - Change the default font from ~~`Microsoft Sans Serif`~~ to **`Arial`**.
    - This setting provides better compatibility on operating systems with fewer fonts.
- **v1.4.1**
  - Add a status strip at the bottom of the window.
  - Add a check of the IP Helper service status, if the service is not running, a prompt will be displayed on the bottom status bar.
- **v1.4.0**
  - Command line calls have been removed to provide better performance.
  - New Feature Added: **Remember Window/Column Size**.
  - New Feature Added: **Flush DNS Cache**.
  - New Feature Added: **Support import and export configuration database**.
- **v1.3.1 - v1.3.2**
  - Fix program crash caused by wrong rules.
- **v1.3.0**
  - Update display, provide comments and grouping.
  - Fix the problem that the window size is not the same in different runtimes.

<br/>

## Information

The configuration file will be generated at:

```
[MyDocuments]\PortProxyGUI\config.db
```

The configuration database will be migrated **automatically** if the newer version software is used.

