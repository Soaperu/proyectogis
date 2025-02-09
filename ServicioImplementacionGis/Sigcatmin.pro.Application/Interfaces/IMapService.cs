using ArcGIS.Desktop.Mapping;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IMapService
    {
        Task<Map> CreateMapAsync(string mapName);
    }
}
