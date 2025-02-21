using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
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
        private List<List<object>> subgroups;
        private Dictionary<int, Dictionary<string, object>> simul = new Dictionary<int, Dictionary<string, object>>();
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
            simul[zone] = new Dictionary<string, object>();

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

        public void GetSubgroups()
        {
            List<List<object>> subgroupsTemp = codes
                .Select(x => new List<object>
                {
                    new List<string>(x), // Convierte la lista interna
                    rls.Where(kv => kv.Value.SequenceEqual(x)) // Filtra `rls` donde el valor es igual a `x`
                       .Select(kv => kv.Key) // Obtiene solo las claves
                       .ToList()
                })
                .ToList();
            GetRows(subgroupsTemp);
            ReviewSimult(subgroupsTemp);
            this.subgroups.Sort();


            if (simul.ContainsKey(zone))
            {
                foreach (var kv in simul[zone])
                {
                    string k = kv.Key;
                    Dictionary<string, object> v = (Dictionary<string, object>) kv.Value;

                    int n = 0; // Contador de subgrupos

                    foreach (var i in subgroups)
                    {
                        List<string> codigouList = (List<string>)i[0];
                        List<string> cuadriculas = (List<string>)i[1];

                        // Verificar si el primer elemento de codigouList está en 'codigou' del diccionario simul
                        if (v.ContainsKey("codigou") && ((List<string>)v["codigou"]).Contains(codigouList[0]))
                        {
                            foreach (var codigou in codigouList)
                            {
                                foreach (var codicua in cuadriculas)
                                {
                                    InsertDataToDatabase(codigou, codicua, codicua, Int32.Parse(k), n + 1, date, zone.ToString(), letters[n]);
                                }
                            }
                            n++;
                        }
                    }
                }
            }
        }


        public void GetRows(List<List<object>> subgroups)
        {
            // Obtener los quads (segundo elemento de cada subgrupo)
            List<string> quads = subgroups
                .SelectMany(n => (List<string>)n[1]) // Extrae la lista de códigos de cuadriculas
                .ToList();

            if (quads.Count == 0) return;

            // Construir la consulta SQL con los códigos de cuadriculas
            string sql = string.Join(",", quads.Select(q => $"'{q}'"));

            try
            {
                // Llamar al procedimiento almacenado y obtener el resultado como DataTable
                DataTable dt = databaseHandler.ObtenerCoordenadasdeQuads(sql, zone.ToString(), date);

                // Procesar los resultados
                foreach (DataRow row in dt.Rows)
                {
                    string key = row["CD_CUAD"].ToString();  // Primera columna: código de la cuadrícula
                    string coordsStr = row["COORDS"].ToString(); // Segunda columna: coordenadas en formato string

                    // Extraer coordenadas con regex y convertirlas en tuplas (X, Y)
                    List<Tuple<double, double>> coordList = Regex.Matches(coordsStr, @"\d+\.\d+")
                        .Cast<Match>()
                        .Select(m => double.Parse(m.Value, CultureInfo.InvariantCulture))
                        .Select((value, index) => new { value, index })
                        .GroupBy(x => x.index / 2)
                        .Select(g => Tuple.Create(g.ElementAt(0).value, g.ElementAt(1).value))
                        .ToList();

                    rows[key] = coordList;
                }

                // Generar `rows` con las coordenadas ordenadas en pares consecutivos
                rows = rows.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value
                        .Select((v, i) => i <= 3 ? Tuple.Create(Math.Min(v.Item1, v.Item2), Math.Max(v.Item1, v.Item2)) : null)
                        .Where(t => t != null)
                        .ToList()
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetRows: {ex.Message}");
            }
        }

        public void ReviewSimult(List<List<object>> subgroups)
        {
            try
            {
                List<List<object>> newSubgroups = new List<List<object>>();

                foreach (var i in subgroups)
                {
                    // Obtener las coordenadas de los quads del subgrupo actual
                    Dictionary<string, List<Tuple<double, double>>> sub =
                        ((List<string>)i[1]) // Convertir el segundo elemento del subgrupo a lista de strings (quads)
                        .Where(x => rows.ContainsKey(x)) // Filtrar solo las quads que existen en `rows`
                        .ToDictionary(x => x, x => rows[x]); // Crear un diccionario con las coordenadas de cada quad

                    // Analizar las cuadriculas para detectar no adyacentes o intersección en un solo vértice
                    List<List<string>> gp = Analysis(sub);

                    if (gp.Count > 0)
                    {
                        // Si hay subgrupos detectados, los añadimos con la referencia del primer elemento de `i`
                        foreach (var x in gp)
                        {
                            newSubgroups.Add(new List<object> { i[0], x });
                        }
                    }
                    else
                    {
                        // Si no hay cambios, se agrega el subgrupo original
                        newSubgroups.Add(i);
                    }
                }

                // Asignar la nueva lista de subgrupos a la propiedad
                this.subgroups = newSubgroups;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ReviewSimult: {ex.Message}");
            }
        }

        private List<List<string>> Analysis(Dictionary<string, List<Tuple<double, double>>> subgroups)
        {
            List<List<string>> gp = new List<List<string>>();

            // Obtener las listas de coordenadas asociadas a cada cuadricula
            List<List<Tuple<double, double>>> coordsForQuads = subgroups.Values.ToList();

            // Unir todas las coordenadas en un conjunto único
            HashSet<Tuple<double, double>> coordsSummary = new HashSet<Tuple<double, double>>(
                coordsForQuads.SelectMany(x => x)
            );

            // Procesar cada coordenada única
            foreach (var each in coordsSummary)
            {
                // Filtrar los elementos de `coordsForQuads` que contienen `each`
                List<List<Tuple<double, double>>> components = coordsForQuads
                    .Where(x => x.Contains(each))
                    .ToList();

                // Remover los elementos originales de la lista
                foreach (var i in components)
                {
                    coordsForQuads.Remove(i);
                }

                // Agregar la combinación de todos los componentes como una lista única
                coordsForQuads.Add(components.SelectMany(x => x).Distinct().ToList());
            }

            // Si existen múltiples listas de coordenadas, identificamos los grupos
            if (coordsForQuads.Count > 1)
            {
                foreach (var n in coordsForQuads)
                {
                    List<string> a = subgroups
                        .Where(kv => kv.Value.Any(v => n.Contains(v))) // Filtrar claves cuyo valor tenga coordenadas en `n`
                        .Select(kv => kv.Key) // Obtener las claves de cuadriculas afectadas
                        .Distinct()
                        .ToList();

                    gp.Add(a);
                }
            }

            return gp;
        }


        public void InsertDataToDatabase(string codigou, string codicua, string cdcuad, Int32 grupo, Int32 grupoF, string fecha, string zona, string psgrupo)
        {
            try
            {
                databaseHandler.InsertRowsSimultaneidad( codigou,  codicua,  cdcuad,  grupo,  grupoF,  fecha,  zona,  psgrupo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar datos en la base de datos: {ex.Message}");
            }
        }

        public void Process(int zone)
        {
            SetZone(zone);
            GetQuadrants();
            PrepareData();
            GetGroups();
            GetSubgroups();
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
            }
            catch (Exception ex)
            {
            }
        }
    }

}
