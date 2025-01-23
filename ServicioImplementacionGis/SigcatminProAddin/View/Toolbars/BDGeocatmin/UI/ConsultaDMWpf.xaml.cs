using ArcGIS.Core.Data;
using ArcGIS.Core.Data.Topology;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.CIM;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using CommonUtilities.ArcgisProUtils.Models;
using DatabaseConnector;
using DevExpress.Xpf.Grid;
using DevExpress.XtraPrinting;
using SigcatminProAddin.Models.Constants;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SigcatminProAddin.View.Toolbars.BDGeocatmin.UI
{
    /// <summary>
    /// Lógica de interacción para ConsultaDMWpf.xaml
    /// </summary>
    public partial class ConsultaDMWpf : Window
    {
        private DatabaseHandler dataBaseHandler;
        public ConsultaDMWpf()
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
                    return;
                }
                else
                {
                    layer.Select(queryFilter);
                };


                featureInfo.Codigo = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                featureInfo.Nombre = selectedFeature.GetOriginalValue(selectedFeature.FindField("CONCESION")).ToString();
                featureInfo.Fecha = selectedFeature.GetOriginalValue(selectedFeature.FindField("FEC_DENU")).ToString();
                featureInfo.Area = selectedFeature.GetOriginalValue(selectedFeature.FindField("HECTAREA")).ToString();
                featureInfo.Titular = selectedFeature.GetOriginalValue(selectedFeature.FindField("TIT_CONCES")).ToString();
                featureInfo.TipoDM = selectedFeature.GetOriginalValue(selectedFeature.FindField("D_ESTADO")).ToString();
                //featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                //featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                //featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                featureInfo.Contador = selectedFeature.GetOriginalValue(selectedFeature.FindField("CONTADOR")).ToString();
                featureInfo.Hora = selectedFeature.GetOriginalValue(selectedFeature.FindField("HOR_DENU")).ToString();
                featureInfo.Prioridad = selectedFeature.GetOriginalValue(selectedFeature.FindField("EVAL")).ToString();
            });
            
            return featureInfo;
        }

        

        private async void DataGridSelectedPolygonsTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
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
                    FeatureInfo selectedFeature = await GetFeatureInfobyQuery($"CODIGOU = '{codigoValue}'", "Catastro");

                    //Actualizamos valores del ui
                    TbxCodigo.Text = selectedFeature.Codigo;
                    TbxNombre.Text = selectedFeature.Nombre;
                    TbxFecha.Text = selectedFeature.Fecha;
                    TbxArea.Text = selectedFeature.Area;
                    TbxTitular.Text = selectedFeature.Titular;
                    TbxTipoDM.Text = selectedFeature.TipoDM;
                    //TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                    //TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                    //TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();

                    TbxContador.Text = selectedFeature.Contador;
                    TbxHora.Text = selectedFeature.Hora;
                    TbxPrioridad.Text = selectedFeature.Prioridad;
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
        public string Prioridad {  get; set; }
    }
}
