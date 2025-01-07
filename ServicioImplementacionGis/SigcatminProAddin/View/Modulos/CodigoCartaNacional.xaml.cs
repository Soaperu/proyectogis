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

using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Core.Geometry;
using System.Collections.ObjectModel;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using DevExpress.Xpo.Helpers;


namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para CodigoCartaNacional.xaml
    /// </summary>
    public partial class CodigoCartaNacional : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;
        string QueryCarta = "";
        string loCartaIGN = "";

        public CodigoCartaNacional()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfigureDataGridResultColumns();
            ConfigureDataGridDetailsColumns();
            dataBaseHandler = new DatabaseHandler();
            CbxTypeConsult.SelectedIndex = 0;
            TbxRadio.Text = "5";
            BtnGraficar.IsEnabled = true;

        }

        private void ConfigureDataGridDetailsColumns()
        {
            var tableView = DataGridDetails.View as DevExpress.Xpf.Grid.TableView;
            DataGridDetails.Columns.Clear();
            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            GridColumn verticeColumn = new GridColumn
            {
                FieldName = DatagridDetailsConstants.ColumnNames.Vertice,
                Header = DatagridDetailsConstants.Headers.Vertice,
                Width = DatagridDetailsConstants.Widths.VerticeWidth
            };

            GridColumn esteColumn = new GridColumn
            {
                FieldName = DatagridDetailsConstants.ColumnNames.Este,
                Header = DatagridDetailsConstants.Headers.Este,
                Width = new GridColumnWidth(DatagridDetailsConstants.Widths.StarWidthRatio, GridColumnUnitType.Star)
            };

            GridColumn norteColumn = new GridColumn
            {
                FieldName = DatagridDetailsConstants.ColumnNames.Norte,
                Header = DatagridDetailsConstants.Headers.Norte,
                Width = new GridColumnWidth(DatagridDetailsConstants.Widths.StarWidthRatio, GridColumnUnitType.Star)
            };

            // Agregar columnas al GridControl
            DataGridDetails.Columns.Add(verticeColumn);
            DataGridDetails.Columns.Add(esteColumn);
            DataGridDetails.Columns.Add(norteColumn);

        }
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
            GridColumn indexCdHoja = new GridColumn
            {
                Header = DatagridResultConstants.HeadersCarta.CdHoja, // Encabezado
                FieldName = DatagridResultConstants.ColumNamesCarta.CdHoja,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstants.WidthsCarta.CdHoja
            };

            GridColumn indexNmHoja = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.NmHoja, // Nombre del campo en el DataTable
                Header = DatagridResultConstants.HeadersCarta.NmHoja,    // Encabezado visible
                Width = DatagridResultConstants.WidthsCarta.NmHoja            // Ancho de la columna
            };

            GridColumn indexZona = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.Zona,
                Header = DatagridResultConstants.HeadersCarta.Zona,
                Width = DatagridResultConstants.WidthsCarta.Zona
            };

            GridColumn indexEsteMin = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.EsteMin,
                Header = DatagridResultConstants.HeadersCarta.EsteMin,
                Width = DatagridResultConstants.WidthsCarta.EsteMin
            };

            GridColumn indexEsteMax = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.EsteMax,
                Header = DatagridResultConstants.HeadersCarta.EsteMax,
                Width = DatagridResultConstants.WidthsCarta.EsteMax
            };
            GridColumn indexNorteMin = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.NorteMin,
                Header = DatagridResultConstants.HeadersCarta.NorteMin,
                Width = DatagridResultConstants.WidthsCarta.NorteMin
            };
            GridColumn indexNorteMax = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.NorteMax,
                Header = DatagridResultConstants.HeadersCarta.NorteMax,
                Width = DatagridResultConstants.WidthsCarta.NorteMax
            };


            // Agregar columnas al GridControl
            DataGridResult.Columns.Add(indexCdHoja);
            DataGridResult.Columns.Add(indexNmHoja);
            DataGridResult.Columns.Add(indexZona);
            DataGridResult.Columns.Add(indexEsteMin);
            DataGridResult.Columns.Add(indexEsteMax);
            DataGridResult.Columns.Add(indexNorteMin);
            DataGridResult.Columns.Add(indexNorteMax);
            //DataGridResult.Columns.Add(naturalezaColumn);
            //DataGridResult.Columns.Add(cartaColumn);
            //DataGridResult.Columns.Add(hectareaColumn);

        }


        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            CbxSistema.SelectedIndex = 0;
            CbxTypeConsult.SelectedIndex = 0;
            CbxZona.SelectedIndex = 1;
            BtnGraficar.IsEnabled = false;

        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }

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

        private void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbxValue.Clear();
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
        private void ClearCanvas()
        {
            PolygonCanvas.Children.Clear();
        }

        private void ClearDatagrids()
        {
            //DataGridResult.ItemsSource = null;
            DataGridDetails.ItemsSource = null;
        }

        public void ClearControls()
        {
            var functions = new PageCommonFunctions();
            functions.ClearControls(this);
            ClearCanvas();
            ClearDatagrids();
        }


        private void TbxRadio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbxRadio_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {

        }

        private void TbxValue_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TbxRadio_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DataGridResultTableView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

        }

        private void DataGridResult_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {

        }

              
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Ingrese código de la hoja.",
                                                                   MessageConstants.Titles.Error,
                                                                   MessageBoxButton.OK,
                                                                   MessageBoxImage.Warning);
                return;
            }

            string valor = TbxValue.Text.TrimEnd().ToUpper();
            //listBoxVertices.Items.Add(valor);
            BtnGraficar.IsEnabled = true;


            try
            {
                QueryCarta =  "'" + valor + "'";
                string value = (string)CbxTypeConsult.SelectedValue.ToString();
                if (value == "1")
                {
                    value = "CODIGO";
                }
                else if(value=="2")
                {
                    value = "NOMBRE";
                }
                string datumStr = CbxSistema.Text;
                var dmrRecords = dataBaseHandler.GetOfficialCartaIn(value, QueryCarta, datumStr);
                //calculatedIndex(DataGridResult, dmrRecords, DatagridResultConstants.ColumNames.Index);
                DataGridResult.ItemsSource = dmrRecords.DefaultView;
                int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
                string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
                CbxZona.SelectedValue = zoneDm;
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format(MessageConstants.Errors.UnexpectedError, ex.Message),
                                                                    MessageConstants.Titles.Error,
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Error);
                DataGridResult.ItemsSource = null;
                return;
            }

            TbxValue.Text = "";
        }

        private void CbxTypeConsult_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("Por Código", 1));
            cbp.Add(new ComboBoxPairs("Por Nombre", 2));

            // Asignar la lista al ComboBox
            CbxTypeConsult.DisplayMemberPath = "_Key";
            CbxTypeConsult.SelectedValuePath = "_Value";
            CbxTypeConsult.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxTypeConsult.SelectedIndex = 0;
        }


    }
}
