using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Domain.Interfaces.Repositories;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class CountRowsGISUseCase
    {
        private IEvaluacionGISRepository _evaluacionGISRepository;
        private readonly ILoggerService _loggerService;
        public CountRowsGISUseCase(
            IEvaluacionGISRepository evaluacionGISRepository,
            ILoggerService loggerService)
        {
            _evaluacionGISRepository = evaluacionGISRepository;
            _loggerService = loggerService;
         }

        public async ValueTask<int> Execute(string type, string search)
        {
            try
            {
               return await _evaluacionGISRepository.CountRecords(type, search);
            }catch (Exception ex) {
                _loggerService.LogError(ex);
                throw;
            }
            
        }

    }
}
