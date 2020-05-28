using System;
using System.ComponentModel;

namespace PortProxyGUI._extern.NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XObject
    {
        /// <summary>
        /// Do a task for itself, then return itself.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TSelf Then<TSelf>(this TSelf @this, Action<TSelf> task)
        {
            task(@this);
            return @this;
        }

    }
}
