using ArcGIS.Desktop.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SigcatminProAddin.View.Toolbars
{

    internal class CustomGalleryItem
    {
        internal CustomGalleryItem(string id, string group, IPlugInWrapper plugin)
        {
            CommandID = id;
            PlugInWrapper = plugin;
            if (PlugInWrapper.LargeImage is ImageSource)
                Icon32 = (ImageSource)PlugInWrapper.LargeImage;
            else
                Icon32 = PlugInWrapper.LargeImage;
            Group = group;
        }

        public IPlugInWrapper PlugInWrapper { get; private set; }
        public object Icon32 { get; private set; }

        public string CommandID { get; private set; }

        public string Group { get; private set; }

        internal void Execute()
        {
            if (PlugInWrapper.IsRelevant)
            {
                ((ICommand)PlugInWrapper).Execute(null);
            }
        }
    }
}
