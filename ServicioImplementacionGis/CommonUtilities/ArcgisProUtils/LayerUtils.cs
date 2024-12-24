using ArcGIS.Core.Data;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Internal.Core.CommonControls;
using ArcGIS.Desktop.Internal.Mapping;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using MessageBox = ArcGIS.Desktop.Framework.Dialogs.MessageBox;


namespace CommonUtilities.ArcgisProUtils
{
    public static class LayerUtils
    {
        public static int datum_WGS84 = 2;
        public static int datum_PSAD56 = 1;
        private static FeatureLayer currentFeatureLayer;

        /// <summary>
        /// Agrega una capa al mapa especificado.
        /// </summary>
        /// <param name="map">Mapa al que se agregará la capa.</param>
        /// <param name="layerPath">Ruta o URL de la capa.</param>
        /// <returns>La capa agregada.</returns>
        public static async Task<Layer> AddLayerAsync(Map map, string layerPath)
        {
            return await QueuedTask.Run(() =>
            {
                try
                {
                    if (!File.Exists(layerPath))
                    {

                        MessageBox.Show($"El archivo {layerPath} no existe.");
                    }
                    Uri uri = new Uri(layerPath);
                    Layer layer = LayerFactory.Instance.CreateLayer(uri, map);
                    //MapView.Active.ZoomTo(layer);
                    return layer;
                }
                catch (Exception ex)
                {
                    // Captura cualquier otra excepción y mostrar el mensaje
                    MessageBox.Show($"Error al agregar la capa: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null; // Retornar null si ocurre un error
                }
            });
        }

        /// <summary>
        /// Agrega múltiples capas al mapa especificado.
        /// </summary>
        /// <param name="map">Mapa al que se agregarán las capas.</param>
        /// <param name="layerPaths">Lista de rutas o URLs de las capas.</param>
        /// <returns>Lista de capas agregadas.</returns>
        public static async Task<List<Layer>> AddLayersAsync(Map map, IEnumerable<string> layerPaths)
        {
            var layers = new List<Layer>();
            foreach (var path in layerPaths)
            {
                var layer = await AddLayerAsync(map, path);
                layers.Add(layer);
            }
            return layers;
        }

        /// <summary>
        /// Elimina las capas de un mapa específico en ArcGIS Pro. Si la lista de capas está vacía, se eliminarán todas las capas.
        /// </summary>
        /// <param name="layersToRemove">Lista opcional de nombres de capas a eliminar. Si está vacía, se eliminarán todas las capas.</param>
        public static async Task RemoveLayersFromActiveMapAsync(List<string> layersToRemove = null)
        {
            if (layersToRemove == null)
            {
                layersToRemove = new List<string>();
            }

            await QueuedTask.Run(() =>
            {
                var targetMap = MapView.Active.Map;

                var layers = targetMap.GetLayersAsFlattenedList();

                if (layersToRemove.Count == 0)
                {
                    // Si la lista de capas está vacía, eliminar todas las capas
                    foreach (var layer in layers)
                    {
                        targetMap.RemoveLayer(layer);
                    }
                }
                else
                {
                    // Eliminar solo las capas que están en la lista layersToRemove
                    foreach (var layer in layers)
                    {
                        // Verificar si el nombre de la capa está en la lista
                        if (layersToRemove.Contains(layer.Name, StringComparer.OrdinalIgnoreCase))
                        {
                            targetMap.RemoveLayer(layer);
                        }
                    }
                }
            });
        }

        public static async void SelectSetAndZoomAsync(FeatureLayer layer, string whereClause="")
        {
            await QueuedTask.Run(() =>
            {
                MapView.Active?.Map.SetSelection(null);
                var qry = new QueryFilter() { WhereClause = whereClause };
                layer.Select(qry);
                MapView.Active?.ZoomTo(layer, true, new TimeSpan(0, 0, 2));
            });
        }

        public static async void SelectSetAndZoomByNameAsync(string layerName, bool selected= true, string whereClause = "")
        {
            // Obtener el mapa activo
            var map = MapView.Active?.Map;
            if (map == null)
                return;

            // Buscar la capa por nombre en el mapa
            var layer = map.Layers.FirstOrDefault(l => l.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase)) as FeatureLayer;
            if (layer == null)
                return; // Si no se encuentra la capa, salimos del método

            await QueuedTask.Run(() =>
            {
                // Limpiar cualquier selección previa en el mapa
                map.SetSelection(null);
                if (whereClause != "")
                {
                    // Crear el filtro de consulta según el whereClause proporcionado
                    var qry = new QueryFilter { WhereClause = whereClause };

                    // Seleccionar las entidades que cumplan con la condición
                    layer.Select(qry);
                }
                // Hacer zoom a la capa seleccionada
                MapView.Active?.ZoomTo(layer, selected, new TimeSpan(0, 0, 2));
            });
        }

