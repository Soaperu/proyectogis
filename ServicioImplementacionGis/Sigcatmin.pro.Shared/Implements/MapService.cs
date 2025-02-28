using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;


namespace Sigcatmin.pro.Shared.Implements
{
    public class MapService : IMapService
    {
        public async void CreateMap(string mapName)
        {
            await QueuedTask.Run(() =>
            {
                Map map = MapFactory.Instance.CreateMap(mapName, MapType.Map, MapViewingMode.Map, Basemap.None);
                ProApp.Panes.CreateMapPaneAsync(map);

            });
        }
        public async Task<Map> FindMapByName(string mapName)
        {
            Map resultMap = null;
            await QueuedTask.Run(() =>
                {
                    var allMaps = Project.Current.GetItems<MapProjectItem>();
                    // Busca el mapa con el nombre especificado
                    var mapItem = allMaps.FirstOrDefault(m => m.Name.Equals(mapName, StringComparison.OrdinalIgnoreCase));
                    // Si se encuentra, se devuelve como objeto Map
                    if (mapItem != null)
                    {
                        resultMap = mapItem.GetMap();
                    }
                });
            return resultMap;
        }
        public void ActivateMap(Map map)
        {
            var mapProjItem = Project.Current.GetItems<MapProjectItem>().FirstOrDefault(mpi => mpi.Name == map.Name);
            if (mapProjItem != null)
            {
                // Activa el mapa en la vista
                //MappingModule.ActiveMapView = MapView.Active;
                var mapPane = ProApp.Panes.OfType<IMapPane>().FirstOrDefault(mPane => (mPane as Pane).ContentID == mapProjItem.Path);
                if (mapPane != null)
                {
                    var pane = mapPane as Pane;
                    pane.Activate();
                }
            }
        }
    }
}
