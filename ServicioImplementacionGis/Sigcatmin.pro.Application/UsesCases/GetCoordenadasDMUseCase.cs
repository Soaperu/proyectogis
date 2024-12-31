using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Domain.Interfaces.Repositories;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class GetCoordenadasDMUseCase
    {
        private readonly IEvaluacionGISRepository _evaluacionGISRepository;
        private readonly ILoggerService _loggerService;

        public GetCoordenadasDMUseCase(IEvaluacionGISRepository evaluacionGISRepository,
            ILoggerService loggerService)
        {
            _evaluacionGISRepository = evaluacionGISRepository;
            _loggerService = loggerService;
        }

        public async Task<IEnumerable<CoordenadaDto>> Execute(string code)
        {
            try
            {
                return await _evaluacionGISRepository.GetCoordenadasDMGS84ByCode(code);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                throw;
            }
        }
    }
}
