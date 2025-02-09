using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;

namespace Sigcatmin.pro.Shared.Implements
{
    public class MapService : IMapService
    {
        public async Task<Map> CreateMapAsync(string mapName)
        {
            Map map = MapFactory.Instance.CreateMap(mapName, MapType.Map, MapViewingMode.Map, Basemap.None);
            return map;

        }
    }
}
