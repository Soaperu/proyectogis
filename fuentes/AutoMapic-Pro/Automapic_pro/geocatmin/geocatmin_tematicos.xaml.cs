using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Internal.Mapping.CommonControls;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Lógica de interacción para geocatmin_tematicos.xaml
    /// </summary>
    public partial class geocatmin_tematicos : Page
    {
        public geocatmin_tematicos()
        {
            InitializeComponent();
            //tabc_tematicos.SelectionChanged += tabc_tematicos_SelectionChanged_1;
            try {
                DataTable dt_cat = accesoDB.ConsultaDB("MAPA-TEMATICO_CATA", "DESCRIPCION");
                dgd_tematicoCAT.Dispatcher.Invoke(() =>
                {
                    dgd_tematicoCAT.ItemsSource = dt_cat.DefaultView;
                });
                DataTable dt_DGAR = accesoDB.ConsultaDB("MAPA-TEMATICO_DGAR", "DESCRIPCION");
                dgd_tematicoDGAR.Dispatcher.Invoke(() =>
                {
                    dt_DGAR.Columns[0].ColumnName = "Capas de información - DGAR";
                    dt_DGAR.Columns[0].MaxLength = 250;
                    dt_DGAR.Columns[0].ReadOnly = true;
                    dgd_tematicoDGAR.ItemsSource = dt_DGAR.DefaultView;
                });
                DataTable dt_DGR = accesoDB.ConsultaDB("MAPA-TEMATICO_DGR", "DESCRIPCION");
                // Filtramos los elementos que no queremos mostrar en el DataGrid
                List<string> filterList = new List<string>() { "Geología Integrada", "Geología Regional", "Indice de Mapas", "Informe Técnico" }; // Tus valores para filtrar.

                string filterExpression = "DESCRIPCION NOT IN ('" + string.Join("','", filterList) + "')";
                DataRow[] filteredRows = dt_DGR.Select(filterExpression);
                DataTable dt_DGR_f = dt_DGR.Clone(); // Clona la estructura de la tabla, no los datos.
                foreach (DataRow row in filteredRows)
                {
                    dt_DGR_f.ImportRow(row);
                }
                dgd_tematicoDGR.Dispatcher.Invoke(() =>
                {
                    dt_DGR_f.Columns[0].ColumnName = "Capas de informacion - DGR";
                    dt_DGR_f.Columns[0].ReadOnly = true;
                    dgd_tematicoDGR.ItemsSource = dt_DGR_f.DefaultView;
                });

                DataTable dt_DRME = accesoDB.ConsultaDB("MAPA-TEMATICO_DRME", "DESCRIPCION");
                dgd_tematicoDRME.Dispatcher.Invoke(() =>
                {
                    dt_DRME.Columns[0].ColumnName = "Capas de informacion - DRME";
                    dt_DRME.Columns[0].ReadOnly = true;
                    dgd_tematicoDRME.ItemsSource = dt_DRME.DefaultView;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en consulta de datos espaciales:\n\n" + ex.Message);
            }
        }
        
        private void dgd_base_tematicoCAT_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgd_tematicoCAT.Columns[0].MinWidth = 35;
                dgd_tematicoCAT.Columns[0].MaxWidth = 35;
                dgd_tematicoCAT.Columns[1].IsReadOnly = true;
                dgd_tematicoCAT.Columns[1].Width = 250;
                dgd_tematicoCAT.Columns[1].MinWidth = 250;
                dgd_tematicoCAT.Columns[1].MaxWidth = 250;
                dgd_tematicoCAT.Columns[1].Header = "Capas de informacion - Catastro";
            }
            catch { }
        }
        private void dgd_base_tematicoDGR_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgd_tematicoDGR.Columns[0].MinWidth = 35;
                dgd_tematicoDGR.Columns[0].MaxWidth = 35;
            }
            catch { }
        }

        private void dgd_base_tematicoDRME_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgd_tematicoDRME.Columns[0].MinWidth = 35;
                dgd_tematicoDRME.Columns[0].MaxWidth = 35;
            }
            catch { }
        }
        private void dgd_base_tematicoDGAR_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgd_tematicoDGAR.Columns[0].MinWidth = 35;
                dgd_tematicoDGAR.Columns[0].MaxWidth = 35;
            }
            catch (Exception ex) { }
        }
       
        // metodo referenciado para el evento checked del checkBox del dataGrid
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

        public async Task AddLayersBase(string pathLyrBase)
        {
            await QueuedTask.Run(() =>
            {
                try
                {
                    if (!File.Exists(pathLyrBase))
                    {
                        throw new FileNotFoundException("El archivo .lyr no existe.");
                    }
                    Map activeMap = MapView.Active.Map;
                    // Cargar el archivo .lyr como una capa en el mapa activo
                    Layer layerBase = LayerFactory.Instance.CreateLayer(new Uri(pathLyrBase), activeMap, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Agregue un Mapa al Proyecto \n" + ex);
                }
            });
        }
        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoCAT.CurrentItem;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();            
            var listaCatastro = SearchLayersInDatoIGN.GetFileNamesFromFolder(System.IO.Path.Combine(fileLayers, "TEMATICOS"));
            string LayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaCatastro);          
            _ = AddLayersBase(LayerOk);
        }

        void UnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoCAT.CurrentItem;
            Map activeMap = MapView.Active.Map;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaTOC  = activeMap.GetLayersAsFlattenedList().Select(layer=> layer.Name).ToList();
            string nameLayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaTOC);
            var LayerOk = activeMap.FindLayers(nameLayerOk).FirstOrDefault();
            removeLayer(LayerOk);
        }

        void OnCheckedDGAR(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoDGAR.CurrentItem;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaCatastro = SearchLayersInDatoIGN.GetFileNamesFromFolder(System.IO.Path.Combine(fileLayers, "TEMATICOS"));
            string LayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaCatastro);            
            _ = AddLayersBase(LayerOk);
        }
        void UnCheckedDGAR(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoDGAR.CurrentItem;
            Map activeMap = MapView.Active.Map;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaTOC = activeMap.GetLayersAsFlattenedList().Select(layer => layer.Name).ToList();
            string nameLayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaTOC);
            var LayerOk = activeMap.FindLayers(nameLayerOk).FirstOrDefault();
            removeLayer(LayerOk);
        }

        void OnCheckedDGR(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoDGR.CurrentItem;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();           
            var listaCatastro = SearchLayersInDatoIGN.GetFileNamesFromFolder(System.IO.Path.Combine(fileLayers, "TEMATICOS"));
            string LayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaCatastro);            
            _ = AddLayersBase(LayerOk);
        }
        void UnCheckedDGR(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoDGR.CurrentItem;
            Map activeMap = MapView.Active.Map;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaTOC = activeMap.GetLayersAsFlattenedList().Select(layer => layer.Name).ToList();
            string nameLayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaTOC);
            var LayerOk = activeMap.FindLayers(nameLayerOk).FirstOrDefault();
            removeLayer(LayerOk);
        }

        void OnCheckedDRME(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoDRME.CurrentItem;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();            
            var listaCatastro = SearchLayersInDatoIGN.GetFileNamesFromFolder(System.IO.Path.Combine(fileLayers, "TEMATICOS"));
            string LayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaCatastro);           
            _ = AddLayersBase(LayerOk);
        }
        void UnCheckedDRME(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_tematicoDRME.CurrentItem;
            Map activeMap = MapView.Active.Map;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaTOC = activeMap.GetLayersAsFlattenedList().Select(layer => layer.Name).ToList();
            string nameLayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaTOC);
            var LayerOk = activeMap.FindLayers(nameLayerOk).FirstOrDefault();
            removeLayer(LayerOk);
        }

        public async void removeLayer(Layer layer)
        {
            await QueuedTask.Run(() =>
            {
                Map activeMap = MapView.Active.Map;
                activeMap.RemoveLayer(layer);
            });
        }
    }
}
