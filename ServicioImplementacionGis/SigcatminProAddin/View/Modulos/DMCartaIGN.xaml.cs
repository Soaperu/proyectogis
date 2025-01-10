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
using ActiproSoftware.Windows.Extensions;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para DMCartaIGN.xaml
    /// </summary>
    public partial class DMCartaIGN : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;

        public DMCartaIGN()
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




        private void CbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            GridColumn indexColumn = new GridColumn
            {
                Header = DatagridResultConstants.Headers.Index, // Encabezado
                FieldName = DatagridResultConstants.ColumNames.Index,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstants.Widths.Index
            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Codigo, // Nombre del campo en el DataTable
                Header = DatagridResultConstants.Headers.Codigo,    // Encabezado visible
                Width = DatagridResultConstants.Widths.Codigo            // Ancho de la columna
            };

            GridColumn nombreColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Nombre,
                Header = DatagridResultConstants.Headers.Nombre,
                Width = DatagridResultConstants.Widths.Nombre
            };

            GridColumn pe_vigcatColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.PeVigCat,
                Header = DatagridResultConstants.Headers.PeVigCat,
                Width = DatagridResultConstants.Widths.PeVigCat
            };

            GridColumn zonaColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Zona,
                Header = DatagridResultConstants.Headers.Zona,
                Width = DatagridResultConstants.Widths.Zona
            };
            GridColumn tipoColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Tipo,
                Header = DatagridResultConstants.Headers.Tipo,
                Width = DatagridResultConstants.Widths.Tipo
            };
            GridColumn estadoColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Estado,
                Header = DatagridResultConstants.Headers.Estado,
                Width = DatagridResultConstants.Widths.Estado
            };
            GridColumn naturalezaColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Naturaleza,
                Header = DatagridResultConstants.Headers.Naturaleza,
                Width = DatagridResultConstants.Widths.Naturaleza
            };
            GridColumn cartaColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Carta,
                Header = DatagridResultConstants.Headers.Carta,
                Width = DatagridResultConstants.Widths.Carta
            };
            GridColumn hectareaColumn = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNames.Hectarea,
                Header = DatagridResultConstants.Headers.Hectarea,
                Width = DatagridResultConstants.Widths.Hectarea
            };

            // Agregar columnas al GridControl
            DataGridResult.Columns.Add(indexColumn);
            DataGridResult.Columns.Add(codigoColumn);
            DataGridResult.Columns.Add(nombreColumn);
            DataGridResult.Columns.Add(pe_vigcatColumn);
            DataGridResult.Columns.Add(zonaColumn);
            DataGridResult.Columns.Add(tipoColumn);
            DataGridResult.Columns.Add(estadoColumn);
            DataGridResult.Columns.Add(naturalezaColumn);
            DataGridResult.Columns.Add(cartaColumn);
            DataGridResult.Columns.Add(hectareaColumn);

        }

        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            CbxSistema.SelectedIndex = 0;
            CbxTypeConsult.SelectedIndex = 0;
            CbxZona.SelectedIndex = 1;
            BtnGraficar.IsEnabled = false;

        }

        private void BtnReporte_Click(object sender, RoutedEventArgs e)
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

        private void BtnAgregarHoja_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Crear la cadena valor con el número de punto y las coordenadas
            //string valor = "Carta IGN " + (listBoxVertices.Items.Count + 1) + ":  " + TbxValue.Text.TrimEnd().ToUpper();
            //listBoxVertices.Items.Add(valor);
            BtnGraficar.IsEnabled = true;
            try
            {
                string value = (string)CbxTypeConsult.SelectedValue.ToString();
                var countRecords = dataBaseHandler.CountRecords(value, TbxValue.Text.TrimEnd().Trim());
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
                var dmrRecords = dataBaseHandler.GetUniqueDM(TbxValue.Text.TrimEnd(), (int)CbxTypeConsult.SelectedValue);
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

        private void DataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var tableView = sender as DevExpress.Xpf.Grid.TableView;
            if (tableView != null && tableView.Grid.VisibleRowCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                int focusedRowHandle = tableView.FocusedRowHandle;
                int currentDatum = (int)CbxSistema.SelectedValue;


                if (focusedRowHandle >= 0) // Verifica si hay una fila seleccionada
                {
                    // Obtener el valor de una columna específica (por ejemplo, "CODIGO")
                    string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
                    int.TryParse(DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString(), out int zona);
                    CbxZona.SelectedValue = zona;
                    GlobalVariables.CurrentNameDm = DataGridResult.GetCellValue(focusedRowHandle, "NOMBRE")?.ToString();
                    // Mostrar el valor obtenido

                    string areaValue = DataGridResult.GetCellValue(focusedRowHandle, "HECTAREA")?.ToString();
                    GlobalVariables.CurrentAreaDm = areaValue;
                    //TbxArea.Text = areaValue;
                    //TbxArea.IsReadOnly = true;
                    //// Llamar a funciones adicionales con el valor seleccionado
                    ClearCanvas();

                    var dmrRecords = ObtenerCoordenadas(codigoValue, currentDatum);
                    //var dmrRecords = ObtenerCoordenadas(codigoValue, currentDatum);
                    DataGridDetails.ItemsSource = dmrRecords.DefaultView;
                    GraficarCoordenadas(dmrRecords);
                    GlobalVariables.CurrentTipoEx = DataGridResult.GetCellValue(focusedRowHandle, "TIPO")?.ToString();
                }
            }
        }


        private void GraficarCoordenadas(DataTable dmrRecords)
        {
            // Verificar que el DataTable no sea nulo y tenga datos
            if (dmrRecords == null || dmrRecords.Rows.Count == 0)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("No hay datos para graficar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var utmCoordinates = new PointCollection();

            foreach (DataRow row in dmrRecords.Rows)
            {
                try
                {
                    // Extraer los valores de las columnas NORTE y ESTE
                    double este = Convert.ToDouble(row["ESTE"]);
                    double norte = Convert.ToDouble(row["NORTE"]);

                    // Agregar el punto a la colección
                    utmCoordinates.Add(new Point(este, norte));
                }
                catch (Exception ex)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Error al procesar las coordenadas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            double canvasWidth = PolygonCanvas.ActualWidth;
            double canvasHeight = PolygonCanvas.ActualHeight;

            var scaledCoordinates = ScaleAndCenterCoordinates(utmCoordinates, canvasWidth, canvasHeight);
            ImagenPoligono.Visibility = Visibility.Collapsed;
            DrawPolygon(scaledCoordinates);
        }

        private void DrawPolygon(PointCollection coordinates)
        {
            // Crear el polígono
            System.Windows.Shapes.Polygon polygon = new System.Windows.Shapes.Polygon
            {
                Stroke = Brushes.Green,
                Fill = Brushes.LightGreen,
                StrokeThickness = 2,
                Points = coordinates
            };

            // Agregar el polígono al Canvas
            PolygonCanvas.Children.Add(polygon);

            // Etiquetar cada vértice
            for (int i = 0; i < coordinates.Count; i++)
            {
                var vertex = coordinates[i];
                var label = new TextBlock
                {
                    Text = $"{i + 1}",
                    FontSize = 10,
                    Foreground = Brushes.Black,
                    Background = Brushes.Transparent
                };

                // Posicionar la etiqueta cerca del vértice
                Canvas.SetLeft(label, vertex.X + 3); // Ajustar posición X
                Canvas.SetTop(label, vertex.Y - 10); // Ajustar posición Y
                PolygonCanvas.Children.Add(label);
            }
        }

        private PointCollection ScaleAndCenterCoordinates(PointCollection coordinates, double canvasWidth, double canvasHeight)
        {
            // Determinar los valores mínimos y máximos de las coordenadas
            double minX = coordinates.Min(p => p.X);
            double maxX = coordinates.Max(p => p.X);
            double minY = coordinates.Min(p => p.Y);
            double maxY = coordinates.Max(p => p.Y);

            // Calcular las proporciones de escalado
            double scaleX = canvasWidth * 0.8 / (maxX - minX);
            double scaleY = canvasHeight * 0.8 / (maxY - minY);
            double scale = Math.Min(scaleX, scaleY); // Mantener proporción

            // Calcular los márgenes para centrar el polígono
            double offsetX = (canvasWidth - (maxX - minX) * scale) / 2;
            double offsetY = (canvasHeight - (maxY - minY) * scale) / 2;

            // Ajustar las coordenadas al Canvas
            var scaledCoordinates = new PointCollection();
            foreach (var point in coordinates)
            {
                double scaledX = offsetX + (point.X - minX) * scale;
                double scaledY = canvasHeight - offsetY - (point.Y - minY) * scale; // Invertir Y
                scaledCoordinates.Add(new Point(scaledX, scaledY));
            }

            return scaledCoordinates;
        }


        private DataTable ObtenerCoordenadas(string codigoValue, int datum)
        {
            DataTable filteredTable;
            string[] requiredColumns = {
                    DatagridDetailsConstants.RawColumNames.Vertice,
                    DatagridDetailsConstants.RawColumNames.CoorEsteE,
                    DatagridDetailsConstants.RawColumNames.CoorNorteE };

            var dmrRecords = dataBaseHandler.GetDMDataWGS84(codigoValue);

            if (datum == datumwgs84)
            {
                requiredColumns = new string[] {
                    DatagridDetailsConstants.RawColumNames.Vertice,
                    DatagridDetailsConstants.RawColumNames.CoorEste,
                    DatagridDetailsConstants.RawColumNames.CoorNorte };
            }
            filteredTable = FilterColumns(dmrRecords, requiredColumns);
            // Renombrar las columnas
            filteredTable.Columns[DatagridDetailsConstants.RawColumNames.Vertice].ColumnName = DatagridDetailsConstants.ColumnNames.Vertice;
            filteredTable.Columns[requiredColumns[1]].ColumnName = DatagridDetailsConstants.ColumnNames.Este;
            filteredTable.Columns[requiredColumns[2]].ColumnName = DatagridDetailsConstants.ColumnNames.Norte;

            return filteredTable;
        }

        public DataTable FilterColumns(DataTable originalTable, params string[] columnNames)
        {
            // Crear un nuevo DataTable para las columnas seleccionadas
            DataTable filteredTable = new DataTable();

            // Agregar las columnas seleccionadas al nuevo DataTable
            foreach (string columnName in columnNames)
            {
                if (originalTable.Columns.Contains(columnName))
                {
                    filteredTable.Columns.Add(columnName, originalTable.Columns[columnName].DataType);
                }
                else
                {
                    throw new ArgumentException($"La columna '{columnName}' no existe en el DataTable original.");
                }
            }

            // Copiar filas con los valores de las columnas seleccionadas
            foreach (DataRow row in originalTable.Rows)
            {
                DataRow newRow = filteredTable.NewRow();
                foreach (string columnName in columnNames)
                {
                    newRow[columnName] = row[columnName];
                }
                filteredTable.Rows.Add(newRow);
            }

            return filteredTable;
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

        private void DataGridResult_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            // Verificar si la columna es la columna de índice
            if (e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Integer && e.IsGetData)
            {
                // Asignar el índice de la fila
                e.Value = e.ListSourceRowIndex + 1; // Los índices son base 0, así que sumamos 1
            }
        }

        private ExtentModel ObtenerExtent(string codigoValue, int datum, int radioKm = 0)
        {
            // Obtener las coordenadas usando la función ObtenerCoordenadas
            DataTable coordenadasTable = ObtenerCoordenadas(codigoValue, datum);

            // Asegurarse de que la tabla contiene filas
            if (coordenadasTable.Rows.Count == 0)
            {
                throw new Exception("No se encontraron coordenadas para calcular el extent.");
            }
            int radioMeters = radioKm * 1000;
            // Inicializar las variables para almacenar los valores extremos
            int xmin = int.MaxValue;
            int xmax = int.MinValue;
            int ymin = int.MaxValue;
            int ymax = int.MinValue;

            // Iterar sobre las filas para calcular los valores extremos
            foreach (DataRow row in coordenadasTable.Rows)
            {
                int este = Convert.ToInt32(row["ESTE"]);
                int norte = Convert.ToInt32(row["NORTE"]);

                if (este < xmin) xmin = este;
                if (este > xmax) xmax = este;
                if (norte < ymin) ymin = norte;
                if (norte > ymax) ymax = norte;
            }

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


        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
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

            List<string> mapsToDelete = new List<string>()
             {
                 //GlobalVariables.mapNameCatastro,
                 //GlobalVariables.mapNameDemarcacionPo,
                 GlobalVariables.mapNameCartaIgn
             };

            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);

            try
            {

                string fechaArchi = DateTime.Now.Ticks.ToString();
                string catastroShpName = "Catastro" + fechaArchi;
                string dmShpName = "DM" + fechaArchi;
                string dmShpNamePath = "DM" + fechaArchi + ".shp";

                string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);

                int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
                string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
                //int datum = (int)CbxSistema.SelectedValue;

                var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);
                //var codigoValue = TbxValue.Text.TrimEnd();
                string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
                //await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
                //Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCartaIgn);
                //var featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");

                await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
                Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCartaIgn);
                var featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");


                //Carga capa Catastro
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }

                int radio = 0;

                var extentDmRadio = ObtenerExtent(codigoValue, datum, radio);
                var extentDm = ObtenerExtent(codigoValue, datum);
                GlobalVariables.currentExtentDM = extentDm;
                // Llamar al método IntersectFeatureClassAsync desde la instancia
                string listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);

                await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");

                //await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
                await featureClassLoader.ExportAttributesTemaAsync(catastroShpName, GlobalVariables.stateDmY, dmShpName, $"CODIGOU='{codigoValue}'");
                var fl1 = await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(mapC, Path.Combine(outputFolder, dmShpNamePath));
                CommonUtilities.ArcgisProUtils.LayerUtils.SelectSetAndZoomByNameAsync(dmShpName, false);


                List<string> layersToRemove = new List<string>() { "Catastro", "Carta IGN", dmShpName, catastroShpName, "Zona Urbana" };
                await CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);

                try
                {
                    //await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
                    //Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCartaIgn);
                    //featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");

                    fl1 = await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(mapC, Path.Combine(outputFolder, dmShpNamePath));
                    //Carga capa Distrito
                    if (datum == datumwgs84)
                    {
                        await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DIS_DISTRITO_WGS_" + zoneDm, false);
                    }
                    else
                    {
                        await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DIS_DISTRITO_" + zoneDm, false);
                    }
                    await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_dist);
                    await CommonUtilities.ArcgisProUtils.LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_dist, "NM_DIST", 7, "#4e4e4e", "Bold");
                    //Carga capa Provincia
                    if (datum == datumwgs84)
                    {
                        await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_PRO_PROVINCIA_WGS_" + zoneDm, false);
                    }
                    else
                    {
                        await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_PRO_PROVINCIA_" + zoneDm, false);
                    }
                    await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_prov);
                    await CommonUtilities.ArcgisProUtils.LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_prov, "NM_PROV", 9, "#343434");

                    //Carga capa Departamento
                    if (datum == datumwgs84)
                    {
                        await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_" + zoneDm, false);
                    }
                    else
                    {
                        await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_" + zoneDm, false);
                    }
                    await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_depa);
                    await CommonUtilities.ArcgisProUtils.LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_depa, "NM_DEPA", 12, "#000000", "Bold");

                    CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer((FeatureLayer)fl1, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(255, 0, 0, 0), CIMColor.CreateRGBColor(255, 0, 0));
                    await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameByFeatureLayerAsync((FeatureLayer)fl1, "Catastro");


                    string listHojas = DataGridResult.GetCellValue(focusedRowHandle, "CARTA")?.ToString();
                    GlobalVariables.CurrentPagesDm = "'" + listHojas.Replace("-", "").ToLower() + "'";

                    string mosaicLayer;
                    if (datum == datumwgs84)
                    {
                        mosaicLayer = FeatureClassConstants.gstrRT_IngMosaic84;
                    }
                    else
                    {
                        mosaicLayer = FeatureClassConstants.gstrRT_IngMosaic56;
                    }
                    string queryListCartaIGN = CommonUtilities.StringProcessorUtils.FormatStringCartaIgnForSql(GlobalVariables.CurrentPagesDm);
                    await CommonUtilities.ArcgisProUtils.RasterUtils.AddRasterCartaIGNLayerAsync(mosaicLayer, geodatabase, mapC, queryListCartaIGN);
                    //await MapView.Active.ZoomToAsync(fl1);

                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
                BtnGraficar.IsEnabled = true;

            }
            catch (Exception ex)
            {

            }
        }

        private void TbxValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                // Evitar que se procese la tecla Enter (si se desea)
                e.Handled = true;

                // Llamar a la acción que deseas ejecutar cuando se presiona Enter
                //MessageBox.Show("Enter");
                BtnAgregarHoja_Click(sender, e);
            }

        }

        private void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbxValue.Clear();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSalir_Click_1(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
