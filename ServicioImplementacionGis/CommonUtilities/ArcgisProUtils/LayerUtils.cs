using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var layer = LayerFactory.Instance.CreateLayer(new Uri(layerPath), map);
                return layer;
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
    }
}
