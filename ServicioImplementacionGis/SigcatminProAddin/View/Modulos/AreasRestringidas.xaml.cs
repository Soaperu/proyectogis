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
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;




namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para AreasRestringidas.xaml
    /// </summary>
    public partial class AreasRestringidas : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;

        public AreasRestringidas()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfigureDataGridResultColumns();
            ConfigureDataGridDetailsColumns();
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
            if (CbxSistema.SelectedValue.ToString() == "2")
            {
                GlobalVariables.CurrentDatumDm = "2";
            }
            else
            {
                GlobalVariables.CurrentDatumDm = "1";
            }
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

        private void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbxValue.Clear();
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
            //GridColumn indexColumn = new GridColumn
            //{
            //    Header = DatagridResultConstants.Headers.Index, // Encabezado
            //    FieldName = DatagridResultConstants.ColumNames.Index,
            //    UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
            //    AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
            //    VisibleIndex = 0, // Mostrar como la primera columna
            //    Width = DatagridResultConstants.Widths.Index
            //};

            GridColumn codigoARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.CodigoAR, // Nombre del campo en el DataTable
                Header = DatagridResultConstants.Headers.CodigoAR,    // Encabezado visible
                Width = DatagridResultConstants.Widths.CodigoAR            // Ancho de la columna
            };

            GridColumn nombreARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.NombreAR,
                Header = DatagridResultConstants.Headers.NombreAR,
                Width = DatagridResultConstants.Widths.NombreAR
            };

            GridColumn zonaARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.ZonaAR,
                Header = DatagridResultConstants.Headers.ZonaAR,
                Width = DatagridResultConstants.Widths.ZonaAR
            };

            GridColumn DescriARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.DescriAR,
                Header = DatagridResultConstants.Headers.DescriAR,
                Width = DatagridResultConstants.Widths.DescriAR
            };
            GridColumn DestipARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.DestipAR,
                Header = DatagridResultConstants.Headers.DestipAR,
                Width = DatagridResultConstants.Widths.DestipAR
            };
            GridColumn DescatARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.DescatAR,
                Header = DatagridResultConstants.Headers.DescatAR,
                Width = DatagridResultConstants.Widths.DescatAR
            };
            GridColumn SituexARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.SituexAR,
                Header = DatagridResultConstants.Headers.SituexAR,
                Width = DatagridResultConstants.Widths.SituexAR
            };
            //GridColumn cartaColumn = new GridColumn
            //{
            //    FieldName = DatagridResultConstants.ColumNames.Carta,
            //    Header = DatagridResultConstants.Headers.Carta,
            //    Width = DatagridResultConstants.Widths.Carta
            //};
            //GridColumn hectareaColumn = new GridColumn
            //{
            //    FieldName = DatagridResultConstants.ColumNames.Hectarea,
            //    Header = DatagridResultConstants.Headers.Hectarea,
            //    Width = DatagridResultConstants.Widths.Hectarea
            //};

            // Agregar columnas al GridControl
            //DataGridResult.Columns.Add(indexColumn);
            DataGridResult.Columns.Add(codigoARColumn);
            DataGridResult.Columns.Add(nombreARColumn);
            DataGridResult.Columns.Add(zonaARColumn);
            DataGridResult.Columns.Add(DescriARColumn);
            DataGridResult.Columns.Add(DestipARColumn);
            DataGridResult.Columns.Add(DescatARColumn);
            DataGridResult.Columns.Add(SituexARColumn);

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

        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            CbxSistema.SelectedIndex = 0;
            CbxTypeConsult.SelectedIndex = 0;
            CbxZona.SelectedIndex = 1;
            BtnGraficar.IsEnabled = false;
        }

        private void ClearCanvas()
        {
            PolygonCanvas.Children.Clear();
        }

        private void ClearDatagrids()
        {
            DataGridResult.ItemsSource = null;
            DataGridDetails.ItemsSource = null;
        }

        public void ClearControls()
        {
            var functions = new PageCommonFunctions();
            functions.ClearControls(this);
            ClearCanvas();
            ClearDatagrids();
        }

        private void calculatedIndex(GridControl table, int records, string columnName)
        {
            int newRowHandle = DataControlBase.NewItemRowHandle;

            // Agregar registros de ejemplo con índice
            for (int i = 1; i <= records; i++)
            {
                table.SetCellValue(newRowHandle, columnName, i);
            }
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
            string value = (string)CbxTypeConsult.SelectedValue.ToString();
            try
            {
                if (value == "1")
                {
                    value = "CODIGO";
                }
                else if (value == "2")
                {
                    value = "NOMBRE";
                }
                var countRecords = dataBaseHandler.CountRecordsAreaRestringida(value, TbxValue.Text.ToUpper());
                int records = int.Parse(countRecords);
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
                var dmrRecords = dataBaseHandler.GetUniqueAresReserva(value, TbxValue.Text.ToUpper());
                calculatedIndex(DataGridResult, records, DatagridResultConstants.ColumNames.Index);
                DataGridResult.ItemsSource = dmrRecords.DefaultView;
                BtnGraficar.IsEnabled = true;
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

        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {

            /*-------------------------*/

            BtnGraficar.IsEnabled = false;
            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                //MessageBox.Show("Por favor ingrese el usuario y la contraseña.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                string message = "Por favor, ingrese un valor de radio";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertencia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                BtnGraficar.IsEnabled = true;
                return;
            }
            if (ChkGraficarDmY.IsChecked == true)
            {
                GlobalVariables.stateDmY = true;
            }
            else
            {
                GlobalVariables.stateDmY = false;
            }
            int datum = (int)CbxSistema.SelectedValue;
            string datumStr = CbxSistema.Text;

            string fechaArchi = DateTime.Now.Ticks.ToString();
            string catastroShpName = "Catastro" + fechaArchi;
            string dmShpName = "DM" + fechaArchi;
            string dmShpNamePath = "DM" + fechaArchi + ".shp";

            string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
            var clase_sele = "";

            int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
            string cod_rese = DataGridResult.GetCellValue(focusedRowHandle, "CG_CODIGO")?.ToString();
            string lostrZona = DataGridResult.GetCellValue(focusedRowHandle, "ZA_ZONA")?.ToString();
            clase_sele = DataGridResult.GetCellValue(focusedRowHandle, "PA_DESCRI")?.ToString();
            string tip_rese_plano = DataGridResult.GetCellValue(focusedRowHandle, "TN_DESTIP")?.ToString();
            string nom_rese = DataGridResult.GetCellValue(focusedRowHandle, "PE_NOMARE")?.ToString();
            string categori_sele = DataGridResult.GetCellValue(focusedRowHandle, "CA_DESCAT")?.ToString();

            var zoneDm = lostrZona;

            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            //var codigoValue = TbxValue.Text.TrimEnd();
            //string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
            //await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
            //Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCartaIgn);
            //var featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");

            await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
            Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCartaIgn);
            var featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");

            ArcGIS.Core.Geometry.Geometry polygon = null;
            ArcGIS.Core.Geometry.Envelope envelope = null;


            //Carga capa Catastro
            if (datum == datumwgs84)
            {
                await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
            }
            else
            {
                await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
            }

            string cod_opcion = cod_rese.Substring(0, 2);

            var queryClause = "";
            switch (cod_opcion)
            {
                case "ZU":
                    queryClause = $"CODIGO = '{cod_rese}'";
                    if (datum == datumwgs84)
                    {
                        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaWgs84 + zoneDm, false, queryClause);
                    }
                    break;
                default:
                    queryClause = $"CODIGO = '{cod_rese}'";
                    if (datum == datumwgs84)
                    {
                        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_AReservada84 + zoneDm, false, queryClause);
                    }
                    break;
            }
            // Encontrando Distritos superpuestos a DM con
            ArcGIS.Core.Data.QueryFilter filter =
               new ArcGIS.Core.Data.QueryFilter()
               {
                   WhereClause = queryClause
               };
            envelope = featureClassLoader.pFeatureLayer_rese.QueryExtent();

            using (RowCursor rowCursor = featureClassLoader.pFeatureLayer_urba.GetFeatureClass().Search(filter, false))
            {
                while (rowCursor.MoveNext())
                {
                    using (var row = rowCursor.Current)
                    {
                        ArcGIS.Core.Data.Feature feature = row as ArcGIS.Core.Data.Feature;
                        polygon = feature.GetShape();
                    }
                }
            }

            //string listDms = await featureClassLoader.IntersectFeatureClassbyGeometryAsync("Catastro", polygon, catastroShpName);









        }

        private async Task<Map> EnsureMapViewIsActiveAsync(string mapName)
        {
            if (MapView.Active != null)
            {
                return MapView.Active.Map;
            }

            // Esperar hasta que MapView.Active esté disponible
            TaskCompletionSource<Map> tcs = new TaskCompletionSource<Map>();

            SubscriptionToken eventToken = null;
            eventToken = DrawCompleteEvent.Subscribe(async args =>
            {
                // Desuscribirse del evento
                // Desuscribirse del evento
                if (eventToken != null)
                {
                    DrawCompleteEvent.Unsubscribe(eventToken);
                }
                // Activar el mapa "CATASTRO MINERO"
                Map map = await CommonUtilities.ArcgisProUtils.MapUtils.FindMapByNameAsync(mapName);
                await CommonUtilities.ArcgisProUtils.MapUtils.ActivateMapAsync(map);

                // Completar la tarea con el mapa activo
                //tcs.SetResult(MapView.Active.Map);
                tcs.SetResult(map);
            });

            // Esperar hasta que el evento se complete
            return await tcs.Task;
        }

    }
}
