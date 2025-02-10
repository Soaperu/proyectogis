//using ArcGIS.Desktop.Mapping;

using ArcGIS.Desktop.Mapping;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IMapService
    {
        void CreateMap(string mapName);
        Map FindMapByName(string mapName);
        void ActivateMap(Map map);
    }
}
