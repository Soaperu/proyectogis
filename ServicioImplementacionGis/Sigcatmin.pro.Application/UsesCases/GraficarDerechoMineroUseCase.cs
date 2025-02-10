
using Sigcatmin.pro.Application.Dtos.Request;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.Managers;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class GraficarDerechoMineroUseCase
    {
        private readonly IMapManager _mapManager;
        private readonly IMapService _mapService;
        public GraficarDerechoMineroUseCase(IMapManager mapManager, IMapService mapService)
        {
            _mapManager = mapManager;
            _mapService = mapService;
        }

        public async Task Execute(GraficarDerechoMineroDto request)
        {
            //_mapService.CreateMap(request.MapName); 
            //Map activeMap = await _mapManager.EnsureMapViewIsActiveAsync(request.MapName);
        }
    }
}
