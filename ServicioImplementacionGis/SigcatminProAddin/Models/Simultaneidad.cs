using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnector;
using Oracle.ManagedDataAccess.Client;

namespace SigcatminProAddin.Models
{
    public class Simultaneidad
    {
        private string date;
        private string id;
        private int zone;
        private OracleConnection conn;
        private OracleCommand command;
        private List<string[]> codigous = new List<string[]>();
        private List<string> quads = new List<string>();
        private Dictionary<int, Dictionary<int, object>> simul = new Dictionary<int, Dictionary<int, object>>();
        private DatabaseHandler databaseHandler = new DatabaseHandler();

        public Simultaneidad(string date, string connectionString)
        {
            this.date = date.PadLeft(10, '0');
            this.conn = new OracleConnection(connectionString);
            this.command = conn.CreateCommand();
            this.id = Guid.NewGuid().ToString().Substring(0, 17);
        }

        public void SetZone(int value)
        {
            this.zone = value;
        }

        public void DelPesicu()
        {
            databaseHandler.DeletePesicuSimultaneidad(date);
        }

        public void GetCodigou()
        {
            DataTable dt = databaseHandler.GetCodigouSimultaneidad(date);
            foreach (DataRow row in dt.Rows) 
            {
                codigous.Add(new string[] { row["CG_CODIGO"].ToString(), row["PE_ZONCAT"].ToString() });
            }

            
            {
                throw new Exception("No existen derechos mineros registrados en la fecha indicada");
            }
        }

        public void GetQuadrants()
        {
            var zn = codigous.Where(x => x[1] == zone.ToString()).ToList();
            string sql = string.Join(",", zn.Select(x => $"'{x[0]}'"));

            string query = "DATA_CAT.PACK_DBA_SIMULTANEIDAD.F_GET_RLS_CODIGOU_QUADS";
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Clear();
            command.Parameters.Add("V_SQL", OracleDbType.Varchar2).Value = sql;
            command.Parameters.Add("V_ZONE", OracleDbType.Int32).Value = zone;
            command.Parameters.Add("V_DATE", OracleDbType.Varchar2).Value = date;

            using (OracleDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    quads.Add(reader.GetString(0));
                }
            }
        }

        public void Process(int zone)
        {
            this.SetZone(zone);
            this.GetQuadrants();
            // Implementar lógica adicional de procesamiento
        }

        public void Main()
        {
            try
            {
                this.DelPesicu();
                this.GetCodigou();
                this.Process(17);
                this.Process(18);
                this.Process(19);
                // Llamar otras funciones necesarias
                Console.WriteLine("{\"state\": 1, \"msg\": \"Success\"}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{\"state\": 0, \"msg\": \"" + ex.Message + "\"}");
            }
        }
    }

}
