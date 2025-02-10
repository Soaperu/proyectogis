using Oracle.ManagedDataAccess.Client;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration;
using Sigcatmin.pro.Persistence.Helpers;
using Sigcatmin.prop.Domain.Interfaces.Repositories;
using Sigcatmin.prop.Domain.Settings;
using System.Data;
using System.Reflection.Metadata;

namespace Sigcatmin.pro.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbManager _dbManager;
        private readonly DdConnectionSettings _DbConnectionSettings;
        public AuthRepository(
            //IDbManager dbManager,
            IOptions<DdConnectionSettings> options)
        {
            _DbConnectionSettings = options.Value;
        }
        public async ValueTask<bool> AuthenticateAsync(string user, string password)
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
