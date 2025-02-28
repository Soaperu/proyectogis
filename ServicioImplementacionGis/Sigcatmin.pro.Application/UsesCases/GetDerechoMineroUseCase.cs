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
    public class GetDerechoMineroUseCase
    {
        private readonly IEvaluacionGISRepository _evaluacionGISRepository;
        private readonly ILoggerService _loggerService;
        public GetDerechoMineroUseCase(IEvaluacionGISRepository evaluacionGISRepository,
            ILoggerService loggerService)
        {
            _evaluacionGISRepository = evaluacionGISRepository;
            _loggerService = loggerService;
        }

        public async Task<IEnumerable<DerechoMineroDto>> Execute(string code, int type)
        {
            try
            {
                return await _evaluacionGISRepository.GetDerechosMinerosUniqueAsync(code, type);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                throw;
            }
        }
    }
}
