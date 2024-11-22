using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using System.Threading.Tasks;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Core.Geometry;

namespace CommonUtilities.ArcgisProUtils
{
    internal class SymbologyUtils
    {
        //private static List<string> fields;

        /// <summary>
        /// Aplica una simbología simple a una capa de puntos.
        /// </summary>
        /// <param name="layer">Capa a la que se le aplicará la simbología.</param>
        /// <param name="color">Color en formato hexadecimal (ejemplo: "#FF0000" para rojo).</param>
        public static async Task ApplySimplePointSymbologyAsync(FeatureLayer layer, CIMColor  color)
        {
            await QueuedTask.Run(() =>
            {
                var renderer = layer.GetRenderer() as CIMSimpleRenderer;
                if (renderer == null)
                {
                    renderer = new CIMSimpleRenderer();
                }
                var symbol = SymbolFactory.Instance.ConstructPointSymbol(color, 5, SimpleMarkerStyle.Circle);
                renderer.Symbol = symbol.MakeSymbolReference();
                layer.SetRenderer(renderer);
            });
        }

        /// <summary>
        /// Aplica una simbología categorizada a una capa.
        /// </summary>
        /// <param name="layer">Capa a la que se le aplicará la simbología.</param>
        /// <param name="fieldName">Nombre del campo para categorizar.</param>
        public static async Task ApplyUniqueValueRendererAsync(FeatureLayer layer, string fieldName)
        {
            await QueuedTask.Run(() =>
            {
                //fields = new List<string> { fieldName };
                var uvRendererDefinition = new UniqueValueRendererDefinition()
                {
                    ValueFields = new List<string> { fieldName },
                };
                var renderer = layer.CreateRenderer(uvRendererDefinition);
                layer.SetRenderer(renderer);
            });
        }
        public async void CustomLineDashPolygonLayer(FeatureLayer layer, CIMColor colorFill, CIMColor colorLine1, CIMColor colorLine2)
        {
            await QueuedTask.Run(() =>
            {
                var trans = 75.0;//semi transparent
                // Linea de brode negra con grosor 2 y de estilo punteado
                CIMStroke InLine = SymbolFactory.Instance.ConstructStroke(colorLine1, 1.5, SimpleLineStyle.Dash);
                CIMStroke OutLine = SymbolFactory.Instance.ConstructStroke(colorLine2, 2.5, SimpleLineStyle.Dash);
                var symbol = layer.GetRenderer() as CIMSimpleRenderer;
                // Crear una nueva simbología con relleno de color transparente
                var newFillSymbol = SymbolFactory.Instance.ConstructPolygonSymbol(
                    colorFill, SimpleFillStyle.Solid, InLine);
                newFillSymbol.SymbolLayers = newFillSymbol.SymbolLayers.Concat(new[] { OutLine }).ToArray();
                symbol.Symbol = newFillSymbol.MakeSymbolReference();
                // Actualiza la simbologia 
                layer.SetRenderer(symbol);
            });
        }

        public async void CustomLineDashPolygonGraphic(ArcGIS.Core.Geometry.Geometry geomPolygon, CIMColor colorFill, CIMColor colorLine1, CIMColor colorLine2)
        {
            await QueuedTask.Run(() =>
            {
                Map activeMap = MapView.Active.Map;
                var trans = 75.0;//semi transparent
                // Linea de brode negra con grosor 2 y de estilo punteado
                CIMStroke InLine = SymbolFactory.Instance.ConstructStroke(colorLine1, 1.5, SimpleLineStyle.Dash);
                CIMStroke OutLine = SymbolFactory.Instance.ConstructStroke(colorLine2, 2.5, SimpleLineStyle.Dash);
                
                // Crear una nueva simbología con relleno de color transparente
                var newFillSymbol = SymbolFactory.Instance.ConstructPolygonSymbol(
                    colorFill, SimpleFillStyle.Solid, InLine);
                newFillSymbol.SymbolLayers = newFillSymbol.SymbolLayers.Concat(new[] { OutLine }).ToArray();
                // Crear y añadir el gráfico
                var cimSymbol = newFillSymbol.MakeSymbolReference();
                var cimGraphicElement = new CIMPolygonGraphic
                {
                    Polygon = geomPolygon as ArcGIS.Core.Geometry.Polygon,
                    Symbol = cimSymbol,
                };
                MapView.Active.AddOverlay(cimGraphicElement);
            });
        }

    }
}
