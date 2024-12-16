using ArcGIS.Core.Geometry;
using ArcGIS.Core.Data;
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
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using SigcatminProAddin.Models.Constants;
using System.Data;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using DevExpress.Xpf.Grid.GroupRowLayout;
using CommonUtilities.ArcgisProUtils.Models;
using DatabaseConnector;
using CommonUtilities;
using DevExpress.Pdf.Native;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin.UI
{
    /// <summary>
    /// Lógica de interacción para ListarCoordenadasWpf.xaml
    /// </summary>
    public partial class ListarCoordenadasWpf : Window
    {
        private List<Row> listRows = new List<Row>();
        private DatabaseHandler dataBaseHandler;
        public ListarCoordenadasWpf()
        {
            InitializeComponent();
            ConfigureDataGridSelectedPolygonsColumns();
            dataBaseHandler = new DatabaseHandler();
        }

        private void ConfigureDataGridSelectedPolygonsColumns()
        {
            var tableView = DataGridSelectedPolygons.View as DevExpress.Xpf.Grid.TableView;
            DataGridSelectedPolygons.Columns.Clear();
            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            GridColumn numeroColumn = new GridColumn
            {
                FieldName = DataGridSelectedPolygonsConstants.ColumnNames.Numero,
                Header = DataGridSelectedPolygonsConstants.Headers.Numero,
                Width = new GridColumnWidth(DataGridSelectedPolygonsConstants.Widths.Numero, GridColumnUnitType.Star)
            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DataGridSelectedPolygonsConstants.ColumnNames.Codigo,
                Header = DataGridSelectedPolygonsConstants.Headers.Codigo,
                Width = new GridColumnWidth(DataGridSelectedPolygonsConstants.Widths.Codigo, GridColumnUnitType.Star)
            };
            GridColumn nombreColumn = new GridColumn
            {
                FieldName = DataGridSelectedPolygonsConstants.ColumnNames.Nombre,
                Header = DataGridSelectedPolygonsConstants.Headers.Nombre,
                Width = new GridColumnWidth(DataGridSelectedPolygonsConstants.Widths.Nombre, GridColumnUnitType.Star)
            };

            GridColumn areaColumn = new GridColumn
            {
                FieldName = DataGridSelectedPolygonsConstants.ColumnNames.Area,
                Header = DataGridSelectedPolygonsConstants.Headers.Area,
                Width = new GridColumnWidth(DataGridSelectedPolygonsConstants.Widths.Area, GridColumnUnitType.Star)
            };

            // Agregar columnas al GridControl
            DataGridSelectedPolygons.Columns.Add(numeroColumn);
            DataGridSelectedPolygons.Columns.Add(codigoColumn);
            DataGridSelectedPolygons.Columns.Add(nombreColumn);
            DataGridSelectedPolygons.Columns.Add(areaColumn);

        }

        public async Task UpdateContent(MapPoint mapPoint)
        {
            // Actualiza el contenido de la ventana con los datos de mapPoint
            // Por ejemplo, mostrar las coordenadas en un TextBlock
            var map = MapView.Active.Map;
            var selectedLayers = MapView.Active.GetSelectedLayers();
            var layer = selectedLayers.OfType<FeatureLayer>().FirstOrDefault();
            var listRows = new List<ListarCoordenadasModel>();
            await QueuedTask.Run(async() =>
            {
                if (layer == null)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, selecciona una capa en el panel de contenido.", "Advertencia");
                    return;
                }
                if (layer.Name.ToLower() != "catastro")
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, selecciona la capa de Catastro.", "Advertencia");
                    return;
                }

                listRows = await CommonUtilities.ArcgisProUtils.MapUtils.GetRowsAslistByClick(mapPoint);
                
            });

        Dispatcher.Invoke(() =>
            {
                DataTable records = new DataTable();
                records.Columns.Add("NUMERO", typeof(string));
                records.Columns.Add("CODIGO", typeof(string));
                records.Columns.Add("NOMBRE", typeof(string));
                records.Columns.Add("AREA", typeof(string));
                foreach (var row in listRows)
                {
                    string codigou = row.codigo;
                    string nombre = row.nombre;
                    string numero = row.numero;
                    string area = row.area;

                    records.Rows.Add(numero, codigou, nombre, area);
                }
                DataGridSelectedPolygons.ItemsSource = records.DefaultView;


                lblPolygonsFound.Content = $"Se encontraron {records.Rows.Count} registros";


                



            });
        }

        private void btnCopiarContenido_Click(object sender, RoutedEventArgs e)
        {
            //string text = "Código = 010090117\r\nNombre = MOGOSENCA 4\r\n--------------------------------------------------\r\n    Vert.          Norte          Este\r\n--------------------------------------------------\r\n     001     9 163 000.00     811 000.00\r\n     002     9 161 000.00     811 000.00\r\n     003     9 161 000.00     809 000.00\r\n     004     9 163 000.00     809 000.00\r\n--------------------------------------------------\r\n          Area UTM =  400.0000  (Ha)\r\n--------------------------------------------------";
            Clipboard.SetText(textBlock.Text);
            MessageBox.Show("Texto copiado");
        }

        private void DataGridSelectedPolygonsTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            string texto = "";
            var tableView = sender as DevExpress.Xpf.Grid.TableView;
            if (tableView != null && tableView.Grid.VisibleRowCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                int focusedRowHandle = tableView.FocusedRowHandle;

                if (focusedRowHandle >= 0) // Verifica si hay una fila seleccionada
                {
                    // Obtener el valor de una columna específica (por ejemplo, "CODIGO")
                    string codigoValue = DataGridSelectedPolygons.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
                    string nombre = DataGridSelectedPolygons.GetCellValue(focusedRowHandle, "NOMBRE")?.ToString();
                    string area = DataGridSelectedPolygons.GetCellValue(focusedRowHandle, "AREA")?.ToString();
                    var records = dataBaseHandler.GetDMData(codigoValue);
                    texto += $"Código = {codigoValue} \n";
                    texto += $"Nombre = {nombre}\n";
                    texto += "--------------------------------------------------\n";
                    texto += new string(' ', 3) + " Vert." + new string(' ', 10) + "Norte" + new string(' ', 10) + "Este\n";
                    texto += "--------------------------------------------------\n";

                    for(int i = 0; i< records.Rows.Count; i++)
                    {
                        var este = Math.Round(double.Parse(records.Rows[i]["CD_COREST"].ToString()),3);
                        var norte = Math.Round(double.Parse(records.Rows[i]["CD_CORNOR"].ToString()),3);
                        var esteFormateado = string.Format("{0:### ###.#0}", este);
                        var norteFormateado = string.Format("{0:# ### ###.#0}", norte);
                        texto += new string(' ', 5) + i.ToString().PadLeft(3, '0');
                        texto += new string(' ', 5) + esteFormateado;
                        texto += new string(' ', 5) + norteFormateado + "\n";

                    }
                    texto += "--------------------------------------------------\n";
                    texto += new string(' ', 10) + $"Área UTM = {area} (Ha)\n";
                    texto += "--------------------------------------------------\n";
                    textBlock.Text = texto;
                }
            }

        }
    }
}
