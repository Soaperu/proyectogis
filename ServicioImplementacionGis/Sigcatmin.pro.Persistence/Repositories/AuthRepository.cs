using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.prop.Domain.Interfaces.Repositories;
using Sigcatmin.prop.Domain.Settings;

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
        public bool Authenticate(string user, string password)
        {
           var dbManager = _dbManagerFactory.CreateDbManager(_DbConnectionSettings.Oracle);
            return dbManager.TestConnection();
        }
    }
}
