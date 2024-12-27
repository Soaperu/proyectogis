using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddinUI.Resources.Extensions
{
    public static class MessageFormatter
    {
        public static string FormatMessage(this string template, params object[] args)
        {
            return string.Format(template, args);
        }
    }
}
