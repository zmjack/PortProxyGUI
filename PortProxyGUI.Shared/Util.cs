using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PortProxyGUI
{
    public class Util
    {
        /// <summary>
        /// Compatibility between .NET Framework and .NET Core.
        /// <see href="https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms" />
        /// </summary>
        public static readonly Font UiFont = new Font(new FontFamily("Microsoft Sans Serif"), 8f);

    }
}
