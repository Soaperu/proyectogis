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
using System.IO;
using static SigcatminProAddin.View.Modulos.EvaluacionDM;
using CommonUtilities.ArcgisProUtils;
using ArcGIS.Core.Data;
using ArcGIS.Desktop.Mapping;
using System.Security.Policy;
using ArcGIS.Core.Events;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping.Events;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Desktop.Core.Geoprocessing;
using System.Data;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para LimiteDepartamental.xaml
    /// </summary>
    public partial class LimiteDepartamental : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        private string ubigeo;
        private string coddema;

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
                Header = DataGridResultsDepartamentoConstants.Headers.Index, // Encabezado
                FieldName = DataGridResultsDepartamentoConstants.ColumNames.Index,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DataGridResultsDepartamentoConstants.Widths.Index
            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DataGridResultsDepartamentoConstants.ColumNames.Codigo, // Nombre del campo en el DataTable
                Header = DataGridResultsDepartamentoConstants.Headers.Codigo,    // Encabezado visible
                Width = DataGridResultsDepartamentoConstants.Widths.Codigo            // Ancho de la columna
            };

            GridColumn departamentoColumn = new GridColumn
            {
                FieldName = DataGridResultsDepartamentoConstants.ColumNames.Departamento,
                Header = DataGridResultsDepartamentoConstants.Headers.Departamento,
                Width = DataGridResultsDepartamentoConstants.Widths.Departamento
            };

            
            GridColumn capitalColumn = new GridColumn
            {
                FieldName = DataGridResultsDepartamentoConstants.ColumNames.Capital,
                Header = DataGridResultsDepartamentoConstants.Headers.Capital,
                Width = DataGridResultsDepartamentoConstants.Widths.Capital
            };
            
            GridColumn ubigeoColumn = new GridColumn
            {
                FieldName = DataGridResultsDepartamentoConstants.ColumNames.Ubigeo,
                Header = DataGridResultsDepartamentoConstants.Headers.Ubigeo,
                Width = DataGridResultsDepartamentoConstants.Widths.Ubigeo
            };


            // Agregar columnas al GridControl
            DataGridResult.Columns.Add(codigoColumn);
            DataGridResult.Columns.Add(departamentoColumn);
            DataGridResult.Columns.Add(capitalColumn);
            DataGridResult.Columns.Add(ubigeoColumn);

        }
        public LimiteDepartamental()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfigureDataGridResultColumns();



            dataBaseHandler = new DatabaseHandler();
            CbxTypeConsult.SelectedIndex = 0;
            TbxRadio.Text = "5";


        }

        private void CbxTypeConsult_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CbxZona_Loaded(object sender, RoutedEventArgs e)
        {
            //List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            //cbp.Add(new ComboBoxPairs("Seleccione Zona", 0));
            //cbp.Add(new ComboBoxPairs("17", 17));
            //cbp.Add(new ComboBoxPairs("18", 18));
            //cbp.Add(new ComboBoxPairs("19", 19));

            //// Asignar la lista al ComboBox
            //CbxZona.DisplayMemberPath = "_Key";
            //CbxZona.SelectedValuePath = "_Value";
            //CbxZona.ItemsSource = cbp;

            //// Seleccionar la opción 18 por defecto
            //CbxZona.SelectedIndex = 0;
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

        }

        private void CbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
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
            try
            {
                var dmrRecords = dataBaseHandler.GetDemarcacion((int)DemarcacionType.Departamento, TbxValue.Text.ToUpper());

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

                coddema = dmrRecords.Rows[0]["UBIGEO"].ToString();
                ubigeo = coddema + "0000";

                var dtZona = dataBaseHandler.GetZonasporUbigeo(ubigeo);
                if (dtZona.Rows.Count == 2)
                {
                    TxtZonaAlerta.Visibility = Visibility.Hidden;
                    CbxZona.ItemsSource = dtZona.DefaultView;
                    CbxZona.DisplayMemberPath = "DESCRIPCION";
                    CbxZona.SelectedValuePath = "CODIGO";
                    CbxZona.SelectedIndex = 1;
                    LblZona.Text = "Zona";
                    TxtZonaAlerta.Visibility = Visibility.Hidden;
                }
                else if (dtZona.Rows.Count == 3)
                {
                    CbxZona.ItemsSource = dtZona.DefaultView;
                    CbxZona.DisplayMemberPath = "DESCRIPCION";
                    CbxZona.SelectedValuePath = "CODIGO";
                    CbxZona.SelectedIndex = 0;
                    LblZona.Text = "Zona *";
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
        private void calculatedIndex(GridControl table, int records, string columnName)
        {
            int newRowHandle = DataControlBase.NewItemRowHandle;

            // Agregar registros de ejemplo con índice
            for (int i = 1; i <= records; i++)
            {
                table.SetCellValue(newRowHandle, columnName, i);
            }
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

        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
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
            if (CbxZona.SelectedIndex == 0)
            {
                string message = "Por favor, Seleccione una zona";
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
            List<string> mapsToDelete = new List<string>()
            {
                GlobalVariables.mapNameCatastro
            };

            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);
            int datum = (int)CbxSistema.SelectedValue;
            string datumStr = CbxSistema.Text;
            int radio = int.Parse(TbxRadio.Text);
            string outputFolder = System.IO.Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
            string zoneDm = CbxZona.SelectedValue.ToString();

            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            string fechaArchi = DateTime.Now.Ticks.ToString();
            GlobalVariables.idExport = fechaArchi;
            string catastroShpName = "Catastro" + fechaArchi;

            ArcGIS.Core.Geometry.Geometry polygon = null;
            ArcGIS.Core.Geometry.Envelope envelope = null;
            await QueuedTask.Run(async() =>
            {
                

                await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync("CATASTRO MINERO");
                Map map = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro);
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");
                var queryClause = $"CD_DEPA = '{coddema}'";

                if (datum == 2)
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_" + zoneDm, false, queryClause);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_" + zoneDm, false, queryClause);
                }

                if (datum == 2)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }
                
                // Encontrando Distritos superpuestos a DM con
                ArcGIS.Core.Data.QueryFilter filter =
                   new ArcGIS.Core.Data.QueryFilter()
                   {
                       WhereClause = queryClause
                   };
                envelope = featureClassLoader.pFeatureLayer_depa.QueryExtent();

                using (RowCursor rowCursor = featureClassLoader.pFeatureLayer_depa.GetFeatureClass().Search(filter, false))
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
                string listDms = await featureClassLoader.IntersectFeatureClassbyGeometryAsync("Catastro", polygon, catastroShpName);



            });

            await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
            await UpdateValueAsync(catastroShpName, " ");
            string styleCat = System.IO.Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCatastro);
            await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, styleCat, "LEYENDA", StyleItemType.PolygonSymbol, "");

            CommonUtilities.ArcgisProUtils.LayerUtils.SelectSetAndZoomByNameAsync(catastroShpName, false);
            List<string> layersToRemove = new List<string>() { "Catastro", "Carta IGN", "Zona Urbana" };
            await CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);
            await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameAsync(catastroShpName, "Catastro");
            GlobalVariables.CurrentShpName = "Catastro";
            MapUtils.AnnotateLayerbyName("Catastro", "CONTADOR", "DM_Anotaciones");
            UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
            var (gridLayer, pointLayer) = await uTMGridGenerator.GenerateUTMGridAsync(envelope.XMin, envelope.YMin, envelope.XMax, envelope.YMax, "Malla", zoneDm);
            await uTMGridGenerator.AnnotateGridLayer(pointLayer, "VALOR");
            await uTMGridGenerator.RemoveGridLayer("Malla", zoneDm);
            string styleGrid = System.IO.Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleMalla);
            await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(gridLayer.Name, styleGrid, "CLASE", StyleItemType.LineSymbol);

        }

        private async void DataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            string demaName = "Departamento";
            ImagenPoligono.Source = null;
            ImagenPoligono.Stretch = Stretch.Fill;

            var tableView = sender as DevExpress.Xpf.Grid.TableView;
            if (tableView != null && tableView.Grid.VisibleRowCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                int focusedRowHandle = tableView.FocusedRowHandle;
                int currentDatum = (int)CbxSistema.SelectedValue;


                if (focusedRowHandle >= 0) // Verifica si hay una fila seleccionada
                {
                    // Obtener el valor de una columna específica (por ejemplo, "CODIGO")
                    string cd_depa = DataGridResult.GetCellValue(focusedRowHandle, "UBIGEO")?.ToString();
                    string query = $"CD_DEPA = '{cd_depa}'";
                    var Params = Geoprocessing.MakeValueArray(demaName, query);
                    var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetDemaImage, Params);
                    string jsonString = response.ReturnValue;

                    jsonString = jsonString.Trim('"');
                    string path = jsonString; 
                    path = path.Replace("\\", "/");
                    string fullUri = "file:///" + path;

                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();

                        // 3) Obligamos a que la imagen se cargue completa en memoria
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;

                        // 4) Asignamos el stream como fuente de la imagen
                        bitmap.StreamSource = fs;
                        bitmap.EndInit();

                        // 5) "Congelamos" la imagen para usarla en WPF sin lock al archivo
                        bitmap.Freeze();

                        // 6) Asignamos la imagen al control
                        ImagenPoligono.Source = bitmap;
                    }

                    //BitmapImage bitmap = new BitmapImage();
                    //bitmap.BeginInit();
                    //bitmap.UriSource = new Uri(fullUri, UriKind.Absolute);
                    //bitmap.EndInit();
                    //ImagenPoligono.Source = bitmap;
                }
            }
        }
    }
}
