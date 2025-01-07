using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ArcGIS.Core.Data;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Mapping.Events;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.Xpf.Grid;
using SigcatminProAddin.Utils.UIUtils;
using FlowDirection = System.Windows.FlowDirection;
using System.Text.RegularExpressions;
using SigcatminProAddin.Models;
using SigcatminProAddin.Models.Constants;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.CIM;
using DevExpress.Xpf.Grid.GroupRowLayout;
using System.Security.Policy;
using DevExpress.Mvvm.Native;
using static SigcatminProAddin.View.Modulos.EvaluacionDM;
using DevExpress.Utils;
using DevExpress.XtraPrinting.Native;


//using ArcGIS.Desktop.Framework.Controls;
//using ArcGIS.Desktop.Editing;


//using ArcGIS.Desktop.Editing.Attributes;
//using System.Windows.Forms;


namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para SimulaPetitorios.xaml
    /// </summary>
    public partial class SimulaPetitorios : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;
        string zona;
        string tipo = "Polígono";
        string archi = GlobalVariables.idExport;
        FeatureLayer layer;

        public SimulaPetitorios()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            //ConfigureDataGridResultColumns();
            //ConfigureDataGridDetailsColumns();
            dataBaseHandler = new DatabaseHandler();
            //CbxTypeConsult.SelectedIndex = 0;
            TbxRadio.Text = "5";
            BtnGraficar.IsEnabled = true;

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxEste.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TbxNorte.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Crear la cadena valor con el número de punto y las coordenadas
            //string valor = "Coordenada " + (listBoxVertices.Items.Count + 1) + ":  " + TbxEste.Text.TrimEnd() + "  :  " + TbxNorte.Text.TrimEnd();
            //listBoxVertices.Items.Add(valor);
            //BtnGraficar.IsEnabled = true;

            // Convertir las coordenadas a numérico (Val en VB se asemeja a Convert.ToDouble)
            double este = 0;
            double norte = 0;

            // Intentar convertir el texto a double. Si falla, podrías manejar el error.
            double.TryParse(TbxEste.Text, out este);
            double.TryParse(TbxNorte.Text, out norte);

            // Crear la cadena valor con el número de punto y las coordenadas
            string valor = "Punto " + (listBoxVertices.Items.Count + 1) + ":  " + este + "; " + norte;
            listBoxVertices.Items.Add(valor);
        }

        private void TbxEste_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^(\d{0,6}(\.\d{0,4})?)$");

            // Validar si el texto ingresado es válido
            e.Handled = !regex.IsMatch(((TextBox)sender).Text.Insert(((TextBox)sender).SelectionStart, e.Text));
        }

        private void TbxNorte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^(\d{0,7}(\.\d{0,4})?)$");

            // Validar si el texto ingresado es válido
            e.Handled = !regex.IsMatch(((TextBox)sender).Text.Insert(((TextBox)sender).SelectionStart, e.Text));
        }

        private void CbxZona_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("17", 17));
            cbp.Add(new ComboBoxPairs("18", 18));
            cbp.Add(new ComboBoxPairs("19", 19));

            // Asignar la lista al ComboBox
            CbxZona.DisplayMemberPath = "_Key";
            CbxZona.SelectedValuePath = "_Value";
            CbxZona.ItemsSource = cbp;

            // Seleccionar la opción 18 por defecto
            CbxZona.SelectedIndex = 1;

        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("WGS-84", 2));
            cbp.Add(new ComboBoxPairs("PSAD-56", 1));

            // Asignar la lista al ComboBox
            CbxSistema.DisplayMemberPath = "_Key";
            CbxSistema.SelectedValuePath = "_Value";
            CbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxSistema.SelectedIndex = 0;
            GlobalVariables.CurrentDatumDm = "2";
        }

        private void AddCheckBoxesToListBox()
        {
            string[] items = {
                                "Caram",
                                "Catastro Forestal",
                                "Predio Rural",
                                "Limite Departamental",
                                "Limite Provincial",
                                "Limite Distrital",
                                "Centros Poblados",
                                "Red Hidrografica",
                                "Red Vial"
                            };

            // Agrega cada elemento como un CheckBox al ListBox
            for (int i = 0; i < items.Length; i++)
            {
                var checkBox = new System.Windows.Controls.CheckBox
                {
                    Content = items[i],
                    Margin = new Thickness(2),
                    Style = (Style)FindResource("Esri_CheckboxToggleSwitch"),

                    FlowDirection = FlowDirection.RightToLeft,
                    IsThreeState = true // Permite el estado Indeterminado
                };

                // Establece el estado Indeterminado para los dos primeros elementos
                if (i == 0 || i == 1)
                {
                    checkBox.IsChecked = true; // Estado Indeterminado
                    checkBox.IsEnabled = false; // Desahibilidato
                }

                LayersListBox.Items.Add(checkBox);
            }

        }

        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //if (listBoxVertices.SelectedItem != null)
            //{
            //    // Obtener el elemento seleccionado
            //    string elementoSeleccionado = listBoxVertices.SelectedItem as string;
            //    listBoxVertices.Items.Remove(elementoSeleccionado);
            //}
            //else
            //{
            //    // Informar al usuario que no hay ningún elemento seleccionado
            //    MessageBox.Show(
            //        "Por favor, selecciona un elemento para eliminar.",
            //        "Sin Selección",
            //        MessageBoxButton.OK,
            //        MessageBoxImage.Information);
            //}

            // Verificar si hay un elemento seleccionado
            if (listBoxVertices.SelectedItem != null)
            {
                // Obtener el elemento seleccionado
                string elementoSeleccionado = listBoxVertices.SelectedItem as string;
                listBoxVertices.Items.Remove(elementoSeleccionado);
            }
            else
            {
                // Informar al usuario que no hay ningún elemento seleccionado
                MessageBox.Show(
                    "Por favor, selecciona un elemento para eliminar.",
                    "Sin Selección",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            if (listBoxVertices.Items.Count >= 4 && tipo != null && zona != null)
            {
                BtnGraficar.IsEnabled = true;
            }
            else
            {
                BtnGraficar.IsEnabled = false;
            }

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxVertices.Items.Count > 0)
            {
                // Confirmar la eliminación (opcional)
                MessageBoxResult resultado = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar todos los elementos?",
                    "Confirmar Eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    // Eliminar todos los elementos del ListBox
                    listBoxVertices.Items.Clear();
                }
            }
            else
            {
                // Informar al usuario que el ListBox ya está vacío
                MessageBox.Show(
                    "El ListBox ya está vacío.",
                    "Información",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            BtnGraficar.IsEnabled = false;
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {


        }

        private void CbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zona = CbxZona.SelectedItem.ToString();
            if (listBoxVertices.Items.Count >= 4 && zona != null)
            {
                BtnGraficar.IsEnabled = true;
            }
        }

        private async void BtnGeneraPoligono_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxVertices.Items.Count >= 4 && tipo != null && zona != null)
                BtnGraficar.IsEnabled = true;
            //zona = CbxZona.SelectedItem.ToString();

            zona = CbxZona.SelectedValue.ToString();
            IEnumerable<string> linesString = listBoxVertices.Items.Cast<string>();
            var vertices = CommonUtilities.ArcgisProUtils.FeatureClassCreatorUtils.GetVerticesFromListBoxItems(linesString);
            layer = await CommonUtilities.ArcgisProUtils.FeatureClassCreatorUtils.CreatePolygonInNewGdbAsync(GlobalVariables.pathFileTemp, "GeneralGDB", "Poligono" + archi, vertices, zona);
            CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer(layer, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(0, 255, 255, 0), CIMColor.CreateRGBColor(255, 0, 0));


        }








    }
}
