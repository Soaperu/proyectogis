using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Oracle;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Domain.Interfaces.Repositories;
using Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration;
using Sigcatmin.pro.Persistence.Constants;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Persistence.Repositories
{
    public class DerechoMineroRepository : IDerechoMineroRepository
    {
        private readonly DdConnectionSettings _DdConnectionSettings;
        private readonly IDbManager _dbManager;
        public DerechoMineroRepository(IOptions<DdConnectionSettings> options, IDbManager dbManager)
        {
            _DdConnectionSettings = options.Value;
            _dbManager = dbManager;
        }
        public async Task<string> VerifyDatum()
        {
            var parameters = new OracleDynamicParameters();
            parameters.Add("VO_VA_RETORNO", null, OracleMappingType.Varchar2, ParameterDirection.Output, size: 500);

            using var connection = await _dbManager.GetConnectionAsync(_DdConnectionSettings.Oracle);

            await connection.ExecuteAsync(
                 EvaluacionGISProcedures.ProcedureCuentaRegistros,
                 parameters,
                 commandType: CommandType.StoredProcedure
             );

            var outputValue = parameters.Get<string>("VO_VA_RETORNO");

            return outputValue;


        }
    }
}
