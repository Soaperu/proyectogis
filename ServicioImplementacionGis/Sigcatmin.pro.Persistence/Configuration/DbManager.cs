using Oracle.ManagedDataAccess.Client;
using Sigcatmin.pro.Persistence.Helpers;
using Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration;
using System.Data;
using System.Data.Common;
using Sigcatmin.prop.Domain.Interfaces.Services;
using Sigcatmin.pro.Application.Interfaces;

namespace Sigcatmin.pro.Persistence.Configuration
{
    public class DbManager: IDbManager
    {
        private readonly IAuthService _userSessionService;
        public DbManager(IAuthService userSessionService)
        {
            _userSessionService  = userSessionService;
        }

        public async Task<DbConnection> GetConnectionAsync(string connectionString)
        {
            var userSession =  _userSessionService.GetSession();

            string connectionComplete = ConnectionHelper.BuildConnectionString(connectionString,
                userSession.UserName,
                userSession.Password);

            var connection = new OracleConnection(connectionComplete);
            await connection.OpenAsync();
            return connection;
        }

    }
}
