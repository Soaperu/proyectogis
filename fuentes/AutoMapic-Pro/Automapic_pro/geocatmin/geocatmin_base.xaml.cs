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

namespace Automapic_pro.modulos
{
    /// <summary>
    /// Lógica de interacción para geocatmin_base.xaml
    /// </summary>
    public partial class geocatmin_base : Page
    {
        public geocatmin_base()
        {
            InitializeComponent();
            DataTable dt = accesoDB.ConsultaDB("MAPA-BASE", "DESCRIPCION");
            dgd_base.Dispatcher.Invoke(() =>
            {
                dgd_base.ItemsSource = dt.DefaultView;
            });

        }
        private void dgd_base_Loaded(object sender, RoutedEventArgs e)
        {
            //DataTable dt = accesoDB.ConsultaDB("MAPA-BASE");
            //dgd_base.Dispatcher.Invoke(() =>
            //{
            //    dgd_base.ItemsSource = dt.DefaultView;
            //});
            dgd_base.Columns[0].MinWidth = 35;
            dgd_base.Columns[0].MaxWidth = 35;
            dgd_base.Columns[1].IsReadOnly = true;
            dgd_base.Columns[1].Width = 250;
            dgd_base.Columns[1].MinWidth = 250;
            dgd_base.Columns[1].MaxWidth = 250;
            dgd_base.Columns[1].Header = "Capas de informacion - Capas Base";
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //// Código para manejar el evento cuando la casilla de verificación está marcada
            CheckBox checkBox = (CheckBox)sender;
            //// Accede a los datos o realiza cualquier acción deseada
            //MessageBox.Show("evento check ok");
        }
        void OnChecked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("evento check ok");
            DataRowView selectRow = (DataRowView)dgd_base.CurrentItem;

            var nameLayer = selectRow.Row.ItemArray;
            //MessageBox.Show(nameLayer[0].ToString());
        }

    }
}
