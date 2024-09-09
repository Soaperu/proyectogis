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
    /// Lógica de interacción para BDGeocientifica.xaml
    /// </summary>
    public partial class geocatmin_BDGeocientifica : Page
    {
        public geocatmin_BDGeocientifica()
        {
            InitializeComponent();
            DataTable dt = accesoDB.ConsultaDB("MAPA-BDGEOCIENTIFICA", "DESCRIPCION");
            dgd_BDGeocientifica.Dispatcher.Invoke(() =>
            {
                dgd_BDGeocientifica.ItemsSource = dt.DefaultView;
            });
        }

        private void dgd_BDGeocientifica_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgd_BDGeocientifica.Columns[0].MinWidth = 35;
                dgd_BDGeocientifica.Columns[0].MaxWidth = 35;
                dgd_BDGeocientifica.Columns[1].IsReadOnly = true;
                dgd_BDGeocientifica.Columns[1].Width = 250;
                dgd_BDGeocientifica.Columns[1].MinWidth = 250;
                dgd_BDGeocientifica.Columns[1].MaxWidth = 250;
                dgd_BDGeocientifica.Columns[1].Header = "Capas BDGeocientifica";
            }
            catch { }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Código para manejar el evento cuando la casilla de verificación está marcada
            CheckBox checkBox = (CheckBox)sender;

        }
        // metodo referenciado para el evento unchecked del checkBox del dataGrid
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Código para manejar el evento cuando la casilla de verificación está marcada
            CheckBox checkBox = (CheckBox)sender;
        }

        void OnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_BDGeocientifica.CurrentItem;
            var preNameLayer = selectRow.Row.ItemArray;
            string nameLayer = preNameLayer[0].ToString();
            var listaCatastro = SearchLayersInDatoIGN.GetFileNamesFromFolder(System.IO.Path.Combine(fileLayers, "GEOLOGIA"));
            string LayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(nameLayer, listaCatastro);
            geocatmin_tematicos tematicos = new geocatmin_tematicos();
            _ = tematicos.AddLayersBase(LayerOk);
        }

        void UnChecked(object sender, RoutedEventArgs e)
        {
            DataRowView selectRow = (DataRowView)dgd_BDGeocientifica.CurrentItem;
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
