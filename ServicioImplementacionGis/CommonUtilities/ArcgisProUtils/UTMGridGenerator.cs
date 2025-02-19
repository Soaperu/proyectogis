using ICIM = ArcGIS.Core.Internal.CIM;
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
using System.Drawing;
using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Core.Internal.CIM;
using System.Windows.Shapes;
using ArcGIS.Desktop.Core;
using System.Data;

namespace CommonUtilities.ArcgisProUtils
{
    public class UTMGridGenerator
    {
        public async Task<(FeatureLayer GridLayer, FeatureLayer PointLayer)> GenerateUTMGridAsync(double minEast, double minNorth, double maxEast, double maxNorth, string layerName, string zone, int interval=0)
        {
            var result = await QueuedTask.Run(() =>
            {
                // Crear o verificar la existencia de la capa de destino (líneas)
                FeatureLayer? gridLayer = GetOrCreateLayerwithNoRows(layerName, zone);
                if (gridLayer == null) throw new Exception("No se pudo crear o acceder a la capa.");

                // Crear o verificar la existencia de la capa de puntos
                FeatureLayer? pointLayer = GetOrCreateLayerwithNoRows($"{layerName}p",zone);
                if (pointLayer == null) throw new Exception("No se pudo crear o acceder a la capa de puntos.");

                // Calcular los límites y el intervalo
                var limits = CalculateLimits(minEast, minNorth, maxEast, maxNorth);
                if(interval == 0) interval = CalculateInterval(minNorth, maxNorth);
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
                //MapUtils.AnnotateLayer(pointLayer, "VALOR","Graphics Layer");
                //List<string> listado = new List<string>();
                //listado.Add($"{layerName}p_{zone}");
                //CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(listado);
                return (GridLayer:gridLayer, PointLayer:pointLayer);
            });
            return result;
        }

        public async Task AnnotateGridLayer(FeatureLayer featurelayer, string field, string graphicLayerName="Grilla")
        {
            await QueuedTask.Run(() =>
            {
                MapUtils.AnnotateLayer(featurelayer, field, graphicLayerName);
            });
        }

