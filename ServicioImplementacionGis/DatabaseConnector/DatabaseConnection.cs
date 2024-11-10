using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseConnector
{
    public class DatabaseConnection
    {
        public readonly string _dbconnectionBdgeocat = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.102.0.67)(PORT = 1521)) (CONNECT_DATA = (SERVICE_NAME = ORACLE)));User Id={0};Password={1};";
        public readonly string _dbconnectionOracle = "Data Source = (DESCRIPTION=(FAILOVER=ON)(LOAD_BALANCE=OFF)(ADDRESS_LIST = (ADDRESS = (PROTOCOL=TCP)(HOST=10.102.0.67) (PORT=1521))(ADDRESS = (PROTOCOL=TCP)(HOST=10.102.0.206)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=ORACLE9I)));User Id={0};Password={1};";

        public readonly string _username;
        public readonly string _password;

        public DatabaseConnection(string username, string password)
        {
            //_dbconnection= "User Id=PAVA2778;Password=ingemmet;Data Source=ORACLE;Connection Timeout=60;";
            _username = username;
            _password = password;
        }
        public string OracleConnectionString()
        {
            //_dbconnection = "User Id=PAVA2778;Password=ingemmet;Data Source=ORACLE;Connection Timeout=60;";
            return String.Format(_dbconnectionOracle, _username, _password); ;
        }

        public string GisConnectionString()
        {
            return String.Format(_dbconnectionBdgeocat, _username, _password);
        }

        public bool TestConnection(string connectionString)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Conexión fallida: " + ex.Message);
                return false;
            }
        }
    }
    public static class AppConfig
    {
        public static string userName = "";
        public static string password = "";
    }
}
