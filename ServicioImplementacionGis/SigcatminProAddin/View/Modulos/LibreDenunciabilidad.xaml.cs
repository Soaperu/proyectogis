using ArcGIS.Desktop.Core.Geoprocessing;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using CommonUtilities.ArcgisProUtils.Models;
using DatabaseConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
using static ArcGIS.Desktop.Internal.Mapping.Views.PropertyPages.Map.TransformationViewModel;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para LibreDenunciabilidad.xaml
    /// </summary>
    public partial class LibreDenunciabilidad : Page
    {
        public DatabaseHandler _dataBaseHandler = new DatabaseHandler();
        public LibreDenunciabilidad()
        {
            InitializeComponent();
        }

        public class ComboBoxPairs
        {
            public string _Key { get; set; }
            public int _Value { get; set; }

            public ComboBoxPairs(string _key, int _value)
            {
                _Key = _key;
                _Value = _value;
            }

        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("Seleccione Datum", 0));
            cbp.Add(new ComboBoxPairs("WGS-84", 2));
            cbp.Add(new ComboBoxPairs("PSAD-56", 1));

            // Asignar la lista al ComboBox
            CbxSistema.DisplayMemberPath = "_Key";
            CbxSistema.SelectedValuePath = "_Value";
            CbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxSistema.SelectedIndex = 0;
            GlobalVariables.CurrentDatumDm = "0";
        }

        private async void BtnReporte_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtRecords =  _dataBaseHandler.GetCodigosLibreDenu();

            foreach (DataRow dtRecord in dtRecords.Rows)
            {
                var codigo = dtRecord["CODIGO"].ToString();
                string datum = dtRecord["DATUM"].ToString();

                DataTable dmrRecords = _dataBaseHandler.GetUniqueDM(codigo, 1);
                DataRow row = dmrRecords.Rows[0];
                string zona = row["ZONA"].ToString();
                //List<ResultadoEvaluacionModel> res = new List<ResultadoEvaluacionModel>();
                var Params = Geoprocessing.MakeValueArray(codigo, datum, zona);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEvalLibreDenu, Params);
                List<ResultadoEval> responseJson = JsonConvert.DeserializeObject<List<ResultadoEval>>(response.ReturnValue);

            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
