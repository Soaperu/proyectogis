using System.Data;
using System.Data.Common;

namespace Sigcatmin.pro.Domain.Interfaces.Repositories.Configuration
{
    public interface IDbManager
    {
        Task<DbConnection> GetConnectionAsync(string connectionString);
        //DbCommand CreateCommand(DbConnection connection, string query, CommandType commandType, params DbParameter[] parameters);
    }
}
