using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnector;
using Oracle.ManagedDataAccess.Client;

namespace SigcatminProAddin.Models
{
    public class Simultaneidad
    {
        private string date;
        private List<string[]> codigous = new List<string[]>();
        private List<string[]> quads = new List<string[]>();
        private string mainCode = "#";
        private List<List<string>> codes;
        private Dictionary<string, List<string>> rls = new Dictionary<string, List<string>>();
        private List<List<string>> subgroups;
        private Dictionary<int, Dictionary<int, object>> simul = new Dictionary<int, Dictionary<int, object>>();
        private int zone;
        private Dictionary<string, List<Tuple<double, double>>> rows = new Dictionary<string, List<Tuple<double, double>>>();
        private int num_group = 1;
        private string id;
        private string tipo = "s";
        
        private DatabaseHandler databaseHandler = new DatabaseHandler();

        public Simultaneidad(string fecha)
        {
            date = fecha.PadLeft(10, '0');
            id = Guid.NewGuid().ToString().Substring(0, 17);
        }

        public void SetZone(int value)
        {
            zone = value;
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
            simul[zone] = new Dictionary<int, object>();

            DataTable dt = databaseHandler.GetCodigoQuadsSimultaneidad(sql, zone.ToString(), date );
            foreach (DataRow row in dt.Rows)
            {
                quads.Add(new string[] { row["CD_CUAD"].ToString(), row["CODIGOU"].ToString() });
            }
                        
        }

        public void Process(int zone)
        {
            SetZone(zone);
            GetQuadrants();
            PrepareData();
            GetGroups();
            GetSubgroups(InsertDataToDataBase);
            // Implementar lógica adicional de procesamiento
        }

        public void Main()
        {
            try
            {
                DelPesicu();
                GetCodigou();
                Process(17);
                Process(18);
                Process(19);
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
