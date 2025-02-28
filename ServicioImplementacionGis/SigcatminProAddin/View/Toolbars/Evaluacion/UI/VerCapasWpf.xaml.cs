using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SigcatminProAddin.View.Toolbars.Evaluacion.UI
{
    /// <summary>
    /// Lógica de interacción para VerCapasWpf.xaml
    /// </summary>
    public partial class VerCapasWpf : Window
    {
        public VerCapasWpf()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
        }

        private void AddCheckBoxesToListBox()
        {
            string[] items = {                                  
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
                // Suscribir el evento Checked y Unchecked
                checkBox.Checked += CheckBox_StateChanged;
                checkBox.Unchecked += CheckBox_StateChanged;
                LayersListBox.Items.Add(checkBox);
            }
        }

        private void CheckBox_StateChanged(object sender, RoutedEventArgs e)
        {
            // Verifica si al menos uno de los CheckBoxes está marcado
            bool anyChecked = false;
            foreach (var item in LayersListBox.Items)
            {
                if (item is CheckBox checkBox && checkBox.IsChecked == true)
                {
                    anyChecked = true;
                    break;
                }
            }

            // Habilitar o deshabilitar el botón "Agregar" según el estado
            btnAgregar.IsEnabled = anyChecked;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var zoneDm = GlobalVariables.CurrentZoneDm;
                var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
                var map = MapUtils.GetActiveMapAsync();
                var datum = int.Parse(GlobalVariables.CurrentDatumDm);
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");
                // Itera todos items seleccionados en el ListBox de WPF
                foreach (var item in LayersListBox.Items)
                {
                    if (item is CheckBox checkBox && checkBox.IsChecked == true)
                    {
                        string capaSeleccionada = checkBox.Content.ToString();
                        await LayerUtils.AddLayerCheckedListBox(capaSeleccionada, zoneDm, featureClassLoader, datum);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en capa de listado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void gridHeader_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            // Comprueba que el botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }
    }
}
