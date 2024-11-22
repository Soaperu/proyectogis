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
    }
}
