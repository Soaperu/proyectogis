using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Core.Internal.Geometry;
using System.ComponentModel;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using System.Data;
using CommonUtilities.ArcgisProUtils.Models;

namespace CommonUtilities.ArcgisProUtils
{
    public class MapUtils
    {
        /// <summary>
        /// Crea un nuevo mapa y lo agrega al proyecto.
        /// </summary>
        /// <param name="mapName">Nombre del nuevo mapa.</param>
        /// <returns>El objeto Map creado.</returns>
        public static async Task CreateMapAsync(string mapName)
        {
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            await QueuedTask.Run(() =>
            {
                // Crear un nuevo mapa
                Map map = MapFactory.Instance.CreateMap(mapName, MapType.Map, MapViewingMode.Map, Basemap.None);
                // Agregar el mapa al proyecto
                ProApp.Panes.CreateMapPaneAsync(map);
                
            });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }

        /// <summary>
        /// Crea múltiples mapas y los agrega al proyecto.
        /// </summary>
        /// <param name="mapNames">Lista de nombres de los mapas a crear.</param>
        /// <returns>Lista de objetos Map creados.</returns>
        public static async Task<List<Map>> CreateMapsAsync(IEnumerable<string> mapNames)
        {
            var maps = new List<Map>();
            foreach (var name in mapNames)
            {
                await CreateMapAsync(name);
                //maps.Add(map);
            }
            return maps;
        }

        public static async Task<Map> FindMapByNameAsync(string mapName)
        {
            return await QueuedTask.Run(() =>
            {
                // Obtiene todos los mapas del proyecto actual
                var allMaps = Project.Current.GetItems<MapProjectItem>();

                // Busca el mapa con el nombre especificado
                var mapItem = allMaps.FirstOrDefault(m => m.Name.Equals(mapName, StringComparison.OrdinalIgnoreCase));

                // Si se encuentra, se devuelve como objeto Map
                return mapItem != null ? mapItem.GetMap() : null;
            });
        }

        public static async Task ActivateMapAsync(Map map)
        {
            await QueuedTask.Run(() =>
            {
                var mapProjItem = Project.Current.GetItems<MapProjectItem>().FirstOrDefault(mpi => mpi.Name == map.Name);
            if (mapProjItem != null)
            {
                // Activa el mapa en la vista
                //MappingModule.ActiveMapView = MapView.Active;
                var mapPane = ProApp.Panes.OfType<IMapPane>().FirstOrDefault(mPane => (mPane as Pane).ContentID == mapProjItem.Path);
                if (mapPane != null)
                {
                    var pane = mapPane as Pane;
                    pane.Activate();
                }
            }
        });
        }

        public static void AnnotateLayer(FeatureLayer layer, string field, string graphicLayerName, string color = "")
        {
            var featureclass = layer.GetFeatureClass();
            var map = MapView.Active.Map;

            GraphicsLayer? graphicsLayer = map.GetLayersAsFlattenedList()
                .OfType<ArcGIS.Desktop.Mapping.GraphicsLayer>().FirstOrDefault(g => g.Name == graphicLayerName);

            if (graphicsLayer != null)
            {
                // Eliminar el graphic existente
                QueuedTask.Run(() => map.RemoveLayer(graphicsLayer));
            }

            var glParams = new GraphicsLayerCreationParams { Name = graphicLayerName };
            LayerFactory.Instance.CreateLayer<ArcGIS.Desktop.Mapping.GraphicsLayer>(glParams, map);
            graphicsLayer = map.GetLayersAsFlattenedList()
                .OfType<ArcGIS.Desktop.Mapping.GraphicsLayer>().FirstOrDefault(g => g.Name == graphicLayerName);

            QueuedTask.Run(() =>
            {              

                // Crear un cursor para iterar sobre las características de la capa
                using (var rowCursor = layer.Search(null))
                {
                    while (rowCursor.MoveNext())
                    {
                        using (Row row = rowCursor.Current)
                        {
                            var geometry = row["SHAPE"] as ArcGIS.Core.Geometry.Geometry ;
                            string fieldValue = Convert.ToString(row[field]);

                            if (geometry != null && !string.IsNullOrEmpty(fieldValue))
                            {
                                // Convertir la geometría a un punto (en este caso, tomamos el centroide de la geometría)
                                var point = geometry.Extent.Center;
                                var textGraphic = new CIMTextGraphic();
                                textGraphic.Symbol = SymbolFactory.Instance.ConstructTextSymbol
                                                                            (CIMColor.CreateRGBColor(255, 0, 0), 8.5, "Arial", "Regular").MakeSymbolReference();
                                textGraphic.Shape = geometry;
                                textGraphic.Text = fieldValue;
                                graphicsLayer.AddElement(textGraphic,fieldValue);
                            }
                            
                        }
                    }
                }
            });
            graphicsLayer.ClearSelection();
        }

