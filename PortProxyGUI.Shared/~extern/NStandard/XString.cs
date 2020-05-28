using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace PortProxyGUI._extern.NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XString
    {
        /// <summary>
        /// Indicates whether the string matches the specified regular expression.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsMatch(this string @this, Regex regex) => regex.Match(@this).Success;

        /// <summary>
        /// Projects the specified string to a new string by using regular expressions.
        ///     If there is no match, this method returns null.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Project(this string @this, Regex regex, string target = null)
        {
            var match = regex.Match(@this);
            if (match.Success)
            {
                if (target is null)
                    return string.Join("", match.Groups.OfType<Group>().Skip(1).Select(g => g.Value).ToArray());
                else return regex.Replace(match.Groups[0].Value, target);
            }
            else return null;
        }

        /// <summary>
        /// Projects the specified string to an array by using regular expressions.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static string[][] Resolve(this string @this, Regex regex)
        {
            if (TryResolve(@this, regex, out var ret)) return ret;
            else throw new ArgumentNullException("Can not match the sepecifed Regex.");
        }

        /// <summary>
        /// Projects the specified string to an array by using regular expressions.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool TryResolve(this string @this, Regex regex, out string[][] ret)
        {
            var match = regex.Match(@this);
            if (match.Success)
            {
                ret = match.Groups.OfType<Group>()
                    .Select(g => g.Captures.OfType<Capture>().Select(c => c.Value).ToArray()).ToArray();
                return true;
            }
            else
            {
                ret = null;
                return false;
            }
        }

    }
}
