using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;


namespace Sigcatmin.pro.Shared.Implements
{
    public class MapService : IMapService
    {
        //public Map CreateMap(string mapName)
        //{
        //    Map map = MapFactory.Instance.CreateMap(mapName, MapType.Map, MapViewingMode.Map, Basemap.None);
        //    return map;
        //}
        //public Map FindMapByName(string mapName)
        //{
        //    var allMaps = Project.Current.GetItems<MapProjectItem>();

        //    // Busca el mapa con el nombre especificado
        //    var mapItem = allMaps.FirstOrDefault(m => m.Name.Equals(mapName, StringComparison.OrdinalIgnoreCase));

        //    // Si se encuentra, se devuelve como objeto Map
        //    return mapItem != null ? mapItem.GetMap() : null;
        //}
        //public void ActivateMap(Map map)
        //{
        //    var mapProjItem = Project.Current.GetItems<MapProjectItem>().FirstOrDefault(mpi => mpi.Name == map.Name);
        //    if (mapProjItem != null)
        //    {
        //        // Activa el mapa en la vista
        //        //MappingModule.ActiveMapView = MapView.Active;
        //        var mapPane = ProApp.Panes.OfType<IMapPane>().FirstOrDefault(mPane => (mPane as Pane).ContentID == mapProjItem.Path);
        //        if (mapPane != null)
        //        {
        //            var pane = mapPane as Pane;
        //            pane.Activate();
        //        }
        //    }
        //}
    }
}
