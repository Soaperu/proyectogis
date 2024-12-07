using System.Data;
using System.Data.Common;

namespace Sigcatmin.prop.Domain.Interfaces.Repositories
{
    public interface IDbManager
    {
        Task<DbConnection> GetConnectionAsync();
        DbCommand CreateCommand(DbConnection connection, string query, CommandType commandType, params DbParameter[] parameters);
        bool TestConnection();
    }
}
