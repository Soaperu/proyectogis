using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.Managers;

namespace Sigcatmin.pro.Shared.Implements
{
    public class MapManager: IMapManager
    {
        private readonly IMapService _mapService;
        private readonly IMapViewService _mapViewService;

        public MapManager(IMapService mapService, IMapViewService mapViewService)
        {
            _mapService = mapService;
            _mapViewService = mapViewService;
        }

        public async Task<Map> EnsureMapViewIsActiveAsync(string mapName)
        {
            if (_mapViewService.ActiveMapView != null)
            {
                return _mapViewService.ActiveMapView.Map;
            }

            var tcs = new TaskCompletionSource<Map>();
            SubscriptionToken eventToken = null;

            eventToken = _mapViewService.SubscribeToDrawCompleteEvent(async args =>
            {
                if (eventToken != null)
                {
                    _mapViewService.UnsubscribeFromDrawCompleteEvent(eventToken);
                }

                Map map = await _mapService.FindMapByName(mapName);
                _mapService.ActivateMap(map);

                tcs.SetResult(map);
            });

            return await tcs.Task;
        }
    }
}
