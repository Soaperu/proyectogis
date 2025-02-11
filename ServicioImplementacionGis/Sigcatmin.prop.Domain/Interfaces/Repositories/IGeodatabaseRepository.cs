//using ArcGIS.Core.Data;

using ArcGIS.Core.Data;

namespace Sigcatmin.pro.Domain.Interfaces.Repositories
{
    public interface IGeodatabaseRepository
    {
        Task<Geodatabase> ConnectToDatabaseAsync(string instance, string version);
    }
}
