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
    /// Lógica de interacción para geocatmin_imagenes.xaml
    /// </summary>
    public partial class geocatmin_imagenes : Page
    {
        public geocatmin_imagenes()
        {
            InitializeComponent();
            DataTable dt = accesoDB.ConsultaDB("MAPA-TELEDETECCION", "DESCRIPCION");
            // Filtramos los elementos que no queremos mostrar en el DataGrid
            List<string> filterList = new List<string>() { "Catálogo de Imágenes Aster" }; // Tus valores para filtrar.
            try { 
            string filterExpression = "DESCRIPCION NOT IN ('" + string.Join("','", filterList) + "')";
            DataRow[] filteredRows = dt.Select(filterExpression);
            DataTable dt_f = dt.Clone(); // Clona la estructura de la tabla, no los datos.
            foreach (DataRow row in filteredRows)
            {
                dt_f.ImportRow(row);
            }
            dgd_imagenes.Dispatcher.Invoke(() =>
            {
                dgd_imagenes.ItemsSource = dt_f.DefaultView;
            });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en consulta de datos espaciales:\n\n" + ex.Message);
            }
        }

        private void dgd_imagenes_Loaded(object sender, RoutedEventArgs e)
        {
            try { 
            dgd_imagenes.Columns[0].MinWidth = 35;
            dgd_imagenes.Columns[0].MaxWidth = 35;
            dgd_imagenes.Columns[1].IsReadOnly = true;
            dgd_imagenes.Columns[1].Width = 250;
            dgd_imagenes.Columns[1].MinWidth = 250;
            dgd_imagenes.Columns[1].MaxWidth = 250;
            dgd_imagenes.Columns[1].Header = " Laboratorio de Teledetección";
            }
            catch { }
        }

        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_imagenes.CurrentItem;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaCatastro = SearchLayersInDatoIGN.GetFileNamesFromFolder(System.IO.Path.Combine(fileLayers, "IMAGEN"));
            string LayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaCatastro);           
            geocatmin_tematicos tematicos = new geocatmin_tematicos();
            _ = tematicos.AddLayersBase(LayerOk);
        }

        void UnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_imagenes.CurrentItem;
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
