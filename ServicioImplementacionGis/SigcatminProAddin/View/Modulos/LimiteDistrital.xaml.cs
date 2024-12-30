using CommonUtilities;
using DatabaseConnector;
using DevExpress.Xpf.Grid;
using SigcatminProAddin.Models.Constants;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SigcatminProAddin.View.Modulos.EvaluacionDM;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para LimiteDistrital.xaml
    /// </summary>
    public partial class LimiteDistrital : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        private string ubigeo;
        private enum DemarcacionType
        {
            Departamento = 1,
            Provincia = 2,
            Distrito = 3
        };
        private void ConfigureDataGridResultColumns()
        {
            // Obtener la vista principal del GridControl
            var tableView = DataGridResult.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DataGridResult.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn indexColumn = new GridColumn
            {
                Header = DataGridResultsDistritoConstants.Headers.Index, // Encabezado
                FieldName = DataGridResultsDistritoConstants.ColumNames.Index,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DataGridResultsDistritoConstants.Widths.Index
            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DataGridResultsDistritoConstants.ColumNames.Codigo, // Nombre del campo en el DataTable
                Header = DataGridResultsDistritoConstants.Headers.Codigo,    // Encabezado visible
                Width = DataGridResultsDistritoConstants.Widths.Codigo            // Ancho de la columna
            };

            GridColumn departamentoColumn = new GridColumn
            {
                FieldName = DataGridResultsDistritoConstants.ColumNames.Departamento,
                Header = DataGridResultsDistritoConstants.Headers.Departamento,
                Width = DataGridResultsDistritoConstants.Widths.Departamento
            };

            GridColumn provinciaColumn = new GridColumn
            {
                FieldName = DataGridResultsDistritoConstants.ColumNames.Provincia,
                Header = DataGridResultsDistritoConstants.Headers.Provincia,
                Width = DataGridResultsDistritoConstants.Widths.Provincia
            };

            GridColumn distritoColumn = new GridColumn
            {
                FieldName = DataGridResultsDistritoConstants.ColumNames.Distrito,
                Header = DataGridResultsDistritoConstants.Headers.Distrito,
                Width = DataGridResultsDistritoConstants.Widths.Distrito
            };
            GridColumn capitalDistritalColumn = new GridColumn
            {
                FieldName = DataGridResultsDistritoConstants.ColumNames.Capital,
                Header = DataGridResultsDistritoConstants.Headers.Capital,
                Width = DataGridResultsDistritoConstants.Widths.Capital
            };
            GridColumn zonaColumn = new GridColumn
            {
                FieldName = DataGridResultsDistritoConstants.ColumNames.Zona,
                Header = DataGridResultsDistritoConstants.Headers.Zona,
                Width = DataGridResultsDistritoConstants.Widths.Zona
            };
            GridColumn ubigeoColumn = new GridColumn
            {
                FieldName = DataGridResultsDistritoConstants.ColumNames.Ubigeo,
                Header = DataGridResultsDistritoConstants.Headers.Ubigeo,
                Width = DataGridResultsDistritoConstants.Widths.Ubigeo
            };


            // Agregar columnas al GridControl
            DataGridResult.Columns.Add(codigoColumn);
            DataGridResult.Columns.Add(departamentoColumn);
            DataGridResult.Columns.Add(provinciaColumn);
            DataGridResult.Columns.Add(distritoColumn);
            DataGridResult.Columns.Add(capitalDistritalColumn);
            DataGridResult.Columns.Add(ubigeoColumn);

        }
        public LimiteDistrital()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfigureDataGridResultColumns();



            dataBaseHandler = new DatabaseHandler();
            CbxTypeConsult.SelectedIndex = 0;
            TbxRadio.Text = "5";
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
        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("WGS-84", 1));
            cbp.Add(new ComboBoxPairs("PSAD-56", 2));

            // Asignar la lista al ComboBox
            CbxSistema.DisplayMemberPath = "_Key";
            CbxSistema.SelectedValuePath = "_Value";
            CbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxSistema.SelectedIndex = 0;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var dmrRecords = dataBaseHandler.GetDemarcacion((int)DemarcacionType.Distrito, TbxValue.Text.ToUpper());

                var countRecords = dmrRecords.Rows.Count;
                int records = countRecords;
                if (records == 0)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format(MessageConstants.Errors.NoRecordsFound, TbxValue.Text),
                                                                    MessageConstants.Titles.NoMatches,
                                                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (records >= 150)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format(MessageConstants.Errors.TooManyMatches, countRecords),
                                                                    MessageConstants.Titles.HighMatchLevel,
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Warning);
                    return;
                }
                LblCountRecords.Content = $"Resultados de Búsqueda: {countRecords.ToString()}";
                DataGridResult.ItemsSource = dmrRecords.DefaultView;
                BtnGraficar.IsEnabled = true;

                ubigeo = dmrRecords.Rows[0]["UBIGEO"].ToString();

                var dtZona = dataBaseHandler.GetZonasporUbigeo(ubigeo);
                if (dtZona.Rows.Count == 2)
                {
                    TxtZonaAlerta.Visibility = Visibility.Hidden;
                    CbxZona.ItemsSource = dtZona.DefaultView;
                    CbxZona.DisplayMemberPath = "DESCRIPCION";
                    CbxZona.SelectedValuePath = "CODIGO";
                    CbxZona.SelectedIndex = 1;
                }
                else if (dtZona.Rows.Count == 3)
                {
                    CbxZona.ItemsSource = dtZona.DefaultView;
                    CbxZona.DisplayMemberPath = "DESCRIPCION";
                    CbxZona.SelectedValuePath = "CODIGO";
                    CbxZona.SelectedIndex = 0;
                    TxtZonaAlerta.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format(MessageConstants.Errors.UnexpectedError, ex.Message),
                                                                    MessageConstants.Titles.Error,
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Error);
                return;
            }
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
