using ArcGIS.Core.Data;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Domain.Interfaces.Repositories;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Persistence.Repositories
{
    public class GeodatabaseRepository : IGeodatabaseRepository
    {

        private readonly IAuthService _authService;

        public GeodatabaseRepository(IAuthService authService, IOptions<DdConnectionSettings> options)
        {
            _authService = authService;

        }
        public async Task<Geodatabase> ConnectToDatabaseAsync(string instance, string version)
        {
            return await QueuedTask.Run(() =>
              {
                  var userSession = _authService.GetSession();

                  var connectionProperties = new DatabaseConnectionProperties(EnterpriseDatabaseType.Oracle)
                  {
                      AuthenticationMode = AuthenticationMode.DBMS,
                      Instance = instance,
                      User = userSession.UserName,
                      Password = userSession.Password,
                      Version = version
                  };
                  return new Geodatabase(connectionProperties);

              });
        }

        public async Task<FeatureClass> OpenFeatureClassAsync(Geodatabase geodatabase, string featureClassName)
        {
            return await QueuedTask.Run(() => geodatabase.OpenDataset<FeatureClass>(featureClassName));
        }
    }
}
