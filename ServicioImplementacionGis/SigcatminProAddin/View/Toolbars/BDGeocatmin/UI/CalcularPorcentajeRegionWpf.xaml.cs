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
using CommonUtilities.ArcgisProUtils;
using System.Security.Policy;
using ArcGIS.Desktop.Core.Geoprocessing;
using Newtonsoft.Json;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin.UI
{
    /// <summary>
    /// Lógica de interacción para ListarCoordenadasWpf.xaml
    /// </summary>
    public partial class CalcularPorcentajeRegionWpf : Window
    {
        private DatabaseHandler dataBaseHandler;
        private Geodatabase _geodatabase;
        private Map _map;
        public FeatureLayer pFeatureLayer_Dep { get; private set; }
        public CalcularPorcentajeRegionWpf()
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
            await QueuedTask.Run(async () =>
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

        private async void DataGridSelectedPolygonsTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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

                    var listaIntersecciones = await GetIntersectionInfobyQuery($"CODIGOU = '{codigoValue}'", codigoValue, "Catastro");

                    texto += "Departamento".PadRight(20, ' ');
                    texto += "Hectáreas(Ha.)".PadRight(20, ' ');
                    texto += "Porcentaje(%)".PadRight(13, ' ') + "\n";

                    foreach (var item in listaIntersecciones)
                    {
                        texto += item.Nombre.PadRight(20,' ');
                        texto += item.area.ToString().PadRight(15,' ').PadLeft(20,' ');
                        texto += item.Porcentaje.ToString().PadRight(8, ' ').PadLeft(13, ' ')+ "\n";
                    }

                    texto += "--------------------------------------------------\n";
                    texto += "TOTAL".PadRight(20, ' ');
                    texto += area.PadRight(15, ' ').PadLeft(20, ' ');
                    texto += "100.00".PadRight(8, ' ').PadLeft(13, ' ') + "\n";

                    
                    textBlock.Text = texto;
                }
            }

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gridHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Comprueba que el botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }

        public async Task<List<IntersectionResult>> GetIntersectionInfobyQuery(string p_Filtro, string codigo,string layerName)
        {
            Feature selectedFeature = null;
            List<IntersectionResult> listaIntersecciones = new List<IntersectionResult>();

            await QueuedTask.Run(async () =>
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

                var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);
                var featureClassLoader = new FeatureClassLoader(geodatabase, MapView.Active.Map, GlobalVariables.CurrentZoneDm, "99");

                var featureclassDepartamento = await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Departamento_WGS + GlobalVariables.CurrentZoneDm, false);

                var Params = Geoprocessing.MakeValueArray(codigo);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetDepas, Params);

                string jsonString = response.ReturnValue;
                listaIntersecciones = JsonConvert.DeserializeObject<List<IntersectionResult>>(jsonString);



            });

            return listaIntersecciones;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            //_geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
            //                                                                            , AppConfig.userName
            //                                                                            , AppConfig.password);
            //var featureClassLoader = new FeatureClassLoader(_geodatabase, MapView.Active.Map, GlobalVariables.CurrentZoneDm, "99");

            //pFeatureLayer_Dep = await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Departamento_WGS + GlobalVariables.CurrentZoneDm, false);
        }
    }
    public class IntersectionResult
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("area")]
        public double area { get; set; }

        [JsonProperty("porcentaje")]
        public double Porcentaje { get; set; }
    }
}
