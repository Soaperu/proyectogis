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

using ArcGIS.Core.Geometry;
//using ArcGIS.Core.Internal.Geometry;
//using ArcGIS.Core.Internal.CIM;
using DevExpress.XtraCharts.Native;
using System.Net.NetworkInformation;
/*----aqui----*/




using ArcGIS.Desktop.Editing;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Desktop.Internal.Core.Conda;
//using System.Windows.Forms;



namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para SimulaPetitorios.xaml
    /// </summary>
    public partial class SimulaPetitorios : Page
    {
        private FeatureClassLoader featureClassLoader;
        public Geodatabase geodatabase;
        //private ArcGIS.Core.Geometry.Polyline polyline;
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;
        string zona;
        string tipo = "Polígono";
        //string archi = GlobalVariables.idExport;
        FeatureLayer layer;
        string poligonoGen = "";

        public SimulaPetitorios()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            //ConfigureDataGridResultColumns();
            //ConfigureDataGridDetailsColumns();
            dataBaseHandler = new DatabaseHandler();
            //CbxTypeConsult.SelectedIndex = 0;
            TbxRadio.Text = "5";
            BtnGraficar.IsEnabled = false;


        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxEste.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TbxNorte.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // Convertir las coordenadas a numérico (Val en VB se asemeja a Convert.ToDouble)
            double este = 0;
            double norte = 0;

            // Intentar convertir el texto a double. Si falla, podrías manejar el error.
            double.TryParse(TbxEste.Text, out este);
            double.TryParse(TbxNorte.Text, out norte);

            // Crear la cadena valor con el número de punto y las coordenadas
            string valor = "Punto " + (listBoxVertices.Items.Count + 1) + ":  " + este + "; " + norte;
            listBoxVertices.Items.Add(valor);
        }

        private void TbxEste_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^(\d{0,6}(\.\d{0,4})?)$");

            // Validar si el texto ingresado es válido
            e.Handled = !regex.IsMatch(((TextBox)sender).Text.Insert(((TextBox)sender).SelectionStart, e.Text));
        }

        private void TbxNorte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^(\d{0,7}(\.\d{0,4})?)$");

            // Validar si el texto ingresado es válido
            e.Handled = !regex.IsMatch(((TextBox)sender).Text.Insert(((TextBox)sender).SelectionStart, e.Text));
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
        private void CbxZona_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
            cbp.Add(new ComboBoxPairs("Seleccionar Zona", 0));
            cbp.Add(new ComboBoxPairs("17", 17));
            cbp.Add(new ComboBoxPairs("18", 18));
            cbp.Add(new ComboBoxPairs("19", 19));

            // Asignar la lista al ComboBox
            CbxZona.DisplayMemberPath = "_Key";
            CbxZona.SelectedValuePath = "_Value";
            CbxZona.ItemsSource = cbp;

            // Seleccionar la opción 18 por defecto
            CbxZona.SelectedIndex = 0;

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

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
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
            if (listBoxVertices.Items.Count >= 4 && tipo != null && zona != null)
            {
                BtnGraficar.IsEnabled = true;
            }
            else
            {
                BtnGraficar.IsEnabled = false;
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

        private void CbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                zona = CbxZona.SelectedItem.ToString();
                if (listBoxVertices.Items.Count >= 4 && zona != null)
                {
                    BtnGraficar.IsEnabled = true;
                }
            }
            catch (Exception)
            {

                //throw;
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

        private async void BtnGeneraPoligono_Click(object sender, RoutedEventArgs e)
        {

            int zona1 = (int)CbxZona.SelectedValue;
            if (zona1 == 0)
            {
                BtnGraficar.IsEnabled = false;
                string message = "Por favor ingrese Zona (17, 18, 19)";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertencia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            List<string> mapsToDelete = new List<string>()
                 {
                     GlobalVariables.mapNameCatastro,
                     GlobalVariables.mapNameDemarcacionPo,
                     GlobalVariables.mapNameCartaIgn
                 };

            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);

            //AddCoordListBox();
            var dmrRecords = ObtenerCoordenadasDesdeListBox(listBoxVertices);
            //DataGridDetails.ItemsSource = dmrRecords.DefaultView;
            GraficarCoordenadas(dmrRecords);



            var zoneDm = CbxZona.SelectedValue.ToString();

            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);

            await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro);
            Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro);
            featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");


            if (listBoxVertices.Items.Count >= 4 && tipo != null && zona != null)
                BtnGraficar.IsEnabled = true;
            //zona = CbxZona.SelectedItem.ToString();
            string archi = DateTime.Now.Ticks.ToString();
            poligonoGen = "Poligono" + archi;
            zona = CbxZona.SelectedValue.ToString();
            IEnumerable<string> linesString = listBoxVertices.Items.Cast<string>();
            var vertices = CommonUtilities.ArcgisProUtils.FeatureClassCreatorUtils.GetVerticesFromListBoxItems(linesString);
            layer = await CommonUtilities.ArcgisProUtils.FeatureClassCreatorUtils.CreatePolygonInNewPoGdbAsync(GlobalVariables.pathFileTemp, "GeneralGDB", poligonoGen, vertices, zona);
            //layer = await CommonUtilities.ArcgisProUtils.FeatureClassCreatorUtils.CreatePolygonInNewGdbAsync(GlobalVariables.pathFileTemp, "GeneralGDB", poligonoGen, vertices, zona);
            CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer(layer, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(0, 255, 255, 0), CIMColor.CreateRGBColor(255, 0, 0));

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Focus();
            }

        }

        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            if (poligonoGen == "")
            {
                return;
            }
            ProgressBarUtils progressBar = new ProgressBarUtils("Evaluando y graficando Simulación de Petitorios");
            progressBar.Show();
            int datum = (int)CbxSistema.SelectedValue;
            int radio = int.Parse(TbxRadio.Text);
            string datumStr = CbxSistema.Text;
            string zoneDm = CbxZona.SelectedValue.ToString();
            var codigoValue = "000000001";
            ArcGIS.Core.Geometry.Geometry polygon = null;
            ArcGIS.Core.Geometry.Envelope envelope = null;
            string fechaArchi = DateTime.Now.Ticks.ToString();
            GlobalVariables.idExport = fechaArchi;
            //string catastroShpName = "Catastro" + fechaArchi;
            string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
            string dmShpNamePath = "DM" + fechaArchi + ".shp";

            GlobalVariables.idExport = fechaArchi;
            string catastroShpName = "Catastro" + fechaArchi;
            string catastroTouchesShpName = "CatastroTouches" + fechaArchi;
            string catastroInterseShpName = "CatastroInterse" + fechaArchi;
            GlobalVariables.CurrentShpName = catastroShpName;
            string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            string dmShpName = "DM" + fechaArchi;



            double minX = 0;
            double minY = 0;
            double maxX = 0;
            double maxY = 0;

            double minX_Carta = 0;
            double minY_Carta = 0;
            double maxX_Carta = 0;
            double maxY_Carta = 0;

            DataTable dtTouches = new DataTable();
            DataTable dtIntersec = new DataTable();
            await QueuedTask.Run(async () =>
            {
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
                       WhereClause = "1=1"
                   };

                var fl = await featureClassLoader.LoadFeatureClassAsyncGDB(poligonoGen, false);
                CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer((FeatureLayer)fl, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(0, 255, 255, 0), CIMColor.CreateRGBColor(255, 0, 0));
                await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameByFeatureLayerAsync((FeatureLayer)fl, "Poligono");

                // Encontrando Distritos superpuestos a DM con

                envelope = featureClassLoader.pFeatureLayer_polygon.QueryExtent();

                using (RowCursor rowCursor = featureClassLoader.pFeatureLayer_polygon.GetFeatureClass().Search(filter, false))
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

                if (polygon is Polygon)
                {
                    // Aquí podemos trabajar con el polígono
                    Polygon polygonGeometry = polygon as Polygon;

                    // Obtener los límites del polígono (Extent)
                    Envelope envelope = polygonGeometry.Extent;

                    // Obtener las coordenadas mínimas y máximas
                    //  if (zoneDm == "18")
                    // {
                    minX = envelope.XMin;
                    minY = envelope.YMin;
                    maxX = envelope.XMax;
                    maxY = envelope.YMax;

                    //reProyectar a Zona 18 en caso sea zona 17 o 19 para caputrar los datos minimos y maximos
                    if (zoneDm == "17")
                    {
                        SpatialReference srZone17 = SpatialReferenceBuilder.CreateSpatialReference(32717); // UTM zona 17
                        SpatialReference srZone18 = SpatialReferenceBuilder.CreateSpatialReference(32718); // UTM zona 18
                        if (polygonGeometry.SpatialReference == srZone17)
                        {
                            // Transformar el polígono a la zona 18 (proyección de UTM zona 18)
                            Polygon projectedPolygon = GeometryEngine.Instance.Project(polygonGeometry, srZone18) as Polygon;

                            if (projectedPolygon != null)
                            {
                                // Ahora el polígono está transformado a la nueva zona (zona 18)
                                Envelope envelope1 = projectedPolygon.Extent;
                                // Obtener las coordenadas mínimas y máximas del polígono transformado
                                minX_Carta = envelope1.XMin;
                                minY_Carta = envelope1.YMin;
                                maxX_Carta = envelope1.XMax;
                                maxY_Carta = envelope1.YMax;
                            }
                        }
                    }
                    if (zoneDm == "19")
                    {
                        SpatialReference srZone19 = SpatialReferenceBuilder.CreateSpatialReference(32719); // UTM zona 19
                        SpatialReference srZone18 = SpatialReferenceBuilder.CreateSpatialReference(32718); // UTM zona 18
                        if (polygonGeometry.SpatialReference == srZone19)
                        {
                            // Transformar el polígono a la zona 18 (proyección de UTM zona 18)
                            Polygon projectedPolygon = GeometryEngine.Instance.Project(polygonGeometry, srZone18) as Polygon;

                            if (projectedPolygon != null)
                            {
                                // Ahora el polígono está transformado a la nueva zona (zona 18)
                                Envelope envelope1 = projectedPolygon.Extent;
                                // Obtener las coordenadas mínimas y máximas del polígono transformado
                                minX_Carta = envelope1.XMin;
                                minY_Carta = envelope1.YMin;
                                maxX_Carta = envelope1.XMax;
                                maxY_Carta = envelope1.YMax;
                            }
                        }
                    }

                }



                var extentDmRadio = ObtenerExtent(minX, minY, maxX, maxY, datum, radio);
                var extentDm = ObtenerExtent(minX, minY, maxX, maxY, datum);
                GlobalVariables.currentExtentDM = extentDm;

                string listDms = "";
                string listDmsTouches = "";
                string listDmsInter = "";
                if (radio == 0)
                {
                    listDms = await featureClassLoader.IntersectFeatureClassbyGeometryAsync("Catastro", polygon, catastroShpName);
                    listDmsTouches = await featureClassLoader.IntersectFeatureClassbyGeometryTouchesAsync("Catastro", polygon, catastroTouchesShpName);
                }
                else
                {


                    listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);
                    //listDmsTouches = await featureClassLoader.IntersectFeatureClassbyGeometryTouchesAsync("Catastro", polygon, catastroTouchesShpName);
                    //listDmsInter = await featureClassLoader.IntersectFeatureClassbyGeometryAsync("Catastro", polygon, catastroInterseShpName);

                    dtTouches = await featureClassLoader.IntersectFeatureClassbyGeometryTouchesDTAsync("Catastro", polygon, catastroTouchesShpName);
                    dtIntersec = await featureClassLoader.IntersectFeatureClassbyGeometryDTAsync("Catastro", polygon, catastroTouchesShpName);

                    // Eliminar filas en dt1 que tengan el mismo CODIGOU en dt2
                   foreach (DataRow row1 in dtIntersec.Rows.Cast<DataRow>().ToList())
                    {
                        // Buscar si existe alguna fila en dt2 con el mismo CODIGOU
                        bool existsInDt2 = dtTouches.AsEnumerable().Any(row2 => row2.Field<string>("CODIGO_SP") == row1.Field<string>("CODIGO_SP"));

                        // Si existe, eliminar la fila de dt1
                        if (existsInDt2)
                        {
                            dtIntersec.Rows.Remove(row1);
                        }
                    }




                }



                if (listDms == "")
                {
                    string message = "No hay información del área seleccionada";
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                     "Advertencia",
                                                                     MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }
                /********/
                // Se debe ejecutar dentro de un QueuedTask para tareas de edición en ArcGIS Pro

                await QueuedTask.Run(() =>
                {
                    try
                    {
                        // Define las rutas a las clases de entidad
                        string targetFc = catastroShpName;
                        string sourceFc = "Poligono";

                        // Llama a la función que realiza el geoproceso de Append
                        AppendFeaturesToTarget(sourceFc, targetFc);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                });



                /*******/
                DataTable intersectDm;
                //if (datum == datumwgs84)
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroWGS84, FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, codigoValue);
                //}
                ///*INTERSECTAR*/
                //DataTable dataTable = new DataTable();
                //dataTable.Columns.Add("CODIGOU", typeof(string));
                //dataTable.Columns.Add("CODIGO_SP", typeof(string));
                //dataTable.Columns.Add("CONCESION", typeof(string));
                //dataTable.Columns.Add("ZONA", typeof(string));
                //dataTable.Columns.Add("TIPO_EX", typeof(string));
                //dataTable.Columns.Add("AREASUP", typeof(decimal));
                //DataRow dataRow = dataTable.NewRow();
                //string codigo;
                //string codigosp;
                //string concesion;
                //string zona;
                //string tipoex;
                //decimal areasup;

                //codigo = "700005820";
                //codigosp = "030016408";
                //concesion = "JUAN DIEGO 2008";
                //zona = "17";
                //tipoex = "PE";
                //areasup = Convert.ToDecimal(27.4439);
                //dataTable.Rows.Add(codigo, codigosp, concesion, zona, tipoex, areasup);

                intersectDm = dtIntersec;


                /*      */

                //var distBorder = dataBaseHandler.CalculateDistanceToBorder(codigoValue, zoneDm, datumStr);
                //GlobalVariables.DistBorder = Math.Round(Convert.ToDouble(distBorder.Rows[0][0]) / 1000.0, 3);
                await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
                await UpdateValueAsync(catastroShpName, codigoValue);

                CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listaCodigoColin, out string listaCodigoSup, out List<string> coleccionesAareaSup);
                await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.UpdateRecordsDmAsync(catastroShpName, listaCodigoColin, listaCodigoSup, coleccionesAareaSup);
                await featureClassLoader.ExportAttributesTemaAsync(catastroShpName, GlobalVariables.stateDmY, dmShpName, $"CODIGOU='{codigoValue}'");
                string styleCat = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCatastro);
                await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, styleCat, "LEYENDA", StyleItemType.PolygonSymbol, codigoValue);
                var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, codigoValue);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEval, Params);
                CommonUtilities.ArcgisProUtils.LayerUtils.SelectSetAndZoomByNameAsync(catastroShpName, false);
                List<string> layersToRemove = new List<string>() { "Catastro", "Carta IGN", dmShpName, "Zona Urbana", "Poligono", poligonoGen };
                await CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);
                await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameAsync(catastroShpName, "Catastro");
                GlobalVariables.CurrentShpName = "Catastro";
                MapUtils.AnnotateLayerbyName("Catastro", "CONTADOR", "DM_Anotaciones");
                UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                var (gridLayer, pointLayer) = await uTMGridGenerator.GenerateUTMGridAsync(extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, "Malla", zoneDm);
                await uTMGridGenerator.AnnotateGridLayer(pointLayer, "VALOR");
                await uTMGridGenerator.RemoveGridLayer("Malla", zoneDm);
                string styleGrid = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleMalla);
                await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(gridLayer.Name, styleGrid, "CLASE", StyleItemType.LineSymbol);

            });


            var extentDmRadio = ObtenerExtent(minX, minY, maxX, maxY, datum, radio);
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
            }

            // Obtener el mapa Demarcacion Politica//
            try
            {
                var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);

                await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameDemarcacionPo); //"DEMARCACION POLITICA"
                Map mapD = await EnsureMapViewIsActiveAsync("DEMARCACION POLITICA");
                var featureClassLoader = new FeatureClassLoader(geodatabase, mapD, zoneDm, "99");

                var fl = await featureClassLoader.LoadFeatureClassAsyncGDB(poligonoGen, true);
                CommonUtilities.ArcgisProUtils.LayerUtils.SelectSetAndZoomByNameAsync("Poligono", false);


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
                //var mapView = MapView.Active as MapView;
                CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer((FeatureLayer)fl, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(0, 255, 255, 0), CIMColor.CreateRGBColor(255, 0, 0));
                await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameByFeatureLayerAsync((FeatureLayer)fl, "Catastro");

            }
            catch (Exception ex) { }

            try
            {
                var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);

                await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync(GlobalVariables.mapNameCartaIgn); //"CARTA IGN"
                Map mapC = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCartaIgn);
                featureClassLoader = new FeatureClassLoader(geodatabase, mapC, zoneDm, "99");

                //var fl1 = await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(mapC, Path.Combine(outputFolder, dmShpNamePath));
                var fl = await featureClassLoader.LoadFeatureClassAsyncGDB(poligonoGen, true);
                CommonUtilities.ArcgisProUtils.LayerUtils.SelectSetAndZoomByNameAsync("Poligono", false);

                //Carga capa Hojas IGN
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta56, false);
                }
                string listHojas = "";
                if (zoneDm == "18")
                {
                    listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", minX, minY, maxX, maxY);
                }
                else
                {
                    listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", minX_Carta, minY_Carta, maxX_Carta, maxY_Carta);
                }

                string pattern = @"IN \((.*)\)";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(listHojas);
                if (match.Success)
                {
                    string result = match.Groups[1].Value;
                    GlobalVariables.CurrentPagesDm = result;

                }




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

                CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer((FeatureLayer)fl, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(0, 255, 255, 0), CIMColor.CreateRGBColor(255, 0, 0));
                await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameByFeatureLayerAsync((FeatureLayer)fl, "Catastro");


                //CommonUtilities.ArcgisProUtils.SymbologyUtils.CustomLinePolygonLayer((FeatureLayer)fl1, SimpleLineStyle.Solid, CIMColor.CreateRGBColor(255, 0, 0, 0), CIMColor.CreateRGBColor(255, 0, 0));
                //await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameByFeatureLayerAsync((FeatureLayer)fl1, "Catastro");


                //string listHojas = DataGridResult.GetCellValue(focusedRowHandle, "CARTA")?.ToString();
                var hojaIGN = GlobalVariables.CurrentPagesDm;
                hojaIGN = hojaIGN.Replace("-", "").ToLower();
                //hojaIGN = hojaIGN.Substring(0, hojaIGN.Length - 1);

                string mosaicLayer;
                if (datum == datumwgs84)
                {
                    mosaicLayer = FeatureClassConstants.gstrRT_IngMosaic84;
                }
                else
                {
                    mosaicLayer = FeatureClassConstants.gstrRT_IngMosaic56;
                }
                string queryListCartaIGN = CommonUtilities.StringProcessorUtils.FormatStringCartaIgnForSql(hojaIGN);
                //string queryListCartaIGN = CommonUtilities.StringProcessorUtils.FormatStringCartaIgnForSql(GlobalVariables.CurrentPagesDm);
                await CommonUtilities.ArcgisProUtils.RasterUtils.AddRasterCartaIGNLayerAsync(mosaicLayer, geodatabase, mapC, queryListCartaIGN);
                List<string> layersToRemove = new List<string>() { "Carta IGN" };
                await CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                progressBar.Dispose();
            }
            finally
            {
                progressBar.Dispose();
            }

        }

        private ExtentModel ObtenerExtent(double XMin, double YMin, double XMax, double YMax, int datum, int radioKm = 0)
        {
            //// Obtener las coordenadas usando la función ObtenerCoordenadas
            int radioMeters = radioKm * 1000;
            // Inicializar las variables para almacenar los valores extremos
            double xmin = XMin; // int.MaxValue;
            double xmax = XMax; // int.MinValue;
            double ymin = YMin; // int.MaxValue;
            double ymax = YMax; // int.MinValue;

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
                                if (v_codigo_dm == "000000001")
                                {
                                    row["EVAL"] = "EV";
                                    row.Store();
                                    return;
                                }

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

        private void AddCoordListBox()
        {
            //string[] items = {
            //                    "Punto 1: 550000; 9455000",
            //                    "Punto 2: 550000; 9465000",
            //                    "Punto 3: 560000; 9465000",
            //                    "Punto 4: 560000; 9460000",
            //                    "Punto 5: 555000; 9460000",
            //                    "Punto 6: 555000; 9455000"
            //                };

            //string[] items = {
            //                    "Punto 1: 449000; 8597000",
            //                    "Punto 2: 449000; 8603000",
            //                    "Punto 3: 455000; 8603000",
            //                    "Punto 4: 455000; 8594000",
            //                    "Punto 5: 452000; 8594000",
            //                    "Punto 6: 452000; 8597000"
            //                };

            string[] items = {
                                "Punto 1: 505000; 9408000",
                                "Punto 2: 505000; 9404000",
                                "Punto 3: 501000; 9404000",
                                "Punto 4: 501000; 9405000",
                                "Punto 5: 504000; 9405000",
                                "Punto 6: 504000; 9408000"
            };


            //string[] items = {
            //                    "Punto 1: 504000; 8380000",
            //                    "Punto 2: 504000; 8381000",
            //                    "Punto 3: 505000; 8381000",
            //                    "Punto 4: 505000; 8380000"
            //                };

            // Agrega cada elemento como un CheckBox al ListBox

            //foreach (string item in items)
            //{
            //    // Crear un nuevo CheckBox
            //    CheckBox checkBox = new CheckBox();
            //    checkBox.Content = item;  // Asignar el texto del CheckBox

            //    // Agregar el CheckBox al ListBox
            //    listBoxVertices.Items.Add(checkBox);
            //}

            foreach (string item in items)
            {
                // Agregar el CheckBox al ListBox
                listBoxVertices.Items.Add(item);
            }

        }




        private void GeneraDatable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Vertice", typeof(string));
            dt.Columns.Add("Este", typeof(string));
            dt.Columns.Add("Norte", typeof(string));


            // Recorrer los ítems del ListBox y agregarlos al DataTable
            foreach (var item in listBoxVertices.Items)
            {
                DataRow row = dt.NewRow();
                row["Vertice"] = 1;
                row["Este"] = 345678;
                row["Norte"] = 9345678;
                dt.Rows.Add(row);
            }

            DataGridDetails.ItemsSource = dt.DefaultView;
        }


        private DataTable ObtenerCoordenadasDesdeListBox(ListBox listBox)
        {
            DataTable filteredTable = new DataTable();

            try
            {
                // Definir las columnas del DataTable
                filteredTable.Columns.Add(DatagridDetailsConstants.ColumnNames.Vertice, typeof(string));
                filteredTable.Columns.Add(DatagridDetailsConstants.ColumnNames.Este, typeof(string));
                filteredTable.Columns.Add(DatagridDetailsConstants.ColumnNames.Norte, typeof(string));

                // Iterar por los elementos del ListBox
                int conta = 1;
                foreach (var item in listBox.Items)
                {
                    if (item is string line)
                    {
                        try
                        {
                            // Dividir la línea en dos partes separadas por ':'
                            string[] parts = line.Split(':');
                            if (parts.Length != 2)
                                throw new FormatException($"La línea '{line}' no contiene ':' para separar el nombre y las coordenadas.");

                            // La segunda parte contiene las coordenadas " X; Y"
                            string coordinatesPart = parts[1].Trim();

                            // Dividir las coordenadas por ';'
                            string[] coordinates = coordinatesPart.Split(';');

                            // Agregar una fila al DataTable
                            DataRow row = filteredTable.NewRow();
                            row[DatagridDetailsConstants.ColumnNames.Vertice] = conta++;
                            row[DatagridDetailsConstants.ColumnNames.Este] = coordinates[0];
                            row[DatagridDetailsConstants.ColumnNames.Norte] = coordinates[1];
                            filteredTable.Rows.Add(row);
                        }

                        catch (FormatException ex)
                        {
                            // Manejar el error según tus necesidades
                            // Por ejemplo, puedes registrar el error y continuar con la siguiente línea
                            // O lanzar la excepción para que el llamador la maneje
                            // Aquí optamos por lanzar la excepción
                            throw new FormatException($"Error al procesar la línea '{line}': {ex.Message}", ex);
                        }
                    }
                }

                return filteredTable;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al procesar las coordenadas: " + ex.Message);
                return filteredTable;
            }

        }

        private void BtnExample_Click(object sender, RoutedEventArgs e)
        {
            AddCoordListBox();
            var dmrRecords = ObtenerCoordenadasDesdeListBox(listBoxVertices);
            //DataGridDetails.ItemsSource = dmrRecords.DefaultView;
            GraficarCoordenadas(dmrRecords);
        }

        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            CbxSistema.SelectedIndex = 0;
            //CbxTypeConsult.SelectedIndex = 0;
            CbxZona.SelectedIndex = 0;
            BtnGraficar.IsEnabled = false;

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

        private void ClearCanvas()
        {
            PolygonCanvas.Children.Clear();
        }

        private void TbxRadio_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private static async void AppendFeaturesToTarget(string sourceFc, string targetFc)
        {
            try
            {
                // Define los parámetros para el geoproceso Append como una lista de strings
                List<string> parameters = new List<string>
       {
           sourceFc,    // Características fuente
           targetFc,    // Características destino
           "NO_TEST"    // Esquema (NO_TEST significa que no se realiza ninguna validación del esquema)
       };

                // Ejecuta el geoproceso Append usando el nombre de la herramienta y los parámetros correctos
                var appendResult = await Geoprocessing.ExecuteToolAsync("management.Append", parameters);

                // Verifica si la operación fue exitosa
                if (appendResult.IsFailed)
                {
                    Console.WriteLine("Append operation failed.");
                }
                else
                {
                    Console.WriteLine("Append operation completed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while appending: " + ex.Message);
            }
        }



        private void reProyectar_Zona(string dmZona, string dmZonaProyectar, ArcGIS.Core.Geometry.Geometry polygon)

        {
            if (polygon is Polygon)
            {
                // Aquí podemos trabajar con el polígono
                Polygon polygonGeometry = polygon as Polygon;
                // Obtener los límites del polígono (Extent)
                Envelope envelope = polygonGeometry.Extent;

                if (dmZonaProyectar == "18")
                {
                    SpatialReference srZone17 = SpatialReferenceBuilder.CreateSpatialReference(32717); // UTM zona 17
                    SpatialReference srZone18 = SpatialReferenceBuilder.CreateSpatialReference(32718); // UTM zona 18
                    if (polygonGeometry.SpatialReference == srZone17)
                    {
                        // Transformar el polígono a la zona 18 (proyección de UTM zona 18)
                        Polygon projectedPolygon = GeometryEngine.Instance.Project(polygonGeometry, srZone18) as Polygon;

                        if (projectedPolygon != null)
                        {
                            // Ahora el polígono está transformado a la nueva zona (zona 18)
                            Envelope envelopeReproyecta = projectedPolygon.Extent;
                        }
                    }
                }
                if (dmZonaProyectar == "19")
                {
                    SpatialReference srZone19 = SpatialReferenceBuilder.CreateSpatialReference(32719); // UTM zona 17
                    SpatialReference srZone18 = SpatialReferenceBuilder.CreateSpatialReference(32718); // UTM zona 18
                    if (polygonGeometry.SpatialReference == srZone19)
                    {
                        // Transformar el polígono a la zona 18 (proyección de UTM zona 18)
                        Polygon projectedPolygon = GeometryEngine.Instance.Project(polygonGeometry, srZone18) as Polygon;

                        if (projectedPolygon != null)
                        {
                            // Ahora el polígono está transformado a la nueva zona (zona 18)
                            Envelope envelopeReproyecta = projectedPolygon.Extent;
                        }
                    }
                }


            }
        }

        private void LayersListBox_KeyDown(object sender, KeyEventArgs e)
        {

        }



    }
}

