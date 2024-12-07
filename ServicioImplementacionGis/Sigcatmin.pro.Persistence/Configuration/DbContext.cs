using Sigcatmin.prop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Persistence.Configuration
{
    public class DbContext : IDbContext
    {
        private readonly IDbManager _dbManager;

        public DbContext(IDbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<DbConnection> GetConnectionAsync()
        {
            return await _dbManager.GetConnectionAsync();
        }

        public async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType, params DbParameter[] parameters)
        {
            using var connection = await GetConnectionAsync();
            using var command = _dbManager.CreateCommand(connection, query, commandType, parameters);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<DbDataReader> ExecuteReaderAsync(string query, CommandType commandType, params DbParameter[] parameters)
        {
            var connection = await GetConnectionAsync();
            var command = _dbManager.CreateCommand(connection, query, commandType, parameters);
            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }
    }
}
