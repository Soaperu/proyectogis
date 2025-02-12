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
        private List<string> letters = new List<string>();


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

        public void PrepareData()
        {
            // Obtiene la relación de cuadriculas únicas
            var keysAll = quads.Select(x => x[0]).Distinct().ToList(); // Ejemplo: ["15-F_1137", "15-F_1138", "15-F_1139"]

            // Obtiene la relación de cuadriculas con todos los derechos traslapados entre sí
            var rlsTmp = keysAll.ToDictionary(
                key => key,
                key => quads.Where(n => n[0] == key).Select(i => i[1]).ToList()
            );

            // Obtiene aquellas cuadriculas que se intersectan con más de un derecho
            rls = rlsTmp
                 .Where(kv => kv.Value.Count > 1)
                 .ToDictionary(
                     kv => kv.Key,
                     kv => kv.Value.OrderBy(v => v).ToList() // Ahora se almacena como List<string>
                 );

            // Obtiene las agrupaciones únicas de derechos en 'rls'
            codes = rls.Values.Distinct().ToList(); // Ejemplo: [("010009218", "010016318"), ("010009318", "010016218")]

            // Generar la lista de letras (A-Z, luego combinaciones AA, AB, etc.)
            letters = Enumerable.Range(0, 26).Select(i => ((char)('A' + i)).ToString()).ToList();

            var extendedLetters = letters
                .Take(4)
                .SelectMany(x => letters, (x, i) => x + i)
                .ToList();

            letters.AddRange(extendedLetters);
        }

        public void GetGroups()
        {
            List<List<string>> groupsTmp  = codes.ToList();

            // Se realiza una iteración sobre todos los valores únicos de derechos
            foreach (var n in groupsTmp.SelectMany(x => x).Distinct().ToList())
            {
                // Se obtiene una lista de todas las agrupaciones que contienen el derecho actual
                var components = groupsTmp.Where(x => x.Contains(n)).ToList();

                // Se eliminan las agrupaciones de la lista principal
                foreach (var group in components)
                {
                    groupsTmp.Remove(group);
                }

                // Se agregan a la lista principal todos los valores únicos de la lista "components"
                groupsTmp.Add(components.SelectMany(x => x).Distinct().ToList());
            }
        }

        public void GetSoubgroups()
        {

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
