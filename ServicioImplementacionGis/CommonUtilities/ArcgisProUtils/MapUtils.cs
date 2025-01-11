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
using System.Windows;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping.Events;
using DatabaseConnector;
using CommonUtilities.ArcgisProUtils.Models.Constants;

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

        public static async Task DeleteSpecifiedMapsAsync(List<string>? mapNamesToDelete= null)
        {
            if (mapNamesToDelete == null || mapNamesToDelete.Count == 0)
            {
               var result =MessageBox.Show("No se proporcionaron nombres de mapas para eliminar. Desea Eliminar todos los mapas del proyecto?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            // Ejecuta en el contexto de ArcGIS Pro
            await QueuedTask.Run(() =>
            {
                // Obtiene todos los mapas del proyecto actual
                var allMaps = Project.Current.GetItems<MapProjectItem>();

                foreach (var mapItem in allMaps)
                {
                    // Si el mapa está en la lista de nombres para eliminar, procede a eliminarlo
                    if (mapNamesToDelete != null && mapNamesToDelete.Contains(mapItem.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        Project.Current.RemoveItem(mapItem);
                    }
                }
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

            // Colores para Catastro
            Dictionary<string, string> coloresCatastro = new Dictionary<string, string>()
            {
                { "G1", "#00FF00" },
                { "G2", "#FF0000" },
                { "G3", "#0000FF" },
                { "G4", "#000000" },
                { "G5", "#4E0000" },
                { "G6", "#CD00ED" },
                { "G7", "#000000" },
            };

            // Colores para SituacionDM
            Dictionary<string, string> coloresSituacionDM = new Dictionary<string, string>()
            {
                { "CONCESIÓN MINERA EN EXPLORACIÓN (1)", "#00A9E6" },
                { "CONCESIÓN MINERA EN EXPLOTACIÓN (1)", "#E69800" },
                { "SOLICITUD DE DERECHO MINERO", "#C5FEC3" },
                { "CONCESIÓN MINERA EXTINGUIDA", "#D2D2D2" },
                { "PLANTAS DE BENEFICIO, CANTERAS (ESTADO)", "#4E0000" },
                { "CONCESIÓN SIN ACTIVIDAD MINERA", "#CD00ED" },
                { "", "#888888" },
            };

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
                            
                            if (layername== "Catastro")
                            {
                                var leyenda = row["LEYENDA"].ToString();
                                if (coloresCatastro.ContainsKey(leyenda))
                                {
                                    color = coloresCatastro[leyenda];
                                }
                            }

                            if (layername == "Situacion_DM")
                            {
                                var tipo = row["TIPO"].ToString();
                                if (coloresSituacionDM.ContainsKey(tipo))
                                {
                                    color = coloresSituacionDM[tipo];
                                }
                            }


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
                string gdbPath = GlobalVariables.localGdbPath; //@"C:\bdgeocatmin\BDGEOCATMINPRO_84.gdb"; // Actualiza esto con la ruta correcta
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
                    string gdbPath = GlobalVariables.localGdbPath; //@"C:\bdgeocatmin\BDGEOCATMINPRO_84.gdb"; // Actualiza esto con la ruta correcta
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

        public static Map? GetActiveMapAsync()
        {
            // Ejecutar la tarea en el contexto adecuado de ArcGIS Pro
            //return QueuedTask.Run(() =>
            //{
                // Obtener el mapa activo desde la vista del mapa activa
            return MapView.Active?.Map;
            //});
        }

        public static async Task<Map> EnsureMapViewIsActiveAsync(string mapName)
        {
            if (MapView.Active != null && string.Equals(MapView.Active.Map.Name, mapName, StringComparison.OrdinalIgnoreCase))
            {
                return MapView.Active.Map;
            }

            // Esperar hasta que MapView.Active esté disponible
            TaskCompletionSource<Map> tcs = new TaskCompletionSource<Map>();

            SubscriptionToken eventToken = null;
            eventToken = DrawCompleteEvent.Subscribe(async args =>
            {
                // Desuscribirse del evento
                // Desuscribirse del evento
                if (eventToken != null)
                {
                    DrawCompleteEvent.Unsubscribe(eventToken);
                }
                // Activar el mapa "CATASTRO MINERO"
                Map map = await FindMapByNameAsync(mapName);
                await ActivateMapAsync(map);

                // Completar la tarea con el mapa activo
                //tcs.SetResult(MapView.Active.Map);
                tcs.SetResult(map);
            });

            // Esperar hasta que el evento se complete
            return await tcs.Task;
        }

        ///// <summary>
        ///// Asegura que el MapView especificado esté activo. Si no lo está, lo activa y espera a que se active.
        ///// </summary>
        ///// <param name="mapName">Nombre del mapa a activar.</param>
        ///// <param name="timeoutMilliseconds">Tiempo máximo de espera en milisegundos.</param>
        ///// <returns>El objeto Map activo.</returns>
        //public static async Task<Map> EnsureMapViewIsActiveAsync(string mapName, int timeoutMilliseconds = 10000)
        //{
        //    if (string.IsNullOrEmpty(mapName))
        //        throw new ArgumentException("El nombre del mapa no puede ser nulo o vacío.", nameof(mapName));

        //    // Si el mapa deseado ya está activo, retornarlo
        //    if (MapView.Active != null &&
        //        string.Equals(MapView.Active.Map.Name, mapName, StringComparison.OrdinalIgnoreCase))
        //    {
        //        return MapView.Active.Map;
        //    }

        //    // Crear un TaskCompletionSource para esperar hasta que el mapa esté activo
        //    TaskCompletionSource<Map> tcs = new TaskCompletionSource<Map>();

        //    // Suscribirse al evento de cambio de mapa activo
        //    SubscriptionToken token = null;
        //    token = ActiveMapViewChangedEvent.Subscribe((OnActiveMapViewChanged) =>
        //    {
        //        if (newMapView != null &&
        //            string.Equals(newMapView.Map.Name, mapName, StringComparison.OrdinalIgnoreCase))
        //        {
        //            // Desuscribirse del evento
        //            MapView.ActiveChanged.Unsubscribe(token);

        //            // Completar la tarea con el mapa activo
        //            tcs.SetResult(newMapView.Map);
        //        }
        //    });

        //    // Ejecutar la activación del mapa en el hilo especializado de ArcGIS Pro
        //    await QueuedTask.Run(async () =>
        //    {
        //        // Buscar el mapa por nombre
        //        Map map = await FindMapByNameAsync(mapName);
        //        if (map == null)
        //        {
        //            // Desuscribirse si el mapa no se encuentra
        //            MapView.ActiveChanged.Unsubscribe(token);
        //            tcs.SetException(new InvalidOperationException($"No se encontró un mapa con el nombre '{mapName}'."));
        //            return;
        //        }

        //        // Activar el mapa
        //        await ActivateMapAsync(map);
        //    });

        //    // Implementar timeout
        //    var delayTask = Task.Delay(timeoutMilliseconds);
        //    var completedTask = await Task.WhenAny(tcs.Task, delayTask);

        //    if (completedTask == delayTask)
        //    {
        //        // Desuscribirse del evento para evitar memory leaks
        //        MapView.ActiveChanged.Unsubscribe(token);
        //        throw new TimeoutException($"No se pudo activar el mapa '{mapName}' dentro del tiempo esperado.");
        //    }

        //    // Retornar el resultado
        //    return await tcs.Task;
        //}

        public static ExtentModel ObtenerExtent(string codigoValue, int datum, int radioKm = 0)
        {
            DatabaseHandler dataBaseHandler = new DatabaseHandler();
            var dmrRecords = dataBaseHandler.GetDMDataWGS84(codigoValue);
            double xmin = int.MaxValue;
            double xmax = int.MinValue;
            double ymin = int.MaxValue;
            double ymax = int.MinValue;
            double radioMeters = radioKm * 1000;
            ExtentModel extent = null;
            if (dmrRecords.Rows.Count > 0)
            {
                var originalDatumDm = dmrRecords.Rows[0]["SC_CODDAT"];
                if (datum == int.Parse(originalDatumDm.ToString()))
                {

                    // Iterar sobre las filas para calcular los valores extremos
                    foreach (DataRow row in dmrRecords.Rows)
                    {
                        double este = Convert.ToDouble(row["CD_COREST"]);
                        double norte = Convert.ToDouble(row["CD_CORNOR"]);

                        if (este < xmin) xmin = este;
                        if (este > xmax) xmax = este;
                        if (norte < ymin) ymin = norte;
                        if (norte > ymax) ymax = norte;
                    }
                }
                else
                {
                    // Iterar sobre las filas para calcular los valores extremos
                    foreach (DataRow row in dmrRecords.Rows)
                    {
                        double este = Convert.ToDouble(row["CD_COREST_E"]);
                        double norte = Convert.ToDouble(row["CD_CORNOR_E"]);

                        if (este < xmin) xmin = este;
                        if (este > xmax) xmax = este;
                        if (norte < ymin) ymin = norte;
                        if (norte > ymax) ymax = norte;
                    }
                }
                extent = new ExtentModel
                {
                    xmin = xmin - radioMeters,
                    xmax = xmax + radioMeters,
                    ymin = ymin - radioMeters,
                    ymax = ymax + radioMeters
                };
            }
            return extent;

        }
    }
}
