# PortProxyGUI

A manager for netsh interface portproxy, which is to evaluate TCP/IP port redirect on windows.

![UI](https://raw.githubusercontent.com/zmjack/PortProxyGUI/master/docs/ui.png)

<br/>

## Upgrade

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

