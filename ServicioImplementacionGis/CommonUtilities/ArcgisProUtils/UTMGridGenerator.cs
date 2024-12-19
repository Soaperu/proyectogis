using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Data;
using System;
using System.Threading.Tasks;
using ArcGIS.Core.Internal.Geometry;
using System.Windows.Documents;

namespace CommonUtilities.ArcgisProUtils
{
    public class UTMGridGenerator
    {
        public async Task<FeatureLayer> GenerateUTMGridAsync(double minEast, double minNorth, double maxEast, double maxNorth, string layerName, string zone)
        {
            FeatureLayer result = await QueuedTask.Run(() =>
            {
                // Crear o verificar la existencia de la capa de destino (líneas)
                FeatureLayer? gridLayer = GetOrCreateGridLayer(layerName, zone);
                if (gridLayer == null) throw new Exception("No se pudo crear o acceder a la capa.");

                // Crear o verificar la existencia de la capa de puntos
                FeatureLayer? pointLayer = GetOrCreatePointLayer($"{layerName}p",zone);
                if (pointLayer == null) throw new Exception("No se pudo crear o acceder a la capa de puntos.");

                // Calcular los límites y el intervalo
                var limits = CalculateLimits(minEast, minNorth, maxEast, maxNorth);
                int interval = CalculateInterval(minNorth, maxNorth);
                string clase_v = "2";
                int contador_v = 0;
                // Generar líneas verticales
                for (double x = limits.xMin; x <= limits.xMax; x += interval)
                {
                    var verticalLine = PolylineBuilder.CreatePolyline(new[]
                    {
                    MapPointBuilder.CreateMapPoint(x, limits.yMin),
                    MapPointBuilder.CreateMapPoint(x, limits.yMax)
                });
                    //if (contador_v>0) { clase_v = "1"; }
                    if (limits.xMin < x && x < limits.xMax)
                    {
                        clase_v = "1";
                    }
                    else
                    {
                        clase_v = "2";
                    }

                    AddFeatureToLayerGrid(gridLayer, verticalLine, clase_v);

                    // Crear puntos en cada intersección de las líneas
                    var point = MapPointBuilder.CreateMapPoint(x, limits.yMin);
                    string valor = (x/1000).ToString();
                    if (contador_v > 0)
                    {
                        AddPointToLayer(pointLayer, point, valor);

                    }
                    contador_v += 1;
                }
                string clase_h = "2";
                int contador_h = 0;
                // Generar líneas horizontales
                for (double y = limits.yMin; y <= limits.yMax; y += interval)
                {
                    var horizontalLine = PolylineBuilder.CreatePolyline(new[]
                    {
                    MapPointBuilder.CreateMapPoint(limits.xMin, y),
                    MapPointBuilder.CreateMapPoint(limits.xMax, y)
                });
                    //if (contador_h>0) { clase_h = "1"; }
                    if (limits.yMin < y && y < limits.yMax)
                    {
                        clase_h = "1";
                    }
                    else
                    {
                        clase_h = "2";
                    }

                    AddFeatureToLayerGrid(gridLayer, horizontalLine, clase_h);

                    // Crear puntos en cada intersección de las líneas
                    var point = MapPointBuilder.CreateMapPoint(limits.xMin, y);
                    string valor = (y / 1000).ToString();
                    if (contador_h > 0)
                    {
                        AddPointToLayer(pointLayer, point, valor);

                    }
                    contador_h += 1;
                }
                MapUtils.AnnotateLayer(pointLayer, "VALOR","Graphics Layer");
                List<string> listado = new List<string>();
                listado.Add($"{layerName}p_{zone}");
                CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(listado);
                return gridLayer;
            });
            return result;
        }

        private (double xMin, double xMax, double yMin, double yMax) CalculateLimits(double minEast, double minNorth, double maxEast, double maxNorth)
        {
            double buffer = 0; // Ajuste de buffer, similar a la variable loint_Buffer en VB.NET
            double xMin = Math.Floor((minEast - buffer) / 1000) * 1000;
            double xMax = Math.Floor((maxEast + buffer) / 1000) * 1000;
            double yMin = Math.Floor((minNorth - buffer) / 1000) * 1000;
            double yMax = Math.Floor((maxNorth + buffer) / 1000) * 1000;

            return (xMin, xMax, yMin, yMax);
        }

        private int CalculateInterval(double minNorth, double maxNorth)
        {
            int range = (int)((maxNorth - minNorth)*4/60 / 1000);
            return range switch
            {
                <=1 => 1000,
                <= 5 => 2000,
                <= 10 => 4000,
                <= 15 => 8000,
                <= 20 => 12000,
                <= 25 => 16000,
                _ => 20000,
            };
        }

        private FeatureLayer? GetOrCreateGridLayer(string layerName, string zone)
        {
            MapUtils.LoadFeatureClassToMap($"{layerName}_{zone}", $"{layerName}_{zone}", true);
            MapUtils.DeleteRowsFromFeatureClass($"{layerName}_{zone}");

            var map = MapView.Active.Map;
            if (map == null)
                throw new Exception("No hay un mapa activo.");

            // Buscar la capa de entidad en el mapa
            foreach (var layer in map.Layers)
            {
                if (layer.Name.Equals($"{layerName}_{zone}", StringComparison.OrdinalIgnoreCase) && layer is FeatureLayer featureLayer)
                {
                    return featureLayer;
                }
            }


            // Si no existe, crea una nueva capa
            return null;
        }


        private FeatureLayer? GetOrCreatePointLayer(string layerName, string zone)
        {
            MapUtils.LoadFeatureClassToMap($"{layerName}_{zone}", $"{layerName}_{zone}", true, -1);
            MapUtils.DeleteRowsFromFeatureClass($"{layerName}_{zone}");

            var map = MapView.Active.Map;
            if (map == null)
                throw new Exception("No hay un mapa activo.");

            // Buscar la capa de entidad en el mapa
            foreach (var layer in map.Layers)
            {
                if (layer.Name.Equals($"{layerName}_{zone}", StringComparison.OrdinalIgnoreCase) && layer is FeatureLayer featureLayer)
                {
                    return featureLayer;
                }
            }

            

            // Si no existe, crea una nueva capa
            return null;

        }


        private void  AddFeatureToLayerGrid(FeatureLayer layer, Polyline line, string value)
        {
            using (var featureClass = layer.GetFeatureClass())
            using (var rowBuffer = featureClass.CreateRowBuffer())
            using (var featureCursor = featureClass.CreateInsertCursor())
            {
                rowBuffer["SHAPE"] = line;
                rowBuffer["CLASE"] = value;
                featureCursor.Insert(rowBuffer);
            }
        }

        private void AddPointToLayer(FeatureLayer layer, MapPoint point, string value)
        {
            // Agregar un punto a la capa de características
            using (var featureClass = layer.GetFeatureClass())
            using (var rowBuffer = featureClass.CreateRowBuffer())
            using (var featureCursor = featureClass.CreateInsertCursor())
            {
                rowBuffer["SHAPE"] = point;
                rowBuffer["VALOR"] = value;
                featureCursor.Insert(rowBuffer);
            }
        }
    }


}