        public static void AnnotateLayerbyName(string layername, string field, string graphicLayerName, string color = "#000000",
            string fontFamily ="Arial", double fontSize=8.5, string fontWeight="Regular")
        {
            
            var map = MapView.Active.Map;

            GraphicsLayer? graphicsLayer = map.GetLayersAsFlattenedList()
                .OfType<ArcGIS.Desktop.Mapping.GraphicsLayer>().FirstOrDefault(g => g.Name == graphicLayerName);

            if (graphicsLayer != null)
            {
                // Eliminar el graphic existente
                QueuedTask.Run(() => map.RemoveLayer(graphicsLayer));
            }
                       

            QueuedTask.Run(() =>
            {
                var glParams = new GraphicsLayerCreationParams { Name = graphicLayerName };
                LayerFactory.Instance.CreateLayer<ArcGIS.Desktop.Mapping.GraphicsLayer>(glParams, map);
                graphicsLayer = map.GetLayersAsFlattenedList()
                    .OfType<ArcGIS.Desktop.Mapping.GraphicsLayer>().FirstOrDefault(g => g.Name == graphicLayerName);
                FeatureLayer layer = CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.GetFeatureLayerFromMap(MapView.Active as MapView, layername);
                var featureclass = layer.GetFeatureClass();

                // Crear un cursor para iterar sobre las características de la capa
                using (var rowCursor = layer.Search(null))
                {
                    while (rowCursor.MoveNext())
                    {
                        using (Row row = rowCursor.Current)
                        {
                            var geometry = row["SHAPE"] as ArcGIS.Core.Geometry.Geometry;
                            string fieldValue = Convert.ToString(row[field]);

                            if (geometry != null && !string.IsNullOrEmpty(fieldValue))
                            {
                                // Convertir la geometría a un punto (en este caso, tomamos el centroide de la geometría)
                                var point = geometry.Extent.Center;
                                var textGraphic = new CIMTextGraphic();
                                CIMColor rgbColor = ColorUtils.HexToCimColorRGB(color);
                                var textSymbol = SymbolFactory.Instance.ConstructTextSymbol
                                                                            (rgbColor, fontSize, fontFamily, fontWeight);
                                textGraphic.Placement = Anchor.CenterPoint;
                                textGraphic.Shape = point;
                                textGraphic.Text = fieldValue;
                                textGraphic.Symbol = textSymbol.MakeSymbolReference();
                                graphicsLayer.AddElement(textGraphic, fieldValue);
                            }

                        }
                    }
                }
                graphicsLayer.ClearSelection();

            });
        }

