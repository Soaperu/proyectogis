using Oracle.ManagedDataAccess.Client;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Persistence.Helpers;
using Sigcatmin.prop.Domain.Interfaces.Repositories;
using Sigcatmin.prop.Domain.Settings;
using System.Data;
using System.Reflection.Metadata;

namespace Sigcatmin.pro.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbManagerFactory _dbManagerFactory;
        private readonly DdConnectionSettings _DbConnectionSettings;
        public AuthRepository(
            IDbManagerFactory dbManagerFactory,
            IOptions<DdConnectionSettings> options)
        {
            _dbManagerFactory = dbManagerFactory;
            _DbConnectionSettings = options.Value;
        }
        public async ValueTask<bool> Authenticate(string user, string password)
        {
            try
            {
                string _connectionString = ConnectionHelper.BuildConnectionString(
                    _DbConnectionSettings.Oracle,
                    user,
                    password);

                using var connection = new OracleConnection(_connectionString);

                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    return true;
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error de conexión Oracle: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            return false;
        }
    }
}
