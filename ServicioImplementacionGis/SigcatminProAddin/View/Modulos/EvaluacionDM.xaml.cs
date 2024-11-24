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
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Internal.KnowledgeGraph.FFP;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Mapping.Events;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.Data;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.XtraExport.Helpers;
using SigcatminProAddin.Utils.UIUtils;
using FlowDirection = System.Windows.FlowDirection;
using System.Text.RegularExpressions;
using SigcatminProAddin.Models;
using DevExpress.Data.Linq.Helpers;
using ArcGIS.Core.Geometry;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para EvaluacionDM.xaml
    /// </summary>
    public partial class EvaluacionDM : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        public EvaluacionDM()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfiguredataGridResultColumns();
            ConfiguredataGridDetailsColumns();
            dataBaseHandler = new DatabaseHandler();
            cbxTypeConsult.SelectedIndex = 0;
            tbxRadio.Text = "5";
        }

        private void AddCheckBoxesToListBox()
        {
            // Lista de elementos para agregar al ListBox
            //string[] items = { "Capa 1", "Capa 2", "Capa 3", "Capa 4" };

            //// Agrega cada elemento como un CheckBox al ListBox
            //foreach (var item in items)
            //{
            //    System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox
            //    {
            //        Content = item,
            //        Margin = new Thickness(3),
            //        Style = (Style)FindResource("Esri_CheckboxToggleSwitch")
            //    };
            //    listLayers.Items.Add(checkBox);
            //}
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
                }

                listLayers.Items.Add(checkBox);
            }
        }

        private void CurrentUser()
        {
            currentUser.Text = GloblalVariables.ToTitleCase(AppConfig.fullUserName);
        }

        //private void cbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        /// <summary>
        /// Escala las coordenadas al espacio del Canvas.
        /// </summary>
        /// <param name="coordinates">Coordenadas UTM originales.</param>
        /// <param name="canvasWidth">Ancho del Canvas.</param>
        /// <param name="canvasHeight">Alto del Canvas.</param>
        /// <returns>Coordenadas escaladas.</returns>
        /// <summary>
        /// Escala y centra las coordenadas en el Canvas.
        /// </summary>
        /// <param name="coordinates">Coordenadas UTM originales.</param>
        /// <param name="canvasWidth">Ancho del Canvas.</param>
        /// <param name="canvasHeight">Alto del Canvas.</param>
        /// <returns>Coordenadas escaladas y centradas.</returns>
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

        private void ClearCanvas()
        {
            PolygonCanvas.Children.Clear();
        }

        private void ClearDatagrids()
        {
            dataGridResult.ItemsSource= null;
            dataGridDetails.ItemsSource= null;
        }


        /// <summary>
        /// Dibuja un polígono en el Canvas.
        /// </summary>
        /// <param name="coordinates">Coordenadas del polígono.</param>
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxValue.Text))
            {
                //MessageBox.Show("Por favor ingrese el usuario y la contraseña.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                string message = "Por favor ingrese un valor para iniciar la busqueda.";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Nivel de coincidencia muy alto",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                string value = (string)cbxTypeConsult.SelectedValue.ToString();
                var countRecords = dataBaseHandler.CountRecords(value, tbxValue.Text);
                int records = int.Parse(countRecords);
                if (records == 0)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"No Existe ningún Registro con esta consulta: {tbxValue.Text}",
                                                                        "Sin coincidencias",
                                                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (records >= 150)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Sea Ud. más específico en la consulta, hay {countRecords} Registro(s)",
                                                                        "Nivel de coincidencia muy alto",
                                                                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                lblCountRecords.Content = $"Resultados de Búsqueda: {countRecords.ToString()}";
                var dmrRecords = dataBaseHandler.GetUniqueDM(tbxValue.Text, (int)cbxTypeConsult.SelectedValue);
                calculatedIndex(dataGridResult, records, "INDEX");
                dataGridResult.ItemsSource = dmrRecords.DefaultView;
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Se produjo un error inesperado: " + ex.Message,
                                                                            "Error",
                                                                            MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private void cbxTypeConsult_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("Por Código", 1));
            cbp.Add(new ComboBoxPairs("Por Nombre", 2));

            // Asignar la lista al ComboBox
            cbxTypeConsult.DisplayMemberPath = "_Key";
            cbxTypeConsult.SelectedValuePath = "_Value";
            cbxTypeConsult.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            cbxTypeConsult.SelectedIndex = 0;
        }

        private void cbxZona_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("17", 17));
            cbp.Add(new ComboBoxPairs("18", 18));
            cbp.Add(new ComboBoxPairs("19", 19));

            // Asignar la lista al ComboBox
            cbxZona.DisplayMemberPath = "_Key";
            cbxZona.SelectedValuePath = "_Value";
            cbxZona.ItemsSource = cbp;

            // Seleccionar la opción 18 por defecto
            cbxZona.SelectedIndex = 1;
        }
        private void cbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("WGS 84", 1));
            cbp.Add(new ComboBoxPairs("PSAD 56", 2));

            // Asignar la lista al ComboBox
            cbxSistema.DisplayMemberPath = "_Key";
            cbxSistema.SelectedValuePath = "_Value";
            cbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            cbxSistema.SelectedIndex = 0;
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
        public class ComboBoxPairsString
        {
            public string _Key { get; set; }
            public string _Value { get; set; }

            public ComboBoxPairsString(string _key, string _value)
            {
                _Key = _key;
                _Value = _value;
            }

        }

        private void ConfiguredataGridDetailsColumns()
        {
            var tableView = dataGridDetails.View as DevExpress.Xpf.Grid.TableView;
            dataGridDetails.Columns.Clear();
            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            GridColumn verticeColumn = new GridColumn
            {
                FieldName = "VERTICE",
                Header = "Vértice",
                Width = 40
            };

            GridColumn esteColumn = new GridColumn
            {
                FieldName = "ESTE",
                Header = "Este",
                Width = new GridColumnWidth(1, GridColumnUnitType.Star)

            };

            GridColumn norteColumn = new GridColumn
            {
                FieldName = "NORTE",
                Header = "Norte",
                Width = new GridColumnWidth(1, GridColumnUnitType.Star)
            };

            // Agregar columnas al GridControl
            dataGridDetails.Columns.Add(verticeColumn);
            dataGridDetails.Columns.Add(esteColumn);
            dataGridDetails.Columns.Add(norteColumn);

        }
        private void ConfiguredataGridResultColumns()
        {
            // Obtener la vista principal del GridControl
            var tableView = dataGridResult.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            dataGridResult.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn indexColumn = new GridColumn
            {
                Header = "N°", // Encabezado
                FieldName = "INDEX",
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = 30
            };
           
            GridColumn codigoColumn = new GridColumn
            {
                FieldName = "CODIGO", // Nombre del campo en el DataTable
                Header = "Codigo",    // Encabezado visible
                Width = 100            // Ancho de la columna
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
                Header = "Estado Graf",
                Width = 70
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
                Width = 60
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
                Width = 80
            };
            GridColumn cartaColumn = new GridColumn
            {
                FieldName = "CARTA",
                Header = "Carta",
                Width = 80
            };
            GridColumn hectareaColumn = new GridColumn
            {
                FieldName = "HECTAREA",
                Header = "Hectarea",
                Width = 80
            };

            // Agregar columnas al GridControl
            dataGridResult.Columns.Add(indexColumn);
            dataGridResult.Columns.Add(codigoColumn);
            dataGridResult.Columns.Add(nombreColumn);
            dataGridResult.Columns.Add(pe_vigcatColumn);
            dataGridResult.Columns.Add(zonaColumn);
            dataGridResult.Columns.Add(tipoColumn);
            dataGridResult.Columns.Add(estadoColumn);
            dataGridResult.Columns.Add(naturalezaColumn);
            dataGridResult.Columns.Add(cartaColumn);
            dataGridResult.Columns.Add(hectareaColumn);
            // Manejar el evento para calcular valores de la columna no vinculada
            //dataGridResult.CustomUnboundColumnData += (sender, e) =>
            //{
            //    if (e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Integer && e.IsGetData)
            //    {
            //        // Asignar el índice de la fila
            //        e.Value = e.ListSourceRowIndex + 1; // Los índices son base 0, así que sumamos 1
            //    }
            //};
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

        private DataTable ObtenerCoordenadas(string codigoValue, int datum)
        {
            DataTable filteredTable;
            string[] requiredColumns = { "CD_NUMVER", "CD_COREST_E", "CD_CORNOR_E" };
            var dmrRecords = dataBaseHandler.GetDMDataWGS84(codigoValue);

            if (datum == 2)
            {
                requiredColumns = new string[] { "CD_NUMVER", "CD_COREST", "CD_CORNOR" };
            }
            filteredTable = FilterColumns(dmrRecords, requiredColumns);
            // Renombrar las columnas
            filteredTable.Columns["CD_NUMVER"].ColumnName = "VERTICE";
            filteredTable.Columns[requiredColumns[1]].ColumnName = "ESTE";
            filteredTable.Columns[requiredColumns[2]].ColumnName = "NORTE";

            return filteredTable;
        }

        private ExtentModel ObtenerExtent(string codigoValue, int datum, int radioKm=0)
        {
            // Obtener las coordenadas usando la función ObtenerCoordenadas
            DataTable coordenadasTable = ObtenerCoordenadas(codigoValue, datum);

            // Asegurarse de que la tabla contiene filas
            if (coordenadasTable.Rows.Count == 0)
            {
                throw new Exception("No se encontraron coordenadas para calcular el extent.");
            }
            int radioMeters = radioKm*1000;
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

        private void dataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //var tableView = sender as TableView;
            var tableView = sender as DevExpress.Xpf.Grid.TableView;
            if (tableView != null && tableView.Grid.VisibleRowCount>0)
            {
                // Obtener el índice de la fila seleccionada
                int focusedRowHandle = tableView.FocusedRowHandle;
                int currentDatum = (int)cbxSistema.SelectedValue;


                if (focusedRowHandle >= 0) // Verifica si hay una fila seleccionada
                {
                    // Obtener el valor de una columna específica (por ejemplo, "CODIGO")
                    string codigoValue = dataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
                    int.TryParse(dataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString(), out int zona);
                    cbxZona.SelectedValue = zona;

                    // Mostrar el valor obtenido
                    //System.Windows.MessageBox.Show($"Valor de CODIGO: {codigoValue}", "Información de la Fila");
                    string areaValue = dataGridResult.GetCellValue(focusedRowHandle, "HECTAREA")?.ToString();
                    tbxArea.Text = areaValue;
                    tbxArea.IsReadOnly = true;
                    // Llamar a funciones adicionales con el valor seleccionado
                    ClearCanvas();
                    var dmrRecords = ObtenerCoordenadas(codigoValue, currentDatum);
                    dataGridDetails.ItemsSource = dmrRecords.DefaultView;
                    GraficarCoordenadas(dmrRecords);
                }

            }
        }

        private void dataGridResult_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            // Verificar si la columna es la columna de índice
            if (e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Integer && e.IsGetData)
            {
                // Asignar el índice de la fila
                e.Value = e.ListSourceRowIndex + 1; // Los índices son base 0, así que sumamos 1
            }
        }

        private void calculatedIndex(GridControl table, int records,string columnName)
        {
            int newRowHandle = DataControlBase.NewItemRowHandle;

            // Agregar registros de ejemplo con índice
            for (int i = 1; i <= records; i++)
            {
                table.SetCellValue(newRowHandle, columnName, i);
            }
        }

        public void ClearControls()
        {
            var functions = new PageCommonFunctions();
            functions.ClearControls(this);
            ClearCanvas();
            ClearDatagrids();



        }

        private void btnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            cbxSistema.SelectedIndex = 0;
            cbxTypeConsult.SelectedIndex = 0;
            cbxZona.SelectedIndex = 1;
        }

        
        private void cbxTypeConsult_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            tbxValue.Clear();
        }

        private async void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxValue.Text))
            {
                //MessageBox.Show("Por favor ingrese el usuario y la contraseña.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                string message = "Por favor ingrese un valor de radio";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertancia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (chkGraficarDmY.IsChecked == true)
            {
                sele_denu = true;
            }
            else
            {
                sele_denu = false;
            }

            //List<string> listMaps = new List<string> {"CATASTRO MINERO"};
            await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync("CATASTRO MINERO");
            int focusedRowHandle= dataGridResult.GetSelectedRowHandles()[0];
            string codigoValue = dataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
            string stateGraphic = dataGridResult.GetCellValue(focusedRowHandle, "PE_VIGCAT")?.ToString();
            string zoneDm = dataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            var v_zona_dm = dataBaseHandler.VerifyDatumDM(codigoValue);
            // Obtener el mapa Catastro

            //await LoadLayersToMapViewActive();
            Map map = await EnsureMapViewIsActiveAsync("CATASTRO MINERO");

            // Crear instancia de FeatureClassLoader y cargar las capas necesarias
            var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");
            await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_"+zoneDm, true);
            //if ((int)cbxSistema.SelectedValue == 1)
            //{
            //    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_CMI_CATASTRO_MINERO_WGS_" + zoneDm, true);
            //}
            var extentDm = ObtenerExtent(codigoValue, (int)cbxSistema.SelectedValue, int.Parse(tbxRadio.Text));
            // Llamar al método IntersectFeatureClassAsync desde la instancia
            string whereClause = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDm.xmin, extentDm.ymin, extentDm.xmax, extentDm.ymax);
        }

        private SubscriptionToken _eventToken = null;
        public async Task LoadLayersToMapViewActive()
        {
            if (MapView.Active == null)
            {
                // Suscribirse al evento DrawCompleteEvent
                _eventToken = DrawCompleteEvent.Subscribe(OnDrawComplete);
            }
            else
            {
                // MapView.Active está disponible, llamar directamente al método
                //_ = AddLayersBase(lyrBase, lyrCartaIGN);
                //EnableRadioButtonsStartingWithRbtn();
                Map map = await CommonUtilities.ArcgisProUtils.MapUtils.FindMapByNameAsync("CATASTRO MINERO");
                await CommonUtilities.ArcgisProUtils.MapUtils.ActivateMapAsync(map);
            }
        }
        private async void OnDrawComplete(MapViewEventArgs args)
        {
            // Desuscribirse del evento
            if (_eventToken != null)
            {
                DrawCompleteEvent.Unsubscribe(_eventToken);
                _eventToken = null;
            }
            // Llamar al método para agregar capas una vez que MapView.Active está disponible
            Map map = await CommonUtilities.ArcgisProUtils.MapUtils.FindMapByNameAsync("CATASTRO MINERO");
            await CommonUtilities.ArcgisProUtils.MapUtils.ActivateMapAsync(map);

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
                tcs.SetResult(MapView.Active?.Map);
            });

            // Esperar hasta que el evento se complete
            return await tcs.Task;
        }

        private static readonly Regex NumberRegex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
        private void tbxRadio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Agregar el nuevo texto al existente en el TextBox
            string currentText = (sender as System.Windows.Controls.TextBox)?.Text ?? string.Empty;
            string newText = currentText.Insert(
                (sender as System.Windows.Controls.TextBox)?.SelectionStart ?? 0, e.Text);

            // Validar si el texto es un número válido
            e.Handled = !NumberRegex.IsMatch(newText);
        }

        private void tbxRadio_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Permitir teclas específicas (como Backspace, Delete, flechas, etc.)
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab ||
                e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Enter || e.Key == Key.Escape)
            {
                e.Handled = false;
            }
        }

        private void tbxValue_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            }
        }
    }
}
