using ArcGIS.Core.CIM;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.Geometry;
using ArcGIS.Desktop.Mapping;
using CommonUtilities;
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
using System.Windows.Shapes;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin.UI
{
    /// <summary>
    /// Lógica de interacción para DibujarPoligonoWpf.xaml
    /// </summary>
    public partial class DibujarPoligonoWpf : Window
    {
        string zona;
        string tipo;
        string archi = GlobalVariables.idExport;
        FeatureLayer layer;
        public DibujarPoligonoWpf()
        {
            InitializeComponent();
            btnGraficar.IsEnabled = false;
        }
        
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Convertir las coordenadas a numérico (Val en VB se asemeja a Convert.ToDouble)
            double este = 0;
            double norte = 0;

            // Intentar convertir el texto a double. Si falla, podrías manejar el error.
            double.TryParse(txtEste.Text, out este);
            double.TryParse(txtNorte.Text, out norte);

            // Crear la cadena valor con el número de punto y las coordenadas
            string valor = "Punto " + (listBoxVertices.Items.Count + 1) + ":  " + este + "; " + norte;
            listBoxVertices.Items.Add(valor);

            //Habilitar el botón de generar / graficar si hay 3 o más puntos
            if (listBoxVertices.Items.Count >= 4 && tipo != null && zona != null)
                btnGraficar.IsEnabled = true;
        }

        private async void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> linesString = listBoxVertices.Items.Cast<string>();
            var vertices = CommonUtilities.ArcgisProUtils.FeatureClassCreatorUtils.GetVerticesFromListBoxItems(linesString);
            layer = await CommonUtilities.ArcgisProUtils.FeatureClassCreatorUtils.CreatePolygonInNewGdbAsync(GlobalVariables.pathFileTemp, "GeneralGDB", "Poligono"+ archi, vertices, zona);
            CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer(layer, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(0, 255, 255, 0), CIMColor.CreateRGBColor(255, 0, 0));
        }

        public static List<MapPoint> CreateSampleVertices()
        {
            // Definir el sistema de referencia espacial UTM Zona 18S, WGS84
            SpatialReference spatialReference = SpatialReferenceBuilder.CreateSpatialReference(32718);

            // Crear los vértices del polígono en UTM Zona 18S
            List<MapPoint> vertices = new List<MapPoint>
        {
            // Vértice 1: Punto Oeste
            MapPointBuilder.CreateMapPoint(500000, 8900000, spatialReference),

            // Vértice 2: Punto Este
            MapPointBuilder.CreateMapPoint(501000, 8900000, spatialReference),

            // Vértice 3: Punto Norte
            MapPointBuilder.CreateMapPoint(501000, 8901000, spatialReference),

            // Vértice 4: Punto Sur
            MapPointBuilder.CreateMapPoint(500000, 8901000, spatialReference)
        };

            return vertices;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
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
                btnGraficar.IsEnabled = true;
            }
            else
            {
                btnGraficar.IsEnabled = false;
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
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
            btnGraficar.IsEnabled = false;

        }

        private void cbxTipo_Loaded(object sender, RoutedEventArgs e)
        {
            cbxTipo.Items.Add("Polígono");
            cbxTipo.Items.Add("Círculo");
            cbxTipo.SelectedIndex = 0;
        }

        private void cbxZona_Loaded(object sender, RoutedEventArgs e)
        {
            cbxZona.Items.Add("17");
            cbxZona.Items.Add("18");
            cbxZona.Items.Add("19");
        }

        private void cbxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tipo = cbxTipo.SelectedItem.ToString();
            if (listBoxVertices.Items.Count >= 4 && tipo != null)
            {
                btnGraficar.IsEnabled = true;
            }
        }

        private void cbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zona = cbxZona.SelectedItem.ToString();
            if (listBoxVertices.Items.Count >= 4 && zona != null)
            {
                btnGraficar.IsEnabled = true;
            }
        }

        private void gridHeader_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            // El botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }
    }
}
