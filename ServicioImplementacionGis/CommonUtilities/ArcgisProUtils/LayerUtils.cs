using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = ArcGIS.Desktop.Framework.Dialogs.MessageBox;

namespace CommonUtilities.ArcgisProUtils
{
    public static class LayerUtils
    {
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
                    MapView.Active.ZoomToAsync(layer);
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
        public static async Task RemoveLayersFromMapAsync(string mapName, List<string> layersToRemove = null)
        {
            if (layersToRemove == null)
            {
                layersToRemove = new List<string>();
            }

            await QueuedTask.Run(() =>
            {
                // Buscar el mapa por nombre
                var mapProjItems = Project.Current.GetItems<MapProjectItem>();
                List<Map> maps = new List<Map>();
                foreach (var item in mapProjItems)
                {
                    maps.Add(item.GetMap());
                }
                Map targetMap = maps.FirstOrDefault(m => m.Name.Equals(mapName, StringComparison.OrdinalIgnoreCase));

                if (targetMap == null)
                {
                    throw new InvalidOperationException($"El mapa con nombre '{mapName}' no se encontró.");
                }

                // Obtener todas las capas del mapa
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

    }
}
