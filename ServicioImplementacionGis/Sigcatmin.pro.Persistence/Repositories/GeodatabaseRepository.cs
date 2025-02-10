using Sigcatmin.pro.Domain.Interfaces.Repositories;

namespace Sigcatmin.pro.Persistence.Repositories
{
    public class GeodatabaseRepository : IGeodatabaseRepository
    {

        //private readonly IAuthService _authService;

        //public GeodatabaseRepository(IAuthService authService, IOptions<DdConnectionSettings> options)
        //{
        //    _authService = authService;

        //}

        //public async Task<Geodatabase> ConnectToDatabaseAsync(string instance, string version)
        //{
        //    return await Task.Run(() =>
        //      {
        //          var userSession = _authService.GetSession();

        //          var connectionProperties = new DatabaseConnectionProperties(EnterpriseDatabaseType.Oracle)
        //          {
        //              AuthenticationMode = AuthenticationMode.DBMS,
        //              Instance = instance,
        //              User = userSession.UserName,
        //              Password = userSession.Password,
        //              Version = version
        //          };
        //          return new Geodatabase(connectionProperties);

        //      });
        //}
    }
}
