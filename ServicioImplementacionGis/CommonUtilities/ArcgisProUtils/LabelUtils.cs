using ArcGIS.Core.CIM;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace CommonUtilities.ArcgisProUtils
{
    public static class LabelUtils
    {
        /// <summary>
        /// Asigna etiquetas a una capa de entidades en ArcGIS Pro basadas en un solo campo.
        /// </summary>
        /// <param name="featureLayer">Capa de entidades en la que se aplicarán las etiquetas.</param>
        /// <param name="fieldName">Nombre del campo cuyos valores serán usados como etiquetas.</param>
        /// <param name="size">Tamaño de la fuente de la etiqueta.</param>
        /// <param name="colorHex">Color de la etiqueta en formato hexadecimal (por defecto, negro "#000000").</param>
        /// <param name="fontWeight">Peso de la fuente (por defecto, "Regular").</param>
        /// <returns>Una tarea asíncrona que configura las etiquetas en la capa.</returns>
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
                featurelayer.SetVisibility(true);
            });
            //MapView.Active.Redraw(true);

        }

        /// <summary>
        /// Asigna etiquetas a una capa de entidades combinando múltiples campos.
        /// </summary>
        /// <param name="featureLayer">La capa de entidades a etiquetar.</param>
        /// <param name="fields">Lista de nombres de campos a combinar en la etiqueta.</param>
        /// <param name="separator">Separador entre los valores de los campos (por defecto un espacio).</param>
        /// <param name="size">Tamaño de la etiqueta.</param>
        /// <param name="colorHex">Color del texto en formato hexadecimal (por defecto negro).</param>
        /// <param name="fontWeight">Peso de la fuente (Regular, Bold, etc.).</param>
        /// <returns>Una tarea asíncrona que aplica la configuración de etiquetas.</returns>
        public static async Task LabelFeatureLayerWithMultipleFields(FeatureLayer featureLayer, List<string> fields, string separator = " ", int size = 12, string colorHex = "#000000", string fontWeight = "Regular")
        {
            await QueuedTask.Run(() =>
            {
                if (featureLayer == null || fields == null || fields.Count == 0) return;

                // Convertir el color hexadecimal a CIMColor
                CIMColor colorRGB = ColorUtils.HexToCimColorRGB(colorHex);

                // Obtener la definición de la capa
                var lyrDefn = featureLayer.GetDefinition() as CIMFeatureLayer;
                if (lyrDefn == null) return;

                // Obtener la lista de clases de etiquetas
                var listLabelClasses = lyrDefn.LabelClasses.ToList();
                var theLabelClass = listLabelClasses.FirstOrDefault();
                if (theLabelClass == null) return;

                // Crear la expresión Arcade para combinar múltiples campos
                string expression = $"return {string.Join($" + \"{separator}\" + ", fields.Select(f => $"$feature.{f}"))};";

                // Configurar la etiqueta
                theLabelClass.Expression = expression;
                var textSymbol = SymbolFactory.Instance.ConstructTextSymbol(colorRGB, size, "Arial", fontWeight);
                theLabelClass.TextSymbol = textSymbol.MakeSymbolReference();

                // Aplicar los cambios a la capa
                featureLayer.SetDefinition(lyrDefn);
                featureLayer.SetLabelVisibility(true);
                featureLayer.SetVisibility(true);
            });
        }
    }
}