        public async Task RemoveGridLayer(string layerName, string zone)
        {
            await QueuedTask.Run(() =>
            {
                List<string> listado = new List<string>();
                listado.Add($"{layerName}p_{zone}");
                CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(listado);
            });
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

        public FeatureLayer? GetOrCreateLayerwithNoRows(string layerName, string zone)
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


        private void  AddFeatureToLayerGrid(FeatureLayer layer, ArcGIS.Core.Geometry.Polyline line, string value)
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


        /// <summary>
        /// Crea medidas grillas en el layout especificado.
        /// </summary>
        /// <param name="nombre_dataframe">Nombre del DataFrame (MapFrame) en el layout.</param>
        public static async Task CreationGridMesaures(LayoutProjectItem layoutItem, string mapName, int escalaf)
        {
            #pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            await QueuedTask.Run(async() =>
            {
                // Obtener el layout activo
                Layout layout = layoutItem.GetLayout();
                if (layout == null)
                {
                    MessageBox.Show("No hay un layout activo.");
                    return;
                }
                MapFrame mapFrame = layout.FindElement(mapName + " Map Frame") as MapFrame;
                Map mapCatastro = await MapUtils.FindMapByNameAsync(mapName);
                // Encontrar el MapFrame por nombre
                //MapFrame mapFrame = layout.FindElement(nombre_dataframe) as MapFrame;
                var srcMap1 = mapFrame.Map.SpatialReference;
                if (mapFrame == null)
                {
                    MessageBox.Show($"No se encontró un MapFrame con el nombre '{mapName}'.");
                    return;
                }

                var mapFrameDefinition = mapFrame.GetDefinition() as CIMMapFrame;
                //mapFrameDefinition.Grids = new CIMMapGrid[] {};

                // Crear una nueva instancia de CIMMeasuredGrid
                CIMMeasuredGrid cimMeasuredGrid = new CIMMeasuredGrid
                {
                    Name = "Coordenadas",
                    IsVisible = true,
                };
                cimMeasuredGrid.ProjectedCoordinateSystem = srcMap1.ToCIMSpatialReference() as ICIM.ProjectedCoordinateSystem;
                //var lineGrids = cimMeasuredGrid.GridLines;
                var lineGrid = new CIMGridLine();
                lineGrid.ElementType = GridElementType.Label;
                lineGrid.GridLineOrientation = GridLineOrientation.NorthSouth;
                

                // Determinar el intervalo de la grilla basado en 'escalaf'
                double gridInterval;
                if (escalaf < 50000)
                {
                    gridInterval = 1000;
                }
                else if (escalaf >= 50000 && escalaf < 100000)
                {
                    gridInterval = 2000;
                }
                else if (escalaf >= 100000 && escalaf <= 150000)
                {
                    gridInterval = 4000;
                }
                else if (escalaf > 150000 && escalaf <= 200000)
                {
                    gridInterval = 5000;
                }
                else if (escalaf > 200000 && escalaf <= 300000)
                {
                    gridInterval = 8000;
                }
                else if (escalaf > 300000 && escalaf <= 400000)
                {
                    gridInterval = 20000;
                }
                else if (escalaf > 400000 && escalaf <= 1000000)
                {
                    gridInterval = 40000;
                }
                else
                {
                    gridInterval = 100000;
                }

                lineGrid.Pattern = new CIMGridPattern { Interval = gridInterval, Start = 1, Stop = 0};
                lineGrid.FromTick = new CIMExteriorTick
                {
                    DrawPerpendicular = true,
                    Length = 0.09,
                    Offset = 0,
                    IsVisible = true,
                    EdgeAffinity = new int[] {}, // <-- This is not working
                    GridEndpoint = new CIMGridEndpoint
                    {
                        GridLabelTemplate = new CIMSimpleGridLabelTemplate
                        {
                            // DynamicStringTemplate = $"<dyn type=\"grid\" units=\"{gridUnits}\" decimalPlaces=\"0\" separator=\"True\" showDirections=\"False\" showZeroMinutes=\"False\" showZeroSeconds=\"False\"/>", 
                            DynamicStringTemplate = $"<dyn type=\"grid\" decimalPlaces=\"0\" separator=\"True\" showDirections=\"False\" showZeroMinutes=\"False\" showZeroSeconds=\"False\"/>",
                            Symbol = (SymbolFactory.Instance.ConstructTextSymbol(ColorFactory.Instance.BlackRGB, 8, "Arial", "Regular")).MakeSymbolReference()
                        },
                        Offset = 0.083,
                        Position = 4,  // <-- This is not working
                        LineSelection = 7
                    }
                    
                };
                lineGrid.ToTick = new CIMExteriorTick
                {
                    Length = 0.09,
                    Offset = 0.5,
                    IsVisible = true,
                    EdgeAffinity = new int[] { }, // <-- This is not working
                    GridEndpoint = new CIMGridEndpoint
                    {
                        
                        GridLabelTemplate = new CIMSimpleGridLabelTemplate
                        {
                            DynamicStringTemplate = "", //TODO                               
                            Symbol = (SymbolFactory.Instance.ConstructTextSymbol(ColorFactory.Instance.BlackRGB, 0.5, "Arial", "Regular")).MakeSymbolReference()
                        },
                        Offset = 0.083,
                        Position = 4,  // <-- This is not working
                        LineSelection = 7
                    }
                };
                var newLineSymbol = SymbolFactory.Instance.ConstructLineSymbol(new CIMRGBColor { R = 235, G = 235, B = 235, Alpha = 255 }, 0.5, SimpleLineStyle.Solid);
                cimMeasuredGrid.NeatlineSymbol = newLineSymbol.MakeSymbolReference();
                cimMeasuredGrid.GridLines = new CIMGridLine[] { lineGrid };
                mapFrameDefinition.Grids = new CIMMapGrid[] { cimMeasuredGrid };
                mapFrame.SetDefinition(mapFrameDefinition);
                
            });
            #pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }

        public DataTable ObtenerVertices100Ha(ExtentModel extent)
        {
            DataTable lodtbDatos = new DataTable();
            int k = 0;
            int step = 1000;
            int numVer = 1;
            lodtbDatos.Columns.Add("CG_CODIGO", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("PE_NOMDER", Type.GetType("System.String"));
            lodtbDatos.Columns.Add("CD_COREST", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_CORNOR", Type.GetType("System.Double"));
            lodtbDatos.Columns.Add("CD_NUMVER", Type.GetType("System.Double"));

            for (int i = (int)extent.xmin; i <= (int)extent.xmax - 1; i += step)
            {


                for (int j = (int)extent.ymin; j <= (int)extent.ymax - 1; j += step)
                {
                    k = k + 1;

                    // Primera fila
                    DataRow dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i;
                    dRow["CD_CORNOR"] = j;
                    dRow["CD_NUMVER"] = numVer;
                    lodtbDatos.Rows.Add(dRow);

                    // Segunda fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i + step;
                    dRow["CD_CORNOR"] = j;
                    dRow["CD_NUMVER"] = numVer + 1;
                    lodtbDatos.Rows.Add(dRow);

                    // Tercera fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i + step;
                    dRow["CD_CORNOR"] = j + step;
                    dRow["CD_NUMVER"] = numVer + 2;
                    lodtbDatos.Rows.Add(dRow);

                    // Cuarta fila
                    dRow = lodtbDatos.NewRow();
                    dRow["CG_CODIGO"] = "DM_" + k;
                    dRow["PE_NOMDER"] = "DM";
                    dRow["CD_COREST"] = i;
                    dRow["CD_CORNOR"] = j + step;
                    dRow["CD_NUMVER"] = numVer + 3;
                    lodtbDatos.Rows.Add(dRow);
                }
            }
            return lodtbDatos;
        }

        public async void Graficarcuadriculas100Ha(DataTable lodtbDatos)
        {
            string[] letrasMayusculas = new string[]
                {
                    "A","B","C","D","E","F","G",
                    "H","I","J","K","L","M","N",
                    "O","P","Q","R","S","T","U",
                    "V","W","X","Y","Z"
                };


            await QueuedTask.Run(async () =>
            {
                // Crear o verificar la existencia de la capa de destino (líneas)
                //UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                FeatureLayer? mallasLayer = GetOrCreateLayerwithNoRows("Recta", GlobalVariables.CurrentZoneDm);
                if (mallasLayer == null) throw new Exception("No se pudo crear o acceder a la capa.");

                // Recorre la tabla
                for (int i = 0; i < lodtbDatos.Rows.Count; i += 4)
                {
                    string codigo = lodtbDatos.Rows[i]["CG_CODIGO"].ToString();
                    int idpolygon = ((i / 4) + 1);

                    var polygon = PolygonBuilder.CreatePolygon(new[]
                        {
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+1]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+1]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+2]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+2]["CD_CORNOR"].ToString())),
                        MapPointBuilder.CreateMapPoint(double.Parse(lodtbDatos.Rows[i+3]["CD_COREST"].ToString()), double.Parse(lodtbDatos.Rows[i+3]["CD_CORNOR"].ToString())),
                    });

                    AddFeatureToLayer(mallasLayer, polygon, letrasMayusculas[idpolygon - 1], "PE", idpolygon.ToString(), "100");
                }
                string newNameMalla = "Cuadriculas_100HA";
                mallasLayer.SetName(newNameMalla);
                await SymbologyUtils.ColorPolygonSimple(mallasLayer);
            });

        }
        private void AddFeatureToLayer(FeatureLayer layer, ArcGIS.Core.Geometry.Polygon line, string codigo, string tipo, string idpol, string area)
        {
            using (var featureClass = layer.GetFeatureClass())
            using (var rowBuffer = featureClass.CreateRowBuffer())
            using (var featureCursor = featureClass.CreateInsertCursor())
            {
                rowBuffer["SHAPE"] = line;
                rowBuffer["CODIGOU"] = codigo;
                rowBuffer["TIPO"] = tipo;
                rowBuffer["POLIGONO"] = idpol;
                rowBuffer["AREA"] = area;
                featureCursor.Insert(rowBuffer);
            }
        }
    }
}