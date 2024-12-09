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
    }
}
