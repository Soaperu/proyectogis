using Oracle.ManagedDataAccess.Client;
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
            _connectionString = connection;
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
            var connection = new OracleConnection(BuildConnectionString());
            await connection.OpenAsync();
            return connection;
        }

        public bool TestConnection()
        {
            using var connection = new OracleConnection(BuildConnectionString());
            
                connection.Open(); // Intenta abrir la conexión
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); // Cierra la conexión si se abrió exitosamente
                    return true; // Conexión exitosa
                }

            return false;
        }

        private string BuildConnectionString()
        {
            return $"{_connectionString}; User Id{_user}; Password={_password}";
        }
    }
}
