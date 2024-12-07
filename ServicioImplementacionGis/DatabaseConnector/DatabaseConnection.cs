using ArcGIS.Core.Data;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
//using ArcGIS.Desktop.Framework.Dialogs;

namespace DatabaseConnector
{
    public class DatabaseConnection
    {
        public readonly string _dbconnectionBdgeocat = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.102.0.66)(PORT = 1521)) (CONNECT_DATA = (SERVICE_NAME = BDGEOCAT)));User Id={0};Password={1};";
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

    public class SdeConnectionGIS
    {
        public async Task<Geodatabase> ConnectToOracleGeodatabaseAsync(string instance, string user, string password, string version = "sde.DEFAULT")
        {
            Geodatabase geodatabase = null;

            try
            {
                await QueuedTask.Run(() =>
                {
                    // Configurar las propiedades de conexión
                    var connectionProperties = new DatabaseConnectionProperties(EnterpriseDatabaseType.Oracle)
                    {
                        AuthenticationMode = AuthenticationMode.DBMS,
                        Instance = instance,
                        User = user,
                        Password = password,
                        Version = version
                    };

                    // Crear la conexión a la geodatabase
                    geodatabase = new Geodatabase(connectionProperties);
                });
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                System.Diagnostics.Debug.WriteLine($"Error al conectar a la geodatabase: {ex.Message}"); ;
                throw; // Re-l.anzar la excepción para manejarla en niveles superiores si es necesario
            }

            return geodatabase;
        }
    }

    public static class AppConfig
    {
        public static string userName = "";
        public static string password = "";
        public static string fullUserName = "";
        public static string serviceNameGis = "BDGEOCAT";
        public static string serviceNameOra = "ORACLE9I";
    }
}
