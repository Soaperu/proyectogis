﻿using ArcGIS.Core.Data;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Internal.Core.CommonControls;
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
        /// <param name="mapName">El nombre del mapa en el que se eliminarán las capas.</param>
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

        public static async void SelectSetAndZoomByNameAsync(string layerName, string whereClause = "")
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
                MapView.Active?.ZoomTo(layer, true, new TimeSpan(0, 0, 2));
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

        public async static Task AddLayerCheckedListBox(string selectCheckedLayer, string zone, FeatureClassLoader function, int datum, ExtentModel extent)
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
                        if (datum == 1)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Caram84 + zone, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Caram56 + zone, false);
                        }
                        await function.IntersectFeatureClassAsync(selectCheckedLayer, extent.xmin, extent.ymin, extent.xmax, extent.ymax, "Caram"+ GlobalVariables.idExport);
                        break;
                    case "Catastro Forestal":
                        if (datum == 1)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Cforestal+zone, false);
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56+zone, false);
                        }
                        await function.IntersectFeatureClassAsync(selectCheckedLayer, extent.xmin, extent.ymin, extent.xmax, extent.ymax, "CForestal" + GlobalVariables.idExport);
                        break;
                    case "Limite Departamental":
                        if (datum == 1)
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
                        if (datum == 1)
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
                        if (datum == 1)
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
                        if (datum == 1)
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
                        if (datum == 1)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Rios84, false);
                            
                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Rios56, false);
                        }
                        //cls_Catastro.DefinitionExpression(lo_Filtro_Dpto_mod, m_Application, "Drenaje");
                        //cls_Catastro.UniqueSymbols(m_Application, "Drenaje");
                        break;

                    case "Red Vial":
                        if (datum == 1)
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Carretera84, false);

                        }
                        else
                        {
                            await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Carretera56, false);
                        }
                        break;

                    case "Centros Poblados":
                        
                        if (datum == 1)
                        {
                            currentFeatureLayer = await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CPoblado84, false);
                        }
                        else
                        {
                            currentFeatureLayer = await function.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CPoblado56, false);
                        }
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
    }
}
