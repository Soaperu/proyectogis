using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SigcatminProAddinUI.Resourecs.Helpers
{
    public static class PathAssembly
    {
        public static string GetExecutingAssembly()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
