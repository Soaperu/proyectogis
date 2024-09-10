using ArcGIS.Core.Data;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Automapic_pro.GlobalVariables;

namespace Automapic_pro.bdgeocientifica
{
    /// <summary>
    /// Lógica de interacción para bd_geocientifica.xaml
    /// </summary>
    public partial class bd_geocientifica : Page
    {
        public bd_geocientifica()
        {
            InitializeComponent();
            _ = MainRequest();
        }

        static readonly HttpClient client = new HttpClient();
        //HashSet<string> capaModulo = new HashSet<string>();
        string capaModulo;
        string pathCapaModulo;
        async Task MainRequest()
        {
            try
            {
                // Reemplaza los parámetros directamente en la URL o constrúyelos dinámicamente
                string url = serviceBdgeoModulos;

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar JSON a una lista de objetos "Respuesta"
                var respuestas = JsonConvert.DeserializeObject<List<Respuesta>>(responseBody);

                //Ejemplo de cómo acceder a los datos
                foreach (var respuesta in respuestas)
                {
                    tbx_bdgeo_modulos.Items.Add(respuesta.Descripcion + " | " + respuesta.Codigo);
                }
                operators.ForEach(o => cbx_bdgeo_operadores.Items.Add(o));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        Dictionary<string, string> criterios = new Dictionary<string, string>();
        private void tbx_bdc_modulos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            getCriterios();
        }

        private int contadorControles = 1;
        private int contadorCriterios = 0;
        private int contadorConstructor = 1;
        //private List<string> registeredNames = new List<string>();
        private void GenerarControlesDinamicos(int id)
        {
            // Crear el nuevo StackPanel que contendrá los controles
            StackPanel stackPanel = new StackPanel
            {
                Name = $"stackpanel_{id}",
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            //Registramos el nombre dentro del NameScope
            RegisterName($"stackpanel_{id}", stackPanel);
            // Crear y configurar los ComboBox's
            ComboBox comboBox1 = new ComboBox // AND - OR
            {
                Name = $"combobox1_{id}",
                Width = 40,
                Height = 25,
                Margin = new Thickness(4)
            };
            comboBox1.Items.Add("And");
            comboBox1.Items.Add("Or");
            comboBox1.SelectedIndex = 0;
            stackPanel.Children.Add(comboBox1);

            ComboBox comboBox2 = new ComboBox // CRITERIO - CAMPO
            {
                Name = $"combobox2_{id}",
                Width = 100,
                Height = 25,
                Margin = new Thickness(4)
            };
            RegisterName($"comboBox2_{id}", comboBox2);
            foreach (var key in criterios.Keys) { comboBox2.Items.Add(key); }
            comboBox2.SelectionChanged += ComboBox2_SelectionChanged;
            stackPanel.Children.Add(comboBox2);

            ComboBox comboBox3 = new ComboBox // OPERADORES
            {
                Name = $"combobox3_{id}",
                Width = 70,
                Height = 25,
                Margin = new Thickness(4)
            };
            operators.ForEach(x => comboBox3.Items.Add(x));
            //comboBox2.Items.AddRange(operators);
            stackPanel.Children.Add(comboBox3);

            ComboBox comboBox4 = new ComboBox // VALORES UNICOS DE CAMPO
            {
                Name = $"combobox4_{id}",
                Width = 100,
                Height = 25,
                Margin = new Thickness(4)
            };
            //comboBox2.SelectedIndex = 0;
            RegisterName($"comboBox4_{id}", comboBox4);
            stackPanel.Children.Add(comboBox4);

            // Crear y configurar el Button que elimina la fila completa
            Image imgDelete = new Image();
            imgDelete.Source = new BitmapImage(new Uri("/Automapic_pro;component/Images/bdgeocientifica/delete.png", UriKind.Relative));
            imgDelete.Width = 20;
            imgDelete.Height = 20;
            Button button = new Button
            {
                Name = $"botondelete_{id}",
                Margin = new Thickness(4),
                Height = 25,
            };
            button.Content = imgDelete;
            // Suscribir el evento Click del botón "Eliminar"
            button.Click += (s, eArgs) => deleteStackpanel(button.Name);
            stackPanel.Children.Add(button);
            // Agregar el StackPanel con los controles al StackPanel principal
            sctkp_bdgeo_MainContainer.Children.Add(stackPanel);
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                string id = comboBox.Name.Split('_')[1];
                string nombrecomboBox = $"comboBox4_{id}";
                string fieldSelected = comboBox.SelectedItem.ToString();
                ComboBox comboBoxFields = FindName(nombrecomboBox) as ComboBox;
                bool arroba = capaModulo.Contains("@");
                string pathSDE;
                string capaFinal;
                if (arroba)
                {
                    pathSDE = System.IO.Path.Combine(currentPath, SDE_huawei);
                    capaFinal = capaModulo.Split("@")[0];
                }
                else
                {
                    pathSDE = System.IO.Path.Combine(currentPath, SDE_bdgeocat);
                    capaFinal = capaModulo;
                }
                GetUniqueValuesFromFeatureClass(pathSDE, capaFinal, fieldSelected, comboBoxFields);
            }
        }

        private void btn_bdgeo_constructor_Click(object sender, RoutedEventArgs e)
        {
            //contadorControles++;
            if (contadorControles >= contadorCriterios)
            {
                btn_bdgeo_constructor.IsEnabled = false;
            }
            else
            {
                contadorControles++;
                GenerarControlesDinamicos(contadorConstructor);
                contadorConstructor++;
            }
        }

        private void deleteStackpanel(string nameid)
        {
            string id= nameid.Split('_')[1];
            string nombreStackPanel = $"stackpanel_{id}";
            StackPanel stackPanel = FindName(nombreStackPanel) as StackPanel;
            if (stackPanel != null)
            {
                // Eliminamos el stackpanel en mencion
                sctkp_bdgeo_MainContainer.Children.Remove(stackPanel);
                if (contadorControles <= contadorCriterios)
                {
                    btn_bdgeo_constructor.IsEnabled = true;
                    //contadorControles--;
                }
                UnregisterName(nombreStackPanel);
                contadorControles--;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getCriterios();
        }

        private async void addLayerModulo(string capa, string path)
        {
            await QueuedTask.Run(() =>
            {
                Map activeMap = MapView.Active.Map;
                bool arroba = capa.Contains("@");
                string lyrPath;
                if (arroba)
                {
                    string pathSDE = System.IO.Path.Combine(currentPath, SDE_huawei);
                    lyrPath = pathSDE + path;
                }
                else
                {
                    string pathSDE = System.IO.Path.Combine(currentPath, SDE_bdgeocat);
                    lyrPath =pathSDE + path;
                }
                Uri uri = new Uri(lyrPath);
                // Crear la capa
                Layer addedLayer = LayerFactory.Instance.CreateLayer(uri, activeMap, 0);
                addedLayer.SetVisibility(true);
                // Centrar la vista en la capa
                MapView.Active.ZoomToAsync(addedLayer);
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            addLayerModulo(capaModulo, pathCapaModulo);
        }

        async private void getCriterios()
        {
            tbx_bdgeo_fields.Items.Clear();
            cbx_bdgeo_criterio.Items.Clear();
            criterios.Clear();
            contadorCriterios = 0;
            
            btn_bdgeo_constructor.IsEnabled = true;
            // Reemplaza los parámetros directamente en la URL o constrúyelos dinámicamente
            string urlFields = serviceBdgeoFields;
            string modulo = tbx_bdgeo_modulos.SelectedItem.ToString().Split(" | ")[1];
            string url = String.Format(urlFields, modulo);

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserializar JSON a una lista de objetos "Respuesta"
            var respuestas = JsonConvert.DeserializeObject<List<Respuesta>>(responseBody);
            // Recorremos todo el Json 
            HashSet<string> conjuntoCapa = new HashSet<string>();
            HashSet<string> conjuntoPathCapa = new HashSet<string>();
            foreach (var respuesta in respuestas)
            {
                tbx_bdgeo_fields.Items.Add(respuesta.Adicional2 + " | " + respuesta.Descripcion);
                cbx_bdgeo_criterio.Items.Add(respuesta.Adicional2);
                criterios.Add(respuesta.Adicional2, respuesta.Adicional1);
                conjuntoCapa.Add(respuesta.Descripcion);
                conjuntoPathCapa.Add(respuesta.Adicional5);
                contadorCriterios++;
            }
            List<string> listaSinDuplicados1 = new List<string>(conjuntoCapa);
            List<string> listaSinDuplicados2 = new List<string>(conjuntoPathCapa);
            capaModulo = listaSinDuplicados1[0];
            pathCapaModulo = listaSinDuplicados2[0];

            if (contadorControles >= 1)
            {
                for (int i = 2; i <= contadorConstructor; i++)
                {
                    try
                    {
                        deleteStackpanel("X_" + i.ToString());
                        UnregisterName($"stackpanel_{i}");
                    }
                    catch {; }
                }
                contadorControles = 1;
            }
        }

        public async void GetUniqueValuesFromFeatureClass(string gdbPath, string featureClassName, string fieldName, ComboBox comboBox)
        {            
            // Lista para almacenar los valores únicos
            HashSet<string> uniqueValues = new HashSet<string>();

            await QueuedTask.Run(() =>
            {
                // Abrir la geodatabase
                //using (Geodatabase geodatabase = new Geodatabase(new DatabaseConnectionFile(new Uri(gdbPath))))
                //using (FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName))
                //using (RowCursor rowCursor = featureClass.Search(null, false))
                using var geodatabase = new Geodatabase(new DatabaseConnectionFile(new Uri(gdbPath, UriKind.Absolute)));
                var featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName);
                using var rowCursor = featureClass.Search(null, false);
                var table = featureClass.GetDefinition() as FeatureClassDefinition;
                   
                    // Definir el índice del campo 'interes'
                    int interesIndex = table.FindField(fieldName);

                    while (rowCursor.MoveNext())
                    {
                        using (Feature feature = (Feature)rowCursor.Current)
                        {
                            // Obtener el valor del campo 'interes'
                            string valorInteres = feature[interesIndex]?.ToString();
                            // Añadir el valor a la lista de valores únicos
                            uniqueValues.Add(valorInteres);
                        }
                    }
            });
            // Si necesitas usar la lista de valores únicos fuera de la tarea QueuedTask
            List<string> valoresUnicosFinal = uniqueValues.ToList();
            // Aquí deposimos todos los elementos unicos del campos
            valoresUnicosFinal.ForEach(x => comboBox.Items.Add(x));
        }

        public async Task GetUniqueValuesFromFeatureClass1(string gdbPath, string featureClassName, string fieldName, ComboBox comboBox)
        {
            // Lista para almacenar los valores únicos
            HashSet<string> uniqueValues = new HashSet<string>();
            try
            {
                await QueuedTask.Run(() =>
                {
                    // Abrir la geodatabase
                    using (Geodatabase geodatabase = new Geodatabase(new DatabaseConnectionFile(new Uri(gdbPath, UriKind.Absolute))))
                    using (FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName))
                    {
                        var table = featureClass.GetDefinition() as FeatureClassDefinition;
                        // Obtener el índice del campo de interés
                        int fieldIndex = table.FindField(fieldName);

                        // Verificar si el campo existe
                        if (fieldIndex == -1)
                        {
                            throw new Exception($"Campo '{fieldName}' no existe en el feature class.");
                        }

                        // Crear una consulta para obtener los valores únicos
                        ArcGIS.Core.Data.QueryFilter queryFilter = new ArcGIS.Core.Data.QueryFilter
                        {
                            SubFields = fieldName,
                            WhereClause = $"{fieldName} IS NOT NULL"
                        };

                        // Ejecutar la consulta y obtener los valores
                        using (RowCursor rowCursor = featureClass.Search(queryFilter, false))
                        {
                            while (rowCursor.MoveNext())
                            {
                                using (Feature feature = (Feature)rowCursor.Current)
                                {
                                    string value = Convert.ToString(feature[fieldIndex]);
                                    uniqueValues.Add(value);
                                }
                            }
                        }
                    }
                });

                // Actualizar el ComboBox en el hilo de la UI
                comboBox.Dispatcher.Invoke(() =>
                {
                    comboBox.Items.Clear();
                    foreach (string value in uniqueValues)
                    {
                        comboBox.Items.Add(value);
                    }
                });
            }
            catch (Exception ex)
            {
                // Manejar adecuadamente la excepción
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void cbx_bdgeo_criterio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                string nombrecomboBox = comboBox.Name;
                string fieldSelected = comboBox.SelectedItem.ToString();
                //ComboBox comboBoxFields = FindName(nombrecomboBox) as ComboBox;
                bool arroba = capaModulo.Contains("@");
                string pathSDE;
                string capaFinal;
                if (arroba)
                {
                    pathSDE = System.IO.Path.Combine(currentPath, SDE_huawei);
                    capaFinal = capaModulo.Split("@")[0];
                }
                else
                {
                    pathSDE = System.IO.Path.Combine(currentPath, SDE_bdgeocat);
                    capaFinal = capaModulo;
                }
                GetUniqueValuesFromFeatureClass(pathSDE, capaFinal, fieldSelected, cbx_bdgeo_values);
            }
        }
    }
    
}
