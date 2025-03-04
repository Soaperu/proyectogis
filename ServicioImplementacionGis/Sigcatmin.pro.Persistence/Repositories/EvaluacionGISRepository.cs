﻿using Dapper;
using Dapper.Oracle;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Domain.Interfaces.Repositories;
using Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration;
using Sigcatmin.pro.Persistence.Constants;
using Sigcatmin.prop.Domain.Settings;
using System.Data;

namespace Sigcatmin.pro.Persistence.Repositories
{
    public class EvaluacionGISRepository : IEvaluacionGISRepository
    {
        private readonly DdConnectionSettings _DdConnectionSettings;
        private readonly IDbManager _dbManager;
        public EvaluacionGISRepository(IOptions<DdConnectionSettings> options, IDbManager dbManager)
        {
            _DdConnectionSettings = options.Value;
            _dbManager = dbManager;
        }
        public async ValueTask<int> CountRecordsAsync(string type, string search)
        {
            var parameters = new OracleDynamicParameters();
            parameters.Add("V_TIPO", type, OracleMappingType.Varchar2, ParameterDirection.Input, size: 1);
            parameters.Add("V_BUSCA", search, OracleMappingType.Varchar2, ParameterDirection.Input, size: 13);
            parameters.Add("V_RETORNO", null, OracleMappingType.Varchar2, ParameterDirection.Output, size: 500);


            using var connection = await _dbManager.GetConnectionAsync(_DdConnectionSettings.Oracle);

            await connection.ExecuteAsync(
                EvaluacionGISProcedures.ProcedureCuentaRegistros,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            var outputValue = parameters.Get<string>("V_RETORNO");
            
            return int.Parse(outputValue);
        }
        public async ValueTask<IEnumerable<DerechoMineroDto>> GetDerechosMinerosUniqueAsync(string code, int type)
        {

            var parameters = new OracleDynamicParameters();
            parameters.Add("V_CODIGO", code, OracleMappingType.Varchar2, ParameterDirection.Input, size: 13);
            parameters.Add("V_TIPO", type, OracleMappingType.Varchar2, ParameterDirection.Input, size: 1);
            parameters.Add("VO_CURSOR", null, OracleMappingType.RefCursor, ParameterDirection.Output);

            using var connection = await _dbManager.GetConnectionAsync(_DdConnectionSettings.Oracle);

           var result = await connection.QueryAsync<DerechoMineroDto>(
                EvaluacionGISProcedures.GetDerechoMineroUnique,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async ValueTask<IEnumerable<CoordinateDto>> GetcoordinatesByCodeAsync(string code)
        {
            var parameters = new OracleDynamicParameters();
            parameters.Add("V_CODIGO", code, OracleMappingType.Varchar2, ParameterDirection.Input, size: 12);
            parameters.Add("VO_CURSOR", null, OracleMappingType.RefCursor, ParameterDirection.Output);

            using var connection = await _dbManager.GetConnectionAsync(_DdConnectionSettings.Oracle);

            return await connection.QueryAsync<CoordinateDto>(
               EvaluacionGISProcedures.GetWGS84Oficial,
               parameters,
               commandType: CommandType.StoredProcedure
           );
        }
    }
}
