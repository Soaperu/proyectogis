using System.Data;
using System.Data.Common;

namespace Sigcatmin.prop.Domain.Interfaces.Repositories
{
    public interface IDbContext
    {
        Task<DbConnection> GetConnectionAsync();
        Task<int> ExecuteNonQueryAsync(string query, CommandType commandType, params DbParameter[] parameters);
        Task<DbDataReader> ExecuteReaderAsync(string query, CommandType commandType, params DbParameter[] parameters);
    }
}
