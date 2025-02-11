
using System.Runtime.CompilerServices;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Dtos.Request;
using Sigcatmin.pro.Application.Enums;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.Managers;
using Sigcatmin.pro.Domain.Interfaces.Repositories;
using Sigcatmin.pro.Domain.Settings;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class GraficarDerechoMineroUseCase
    {
        private readonly IMapManager _mapManager;
        private readonly IMapService _mapService;
        private readonly ICacheService _cacheService;
        private readonly IGeodatabaseRepository _geodatabaseRepository;
        private readonly GeoDatabaseSettings _geoDatabaseSettings;
        public GraficarDerechoMineroUseCase(
            IOptions<GeoDatabaseSettings> geoDatabaseSettings,
            IMapManager mapManager, 
            IMapService mapService,
            ICacheService cacheService,
            IGeodatabaseRepository geodatabaseRepository)
        {
            _mapManager = mapManager;
            _mapService = mapService;
            _cacheService = cacheService;
            _geodatabaseRepository = geodatabaseRepository;
            _geoDatabaseSettings = geoDatabaseSettings.Value;
        }

        public async Task Execute(GraficarDerechoMineroDto request)
        {
             _mapService.CreateMap(request.MapName);
             Map activeMap = await _mapManager.EnsureMapViewIsActiveAsync(request.MapName);

            _cacheService.SetValue(CacheKeysEnum.StateDmY, request.IsDMGraphVisible);
            _cacheService.SetValue(CacheKeysEnum.CurrentCodeDerechoMinero, request.Codigo);
            _cacheService.SetValue(CacheKeysEnum.CurrentZone, request.Zona);

            var geodatabase  = await _geodatabaseRepository.ConnectToDatabaseAsync(
                _geoDatabaseSettings.Instance,
               "sde.DEFAULT");

        }
    }
}
