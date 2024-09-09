using System;
using System.Collections.Generic;
using System.Linq;
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
using static Automapic_pro.toolBox;
using static Automapic_pro.GlobalVariables;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Internal.Mapping;
using Newtonsoft.Json;
using System.Windows.Media.Animation;
using System.Net.Http;

namespace Automapic_pro.mapa_geologico_50k
{
    /// <summary>
    /// Lógica de interacción para mp_geologico_50k.xaml
    /// </summary>
    /// 
    public partial class mp_geologico_50k : Page
    {
        //private Frame currentframe = new Frame();
        public mp_geologico_50k()
        {
            InitializeComponent();
            this.Loaded += async (sender, e) => await InitializeAsync();

        }

        public async Task InitializeAsync()
        {
            var response_load_cuad = await ExecuteGPAsync(_toolBoxPath_mapa_geologico, _tool_addFeatureQuadsToMapMg, Geoprocessing.MakeValueArray());
            var response = await ExecuteGPAsync(_toolBoxPath_mapa_geologico, _tool_getComponentCodeSheetMg, Geoprocessing.MakeValueArray());

            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ReturnValue);
            string result = (string)responseJson["response"];
            //var responseJson2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());
            var list_fila = result.Split(',');
            foreach (var item in list_fila)
            {
                cbx_mg_fila.Items.Add(item);
            }
        }

        private async void cbx_mg_fila_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbx_mg_coln.Items.Clear();
            cbx_mg_cuad.Items.Clear();
            if (cbx_mg_fila.SelectedItem == null)
            {
                return;
            }
            var response = await ExecuteGPAsync(_toolBoxPath_mapa_geologico, _tool_getComponentCodeSheetMg, Geoprocessing.MakeValueArray(cbx_mg_fila.SelectedItem));
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ReturnValue);
            string result = (string)responseJson["response"];
            //var responseJson2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());
            var list_col = result.Split(',');
            foreach (var item in list_col)
            {
                cbx_mg_coln.Items.Add(item);
            }
        }

        private async void cbx_mg_coln_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbx_mg_cuad.Items.Clear();
            if (cbx_mg_coln.SelectedItem == null)
            {
                return;
            }
            var response = await ExecuteGPAsync(_toolBoxPath_mapa_geologico, _tool_getComponentCodeSheetMg, Geoprocessing.MakeValueArray(cbx_mg_fila.SelectedItem, cbx_mg_coln.SelectedItem));
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ReturnValue);
            string result = (string)responseJson["response"];
            //var responseJson2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());
            var list_col = result.Split(',');
            foreach (var item in list_col)
            {
                cbx_mg_cuad.Items.Add(item);
            }
        }

        private async void cbx_mg_cuad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_mg_coln.SelectedItem == null)
            {
                return;
            }
            var response = await ExecuteGPAsync(_toolBoxPath_mapa_geologico, _tool_getComponentCodeSheetMg, Geoprocessing.MakeValueArray(cbx_mg_fila.SelectedItem, cbx_mg_coln.SelectedItem, cbx_mg_cuad.SelectedItem));

        }

        public delegate void change_state_progressBar(bool indeterminado);
        public event change_state_progressBar change_state_progress;
        private async void btn_mg_loadCode_Click(object sender, RoutedEventArgs e)
        {
            change_state_progress?.Invoke(true);
            Storyboard storyboard = (Storyboard)this.FindResource("FadeInOutStoryboard");
            Storyboard.SetTarget(storyboard, btn_mg_loadCode);
            storyboard.Begin();
            await TuTareaLarga();
            //_=Main();
            storyboard.Stop();
            change_state_progress?.Invoke(false);
        }
        private Task TuTareaLarga()
        {
            // Simula una tarea larga
            return Task.Delay(3000); // Retraso de 5 segundos
        }

        static readonly HttpClient client = new HttpClient();
        public string TipoRespuesta { get; set; }
        public string ValorRespuesta { get; set; }
        static async Task Main()
        {
            try
            {
                // Reemplaza los parámetros directamente en la URL o constrúyelos dinámicamente
                string url = "https://geocatminapp.ingemmet.gob.pe/bdgeocientifica/app/api/account/Listacriterio/1/AA";

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar JSON a una lista de objetos "Respuesta"
                var respuestas = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                //Ejemplo de cómo acceder a los datos
                foreach (var respuesta in respuestas)
                {
                    respuesta.ToString().Trim();
                    MessageBox.Show(respuesta.ToString());
                    //Console.WriteLine($"TipoRespuesta: {respuesta.TipoRespuesta}, ValorRespuesta: {respuesta.ValorRespuesta}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
