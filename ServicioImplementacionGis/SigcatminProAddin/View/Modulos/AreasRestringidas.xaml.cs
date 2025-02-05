using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
//using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
//using System.Windows.Media;
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
//using System.Text.RegularExpressions;
//using SigcatminProAddin.Models;
using SigcatminProAddin.Models.Constants;
//using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
//using ArcGIS.Core.CIM;
//using DevExpress.Xpf.Grid.GroupRowLayout;
//using ArcGIS.Core.Data.UtilityNetwork.Trace;
//using DevExpress.CodeParser;
//using DevExpress.DataProcessing.InMemoryDataProcessor;
//using System.Windows.Markup.Localizer;
//using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Editors.Settings;
using System.Security.Policy;
using SigcatminProAddin.View.Interfaces;
using DevExpress.XtraCharts.Native;
//using ArcGIS.Core.Internal.CIM;
//using System.Collections.ObjectModel;
//using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
//using System.Windows.Media.Imaging;
//using ArcGIS.Core.Internal.CIM;
//using System.Windows.Forms;
//using DevExpress.Xpf.Editors.Settings;



namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para AreasRestringidas.xaml
    /// </summary>
    public partial class AreasRestringidas : Page, ITitledPage
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;

        //public ObservableCollection<MyData> MyDataCollection { get; set; }

        public AreasRestringidas()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfigureDataGridResultColumns();
            ConfigureDataGridDetailsColumns();
            dataBaseHandler = new DatabaseHandler();
            CbxTypeConsult.SelectedIndex = 2;
            TbxRadio.Text = "5";

        }

        public void SetPageTitle(string title)
        {
            if (GlobalVariables.currentModule == "Plano Áreas Restringidas")
            {
                CurrentTittleModule.Text = "Planos | " + title;  // Cambia el contenido del TextBlock en la página
            }
            else
            {
                CurrentTittleModule.Text = "Consulta | " + title;
            }
        }
        private void AddCheckBoxesToListBox()
        {
            string[] items = {
                                //"Caram"
                                "Catastro Forestal",
                                //"Predio Rural",
                                //"Limite Departamental",
                                //"Limite Provincial",
                                //"Limite Distrital",
                                //"Centros Poblados",
                                //"Red Hidrografica",
                                //"Red Vial"
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
            GlobalVariables.currentUser = CurrentUserLabel.Text;
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
            if (CbxSistema.SelectedValue?.ToString() == "2")
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
            CbxTypeConsult.SelectedIndex = 1;
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
                //tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
                tableView.AllowEditing = true; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas
            GridColumn chkColumn = new GridColumn
            {
                FieldName = "FLG_SEL",
                Header = "Select",
                Width = 50,
                EditSettings = new CheckEditSettings
                {
                    
                    //TrueValue = true,        // Valor asignado cuando está marcado
                    //FalseValue = false,      // Valor asignado cuando está desmarcado
                    IsThreeState = true,     // Permite un estado indeterminado (null)
                    AllowNullInput = true,   // Permite valores nulos (indeterminado)
                    //ValueType = typeof(bool) // El tipo de valor es booleano

                }

            };

            GridColumn codigoARColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.CodigoAR, // Nombre del campo en el DataTable
                Header = DatagridResultConstants.Headers.CodigoAR,    // Encabezado visible
                Width = DatagridResultConstants.Widths.CodigoAR,            // Ancho de la columna
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

            // Agregar columnas al GridControl
            //DataGridResult.Columns.Add(indexColumn);
            DataGridResult.Columns.Add(chkColumn);
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
                if (countRecords == "null")
                {
                    string message = "No existe ningún registro con esta consulta.";
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                     "Advertencia",
                                                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
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
            string listDms = "";
            

            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                //MessageBox.Show("Por favor ingrese el usuario y la contraseña.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                string message = "Por favor ingrese un valor de Áreas Restringidas";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertencia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                BtnGraficar.IsEnabled = true;
                return;
            }
            ProgressBarUtils progressBar = new ProgressBarUtils($"Graficando {GlobalVariables.currentModule}...");
            progressBar.Show();
            if (ChkGraficarDmY.IsChecked == true)
            {
                GlobalVariables.stateDmY = true;
            }
            else
            {
                GlobalVariables.stateDmY = false;
            }

            try
            {
                var verificaCodigo = "";
                string previousCodigo = null; // Variable para almacenar el código anterior
                if (DataGridResult.ItemsSource is DataView dataView1)
                {
                    foreach (DataRowView row in dataView1)
                    {
                        var isChecked = row["FLG_SEL"].ToString();
                        if (isChecked == "1")
                        {
                            string currentCodigo = row["CG_CODIGO"].ToString(); // Obtener el código actual
                            string currentClase = row["PA_DESCRI"].ToString(); // Obtener la clase actual

                            // Verificar si estamos en la primera fila seleccionada o si el código es el mismo que el anterior
                            if (previousCodigo == null || currentCodigo == previousCodigo)
                            {
                                // Si es la primera fila o el código es el mismo, concatenamos con el filtro
                                if (verificaCodigo.Length > 0)
                                {
                                    verificaCodigo += " AND "; // Añadir un "AND" si ya hay condiciones previas
                                }
                                verificaCodigo += "CODIGO = '" + currentCodigo + "' AND CLASE = '" + currentClase + "'";

                                // Actualizamos el código previo
                                previousCodigo = currentCodigo;
                            }
                            else
                            {
                                string message = "Usted está seleccionando diferentes areas, debe seleccionar el mismo código de área";
                                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                                 "Advertencia",
                                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                                progressBar.Dispose();
                                return;
                            }

                        }

                    }

                }
                else
                {
                    MessageBox.Show("La fuente de datos no es compatible con DataView.");
                    progressBar.Dispose();
                    return;
                }

                /**/


                List<string> mapsToDelete = new List<string>()
                {
                GlobalVariables.mapNameCatastro,
                };
                await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);

                int datum = (int)CbxSistema.SelectedValue;
                string datumStr = CbxSistema.Text;
                GlobalVariables.CurrentDatumStrDm = CbxSistema.Text;
                string fechaArchi = DateTime.Now.Ticks.ToString();
                GlobalVariables.idExport = fechaArchi;
                string catastroShpName = "Catastro" + fechaArchi;
                string dmShpName = "DM" + fechaArchi;
                string dmShpNamePath = "DM" + fechaArchi + ".shp";

                string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
                var clase_sele = "";

                int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
                string cod_rese = DataGridResult.GetCellValue(focusedRowHandle, "CG_CODIGO")?.ToString();
                string lostrZona = DataGridResult.GetCellValue(focusedRowHandle, "ZA_ZONA")?.ToString();
                GlobalVariables.CurrentZoneDm = lostrZona;
                clase_sele = DataGridResult.GetCellValue(focusedRowHandle, "PA_DESCRI")?.ToString();
                string tip_rese_plano = DataGridResult.GetCellValue(focusedRowHandle, "TN_DESTIP")?.ToString();
                string nom_rese = DataGridResult.GetCellValue(focusedRowHandle, "PE_NOMARE")?.ToString();
                GlobalVariables.CurrentNameDm = nom_rese;
                string categori_sele = DataGridResult.GetCellValue(focusedRowHandle, "CA_DESCAT")?.ToString();
                var zoneDm = lostrZona;

                var queryClause = "";
                var cod_opcion = cod_rese.Substring(0, 2);

                if (cod_opcion == "U1")
                    cod_opcion = "ZU";

                switch (cod_opcion)
                {
                    case "ZU": // Zona Urbana
                        queryClause = $"CODIGO = '{cod_rese}'";
                        break;

                    default: //Otros Casos
                        var claseRes = "";
                        var contador = 0;
                        var claseOne = "";

                        if (DataGridResult.ItemsSource is DataView dataView)
                        {
                            foreach (DataRowView row in dataView)
                            {
                                // Acceder a la columna CheckBox
                                var isChecked = row["FLG_SEL"].ToString();
                                if (isChecked == "1")
                                {
                                    contador++;
                                    claseOne = "CODIGO = '" + row["CG_CODIGO"].ToString() + "'";
                                    claseRes = claseRes + "CODIGO = '" + row["CG_CODIGO"].ToString() + "' and CLASE = '" + row["PA_DESCRI"].ToString() + "' OR ";
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("La fuente de datos no es compatible con DataView.");
                        }
                        if (contador == 1 && cod_opcion != "AN")
                        {
                            queryClause = claseOne;
                        }
                        else
                        {
                            claseRes = claseRes.Substring(0, claseRes.Length - 4);
                            queryClause = claseRes;
                        }
                        break;
                };

                var sdeHelper = new SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);

                await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro); //"CARTA IGN"
                Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro);
                var featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");

                ArcGIS.Core.Geometry.Geometry polygon = null;
                ArcGIS.Core.Geometry.Envelope envelope = null;
                string listDist = "";
                string listProv = "";
                string listDep = "";
                await QueuedTask.Run(async () =>
                {
                    if (datum == 2)
                    {
                        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                    }
                    else
                    {
                        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                    }

                    if (datum == 2) //WGS84
                    {
                        switch (cod_opcion)
                        {
                            case "ZU":
                                await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaWgs84 + zoneDm, false, queryClause);
                                break;

                            default:
                                await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_AReservada84 + zoneDm, false, queryClause);
                                break;

                        }
                    }

                    /*************************************/
                    //caso Caram
                    if (datum == int.Parse(GlobalVariables.CurrentDatumDm))
                    {
                        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Caram84 + zoneDm, true, queryClause);
                    }
                    else
                    {
                        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Caram56 + zoneDm, true, queryClause);
                    }

                    /*************************************/
                    if (GlobalVariables.currentModule == "Plano Áreas Restringidas")
                    {
                        // Distritos 
                        if (datum == int.Parse(GlobalVariables.CurrentDatumDm))
                        {
                            await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_WGS + zoneDm, false);
                        }
                        else
                        {
                            await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_Z + zoneDm, false);
                        }
                    }
                    
                    // Encontrando Distritos superpuestos a DM con
                    QueryFilter filter =
                       new QueryFilter()
                       {
                           WhereClause = queryClause
                       };

                    switch (cod_opcion)
                    {
                        case "ZU":
                            envelope = featureClassLoader.pFeatureLayer_urba.QueryExtent();
                            using (RowCursor rowCursor = featureClassLoader.pFeatureLayer_urba.GetFeatureClass().Search(filter, false))
                            {
                                while (rowCursor.MoveNext())
                                {
                                    using (var row = rowCursor.Current)
                                    {
                                        Feature feature = row as Feature;
                                        polygon = feature.GetShape();
                                    }
                                }
                            }

                            break;
                        case "AN":
                            envelope = featureClassLoader.pFeatureLayer_caram.QueryExtent();
                            using (RowCursor rowCursor = featureClassLoader.pFeatureLayer_caram.GetFeatureClass().Search(filter, false))
                            {
                                while (rowCursor.MoveNext())
                                {
                                    using (var row = rowCursor.Current)
                                    {
                                        Feature feature = row as Feature;
                                        polygon = feature.GetShape();
                                    }
                                }
                            }

                            break;
                        default:
                            envelope = featureClassLoader.pFeatureLayer_rese.QueryExtent();
                            using (RowCursor rowCursor = featureClassLoader.pFeatureLayer_rese.GetFeatureClass().Search(filter, false))
                            {
                                while (rowCursor.MoveNext())
                                {
                                    using (var row = rowCursor.Current)
                                    {
                                        Feature feature = row as Feature;
                                        polygon = feature.GetShape();
                                    }
                                }
                            }
                            break;
                    }
                    listDms = await featureClassLoader.IntersectFeatureClassbyGeometryAsync("Catastro", polygon, catastroShpName);
                    if (!string.IsNullOrEmpty(listDms))
                    {
                    }
                    else
                    {
                        string message = "No existe Derechos Mineros en la consulta seleccionada";
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                         "Advertencia",
                                                                         MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    await FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
                    await UpdateValueAsync(catastroShpName, " ");
                    string styleCat = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCatastro);
                    await SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, styleCat, "LEYENDA", StyleItemType.PolygonSymbol, "");
                    LayerUtils.SelectSetAndZoomByNameAsync(catastroShpName, false);
                    List<string> layersToRemove = new List<string>() { "Catastro", "Carta IGN", "Zona Urbana", "Zona Reservada" };

                    await LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);
                    await LayerUtils.ChangeLayerNameAsync(catastroShpName, "Catastro");
                    GlobalVariables.CurrentShpName = "Catastro";
                    MapUtils.AnnotateLayerbyName("Catastro", "CONTADOR", "DM_Anotaciones");
                    UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                    var (gridLayer, pointLayer) = await uTMGridGenerator.GenerateUTMGridAsync(envelope.XMin, envelope.YMin, envelope.XMax, envelope.YMax, "Malla", zoneDm);
                    await uTMGridGenerator.AnnotateGridLayer(pointLayer, "VALOR");
                    await uTMGridGenerator.RemoveGridLayer("Malla", zoneDm);
                    string styleGrid = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleMalla);
                    await SymbologyUtils.ApplySymbologyFromStyleAsync(gridLayer.Name, styleGrid, "CLASE", StyleItemType.LineSymbol);

                    string layerExportName = "Caram"; /* + GlobalVariables.idExport;*/
                    string styleCaramPath = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCaram);
                    await SymbologyUtils.ApplySymbologyFromStyleAsync(layerExportName, styleCaramPath, "ESTILO", StyleItemType.PolygonSymbol);
                    //await ChangeLayerNameAsync(layerExportName, "Caram");
                });

                if (string.IsNullOrEmpty(listDms))
                {
                    return;
                }
                // Itera todos items seleccionados en el ListBox de WPF
                var extentDmRadio = ObtenerExtent(envelope.XMin, envelope.YMin, envelope.XMax, envelope.YMax, datum, 0);
                GlobalVariables.currentExtentDM = extentDmRadio;
                try
                {
                    // Itera todos items seleccionados en el ListBox de WPF
                    foreach (var item in LayersListBox.Items)
                    {
                        if (item is CheckBox checkBox && checkBox.IsChecked == true)
                        {
                            string capaSeleccionada = checkBox.Content.ToString();
                            await LayerUtils.AddLayerCheckedListBox(capaSeleccionada, zoneDm, featureClassLoader, datum, extentDmRadio);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error en capa de listado", MessageBoxButton.OK, MessageBoxImage.Error);
                    progressBar.Dispose();
                }

                if (GlobalVariables.currentModule == "Plano Áreas Restringidas")
                {
                    listDist = await FeatureProcessorUtils.IntersectFeatureLayerWithGeometry(featureClassLoader.pFeatureLayer_dist, polygon, "NM_DIST");
                    listProv = await FeatureProcessorUtils.IntersectFeatureLayerWithGeometry(featureClassLoader.pFeatureLayer_dist, polygon, "NM_PROV");
                    listDep = await FeatureProcessorUtils.IntersectFeatureLayerWithGeometry(featureClassLoader.pFeatureLayer_dist, polygon, "NM_DEPA");

                    GlobalVariables.listadoLimitesAdministrativos.listaDistritos = listDist;
                    GlobalVariables.listadoLimitesAdministrativos.listaProvincias = listProv;
                    GlobalVariables.listadoLimitesAdministrativos.listaDepartamentos = listDep;

                    var layoutConfiguration = new LayoutConfiguration();
                    layoutConfiguration.BasePath = GlobalVariables.ContaninerTemplatesReport;

                    layoutConfiguration.SelePlano = "Plano Area Restringida";
                    var layoutUtils = new LayoutUtils(layoutConfiguration);
                    var layoutPath = layoutUtils.DeterminarRutaPlantilla("Plano Area Restringida");
                    string nameWithoutExtention = Path.GetFileNameWithoutExtension(layoutPath);
                    List<string> layoutsToDelete = new List<string>()
                    {
                        nameWithoutExtention,
                    };
                    await LayoutUtils.DeleteSpecifiedLayoutsAsync(layoutsToDelete);
                    var layoutProjectItem = await LayoutUtils.AddLayoutPath(layoutPath, "Catastro", GlobalVariables.mapNameCatastro, nameWithoutExtention);
                    AreaRestElementsLayout areaRestElementsLayout = new AreaRestElementsLayout();
                    await areaRestElementsLayout.AgregarTextosLayoutAsync(layoutProjectItem, 10.4);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en capa de listado", MessageBoxButton.OK, MessageBoxImage.Error);
                progressBar.Dispose();
            }

            progressBar.Dispose();
            // string styleGrid = System.IO.Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCaram);
            // await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync("Zona Reservada", styleGrid, "CLASE", StyleItemType.LineSymbol);
        }

        private ExtentModel ObtenerExtent(double XMin, double YMin, double XMax, double YMax, int datum, int radioKm = 0)
        {
            //// Obtener las coordenadas usando la función ObtenerCoordenadas
            //DataTable coordenadasTable = ObtenerCoordenadas(codigoValue, datum);

            //// Asegurarse de que la tabla contiene filas
            //if (coordenadasTable.Rows.Count == 0)
            //{
            //    throw new Exception("No se encontraron coordenadas para calcular el extent.");
            //}
            int radioMeters = radioKm * 1000;
            // Inicializar las variables para almacenar los valores extremos
            double xmin = XMin; // int.MaxValue;
            double xmax = XMax; // int.MinValue;
            double ymin = YMin; // int.MaxValue;
            double ymax = YMax; // int.MinValue;

            // Crear el objeto ExtentModel con los valores calculados
            ExtentModel extent = new ExtentModel
            {
                xmin = xmin - radioMeters,
                xmax = xmax + radioMeters,
                ymin = ymin - radioMeters,
                ymax = ymax + radioMeters
            };

            return extent;
        }
        public async Task UpdateValueAsync(string capa, string codigoValue)
        {
            await QueuedTask.Run(() =>
            {
                try
                {
                    // Obtener el documento del mapa y la capa
                    Map pMap = MapView.Active.Map;
                    FeatureLayer pFeatLayer1 = null;
                    foreach (var layer in pMap.Layers)
                    {
                        if (layer.Name.ToUpper() == capa.ToUpper())
                        {
                            pFeatLayer1 = layer as FeatureLayer;
                            break;
                        }
                    }

                    if (pFeatLayer1 == null)
                    {
                        System.Windows.MessageBox.Show("No se encuentra el Layer");
                        return;
                    }

                    // Obtener la clase de entidades de la capa
                    FeatureClass pFeatureClas1 = pFeatLayer1.GetTable() as FeatureClass;

                    // Preparar la fecha y hora
                    string fecha = DateTime.Now.ToString("yyyy/MM/dd");
                    string v_fec_denun = fecha + " 00:00";
                    string v_hor_denun = DateTime.Now.ToString("HH:mm:ss");

                    // Comenzar la transacción
                    using (RowCursor pUpdateFeatures = pFeatureClas1.Search(null, false))
                    {
                        int contador = 0;
                        while (pUpdateFeatures.MoveNext())
                        {
                            contador++;
                            using (Row row = pUpdateFeatures.Current)
                            {
                                string v_codigo_dm = row["CODIGOU"].ToString();

                                // Llamar al procedimiento para obtener datos de Datum y bloquear estado
                                DataTable lodtbDatos_dm = dataBaseHandler.ObtenerDatumDm(v_codigo_dm);
                                if (lodtbDatos_dm.Rows.Count > 0)
                                {
                                    row["DATUM"] = lodtbDatos_dm.Rows[0]["ESTADO"].ToString();
                                }

                                // Llamar a otros procedimientos para obtener situación y estado
                                DataTable lodtbDatos1 = dataBaseHandler.ObtenerBloqueadoDm(v_codigo_dm);
                                if (lodtbDatos1.Rows.Count > 0)
                                {
                                    if (lodtbDatos1.Rows[0]["CODIGO"].ToString() == "1")
                                    {
                                        row["BLOQUEO"] = "1";
                                        row["CASO"] = "D.M. - ANAP";
                                    }

                                }
                                else
                                {
                                    row["BLOQUEO"] = "0";
                                }

                                row["CONTADOR"] = contador;

                                DataTable lodtbDatos2 = dataBaseHandler.ObtenerDatosGeneralesDM(v_codigo_dm);
                                if (lodtbDatos2.Rows.Count > 0)
                                {
                                    row["SITUACION"] = lodtbDatos2.Rows[0]["SITUACION"].ToString();
                                }
                                else
                                {
                                    row["SITUACION"] = "X";
                                }

                                // Lógica de asignación de leyenda dependiendo del estado
                                string estado = row["ESTADO"].ToString();
                                string leyenda = string.Empty;
                                switch (estado)
                                {
                                    case " ":
                                        leyenda = "G4";  // Denuncios Extinguidos
                                        break;
                                    case "P":
                                        leyenda = "G1";  // Petitorio Tramite
                                        break;
                                    case "D":
                                        leyenda = "G2";  // Denuncio Tramite
                                        break;
                                    case "E":
                                    case "N":
                                    case "Q":
                                    case "T":
                                        leyenda = "G3";  // Denuncios Titulados
                                        break;
                                    case "F":
                                    case "J":
                                    case "L":
                                    case "H":
                                    case "Y":
                                    case "9":
                                    case "X":
                                        leyenda = "G4";  // Denuncios Extinguidos
                                        break;
                                    case "C":
                                        DataTable lodtbDatos3 = dataBaseHandler.ObtenerDatosDM(v_codigo_dm);
                                        string v_situacion_dm = "";
                                        string v_estado_dm = "";
                                        if (lodtbDatos3.Rows.Count > 0)
                                        {
                                            v_situacion_dm = lodtbDatos3.Rows[0]["SITUACION"].ToString();
                                            v_estado_dm = lodtbDatos3.Rows[0]["ESTADO"].ToString();
                                        }
                                        if (v_situacion_dm == "V" && v_estado_dm == "T")
                                        {
                                            leyenda = "G3";
                                        }
                                        else if (v_situacion_dm == "V" && v_estado_dm == "R")
                                        {
                                            leyenda = "G1";
                                        }
                                        else
                                        {
                                            leyenda = "G4";
                                        }
                                        break;
                                    case "A":
                                    case "B":
                                    case "S":
                                    case "M":
                                    case "G":
                                    case "R":
                                    case "Z":
                                    case "K":
                                    case "V":

                                        leyenda = "G5";  // Otros
                                        break;
                                    default:
                                        row["LEYENDA"] = "";
                                        row["EVAL"] = "EV";
                                        row["TIPO_EX"] = "PE";
                                        row["CONCESION"] = "Dm_Simulado";
                                        row["FEC_DENU"] = v_fec_denun;
                                        row["HOR_DENU"] = v_hor_denun;
                                        row["CARTA"] = "CARTA";

                                        break;

                                }
                                if (row["BLOQUEO"].ToString() == "1")
                                {
                                    leyenda = "G7";
                                }

                                // Actualizar los valores de departamento, provincia y distrito
                                DataTable lodtbDemarca = dataBaseHandler.ObtenerDatosUbigeo(row["DEMAGIS"].ToString().Substring(0, 6));
                                if (lodtbDemarca.Rows.Count > 0)
                                {
                                    row["DPTO"] = lodtbDemarca.Rows[0]["DPTO"].ToString();
                                    row["PROV"] = lodtbDemarca.Rows[0]["PROV"].ToString();
                                    row["DIST"] = lodtbDemarca.Rows[0]["DIST"].ToString();
                                }
                                if (codigoValue == row["CODIGOU"].ToString())
                                {
                                    leyenda = "G6";
                                }

                                row["LEYENDA"] = leyenda;

                                row.Store();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error en UpdateValue: " + ex.Message);
                }
            });

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
                Map map = await MapUtils.FindMapByNameAsync(mapName);
                await MapUtils.ActivateMapAsync(map);

                // Completar la tarea con el mapa activo
                //tcs.SetResult(MapView.Active.Map);
                tcs.SetResult(map);
            });

            // Esperar hasta que el evento se complete
            return await tcs.Task;
        }

        private void TbxValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // Evitar que se procese la tecla Enter (si se desea)
                e.Handled = true;

                // Llamar a la acción que deseas ejecutar cuando se presiona Enter
                //MessageBox.Show("Enter");
                BtnSearch_Click(sender, e);
            }

        }

        //private async void DataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        //{
        //string demaName = "AreaRestringida";
        //ImagenPoligono.Source = null;
        //ImagenPoligono.Stretch = Stretch.Fill;

        //var tableView = sender as DevExpress.Xpf.Grid.TableView;
        //if (tableView != null && tableView.Grid.VisibleRowCount > 0)
        //{
        //    // Obtener el índice de la fila seleccionada
        //    int focusedRowHandle = tableView.FocusedRowHandle;
        //    int currentDatum = (int)CbxSistema.SelectedValue;


        //    if (focusedRowHandle >= 0) // Verifica si hay una fila seleccionada
        //    {
        //        // Obtener el valor de una columna específica (por ejemplo, "CODIGO")
        //        string cdCodigo = DataGridResult.GetCellValue(focusedRowHandle, "CG_CODIGO")?.ToString();
        //        string cdClase = DataGridResult.GetCellValue(focusedRowHandle, "PA_DESCRI")?.ToString();

        //        string query = $"CODIGO = '{cdCodigo}' and CLASE = '{cdClase}'";



        //        var Params = Geoprocessing.MakeValueArray(demaName, query);
        //        var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetDemaImage, Params);
        //        string jsonString = response.ReturnValue;

        //        jsonString = jsonString.Trim('"');
        //        string path = jsonString;
        //        path = path.Replace("\\", "/");
        //        string fullUri = "file:///" + path;

        //        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        //        {
        //            BitmapImage bitmap = new BitmapImage();
        //            bitmap.BeginInit();

        //            // 3) Obligamos a que la imagen se cargue completa en memoria
        //            bitmap.CacheOption = BitmapCacheOption.OnLoad;

        //            // 4) Asignamos el stream como fuente de la imagen
        //            bitmap.StreamSource = fs;
        //            bitmap.EndInit();

        //            // 5) "Congelamos" la imagen para usarla en WPF sin lock al archivo
        //            bitmap.Freeze();

        //            // 6) Asignamos la imagen al control
        //            ImagenPoligono.Source = bitmap;
        //        }

        //        //BitmapImage bitmap = new BitmapImage();
        //        //bitmap.BeginInit();
        //        //bitmap.UriSource = new Uri(fullUri, UriKind.Absolute);
        //        //bitmap.EndInit();
        //        //ImagenPoligono.Source = bitmap;
        //    }
        //}

        //}





    }
}
