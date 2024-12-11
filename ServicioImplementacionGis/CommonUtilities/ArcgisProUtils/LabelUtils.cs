using ArcGIS.Core.CIM;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace CommonUtilities.ArcgisProUtils
{
    public static class LabelUtils
    {
        public static async Task LabelFeatureLayer(FeatureLayer featurelayer, string fieldname, int size, string colorhex="#000000", string fontWeight ="Regular")
        {
            await QueuedTask.Run(() =>
            {
                CIMColor colorRGB = ColorUtils.HexToCimColorRGB(colorhex);
                //Get the layer's definition
                //community sample Data\Admin\AdminSample.aprx
                var lyrDefn = featurelayer.GetDefinition() as CIMFeatureLayer;
                if (lyrDefn == null) return;
                //Get the label classes - we need the first one
                var listLabelClasses = lyrDefn.LabelClasses.ToList();
                var theLabelClass = listLabelClasses.FirstOrDefault();
                //set the label class Expression to use the Arcade expression
                theLabelClass.Expression = $"return $feature.{fieldname} ";

                var textsymbol =SymbolFactory.Instance.ConstructTextSymbol(colorRGB, size, "Arial", fontWeight);
                theLabelClass.TextSymbol = textsymbol.MakeSymbolReference();
                //Set the label definition back to the layer.
                featurelayer.SetDefinition(lyrDefn);
                featurelayer.SetLabelVisibility(true);
                MapView.Active.Redraw(true);
            });
        }
    }
}
