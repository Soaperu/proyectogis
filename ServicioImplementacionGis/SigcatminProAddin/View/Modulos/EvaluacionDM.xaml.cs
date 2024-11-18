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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using CommonUtilities;
using DatabaseConnector;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Xpf.Grid;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para EvaluacionDM.xaml
    /// </summary>
    public partial class EvaluacionDM : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        public EvaluacionDM()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfigureGridColumns();
            dataBaseHandler = new DatabaseHandler();
        }

        private void AddCheckBoxesToListBox()
        {
            // Lista de elementos para agregar al ListBox
            string[] items = { "Capa 1", "Capa 2", "Capa 3", "Capa 4" };

            // Agrega cada elemento como un CheckBox al ListBox
            foreach (var item in items)
            {
                System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox
                {
                    Content = item,
                    Margin = new Thickness(3),
                    Style = (Style)FindResource("Esri_CheckboxToggleSwitch")
                };
                listLayers.Items.Add(checkBox);
            }
        }

        private void CurrentUser()
        {
            currentUser.Text = GloblalVariables.ToTitleCase(GloblalVariables.currentUser);
        }

        private void cbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string value = (string)cbxTypeConsult.SelectedValue.ToString();
                var countRecords = dataBaseHandler.CountRecords(value, tbxValue.Text);
                if (int.Parse(countRecords) >= 150)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Sea Ud. más específico en la consulta, hay {countRecords} Registro(s)",
                                                                        "Nivel de coincidencia muy alto",
                                                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var dmrRecords = dataBaseHandler.GetUniqueDM(tbxValue.Text, (int)cbxTypeConsult.SelectedValue);
                //DataRow firstRow1 = countRecords.Rows[0];
                //DataRow firstRow2 = dmrRecords.Rows[0];
                dataGridResult.ItemsSource = dmrRecords.DefaultView;
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Se produjo un error inesperado: "+ ex.Message,
                                                                            "Error",
                                                                            MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }

        private void cbxTypeConsult_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("Por Codigo", 1));
            cbp.Add(new ComboBoxPairs("Por Nombre", 2));

            // Asignar la lista al ComboBox
            cbxTypeConsult.DisplayMemberPath = "_Key";
            cbxTypeConsult.SelectedValuePath = "_Value";
            cbxTypeConsult.ItemsSource = cbp;
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

        private void ConfigureGridColumns()
        {
            // Obtener la vista principal del GridControl
            var tableView = dataGridResult.View as TableView;

            // Limpiar columnas existentes
            dataGridResult.Columns.Clear();
            
            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas
            GridColumn codigoColumn = new GridColumn
            {
                FieldName = "CODIGO", // Nombre del campo en el DataTable
                Header = "Codigo",    // Encabezado visible
                Width = 120            // Ancho de la columna
            };

            GridColumn nombreColumn = new GridColumn
            {
                FieldName = "NOMBRE",
                Header = "Nombre",
                Width = 120
            };

            GridColumn pe_vigcatColumn = new GridColumn
            {
                FieldName = "PE_VIGCAT",
                Header = "PE_VIGCAT",
                Width = 100
            };

            GridColumn zonaColumn = new GridColumn
            {
                FieldName = "ZONA",
                Header = "Zona",
                Width = 50
            };
            GridColumn tipoColumn = new GridColumn
            {
                FieldName = "TIPO",
                Header = "Tipo",
                Width = 100
            };
            GridColumn estadoColumn = new GridColumn
            {
                FieldName = "ESTADO",
                Header = "Estado",
                Width = 100
            };
            GridColumn naturalezaColumn = new GridColumn
            {
                FieldName = "NATURALEZA",
                Header = "Naturaleza",
                Width = 100
            };
            GridColumn cartaColumn = new GridColumn
            {
                FieldName = "CARTA",
                Header = "Carta",
                Width = 100
            };
            GridColumn hectareaColumn = new GridColumn
            {
                FieldName = "HECTAREA",
                Header = "Hectarea",
                Width = 100
            };

            // Agregar columnas al GridControl
            dataGridResult.Columns.Add(codigoColumn);
            dataGridResult.Columns.Add(nombreColumn);
            dataGridResult.Columns.Add(pe_vigcatColumn);
            dataGridResult.Columns.Add(zonaColumn);
            dataGridResult.Columns.Add(tipoColumn);
            dataGridResult.Columns.Add(estadoColumn);
            dataGridResult.Columns.Add(naturalezaColumn);
            dataGridResult.Columns.Add(cartaColumn);
            dataGridResult.Columns.Add(hectareaColumn);
        }
    }
}
