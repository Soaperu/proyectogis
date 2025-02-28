using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Core.Geoprocessing;
using CommonUtilities;
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

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para ActualizacionCuadriculasRegionales.xaml
    /// </summary>
    public partial class ActualizacionCuadriculasRegionales : Page
    {
        string datum = "";
        string fileInputs = string.Empty;
        string fileOutputs = string.Empty;
        public ActualizacionCuadriculasRegionales()
        {
            InitializeComponent();
            CurrentUser();
        }
        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
            GlobalVariables.currentUser = CurrentUserLabel.Text;
        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("Seleccione Datum", 0));
            cbp.Add(new ComboBoxPairs("WGS-84", 2));
            cbp.Add(new ComboBoxPairs("PSAD-56", 1));
            cbp.Add(new ComboBoxPairs("Ambos", 3));

            // Asignar la lista al ComboBox
            CbxSistema.DisplayMemberPath = "_Key";
            CbxSistema.SelectedValuePath = "_Value";
            CbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxSistema.SelectedIndex = 0;
            GlobalVariables.CurrentDatumDm = "0";
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

        private void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbxSistema.SelectedValue != null)
            {
                int selectedValue = (int)CbxSistema.SelectedValue;

                // Si el valor seleccionado es distinto de 0, activamos el botón
                BtnProcesar.IsEnabled = selectedValue != 0;
                if (selectedValue == 1)
                {
                    datum = "PSAD56";
                }
                else  if (selectedValue == 2)
                {
                    datum = "WGS84";
                }
                else if (selectedValue == 3)
                {
                    // Si es "Ambos", tomamos ambos datums
                    datum = "WGS84;PSAD56";
                }
                else
                {
                    datum = null;
                }
            }
            else
            {
                // Si no hay selección, desactivamos el botón
                BtnProcesar.IsEnabled = false;
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

        private void BtnFileInputs_Click(object sender, RoutedEventArgs e)
        {
            OpenItemDialog openFileDialog = new OpenItemDialog
            {
                Title = "Seleccionar carpeta de Insumos (INPUTS)",
                MultiSelect = false,
                BrowseFilter = BrowseProjectFilter.GetFilter(ItemFilters.Folders)
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                fileInputs = openFileDialog.Items[0].Path;
                TbxFileInputs.Text = fileInputs;
            }
        }

        private void BtnFileOutputs_Click(object sender, RoutedEventArgs e)
        {
            OpenItemDialog openFileDialog = new OpenItemDialog
            {
                Title = "Seleccionar carpeta de Resultados (OUTPUTS)",
                MultiSelect = false,
                BrowseFilter = BrowseProjectFilter.GetFilter(ItemFilters.Folders)
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                fileOutputs = openFileDialog.Items[0].Path;
                TbxFileOutputs.Text = fileOutputs;
            }
        }

        private async void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarUtils progressBar = new ProgressBarUtils("Procesando Actualización de Cuadriculas Regionles...");
            progressBar.Show();
            try
            {
                var ParamsInsumos = Geoprocessing.MakeValueArray(datum, TbxFileInputs.Text);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetInputCuad, ParamsInsumos);
                var responseJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.ReturnValue);
                var nameFolderInputs = responseJson["folder_insumos"];
                var ParamsCuadriculas = Geoprocessing.MakeValueArray(nameFolderInputs, TbxFileOutputs.Text, datum);
                var response2 = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetOutputCuad, ParamsCuadriculas);
            }
            catch (Exception ex)
            {
                progressBar.Dispose();
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            progressBar.Dispose();
        }
    }
}
