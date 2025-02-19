
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
        private readonly IDerechoMineroRepository _derechoMineroRepository;
        private readonly IFeatureLayerServiceFactory _featureLayerServiceFactory;
        private readonly GeoDatabaseSettings _geoDatabaseSettings;
        public GraficarDerechoMineroUseCase(
            IOptions<GeoDatabaseSettings> geoDatabaseSettings,
            IMapManager mapManager, 
            IMapService mapService,
            ICacheService cacheService,
            IGeodatabaseRepository geodatabaseRepository,
            IDerechoMineroRepository derechoMineroRepository,
            IFeatureLayerServiceFactory featureLayerServiceFactory)
        {
            _mapManager = mapManager;
            _mapService = mapService;
            _cacheService = cacheService;
            _geodatabaseRepository = geodatabaseRepository;
            _geoDatabaseSettings = geoDatabaseSettings.Value;
            _derechoMineroRepository = derechoMineroRepository;
            _featureLayerServiceFactory = featureLayerServiceFactory;
        }

        public async Task Execute(GraficarDerechoMineroDto request)
        {
             _mapService.CreateMap(request.MapName);
             Map activeMap = await _mapManager.EnsureMapViewIsActiveAsync(request.MapName);
             var geodatabase  = await _geodatabaseRepository.ConnectToDatabaseAsync(
                 _geoDatabaseSettings.Instance, 
                 _geoDatabaseSettings.Version);

             string zonaDM = await _derechoMineroRepository.VerifyDatumAsync(request.Codigo);

            IFeatureLayerService featureLayerService = _featureLayerServiceFactory.CreateFeatureLayerService(geodatabase, activeMap);

            await featureLayerService.LoadFeatureLayerAsync($"DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_" + request.Zona, false);

            SetCacheValues(request);

        }

        private void SetCacheValues(GraficarDerechoMineroDto request)
        {
            _cacheService.SetValue(CacheKeysEnum.StateDmY, request.IsDMGraphVisible);
            _cacheService.SetValue(CacheKeysEnum.CurrentCodeDerechoMinero, request.Codigo);
            _cacheService.SetValue(CacheKeysEnum.CurrentZone, request.Zona);
        }
    }
}
