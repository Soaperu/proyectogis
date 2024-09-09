using ArcGIS.Desktop.Mapping;
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
using static Automapic_pro.GlobalVariables;

namespace Automapic_pro.modulos
{
    /// <summary>
    /// Lógica de interacción para otras_fuentes.xaml
    /// </summary>
    public partial class geocatmin_otras_fuentes : Page
    {
        public geocatmin_otras_fuentes()
        {
            InitializeComponent();
            DataTable dt = accesoDB.ConsultaDB("MAPA-TEMATICO_OTROS", "DESCRIPCION");
            List<string> filterList = new List<string>() { "INDECI - SIRAD Sistema de información sobre recursos para atención de desastres",
                                                            "INDECI-AtencionEmergencias",
                                                            "INDECI-Emergencias",
                                                            "INDECI-GestionRiesgo-CEPIG",
                                                            "MINAM-DensidadCarbono",
                                                            "MINAM-ServicioAmbiental",
                                                            "MINAM-TerritorioMedioAmbiente",
                                                            "MINAM-ZonificacionZEE",
                                                            "Minem Energia",
                                                            "Minem Eolico",
                                                            "MINEM-ActividadMinera",
                                                            "MINEM-LotePetrolero",
                                                            "MINEM-RadiacionSolar",
                                                            "MINEM-RedAltaTension",
                                                            "MINEM-RedElectrica",
                                                            "MINEM-RedMediaTension",
                                                            "Osinergmin",
                                                            "OSINERGMIN-Hidrocarburos",
                                                            "SENAMHI - Mapa Climático",
                                                            "SENAMHI - Mapa Frecuencia de Heladas",
                                                            "SENAMHI - Mapa Radiación Solar"}; // Los valores para filtrar.

            string filterExpression = "DESCRIPCION NOT IN ('" + string.Join("','", filterList) + "')";
            try
            {
                DataRow[] filteredRows = dt.Select(filterExpression);
                DataTable dt_f = dt.Clone(); // Clona la estructura de la tabla, no los datos.
                foreach (DataRow row in filteredRows)
                {
                    dt_f.ImportRow(row);
                }
                dgd_otrasfuentes.Dispatcher.Invoke(() =>
                {
                    dgd_otrasfuentes.ItemsSource = dt_f.DefaultView;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en consulta de datos espaciales:\n\n" + ex.Message);
            }
        }

        private void dgd_otrasfuentes_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgd_otrasfuentes.Columns[0].MinWidth = 35;
                dgd_otrasfuentes.Columns[0].MaxWidth = 35;
                dgd_otrasfuentes.Columns[1].IsReadOnly = true;
                dgd_otrasfuentes.Columns[1].Width = 250;
                dgd_otrasfuentes.Columns[1].MinWidth = 250;
                dgd_otrasfuentes.Columns[1].MaxWidth = 250;
                dgd_otrasfuentes.Columns[1].Header = " Capas de Información - Otras fuentes";
            }
            catch { }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Código para manejar el evento cuando la casilla de verificación está marcada
            CheckBox checkBox = (CheckBox)sender;
            // Accede a los datos o realiza cualquier acción deseada
        }
        // metodo referenciado para el evento unchecked del checkBox del dataGrid
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Código para manejar el evento cuando la casilla de verificación está marcada
            CheckBox checkBox = (CheckBox)sender;
            // Accede a los datos o realiza cualquier acción deseada
            //MessageBox.Show("evento check ok");
        }

        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_otrasfuentes.CurrentItem;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaCatastro = SearchLayersInDatoIGN.GetFileNamesFromFolder(System.IO.Path.Combine(fileLayers, "OTRAS_FUENTES"));
            string LayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaCatastro);
            geocatmin_tematicos tematicos = new geocatmin_tematicos();
            _ = tematicos.AddLayersBase(LayerOk);
        }

        void UnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_otrasfuentes.CurrentItem;
            Map activeMap = MapView.Active.Map;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaTOC = activeMap.GetLayersAsFlattenedList().Select(layer => layer.Name).ToList();
            string nameLayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaTOC);
            var LayerOk = activeMap.FindLayers(nameLayerOk).FirstOrDefault();
            geocatmin_tematicos tematicos = new geocatmin_tematicos();
            tematicos.removeLayer(LayerOk);
        }
    }
}
