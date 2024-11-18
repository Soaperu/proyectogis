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
using DevExpress.XtraExport.Helpers;

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
            ConfiguredataGridResultColumns();
            ConfiguredataGridDetailsColumns();
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
            double scaleX = canvasWidth*0.8 / (maxX - minX);
            double scaleY = canvasHeight*0.8 / (maxY - minY);
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


        /// <summary>
        /// Dibuja un polígono en el Canvas.
        /// </summary>
        /// <param name="coordinates">Coordenadas del polígono.</param>
        private void DrawPolygon(PointCollection coordinates)
        {
            // Crear el polígono
            Polygon polygon = new Polygon
            {
                Stroke = Brushes.Green,
                Fill = Brushes.LightGreen,
                StrokeThickness = 2,
                Points = coordinates
            };

            // Limpiar el Canvas
            PolygonCanvas.Children.Clear();
            // Agregar el polígono al Canvas
            PolygonCanvas.Children.Add(polygon);

            // Etiquetar cada vértice
            for (int i = 0; i < coordinates.Count; i++)
            {
                var vertex = coordinates[i];
                var label = new TextBlock
                {
                    Text = $"{i+1}",
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

            cbp.Add(new ComboBoxPairs("Por Código", 1));
            cbp.Add(new ComboBoxPairs("Por Nombre", 2));

            // Asignar la lista al ComboBox
            cbxTypeConsult.DisplayMemberPath = "_Key";
            cbxTypeConsult.SelectedValuePath = "_Value";
            cbxTypeConsult.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            cbxTypeConsult.SelectedIndex = 0;
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

        private void ConfiguredataGridDetailsColumns()
        {
            var tableView = dataGridDetails.View as TableView;
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
                Width = 120
            };

            GridColumn norteColumn = new GridColumn
            {
                FieldName = "NORTE",
                Header = "Norte",
                Width = 120
            };

            // Agregar columnas al GridControl
            dataGridDetails.Columns.Add(verticeColumn);
            dataGridDetails.Columns.Add(esteColumn);
            dataGridDetails.Columns.Add(norteColumn);

        }
        private void ConfiguredataGridResultColumns()
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

        private DataTable ObtenerCoordenadas(string codigovalue, int datum)
        {
            DataTable filteredTable;
            string[] requiredColumns = { "CD_NUMVER", "CD_COREST_E", "CD_CORNOR_E" };
            var dmrRecords = dataBaseHandler.GetDMDataWGS84(codigovalue);

            if (datum == 2)
            {            
                requiredColumns = new string[]{ "CD_NUMVER", "CD_COREST", "CD_CORNOR"};
            }
            filteredTable = FilterColumns(dmrRecords, requiredColumns);
            // Renombrar las columnas
            filteredTable.Columns["CD_NUMVER"].ColumnName = "VERTICE";
            filteredTable.Columns[requiredColumns[1]].ColumnName = "ESTE";
            filteredTable.Columns[requiredColumns[2]].ColumnName = "NORTE";


            return filteredTable;
        }

        private void dataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var tableView = sender as TableView;
            int currentDatum = (int)cbxSistema.SelectedValue;
            if (tableView != null)
            {
                // Obtener el índice de la fila seleccionada
                int focusedRowHandle = tableView.FocusedRowHandle;

                if (focusedRowHandle >= 0) // Verifica si hay una fila seleccionada
                {
                    // Obtener el valor de una columna específica (por ejemplo, "CODIGO")
                    string codigoValue = dataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();

                    // Mostrar el valor obtenido
                    System.Windows.MessageBox.Show($"Valor de CODIGO: {codigoValue}", "Información de la Fila");

                    // Llamar a funciones adicionales con el valor seleccionado
                    var dmrRecords = ObtenerCoordenadas(codigoValue, currentDatum);
                    dataGridDetails.ItemsSource = dmrRecords.DefaultView;
                    GraficarCoordenadas(dmrRecords);
                }
                else
                {
                    System.Windows.MessageBox.Show("No hay ninguna fila seleccionada.", "Error");
                }
            }
        }

        
    }
}