        // Método para cambiar el nombre de una capa por un nuevo nombre
        public static async Task ChangeLayerNameAsync(string oldLayerName, string newLayerName)
        {
            // Ejecutamos el cambio de nombre dentro de un contexto de cola (para asegurarnos de que se ejecute en el hilo correcto)
            await QueuedTask.Run(() =>
            {
                // Obtener el mapa activo en ArcGIS Pro
                Map map = MapView.Active.Map;

                // Buscar la capa con el nombre original (oldLayerName)
                var layer = map.Layers.FirstOrDefault(l => l.Name == oldLayerName);

                // Si se encuentra la capa, cambiar el nombre
                if (layer != null)
                {
                    layer.SetName(newLayerName);
                }
                else
                {
                    // Si la capa no se encuentra, mostramos un mensaje de error (opcional)
                    System.Windows.MessageBox.Show($"Capa con nombre '{oldLayerName}' no encontrada.");
                }
            });
        }
        public static async Task ChangeLayerNameByFeatureLayerAsync(FeatureLayer fLayer ,string newName)
        {
            await QueuedTask.Run(() =>
            {
                  // Si se encuentra la capa, cambiar el nombre
                if (fLayer == null)
                {
                    return;
                }
                fLayer.SetName(newName);
            });
        }

        public async static Task AddLayerCheckedListBox(string selectCheckedLayer, string zone, FeatureClassLoader function, int datum, ExtentModel extent = null)
        {
            try
            {
                string layer = selectCheckedLayer;
                switch (selectCheckedLayer)
                {
                    case "Zona Reservada":

                        await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_AReservada84, false);
                        //await function.ExportSpatialTemaAsync(layer, GlobalVariables.stateDmY, "zonaRese");
                        break;
                    case "Caram":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Caram84 + zone, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Caram56 + zone, false);
                        }
                        string layerExportName = "Caram" + GlobalVariables.idExport;
                        await function.IntersectFeatureClassAsync(selectCheckedLayer, extent.xmin, extent.ymin, extent.xmax, extent.ymax, layerExportName);
                        List<string> layersToRemove = new List<string>() { "Caram" };
                        string styleCaramPath = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCaram);
                        await RemoveLayersFromActiveMapAsync(layersToRemove);
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(layerExportName, styleCaramPath, "ESTILO", StyleItemType.PolygonSymbol);
                        await ChangeLayerNameAsync(layerExportName, "Caram");
                        break;
                    case "Catastro Forestal":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Cforestal + zone, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zone, false);
                        }
                        string layerExportNameCForestal = "Catastro Forestal" + GlobalVariables.idExport;
                        await function.IntersectFeatureClassAsync(selectCheckedLayer, extent.xmin, extent.ymin, extent.xmax, extent.ymax, layerExportNameCForestal);
                        List<string> cforestalToRemove = new List<string>() { "Catastro Forestal" };
                        string styleCForestalPath = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCForestal);
                        await RemoveLayersFromActiveMapAsync(cforestalToRemove);
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplyUniqueSymbologyFromStyleAsync(layerExportNameCForestal, styleCForestalPath, StyleItemType.PolygonSymbol);
                        await ChangeLayerNameAsync(layerExportNameCForestal, "Catastro Forestal");
                        break;
                    case "Limite Departamental":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Departamento_WGS + zone, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Departamento_Z + zone, false);
                        }
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(function.pFeatureLayer_depa);
                        break;
                    case "Limite Provincial":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Provincia_WGS + zone, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Provincia_Z + zone, false);
                        }
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(function.pFeatureLayer_prov);
                        break;
                    case "Limite Distrital":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_WGS + zone, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_Z + zone, false);
                        }
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(function.pFeatureLayer_dist);
                        break;

                    case "Capitales Distritales":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CDistrito84, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CDistrito56, false);
                        }
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(function.pFeatureLayer_capdist);
                        break;

                    //        case "Predio Rural":
                    //            cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_prediorural, m_Application, "1", false);
                    //            cls_Catastro.Color_Poligono_Simple(m_Application, "Predio Rural");
                    //            break;

                    case "Red Hidrografica":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Rios84, false);
                            
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Rios56, false);
                        }
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorLineSimple(function.pFeatureLayer_drena);
                        //cls_Catastro.UniqueSymbols(m_Application, "Drenaje");
                        break;

                    case "Red Vial":
                        if (datum == datum_WGS84)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Carretera84, false);

                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Carretera56, false);
                        }
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorLineSimple(function.pFeatureLayer_vias);
                        break;

                    case "Centros Poblados":
                        
                        if (datum == datum_WGS84)
                        {
                            currentFeatureLayer = await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CPoblado84, false);
                        }
                        else
                        {
                            currentFeatureLayer = await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CPoblado56, false);
                        }
                        await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorLineSimple(function.pFeatureLayer_ccpp);
                        break;
                        
                    default:
                        // Si hay algún item seleccionado que no coincide con ninguno de los case
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task AplicarFiltroYZoomAsync(string nameMap, string nameLayer, string definitionQuery)
        {
            await QueuedTask.Run(() =>
            {
                // 1. Nombre del mapa y la capa a buscar
                string nombreMapa = "Catastro Minero";
                string nombreCapa = "Catastro";

                // 2. Buscar el mapa "Catastro Minero" en el proyecto actual
                Map mapaObjetivo = Project.Current.GetMaps().FirstOrDefault(m => m.Name.Equals(nameMap, System.StringComparison.OrdinalIgnoreCase)).GetMap();

                if (mapaObjetivo == null)
                {
                    // Mostrar mensaje de error si el mapa no se encuentra
                    MessageBox.Show($"El mapa '{nameMap}' no fue encontrado en el proyecto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 3. Buscar la capa "Catastro" dentro del mapa encontrado
                Layer capaObjetivo = mapaObjetivo.GetLayersAsFlattenedList().FirstOrDefault(l => l.Name.Equals(nameLayer, System.StringComparison.OrdinalIgnoreCase));

                if (capaObjetivo == null)
                {
                    // Mostrar mensaje de error si la capa no se encuentra
                    MessageBox.Show($"La capa '{nameLayer}' no fue encontrada en el mapa '{nameMap}'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 4. Verificar que la capa es de tipo FeatureLayer
                if (!(capaObjetivo is FeatureLayer featureLayer))
                {
                    MessageBox.Show($"La capa '{nombreCapa}' no es una FeatureLayer válida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 5. Definir el DefinitionQuery
                //string definitionQuery = "EVAL IN ('EV','PR','PO','SI','CO','EX','VE','AR')";

                // 6. Aplicar el DefinitionQuery a la capa
                featureLayer.SetDefinitionQuery(definitionQuery);

                // 7. Asegurar que la capa está visible
                featureLayer.SetVisibility(true);

                // 8. Limpiar cualquier selección existente en la capa
                featureLayer.ClearSelection();

                // 9. Seleccionar todas las features que cumplen con el DefinitionQuery
                //featureLayer.Select(null, SelectionCombinationMethod.New);

                // 10. Obtener la extensión (extent) de las features seleccionadas
                Envelope extentSeleccion = featureLayer.GetFeatureClass().GetExtent();

                if (extentSeleccion.IsEmpty)
                {
                    // Informar al usuario si no se encontraron elementos que coincidan con el filtro
                    MessageBox.Show("No se encontraron elementos que coincidan con el filtro aplicado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // 11. Centrar y hacer zoom en el mapa a la extensión de las features seleccionadas
                MapView.Active.ZoomTo(extentSeleccion, new TimeSpan(0, 0, 1)); // 1 segundo de animación

                // Opcional: Resaltar o aplicar simbología adicional si es necesario
            });
        }
        public static async Task<FeatureLayer> AddFeatureClassToMapFromGdbAsync(Geodatabase geodatabase, string featureClassName, string featureLayerName, bool isVisible = true)
        {
            FeatureLayer featureLayer = null;
            return await QueuedTask.Run(() =>
            {
                // Obtener el mapa activo
                Map map = MapView.Active?.Map;
                if (map == null)
                {
                    System.Windows.MessageBox.Show("No se encontró un mapa activo.");
                    return null;
                }

                try
                {
                    // Buscar el FeatureClass por nombre dentro de la Geodatabase
                    using (FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName))
                    {
                        // Verificar si el FeatureClass fue encontrado
                        if (featureClass == null)
                        {
                            System.Windows.MessageBox.Show($"No se encontró el FeatureClass con el nombre '{featureClassName}' en la geodatabase.");
                            return null;
                        }

                        // Crear parámetros para el FeatureLayer
                        FeatureLayerCreationParams flParams = new FeatureLayerCreationParams(featureClass)
                        {
                            Name = featureLayerName, // Puedes personalizar el nombre que aparecerá en el mapa
                            IsVisible = isVisible
                        };

                        // Crear y agregar el FeatureLayer al mapa
                        featureLayer = LayerFactory.Instance.CreateLayer<FeatureLayer>(flParams, map);
                        return featureLayer;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error al agregar el FeatureClass '{featureClassName}' al mapa: {ex.Message}");
                    return null;
                }
            });
        }

    }
}
