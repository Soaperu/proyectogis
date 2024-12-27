using Oracle.ManagedDataAccess.Client;
using Sigcatmin.pro.Persistence.Helpers;
using Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration;
using System.Data;
using System.Data.Common;
using Sigcatmin.prop.Domain.Interfaces.Services;

namespace Sigcatmin.pro.Persistence.Configuration
{
    public class DbManager: IDbManager
    {
        private readonly IUserSessionService _userSessionService;
        public DbManager(IUserSessionService userSessionService)
        {
            _userSessionService  = userSessionService;
        }

        public async Task<DbConnection> GetConnectionAsync(string connectionString)
        {
            var userSession =  _userSessionService.GetUserSession();

            string connectionComplete = ConnectionHelper.BuildConnectionString(connectionString,
                userSession.UserName,
                userSession.Password);

            var connection = new OracleConnection(connectionComplete);
            await connection.OpenAsync();
            return connection;
        }

    }
}
