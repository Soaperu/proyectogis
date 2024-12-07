using Oracle.ManagedDataAccess.Client;
using Sigcatmin.pro.Persistence.Helpers;
using Sigcatmin.prop.Domain.Interfaces.Repositories;
using System.Data;
using System.Data.Common;

namespace Sigcatmin.pro.Persistence.Configuration
{
    public class DbManager: IDbManager
    {
        private readonly string _connectionString;
        private readonly string _user;
        private readonly string _password;
        public DbManager(string connection, string user, string password)
        {
            _connectionString = ConnectionHelper.BuildConnectionString(connection, user, password);
            _user = user;
            _password = password;
        }
        public DbCommand CreateCommand(DbConnection connection, string query, CommandType commandType, params DbParameter[] parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = commandType;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<DbConnection> GetConnectionAsync()
        {
            var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

    }
}
