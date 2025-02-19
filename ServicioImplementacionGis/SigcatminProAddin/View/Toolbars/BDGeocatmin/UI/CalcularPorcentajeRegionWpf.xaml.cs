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
        private FeatureLayer pFeatureLayer_Dep { get; set; }
        private FeatureClass _featureClass_Dep { get; set; }
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

                //var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
                //Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                //                                                                            , AppConfig.userName
                //                                                                            , AppConfig.password);
                //var featureClassLoader = new FeatureClassLoader(geodatabase, MapView.Active.Map, GlobalVariables.CurrentZoneDm, "99");

                //var featureclassDepartamento = await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Departamento_WGS + GlobalVariables.CurrentZoneDm, false);

                //var Params = Geoprocessing.MakeValueArray(codigo, _featureClass_Dep);
                //var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetDepas, Params);

                //string jsonString = response.ReturnValue;
                listaIntersecciones = await ObtenerDepartamentosAsync(codigo, _featureClass_Dep);//JsonConvert.DeserializeObject<List<IntersectionResult>>(jsonString);



            });

            return listaIntersecciones;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            _geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                            , AppConfig.userName
                                                                            , AppConfig.password);
            var featureClassLoader = new FeatureClassLoader(_geodatabase, MapView.Active.Map, GlobalVariables.CurrentZoneDm, "99");

            //pFeatureLayer_Dep = await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Departamento_WGS + GlobalVariables.CurrentZoneDm, false);
             await QueuedTask.Run(() =>
            {
                _featureClass_Dep = _geodatabase.OpenDataset<FeatureClass>(FeatureClassConstants.gstrFC_Departamento_WGS + GlobalVariables.CurrentZoneDm);
            });

        }

        /// <summary>
        /// Emula la lógica de tu función "obtenerDepartamentos" de Python/arcpy,
        /// pero en C# usando ArcGIS Pro SDK.
        /// </summary>
        /// <param name="codigo">Valor para filtrar la capa 'Catastro' (campo CODIGOU)</param>
        /// <param name="flDepartamento">FeatureLayer que representa departamentos</param>
        /// <returns>Lista de DepartmentInfo con nombre, área y porcentaje</returns>
        public static async Task<List<IntersectionResult>> ObtenerDepartamentosAsync(string codigo, FeatureClass featureClass)
        {
            return await QueuedTask.Run(() =>
            {
                // 1) Buscar la capa "Catastro" en el mapa activo
                var map = MapView.Active?.Map;
                if (map == null)
                    throw new Exception("No hay un mapa activo.");

                // Suponiendo que la capa se llama "Catastro"
                var catastroLayer = map.Layers.FirstOrDefault(l => l.Name.Equals("Catastro", StringComparison.OrdinalIgnoreCase)) as FeatureLayer;
                if (catastroLayer == null)
                    throw new Exception("No se encontró la capa 'Catastro' en el mapa.");

                // 2) Obtener FeatureClass de Catastro y Departamentos
                FeatureClass catastroFC = catastroLayer.GetFeatureClass();
                FeatureClass depFC = featureClass;//flDepartamento.GetFeatureClass();

                // 3) Crear QueryFilter para filtrar CODIGOU = {codigo}
                var qf = new QueryFilter
                {
                    WhereClause = $"CODIGOU = '{codigo}'"
                };

                // 4) Buscar la entidad en "Catastro" y extraer área y geometría
                double areaDM = 0.0;    // HECTAGIS
                ArcGIS.Core.Geometry.Geometry geomDM = null; // Geometry del catastro
                using (RowCursor catCursor = catastroFC.Search(qf, false))
                {
                    if (catCursor.MoveNext())
                    {
                        using (Feature catRow = catCursor.Current as Feature)
                        {
                            // Campo HECTAGIS (área en hectáreas) 
                            areaDM = Convert.ToDouble(catRow["HECTAGIS"]);
                            // SHAPE
                            geomDM = catRow.GetShape();
                        }
                    }
                    else
                    {
                        // Si no encontró ninguna entidad con ese CODIGOU
                        throw new Exception($"No se encontró la entidad en Catastro con CODIGOU = {codigo}");
                    }
                }

                // 5) Iterar sobre todos los departamentos para intersectar con la geom del catastro
                List<IntersectionResult> listado = new List<IntersectionResult>();
                var spqf = new SpatialQueryFilter
                {
                    FilterGeometry = geomDM,
                    SpatialRelationship = SpatialRelationship.Intersects,
                };
                using (RowCursor depCursor = depFC.Search(spqf, false))
                {
                    while (depCursor.MoveNext())
                    {
                        using (Feature depRow = depCursor.Current as Feature)
                        {
                            // Campo con nombre de departamento, p.ej. "NM_DEPA"
                            string nombreDep = depRow["NM_DEPA"]?.ToString();
                            ArcGIS.Core.Geometry.Geometry depGeom = depRow.GetShape();

                            // Intersección
                            ArcGIS.Core.Geometry.Geometry intersection = GeometryEngine.Instance.Intersection(geomDM, depGeom);

                            double intersHectareas = 0.0;
                            if (intersection != null && !intersection.IsEmpty)
                            {
                                // El método .Area calcula el área según la proyección de la geometría
                                // Retorna m^2, por lo tanto / 10_000 para hectáreas
                                double areaM2 = GeometryEngine.Instance.Area(intersection);
                                intersHectareas = Math.Round(areaM2 / 10000.0, 2);
                            }

                            // 6) Calcular porcentaje en relación al área original "areaDM"
                            double porcentaje = 0.0;
                            if (areaDM > 0.0)
                                porcentaje = Math.Round((intersHectareas / areaDM) * 100.0, 2);

                            // 7) Agregar resultado a la lista
                            listado.Add(new IntersectionResult
                            {
                                Nombre = nombreDep,
                                area = intersHectareas,
                                Porcentaje = porcentaje
                            });
                        }
                    }
                }

                return listado;
            });
        }

        public class DepartmentInfo
        {
            public string Nombre { get; set; }
            public double Area { get; set; }         // Área de intersección
            public double Porcentaje { get; set; }   // Porcentaje respecto al área original
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