        public static void DeleteGraphicLayerByName(string graphicLayerName)
        {
            var map = MapView.Active.Map;

            GraphicsLayer? graphicsLayer = map.GetLayersAsFlattenedList()
                .OfType<ArcGIS.Desktop.Mapping.GraphicsLayer>().FirstOrDefault(g => g.Name == graphicLayerName);

            if (graphicsLayer != null)
            {
                // Eliminar el graphic existente
                QueuedTask.Run(() => map.RemoveLayer(graphicsLayer));
            }

        }

        
        public static async Task<List<ListarCoordenadasModel>> GetRowsAslistByClick(MapPoint clickedPoint)
        {
            var map = MapView.Active.Map;
            var selectedLayers = MapView.Active.GetSelectedLayers();
            var layer = selectedLayers.OfType<FeatureLayer>().FirstOrDefault();
            var listRows = new List<ListarCoordenadasModel>();

            await QueuedTask.Run(() =>
            {
                if (layer == null)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, selecciona una capa en el panel de contenido.", "Advertencia");
                    return;
                }
                if (layer.Name.ToLower() != "catastro")
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, selecciona la capa de Catastro.", "Advertencia");
                    return;
                }

                using (var rowCursor = layer.Search(null))
                {
                    while (rowCursor.MoveNext())
                    {
                        using (Row row = rowCursor.Current)
                        {
                            var geometry = row["SHAPE"] as ArcGIS.Core.Geometry.Geometry;
                            var polygon = geometry as ArcGIS.Core.Geometry.Polygon;
                            //string fieldValue = Convert.ToString(row[field]);

                            if (geometry != null && GeometryEngine.Instance.Contains(geometry, clickedPoint))
                            {
                                var customRow = new ListarCoordenadasModel
                                {
                                    nombre = row["CONCESION"].ToString(),
                                    numero = row["CONTADOR"].ToString(),
                                    codigo = row["CODIGOU"].ToString(),
                                    area = row["HECTAGIS"].ToString()
                                };

                                listRows.Add(customRow);
                            }
                        }
                    }
                }
            });
            return listRows;


        }

        public static void AnnotateVertices(MapPoint clickedPoint)
        {
            //var featureclass = layer.GetFeatureClass();
            var map = MapView.Active.Map;
            var selectedLayers = MapView.Active.GetSelectedLayers();
            var layer = selectedLayers.OfType<FeatureLayer>().FirstOrDefault();
            var gl_param = new GraphicsLayerCreationParams { Name = "Vertices" };
            var graphicsLayerItem = LayerFactory.Instance.CreateLayer<ArcGIS.Desktop.Mapping.GraphicsLayer>(gl_param, map);

            GraphicsLayer? graphicsLayer = map.GetLayersAsFlattenedList()
                .OfType<ArcGIS.Desktop.Mapping.GraphicsLayer>().FirstOrDefault();

            QueuedTask.Run(() =>
            {
                if (layer == null)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, selecciona una capa en el panel de contenido.", "Advertencia");
                    return;
                }
                if (clickedPoint == null) return;


                // Crear un cursor para iterar sobre las características de la capa
                using (var rowCursor = layer.Search(null))
                {
                    while (rowCursor.MoveNext())
                    {
                        using (Row row = rowCursor.Current)
                        {
                            var geometry = row["SHAPE"] as ArcGIS.Core.Geometry.Geometry;
                            var polygon = geometry as ArcGIS.Core.Geometry.Polygon;
                            //string fieldValue = Convert.ToString(row[field]);

                            if (geometry != null && GeometryEngine.Instance.Contains(geometry, clickedPoint))
                            {
                                int contador = 1;
                                    for(int i = 0; i < polygon.PointCount - 1; i++)
                                    {
                                    var vertex = polygon.Points[i];
                                    var textGraphic = new CIMTextGraphic();
                                    textGraphic.Symbol = SymbolFactory.Instance.ConstructTextSymbol
                                                                                (CIMColor.CreateRGBColor(255, 0, 0), 9, "Arial", "Regular").MakeSymbolReference();
                                    textGraphic.Shape = vertex;
                                    textGraphic.Text = contador.ToString();
                                    graphicsLayer.AddElement(textGraphic, contador.ToString());
                                    contador += 1;
                                }
                                
                            }

                        }
                    }
                }
            });
            graphicsLayer.ClearSelection();

        }


       
        public static void LoadFeatureClassToMap(string featureClassName, string layerName, bool isVisible, int mapIndex=0)
        {
            QueuedTask.Run(() =>
            {
                // Obtener el mapa activo
                var map = MapView.Active.Map;
                if (map == null)
                    throw new Exception("No hay un mapa activo.");

                // Conectar a la geodatabase
                string gdbPath = @"C:\bdgeocatmin\BDGEOCATMINPRO_84.gdb"; // Actualiza esto con la ruta correcta
                using (var gdb = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri(gdbPath))))
                {
                    // Abrir la clase de entidad
                    var featureClass = gdb.OpenDataset<FeatureClass>(featureClassName);

                    // Crear los parámetros de la capa de entidades
                    var layerParams = new FeatureLayerCreationParams(featureClass)
                    {
                        Name = layerName, // Asignar el nombre
                        MapMemberIndex= mapIndex
                    };

                    // Crear la capa de entidad
                    var featureLayer = LayerFactory.Instance.CreateLayer<FeatureLayer>(layerParams, map);
                    featureLayer.SetVisibility(isVisible);

                }
            });
        }

        // Función para eliminar filas de una clase de entidad en una geodatabase
        public static void DeleteRowsFromFeatureClass(string featureClassName)
        {
            QueuedTask.Run(() =>
            {
                try
                {
                    // Conectar a la geodatabase
                    string gdbPath = @"C:\bdgeocatmin\BDGEOCATMINPRO_84.gdb"; // Actualiza esto con la ruta correcta
                    using (var gdb = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri(gdbPath))))
                    {
                        // Abrir la clase de entidad
                        var featureClass = gdb.OpenDataset<FeatureClass>(featureClassName);
                        if (featureClass == null)
                            throw new Exception($"No se encontró la clase de entidad: {featureClassName}");

                        // Crear un cursor para eliminar las filas
                        using (var rowCursor = featureClass.Search(null, false))
                        {
                            // Empezamos a eliminar las filas
                            while (rowCursor.MoveNext())
                            {
                                using (var row = rowCursor.Current)
                                {
                                    // Eliminar la fila (puedes agregar condiciones aquí si es necesario)
                                    row.Delete();
                                }
                            }
                        }

                        // Puedes agregar un mensaje de éxito si lo deseas
                        Console.WriteLine($"Filas eliminadas de la clase de entidad: {featureClassName}");
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine($"Error al eliminar filas: {ex.Message}");
                }
            });
        }


        


    }
}
