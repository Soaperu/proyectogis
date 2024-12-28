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






namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para VariasCartasNacional.xaml
    /// </summary>
    public partial class VariasCartasNacional : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;

        //public ObservableCollection<CartaIGN> CartaIGNs { get; set; }


        public class CartaIGN
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public int Zona { get; set; }
            public int EsteMin { get; set; }
            public int NorteMin { get; set; }
            public int EsteMax { get; set; }
            public int NorteMax { get; set; }
            public override string ToString()
            {
                return $"Codigo: {Codigo}, Nombre: {Nombre}, Zona: {Zona}, EsteMin: {EsteMin}, NorteMin: {NorteMin}, EsteMax: {EsteMax}, NorteMax: {NorteMax}";
            }
        }

        public class Vertice
        {
            public string Nombre { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return $"Nombre: {Nombre}, X: {X}, Y: {Y}";
            }
        }



        public VariasCartasNacional()
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

        private void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



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
            //GridColumn naturalezaColumn = new GridColumn
            //{
            //    FieldName = DatagridResultConstants.ColumNames.Naturaleza,
            //    Header = DatagridResultConstants.Headers.Naturaleza,
            //    Width = DatagridResultConstants.Widths.Naturaleza
            //};
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

        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            CbxSistema.SelectedIndex = 0;
            CbxTypeConsult.SelectedIndex = 0;
            CbxZona.SelectedIndex = 1;
            BtnGraficar.IsEnabled = false;
        }


        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

            //var seleccionados = DataGridResult.GetSelectedRowHandles()
            //                          .Select(handle => DataGridResult.GetRow(handle) as Registro)
            //                          .ToList();

            //foreach (var registro in seleccionados)
            //{
            //    Registros.Remove(registro);
            //}


            // Verificar si hay un elemento seleccionado
            if (listBoxVertices.SelectedItem != null)
            {
                // Obtener el elemento seleccionado
                string elementoSeleccionado = listBoxVertices.SelectedItem as string;
                listBoxVertices.Items.Remove(elementoSeleccionado);
            }
            else
            {
                // Informar al usuario que no hay ningún elemento seleccionado
                MessageBox.Show(
                    "Por favor, selecciona un elemento para eliminar.",
                    "Sin Selección",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxVertices.Items.Count > 0)
            {
                // Confirmar la eliminación (opcional)
                MessageBoxResult resultado = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar todos los elementos?",
                    "Confirmar Eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    // Eliminar todos los elementos del ListBox
                    listBoxVertices.Items.Clear();
                }
            }
            else
            {
                // Informar al usuario que el ListBox ya está vacío
                MessageBox.Show(
                    "El ListBox ya está vacío.",
                    "Información",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            BtnGraficar.IsEnabled = false;

        }

        private void BtnAgregarHoja_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Crear la cadena valor con el número de punto y las coordenadas
            string valor = "Carta IGN " + (listBoxVertices.Items.Count + 1) + ":  " + TbxValue.Text.TrimEnd().ToUpper();
            listBoxVertices.Items.Add(valor);
            BtnGraficar.IsEnabled = true;
            //try
            //{
            //    string value = "CODIGO";// (string)CbxTypeConsult.SelectedValue.ToString();
            //    string datumStr = CbxSistema.Text;
            //    var dmrRecords = dataBaseHandler.GetOfficialCarta(value, TbxValue.Text.TrimEnd(), datumStr);
            //    //calculatedIndex(DataGridResult, records, DatagridResultConstants.ColumNames.Index);
            //    DataGridResult.ItemsSource = dmrRecords.DefaultView;
            //}
            //catch (Exception ex)
            //{
            //    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format(MessageConstants.Errors.UnexpectedError, ex.Message),
            //                                                        MessageConstants.Titles.Error,
            //                                                        MessageBoxButton.OK,
            //                                                        MessageBoxImage.Error);
            //    return;
            //}

            TbxValue.Text = "";


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


        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            BtnGraficar.IsEnabled = false;
            int FlagL = listBoxVertices.Items.Count;
            if (FlagL == 0)
            {
                MessageBox.Show(
                   "Por favor, escribir nombre de la carta. ejm: 19-i.",
                   "Sin Selección",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
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
            int radio = int.Parse(TbxRadio.Text);
            string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);

            await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync("CATASTRO MINERO");
            //int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
            //string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
            //GlobalVariables.CurrentCodeDm = codigoValue;
            //string stateGraphic = DataGridResult.GetCellValue(focusedRowHandle, "PE_VIGCAT")?.ToString();
            //string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
            //GlobalVariables.CurrentZoneDm = zoneDm;
            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            //var v_zona_dm = "";
            string zoneDm = CbxZona.SelectedValue.ToString();
            //string fechaArchi = DateTime.Now.Ticks.ToString();
            //GlobalVariables.idExport = fechaArchi;
            //string catastroShpName = "Catastro" + fechaArchi;
            //GlobalVariables.CurrentShpName = catastroShpName;
            //string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            //string dmShpName = "DM" + fechaArchi;
            //string dmShpNamePath = "DM" + fechaArchi + ".shp";
            try
            {
                // Obtener el mapa Catastro//

                Map map = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");

                //Carga capa Catastro
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta56, false);
                }


                BtnGraficar.IsEnabled = true;

                string resultado = "";
                // Recorrer el ListBox con un bucle for
                for (int i = 0; i < listBoxVertices.Items.Count; i++)
                {
                    string item = listBoxVertices.Items[i].ToString();
                    // Dividir el texto por el delimitador
                    string[] partes = item.Split(":");

                    string dato = partes[1];
                    dato = dato.Trim();
                    if (i == listBoxVertices.Items.Count - 1)
                    {
                        resultado = resultado + "'" + dato + "'";
                    }
                    else
                    {
                        resultado = resultado + "'" + dato + "'" + ",";
                    }

                    // Mostrar las partes
                    //foreach (string parte in partes)
                    //{
                    //    Console.WriteLine(parte);
                    //}
                }
                MessageBox.Show(resultado);
                //FeatureInfo selectedFeature = await GetFeatureInfobyQuery($"{resultado}'", "Carta IGN");
                FeatureInfo selectedFeature = await GetFeatureInfobyQuery($"CD_HOJA IN ({resultado})", "Carta IGN");

            }




            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error en UpdateValue: " + ex.Message);
            }
        }

        public async Task<FeatureInfo> GetFeatureInfobyQuery(string p_Filtro, string layerName)
        {
            Feature selectedFeature = null;
            FeatureInfo featureInfo = new FeatureInfo();

            await QueuedTask.Run(() =>
            {
                // Obtener la capa por nombre
                var layer = MapView.Active.Map.Layers.FirstOrDefault(l => l.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase)) as FeatureLayer;

                if (layer == null)
                {
                    MessageBox.Show("No se encuentra el Layer");
                    return;
                }

                // Crear un query filter
                var queryFilter = new ArcGIS.Core.Data.QueryFilter
                {
                    WhereClause = p_Filtro
                    //WhereClause = $"CD_HOJA IN ({p_Filtro})"//p_Filtro
                };

                using (var rowCursor = layer.Search(queryFilter))
                {
                    if (rowCursor.MoveNext())
                    {
                        selectedFeature = rowCursor.Current as Feature;
                    }
                }

                if (selectedFeature == null)
                {
                    MessageBox.Show("No hay ninguna Selección");
                }
                else
                {
                    layer.Select(queryFilter);
                };


                //featureInfo.Codigo = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                //featureInfo.Nombre = selectedFeature.GetOriginalValue(selectedFeature.FindField("CONCESION")).ToString();
                //featureInfo.Fecha = selectedFeature.GetOriginalValue(selectedFeature.FindField("FEC_DENU")).ToString();
                //featureInfo.Area = selectedFeature.GetOriginalValue(selectedFeature.FindField("HECTAREA")).ToString();
                //featureInfo.Titular = selectedFeature.GetOriginalValue(selectedFeature.FindField("TIT_CONCES")).ToString();
                //featureInfo.TipoDM = selectedFeature.GetOriginalValue(selectedFeature.FindField("D_ESTADO")).ToString();
                ////featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                ////featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                ////featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                //featureInfo.Contador = selectedFeature.GetOriginalValue(selectedFeature.FindField("CONTADOR")).ToString();
                //featureInfo.Hora = selectedFeature.GetOriginalValue(selectedFeature.FindField("HOR_DENU")).ToString();
                //featureInfo.Prioridad = selectedFeature.GetOriginalValue(selectedFeature.FindField("EVAL")).ToString();
            });

            return featureInfo;
        }

        public class FeatureInfo
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Fecha { get; set; }
            public string Area { get; set; }
            public string Titular { get; set; }
            public string TipoDM { get; set; }
            public string Contador { get; set; }
            public string Hora { get; set; }
            public string Prioridad { get; set; }
        }

        private void CbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

