using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.Managers;
using Sigcatmin.pro.Application.Contracts.Constants;
using Sigcatmin.pro.Application.Contracts.Enums;
using Sigcatmin.pro.Application.Contracts.Requests;
using Sigcatmin.pro.Domain.Interfaces.Repositories;
using Sigcatmin.pro.Domain.Settings;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.Data;

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
        private readonly IFeatureClassNameResolver _featureClassNameResolver;
        private readonly GeoDatabaseSettings _geoDatabaseSettings;
        public GraficarDerechoMineroUseCase(
            IOptions<GeoDatabaseSettings> geoDatabaseSettings,
            IMapManager mapManager,
            IMapService mapService,
            ICacheService cacheService,
            IGeodatabaseRepository geodatabaseRepository,
            IDerechoMineroRepository derechoMineroRepository,
            IFeatureLayerServiceFactory featureLayerServiceFactory,
            IFeatureClassNameResolver featureClassNameResolver)
        {
            _mapManager = mapManager;
            _mapService = mapService;
            _cacheService = cacheService;
            _geodatabaseRepository = geodatabaseRepository;
            _geoDatabaseSettings = geoDatabaseSettings.Value;
            _derechoMineroRepository = derechoMineroRepository;
            _featureLayerServiceFactory = featureLayerServiceFactory;
            _featureClassNameResolver = featureClassNameResolver;
        }

        public async Task Execute(GraficarDerechoMineroRequest request)
        {
            _mapService.CreateMap(request.MapName);
            Map activeMap = await _mapManager.EnsureMapViewIsActiveAsync(request.MapName);

            var geodatabase = await _geodatabaseRepository.ConnectToDatabaseAsync(
                _geoDatabaseSettings.Instance,
                _geoDatabaseSettings.Version);

            //string zonaDM = await _derechoMineroRepository.VerifyDatumAsync(request.Codigo);

            IFeatureLayerService featureLayerService = _featureLayerServiceFactory.CreateFeatureLayerService(geodatabase, activeMap, request.Zona, "99");

             await featureLayerService.LoadFeatureLayerAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + request.Zona, false);

            SetCacheValues(request);

        }

        private void SetCacheValues(GraficarDerechoMineroRequest request)
        {
            _cacheService.SetValue(CacheKeysEnum.StateDmY, request.IsDMGraphVisible);
            _cacheService.SetValue(CacheKeysEnum.CurrentCodeDerechoMinero, request.Codigo);
            _cacheService.SetValue(CacheKeysEnum.CurrentZone, request.Zona);
        }
    }
}
