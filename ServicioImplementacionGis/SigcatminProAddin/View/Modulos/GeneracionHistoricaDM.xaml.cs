﻿using System;
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
using DevExpress.DataAccess.Native.Web;
using Newtonsoft.Json;
using CommonUtilities.ArcgisProUtils.Models;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para GeneracionHistoricaDM.xaml
    /// </summary>
    public partial class GeneracionHistoricaDM : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;

        public GeneracionHistoricaDM()
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

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

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
            DataGridResult.ItemsSource = null;
            DataGridDetails.ItemsSource = null;
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
                string value = (string)CbxTypeConsult.SelectedValue.ToString();
                var countRecords = dataBaseHandler.CountRecords(value, TbxValue.Text);
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
                var dmrRecords = dataBaseHandler.GetUniqueDM_H(TbxValue.Text, (int)CbxTypeConsult.SelectedValue);
                calculatedIndex(DataGridResult, records, DatagridResultConstantsDM.ColumNames.Index);
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

            var tableView = DataGridResult.View as DevExpress.Xpf.Grid.TableView;
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
                    TbxArea.Text = areaValue;
                    TbxArea.IsReadOnly = true;
                    // Llamar a funciones adicionales con el valor seleccionado
                    ClearCanvas();
                    var dmrRecords = ObtenerCoordenadas(codigoValue, currentDatum);
                    DataGridDetails.ItemsSource = dmrRecords.DefaultView;
                    GraficarCoordenadas(dmrRecords);
                    GlobalVariables.CurrentTipoEx = DataGridResult.GetCellValue(focusedRowHandle, "TIPO")?.ToString();
                }
            }

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
                Header = DatagridResultConstantsDM.Headers.Index, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNames.Index,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.Widths.Index
            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Codigo, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.Headers.Codigo,    // Encabezado visible
                Width = DatagridResultConstantsDM.Widths.Codigo            // Ancho de la columna
            };

            GridColumn nombreColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Nombre,
                Header = DatagridResultConstantsDM.Headers.Nombre,
                Width = DatagridResultConstantsDM.Widths.Nombre
            };

            GridColumn pe_vigcatColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.PeVigCat,
                Header = DatagridResultConstantsDM.Headers.PeVigCat,
                Width = DatagridResultConstantsDM.Widths.PeVigCat
            };

            GridColumn zonaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Zona,
                Header = DatagridResultConstantsDM.Headers.Zona,
                Width = DatagridResultConstantsDM.Widths.Zona
            };
            GridColumn tipoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Tipo,
                Header = DatagridResultConstantsDM.Headers.Tipo,
                Width = DatagridResultConstantsDM.Widths.Tipo
            };
            GridColumn estadoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Estado,
                Header = DatagridResultConstantsDM.Headers.Estado,
                Width = DatagridResultConstantsDM.Widths.Estado
            };
            GridColumn naturalezaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Naturaleza,
                Header = DatagridResultConstantsDM.Headers.Naturaleza,
                Width = DatagridResultConstantsDM.Widths.Naturaleza
            };
            GridColumn cartaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Carta,
                Header = DatagridResultConstantsDM.Headers.Carta,
                Width = DatagridResultConstantsDM.Widths.Carta
            };
            GridColumn hectareaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Hectarea,
                Header = DatagridResultConstantsDM.Headers.Hectarea,
                Width = DatagridResultConstantsDM.Widths.Hectarea
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
            DataTable filteredTable = null;
            try
            {

                string[] requiredColumns = {
                        DatagridDetailsConstants.RawColumNames.Vertice,
                        DatagridDetailsConstants.RawColumNames.CoorEsteE,
                        DatagridDetailsConstants.RawColumNames.CoorNorteE };

                var dmrRecords = dataBaseHandler.GetDMDataWGS84(codigoValue);
                if (dmrRecords.Rows.Count > 0)
                {
                    var originalDatumDm = dmrRecords.Rows[0]["SC_CODDAT"];
                    if (datum == int.Parse(originalDatumDm.ToString()))
                    {
                        requiredColumns = new string[] {
                        DatagridDetailsConstants.RawColumNames.Vertice,
                        DatagridDetailsConstants.RawColumNames.CoorEste,
                        DatagridDetailsConstants.RawColumNames.CoorNorte };
                    }
                }

                filteredTable = FilterColumns(dmrRecords, requiredColumns);
                // Renombrar las columnas
                filteredTable.Columns[DatagridDetailsConstants.RawColumNames.Vertice].ColumnName = DatagridDetailsConstants.ColumnNames.Vertice;
                filteredTable.Columns[requiredColumns[1]].ColumnName = DatagridDetailsConstants.ColumnNames.Este;
                filteredTable.Columns[requiredColumns[2]].ColumnName = DatagridDetailsConstants.ColumnNames.Norte;

                return filteredTable;
            }
            catch (Exception ex)
            {
                return filteredTable;
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
            double radioMeters = radioKm * 1000;
            // Inicializar las variables para almacenar los valores extremos
            double xmin = int.MaxValue;
            double xmax = int.MinValue;
            double ymin = int.MaxValue;
            double ymax = int.MinValue;

            // Iterar sobre las filas para calcular los valores extremos
            foreach (DataRow row in coordenadasTable.Rows)
            {
                double este = Convert.ToDouble(row["ESTE"]);
                double norte = Convert.ToDouble(row["NORTE"]);

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

        private void DataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //var tableView = sender as TableView;
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
                    TbxArea.Text = areaValue;
                    TbxArea.IsReadOnly = true;
                    // Llamar a funciones adicionales con el valor seleccionado
                    ClearCanvas();
                    var dmrRecords = ObtenerCoordenadas(codigoValue, currentDatum);
                    DataGridDetails.ItemsSource = dmrRecords.DefaultView;
                    GraficarCoordenadas(dmrRecords);
                    GlobalVariables.CurrentTipoEx = DataGridResult.GetCellValue(focusedRowHandle, "TIPO")?.ToString();
                }
            }
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

        private void calculatedIndex(GridControl table, int records, string columnName)
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

        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            CbxSistema.SelectedIndex = 0;
            CbxTypeConsult.SelectedIndex = 0;
            CbxZona.SelectedIndex = 1;
            BtnGraficar.IsEnabled = false;
        }


        private void CbxTypeConsult_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TbxValue.Clear();
        }

        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
           
            BtnGraficar.IsEnabled = false;
            if (string.IsNullOrEmpty(TbxRadio.Text))
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

            List<string> mapsToDelete = new List<string>()
            {
                GlobalVariables.mapNameCatastro,
                GlobalVariables.mapNameDemarcacionPo,
                GlobalVariables.mapNameCartaIgn
            };
            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);
            ProgressBarUtils progressBar = new ProgressBarUtils("Evaluando y graficando Generación Histórica DM.");
            progressBar.Show();

            int datum = (int)CbxSistema.SelectedValue;
            string datumStr = CbxSistema.Text;
            int radio = int.Parse(TbxRadio.Text);
            string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
            GlobalVariables.CurrentRadioDm = radio;
            await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync("CATASTRO MINERO");
            int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
            string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
            GlobalVariables.CurrentCodeDm = codigoValue;
            string stateGraphic = DataGridResult.GetCellValue(focusedRowHandle, "PE_VIGCAT")?.ToString();
            string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
            GlobalVariables.CurrentZoneDm = zoneDm;
            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();

            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            //var v_zona_dm = dataBaseHandler.VerifyDatumDM(codigoValue);
            //var v_zona_dm = "18";
            string fechaArchi = DateTime.Now.Ticks.ToString();
            GlobalVariables.idExport = fechaArchi;
            string catastroShpName = "Catastro" + fechaArchi;
            GlobalVariables.CurrentShpName = catastroShpName;
            string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            string dmShpName = "DM" + fechaArchi;
            string dmShpNamePath = "DM" + fechaArchi + ".shp";


            DataRowView rowView = (DataRowView)DataGridResult.GetRow(focusedRowHandle);
            DataRow row = rowView.Row;

            var extentDmRadio = ObtenerExtent(codigoValue, datum, radio);

            await ComplementaryProcessesUtils.EvaluationDmByCodeHistorico(codigoValue, row, radio, datum);



            Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                                                                                                  // Crear instancia de FeatureClassLoader y cargar las capas necesarias
            var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");
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

            /*DEMARCACION*/

            try
            {
                await MapUtils.CreateMapAsync(GlobalVariables.mapNameDemarcacionPo); //"DEMARCACION POLITICA"
                Map mapD = await EnsureMapViewIsActiveAsync("DEMARCACION POLITICA");
                featureClassLoader = new FeatureClassLoader(geodatabase, mapD, zoneDm, "99");
                var fl = await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(mapD, Path.Combine(outputFolder, GlobalVariables.dmShpNamePath));
                //Carga capa Distrito
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DIS_DISTRITO_WGS_" + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DIS_DISTRITO_" + zoneDm, false);
                }
                await SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_dist);
                await LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_dist, "NM_DIST", 7, "#4e4e4e", "Bold");
                //Carga capa Provincia
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_PRO_PROVINCIA_WGS_" + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_PRO_PROVINCIA_" + zoneDm, false);
                }
                await SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_prov);
                await LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_prov, "NM_PROV", 9, "#343434");
                //Carga capa Departamento
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_" + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_" + zoneDm, false);
                }
                await SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_depa);
                await LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_depa, "NM_DEPA", 12, "#000000", "Bold");
                //var mapView = MapView.Active as MapView;
                SymbologyUtils.CustomLinePolygonLayer((FeatureLayer)fl, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(0, 255, 255, 0), CIMColor.CreateRGBColor(255, 0, 0));
                await LayerUtils.ChangeLayerNameByFeatureLayerAsync((FeatureLayer)fl, "Catastro");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                progressBar.Dispose();
            }

            /*MAPA IGN*/
            // Obtener el mapa Carta IGN//
            try
            {
                await MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
                Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCartaIgn);
                featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");
                var fl1 = await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(mapC, Path.Combine(outputFolder, GlobalVariables.dmShpNamePath));
                //Carga capa Distrito
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DIS_DISTRITO_WGS_" + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DIS_DISTRITO_" + zoneDm, false);
                }
                await SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_dist);
                await LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_dist, "NM_DIST", 7, "#4e4e4e", "Bold");
                //Carga capa Provincia
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_PRO_PROVINCIA_WGS_" + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_PRO_PROVINCIA_" + zoneDm, false);
                }
                await SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_prov);
                await LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_prov, "NM_PROV", 9, "#343434");

                //Carga capa Departamento
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_" + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync("DATA_GIS.GPO_DEP_DEPARTAMENTO_" + zoneDm, false);
                }
                await SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_depa);
                await LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_depa, "NM_DEPA", 12, "#000000", "Bold");

                SymbologyUtils.CustomLinePolygonLayer((FeatureLayer)fl1, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(255, 0, 0, 0), CIMColor.CreateRGBColor(255, 0, 0));
                await LayerUtils.ChangeLayerNameByFeatureLayerAsync((FeatureLayer)fl1, "Catastro");
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
                await RasterUtils.AddRasterCartaIGNLayerAsync(mosaicLayer, geodatabase, mapC, queryListCartaIGN);
                //await MapView.Active.ZoomToAsync(fl1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                progressBar.Dispose();
            }
            progressBar.Dispose();
            BtnGraficar.IsEnabled = true;

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
                                        row["LEYENDA"] = "G6";
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
                Map map = await CommonUtilities.ArcgisProUtils.MapUtils.FindMapByNameAsync(mapName);
                await CommonUtilities.ArcgisProUtils.MapUtils.ActivateMapAsync(map);

                // Completar la tarea con el mapa activo
                //tcs.SetResult(MapView.Active.Map);
                tcs.SetResult(map);
            });

            // Esperar hasta que el evento se complete
            return await tcs.Task;
        }

        private static readonly Regex NumberRegex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
        private void TbxRadio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Agregar el nuevo texto al existente en el TextBox
            string currentText = (sender as System.Windows.Controls.TextBox)?.Text ?? string.Empty;
            string newText = currentText.Insert(
                (sender as System.Windows.Controls.TextBox)?.SelectionStart ?? 0, e.Text);

            // Validar si el texto es un número válido
            e.Handled = !NumberRegex.IsMatch(newText);
        }

        private void TbxRadio_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Permitir teclas específicas (como Backspace, Delete, flechas, etc.)
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab ||
                e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Enter || e.Key == Key.Escape)
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Permite usar la tecla enter para realizar la búsqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxValue_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TbxValue.Focus();
        }
    }
}
