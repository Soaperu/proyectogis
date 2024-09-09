using Oracle.ManagedDataAccess.EntityFramework;
using System.Data.Entity;

namespace Automapic.Service.Data
{
    public class OracleDbConfiguration : DbConfiguration
    {
        public OracleDbConfiguration()
        {
            SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
        }
    }
}
