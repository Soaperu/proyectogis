using AutoMapper;
using Sigcatmin.pro.Application.Dtos.Response;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Domain.Interfaces.Repositories;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class GetCoordenadasDMUseCase
    {
        private readonly IEvaluacionGISRepository _evaluacionGISRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public GetCoordenadasDMUseCase(IEvaluacionGISRepository evaluacionGISRepository,
            ILoggerService loggerService,
            IMapper mapper)
        {
            _evaluacionGISRepository = evaluacionGISRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CoordinatedResponseDto>> Execute(string code, int typeSystem)
        {
            try
            {
                var coordinates = await _evaluacionGISRepository.GetcoordinatesByCodeAsync(code);
               return _mapper.Map<IEnumerable<CoordinatedResponseDto>>(coordinates);

            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                throw;
            }
        }
    }
}
