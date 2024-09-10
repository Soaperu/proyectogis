using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using static Automapic_pro.GlobalVariables;
using System.IO;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Internal.Mapping;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Core.CIM;
using System.Threading;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping.Events;
//using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Core.Internal.Geometry;
using System.Drawing;
using System.Windows.Documents;
using ArcGIS.Desktop.Framework;
using System.Windows.Input;
using DevExpress.Spreadsheet;
using DevExpress.XtraMap.Native;


namespace Automapic_pro.modulos
{
    /// <summary>
    /// Lógica de interacción para geocatmin_cartaIGN.xaml
    /// </summary>
    public partial class geocatmin_cartaIGN : Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt_dpto = new DataTable();
        DataTable dt_prov = new DataTable();
        DataTable dt_dist = new DataTable();
        public geocatmin_cartaIGN()
        {
            InitializeComponent();
            dt = accesoDB.ConsultaGIS("QDR_CODIGO_ALFANUMERICO AS CODHOJA, QDR_NOMBRE AS NAME, DOM_PROYECCION AS ZONA", layerHoja100K, "1=1 ORDER BY QDR_CODIGO ASC");
            dt1 = accesoDB.ConsultaGIS("HOJA250 AS CODHOJA", layerHoja250K);
            dt_dpto = accesoDB.ConsultaGIS("NM_DEPA", layerDpto, "CD_DEPA <> 99 ORDER BY CASE WHEN NM_DEPA IS NOT NULL THEN NM_DEPA ELSE CD_DEPA END ASC");
            _ = AddMapToProject();
            grid_UG.Visibility = Visibility.Collapsed;
            tbx_nombre.IsReadOnly = true;
            tbx_Xmax.IsReadOnly = true;
            tbx_Xmin.IsReadOnly = true;
            tbx_Ymax.IsReadOnly = true;
            tbx_Ymin.IsReadOnly = true;
            tbx_zona.IsReadOnly = true;
            AddLayersToMapViewActive();
            //EnableRadioButtonsStartingWithRbtn();
        }
        private void rbtn_ign100k_Checked(object sender, RoutedEventArgs e)
        {
            grid_UG.Visibility = Visibility.Collapsed;
            gbox_datosIGN.Visibility = Visibility.Visible;
            stackp_carta.Visibility = Visibility.Visible;
            stackp_cuadr.Visibility = Visibility.Visible;
            cbx_cartaIGN.SelectedIndex = -1;
            cbx_cartaIGN.Items.Clear();
            cbx_cuadrante.Items.Clear();
            cbx_cartaIGN.Items.Add("--Seleccionar--");
            foreach (DataRow row in dt.Rows)
            {
                string valorConcatenado = row["CODHOJA"].ToString() + " (" + row["NAME"].ToString() + ")";
                cbx_cartaIGN.Items.Add(valorConcatenado); // Agrega el valor de la columna deseada al ComboBox
            }
            //cbx_cuadrante.Visibility = Visibility.Visible;
            //lbl_cuadrante.Visibility = Visibility.Visible;
            cbx_cartaIGN.SelectedIndex = 0;
            string[] lista = { "-Seleccionar-", "I", "II", "III", "IV" };
            foreach (string i in lista)
            {
                cbx_cuadrante.Items.Add(i);
            }
            cbx_cuadrante.SelectedIndex = 0;
            sheetChecked = "100k";
        }
        private void rbtn_ign250k_Checked(object sender, RoutedEventArgs e)
        {
            cbx_cartaIGN.SelectedIndex = -1;
            cbx_cartaIGN.Items.Clear();
            //cbx_cuadrante.Items.Clear();
            cbx_cartaIGN.Items.Add("--Seleccionar--");
            foreach (DataRow row in dt1.Rows)
            {
                cbx_cartaIGN.Items.Add(row["CODHOJA"]); // Agrega el valor de la columna deseada al ComboBox
            }
            string[] layers = { "Carta250" };
            _ = OnOffLayers("On", layers, nameLyrCartaIGN);
            //cbx_cuadrante.Visibility = Visibility.Collapsed;
            //lbl_cuadrante.Visibility = Visibility.Collapsed;
            grid_UG.Visibility = Visibility.Collapsed;
            gbox_datosIGN.Visibility = Visibility.Visible;
            stackp_carta.Visibility = Visibility.Visible;
            stackp_cuadr.Visibility = Visibility.Hidden; 
            cbx_cartaIGN.SelectedIndex = 0;
            sheetChecked = "250k";
        }
        public static Task<string[]> ObtenerExtensionFeatureClass(string geodatabasePath, string featureClassName, string filter)
        {
            //string[] xy = new string[4];
            return QueuedTask.Run(() =>
            {
                string[] xy = new string[4];
                // Obtener el mapa activo
                Map activeMap = MapView.Active.Map;
                Geodatabase geodatabase = new Geodatabase(new DatabaseConnectionFile(new Uri(geodatabasePath)));
                FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName);
                int count = (int)featureClass.GetCount();
                QueryFilter query = new QueryFilter { WhereClause = filter };
                using (RowCursor featureCursor = featureClass.Search(query))
                {
                    while (featureCursor.MoveNext())
                    {
                        Feature pfeature = (Feature)featureCursor.Current;
                        Envelope extent = pfeature.GetShape().Extent;
                        xy[0] = Math.Round(extent.XMin, 2).ToString();
                        xy[1] = Math.Round(extent.YMin, 2).ToString();
                        xy[2] = Math.Round(extent.XMax, 2).ToString();
                        xy[3] = Math.Round(extent.YMax, 2).ToString();
                        //Envelope extentZoom = (FeatureLayer)pfeature.QueryExtent();
                        MapView.Active.ZoomTo(extent);
                        ExtentMain100k = extent;
                    }
                }
                return xy;
            });
        }
        private async void cbx_cartaIGN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_cartaIGN.SelectedItem == null)
            {
                return;
            }
            if (cbx_cartaIGN.SelectedItem.ToString() == "--Seleccionar--")
            {
                return;
            }
            string pathSDE = System.IO.Path.Combine(currentPath, SDE);
            string selection = cbx_cartaIGN.SelectedItem.ToString();
            String[] task;
            tbx_nombre.Text = cbx_cartaIGN.SelectedItem.ToString();

            try
            {
                if (rbtn_ign100k.IsChecked == true)
                {
                    string sheet = selection.Split(" (")[0];
                    string filterSheet = "QDR_CODIGO_ALFANUMERICO='{0}'";
                    string filterSheet2 = "QDR_CODIGO='{0}'";
                    var query_codhoja = dt.Select(String.Format("CODHOJA='{0}'", sheet));
                    tbx_zona.Text = query_codhoja.First()["ZONA"].ToString();
                    task = await ObtenerExtensionFeatureClass(pathSDE, layerHoja100K, String.Format(filterSheet, sheet));
                    tbx_Xmin.Text = task[0];
                    tbx_Xmax.Text = task[2];
                    tbx_Ymin.Text = task[1];
                    tbx_Ymax.Text = task[3];
                    _ = ApplyQueryDefinitionWithZoomAsync(nameLyrCartografiaBase, String.Format(filterSheet2, sheet));
                    SetMapSpatialReferenceToUTM(tbx_zona.Text);
                    utm = tbx_zona.Text;
                    GeneralQuery = String.Format(filterSheet, sheet);
                }
                else if (rbtn_ign250k.IsChecked == true)
                {
                    string sheet = selection;
                    string filterSheet = "HOJA250 ='{0}'";
                    //string filterSheet2 = "QDR_CODIGO='{0}'";
                    task = await ObtenerExtensionFeatureClass(pathSDE, layerHoja250K, String.Format(filterSheet, sheet));
                    tbx_Xmin.Text = task[0];
                    tbx_Xmax.Text = task[2];
                    tbx_Ymin.Text = task[1];
                    tbx_Ymax.Text = task[3];
                    _ = ApplyQueryDefinitionWithZoomAsync(nameLyrCartografiaBase, null);
                    GeneralQuery = String.Format(filterSheet, sheet);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error inesperado con la capa \n" + ex);
            }
            cbx_cuadrante.SelectedIndex = 0;
        }

        public async Task AddMapToProject()
        {
            Project aprx = Project.Current;
            if (aprx.GetMaps().Count() > 0)
            {
                Map map = aprx.GetItems<MapProjectItem>().FirstOrDefault().GetMap();
                await ProApp.Panes.CreateMapPaneAsync(map);
            }
            else
            {
                await QueuedTask.Run(() =>
                {
                    var map = MapFactory.Instance.CreateMap("Mapa Geocatmin", basemap: Basemap.ProjectDefault);
                    ProApp.Panes.CreateMapPaneAsync(map);
                });
            }
        }
        public async Task AddLayersBase(string lyr1, string lyr2)
        {
            await QueuedTask.Run(() =>
            {
                string pathLyrBase = System.IO.Path.Combine(fileLayers, lyr1);
                string pathLyrCartaIGN = System.IO.Path.Combine(fileLayers, lyr2);
                try
                {
                    if (!File.Exists(pathLyrBase))
                    {
                        throw new FileNotFoundException("El archivo .lyr no existe.");
                    }
                    Map activeMap = MapView.Active.Map;
                    // Cargar el archivo .lyr como una capa en el mapa activo
                    Layer layerBase = LayerFactory.Instance.CreateLayer(new Uri(pathLyrBase), activeMap, 0);
                    Layer layerCartaIGN = LayerFactory.Instance.CreateLayer(new Uri(pathLyrCartaIGN), activeMap, 1);
                    FeatureLayer lyr = (FeatureLayer)activeMap.FindLayers("Departamentos").First();
                    MapView.Active.Redraw(true);
                    Envelope envelope = lyr.QueryExtent();
                    ExtentSecond = envelope;
                    //// Zoom a la extensión de los elementos espaciales de cada capa
                    MapView.Active.ZoomTo(envelope);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Agregue un Mapa al Proyecto \n" + ex);
                }
            });
            ExtentSecond = MapView.Active.Map.FindLayers("Departamentos").First().QueryExtent();
        }

        private SubscriptionToken _eventToken = null;

        public void AddLayersToMapViewActive()
        {
            if (MapView.Active == null)
            {
                // Suscribirse al evento DrawCompleteEvent
                _eventToken = DrawCompleteEvent.Subscribe(OnDrawComplete);
            }
            else
            {
                // MapView.Active está disponible, llamar directamente al método
                _ = AddLayersBase(lyrBase, lyrCartaIGN);
                EnableRadioButtonsStartingWithRbtn();
            }
        }
        private void OnDrawComplete(MapViewEventArgs args)
        {
            // Desuscribirse del evento
            if (_eventToken != null)
            {
                DrawCompleteEvent.Unsubscribe(_eventToken);
                _eventToken = null;
            }
            // Llamar al método para agregar capas una vez que MapView.Active está disponible
            _ = AddLayersBase(lyrBase, lyrCartaIGN);
            EnableRadioButtonsStartingWithRbtn();
        }

        public async Task ApplyQueryDefinitionWithZoomAsync(string groupName, string queryDefinition)
        {
            await QueuedTask.Run(() =>
            {
                // Obtener el mapa activo
                Map activeMap = MapView.Active.Map;
                // Buscar el grupo de capas por su nombre
                GroupLayer layerGroup = activeMap.GetLayersAsFlattenedList().OfType<GroupLayer>().FirstOrDefault(group => group.Name == groupName);
                if (layerGroup != null)
                {
                    // Obtener todas las capas dentro del grupo
                    var featureLayers = layerGroup.GetLayersAsFlattenedList().OfType<FeatureLayer>().ToList();
                    //int onlyzoom = 0;
                    // Lista de capas que no contienen el campo "QDR_CODIGO"
                    string[] exceptLayers = { "Señal geodesica", "Capital Departamental", "Capital Provincial", "Geoforma" };
                    // Recorremos todas las capas para aplicar la definicion de la consulta
                    foreach (var featureLayer in featureLayers)
                    {
                        if (!exceptLayers.Contains(featureLayer.Name))
                        {
                            // Aplicar el QueryFilter a cada capa dentro del grupo
                            featureLayer.SetDefinitionQuery(queryDefinition);
                            //// Obtener la extensión de los elementos espaciales que cumplen con la consulta en cada capa
                            //Envelope envelope = featureLayer.QueryExtent();
                        }
                    }
                }
            });
        }

        private void cbx_cuadrante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_cuadrante.SelectedItem == null)
            {
                return;
            }
            if (cbx_cuadrante.SelectedItem.ToString() == "-Seleccionar-")
            {
                return;
            }
            string pathSDE = System.IO.Path.Combine(currentPath, SDE);
            _ = ApplyQueryWithZoomToCarta50(nameLyrCartaIGN, "Carta50", pathSDE, layerHoja50K);
            //_ = ObtenerExtensionFeatureClass(pathSDE, layerHoja50K, task);
            sheetChecked = "50k";
        }

        public async Task ApplyQueryWithZoomToCarta50(string groupCartaIGN, string Carta50, string geodatabasePath, string featureClassName)
        {
            string sheet = cbx_cartaIGN.SelectedItem.ToString().Split(" (")[0];
            string quadrant = cbx_cuadrante.SelectedItem.ToString();
            await QueuedTask.Run(() =>
           {
               Map activeMap = MapView.Active.Map;
               GroupLayer layerGroup = activeMap.GetLayersAsFlattenedList().OfType<GroupLayer>().FirstOrDefault(group => group.Name == groupCartaIGN);
               string query = "";
               if (layerGroup != null)
               {
                   // Buscar la capa por su nombre dentro del grupo de capas
                   FeatureLayer featureLayer = layerGroup.FindLayers(Carta50).FirstOrDefault() as FeatureLayer;
                   if (featureLayer != null)
                   {
                       featureLayer.SetVisibility(true);
                       Dictionary<string, int> cuadrantes = new Dictionary<string, int>()
                   {
                        { "I", 1 }, { "II", 2 }, {"III", 3},{ "IV", 4}
                   };
                       query = string.Format("COD_CARTA='{0}' AND CUADRANTE={1}", sheet, cuadrantes[quadrant]);
                       featureLayer.SetDefinitionQuery(query);
                       GeneralQuery = query;
                       // Obtener la extensión de los elementos espaciales que cumplen con la consulta en cada capa
                       Envelope envelope = featureLayer.QueryExtent();
                       // Zoom a la extensión de los elementos espaciales de cada capa
                       MapView.Active.ZoomTo(envelope);
                   }
               }
               Geodatabase geodatabase = new Geodatabase(new DatabaseConnectionFile(new Uri(geodatabasePath)));
               FeatureClass featureClass = geodatabase.OpenDataset<FeatureClass>(featureClassName);
               int count = (int)featureClass.GetCount();
               QueryFilter queryfilter = new QueryFilter { WhereClause = query };
               using (RowCursor featureCursor = featureClass.Search(queryfilter))
               {
                   while (featureCursor.MoveNext())
                   {
                       Feature pfeature = (Feature)featureCursor.Current;
                       Envelope extent = pfeature.GetShape().Extent;
                       ExtentMain50k = extent;
                   }
               }
           });
        }

        public async Task ApplyQueryWithZoomLayersUG(string groupCartaIGN, string Layer)
        {
            var dpto = cbx_dpto.SelectedItem;
            var prov = cbx_prov.SelectedItem;
            var dist = cbx_dist.SelectedItem;
            string[] XY = new string[4];
            await QueuedTask.Run(() =>
            {
                string[] xy = new string[4];
                Map activeMap = MapView.Active.Map;
                GroupLayer layerGroup = activeMap.GetLayersAsFlattenedList().OfType<GroupLayer>().FirstOrDefault(group => group.Name == groupCartaIGN);
                //Geometry PreGeomReturn = null;
                if (layerGroup != null)
                {
                    // Buscar la capa por su nombre dentro del grupo de capas
                    FeatureLayer featureLayer = layerGroup.FindLayers(Layer).FirstOrDefault() as FeatureLayer;
                    if (featureLayer != null)
                    {
                        featureLayer.SetVisibility(true);
                        string query = "";
                        if (Layer == "Departamentos")
                        {
                            string query_dpto = string.Format("NM_DEPA='{0}' ", dpto.ToString());
                            query = query_dpto;
                        }
                        if (Layer == "Provincias")
                        {
                            string query_prov = string.Format("NM_DEPA='{0}' AND NM_PROV='{1}'", dpto.ToString(), prov.ToString());
                            query = query_prov;
                        }
                        if (Layer == "Distritos")
                        {
                            string query_dist = string.Format("NM_DEPA='{0}' AND NM_PROV='{1}' AND NM_DIST='{2}' ", dpto.ToString(), prov.ToString(), dist.ToString());
                            query = query_dist;
                        }
                        featureLayer.SetDefinitionQuery(query);
                        // Obtener la extensión de los elementos espaciales que cumplen con la consulta en cada capa
                        Envelope envelope = featureLayer.QueryExtent();
                        // Zoom a la extensión de los elementos espaciales de cada capa
                        MapView.Active.ZoomTo(envelope);
                        xy[0] = Math.Round(envelope.XMin, 6).ToString();
                        xy[1] = Math.Round(envelope.YMin, 6).ToString();
                        xy[2] = Math.Round(envelope.XMax, 6).ToString();
                        xy[3] = Math.Round(envelope.YMax, 6).ToString();
                        featureLayer.SetDefinitionQuery(null);
                    }
                }
            });
            tbx_Xmin.Text = XY[0];
            tbx_Ymin.Text = XY[1];
            tbx_Xmax.Text = XY[2];
            tbx_Ymax.Text = XY[3];
        }

        private void rbtn_UG_Checked(object sender, RoutedEventArgs e)
        {
            grid_UG.Visibility = Visibility.Visible;
            gbox_datosIGN.Visibility = Visibility.Visible;
            gbox_area_estudio.Visibility = Visibility.Collapsed;
            stackp_carta.Visibility = Visibility.Collapsed;
            stackp_cuadr.Visibility = Visibility.Collapsed;
            foreach (DataRow row in dt_dpto.Rows)
            {
                cbx_dpto.Items.Add(row["NM_DEPA"]); // Agrega el valor de la columna deseada al ComboBox
            }
            tbx_nombre.Text = string.Empty;
            tbx_zona.Text = string.Empty;
            tbx_Xmax.Text = string.Empty;
            tbx_Xmin.Text = string.Empty;
            tbx_Ymax.Text = string.Empty;
            tbx_Ymin.Text = string.Empty;
            SetMapSpatialReferenceToUTM("4326");
        }

        private void cbx_dpto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Geometry geometryLayer = null;
            cbx_prov.Items.Clear();
            cbx_dist.Items.Clear();
            dt_prov = accesoDB.ConsultaGIS("NM_PROV", layerProv, "CD_PROV <> 99 AND NM_DEPA='" +
                                                                  cbx_dpto.SelectedItem.ToString() +
                                                                  "' ORDER BY CASE WHEN NM_PROV IS NOT NULL THEN NM_PROV ELSE CD_PROV END ASC");
            foreach (DataRow row in dt_prov.Rows)
            {
                cbx_prov.Items.Add(row["NM_PROV"]); // Agrega el valor de la columna deseada al ComboBox
            }

            _ = ApplyQueryWithZoomLayersUG(nameLyrCartografiaBase, "Departamentos");
            _ = ApplyQueryDefinitionWithZoomAsync(nameLyrCartografiaBase, null);
            _=AddGraphicWithOutlineStyle(nameLyrCartografiaBase, "Departamentos");
            ;
        }

        private void cbx_prov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_prov.SelectedItem == null)
            {
                return;
            }
            cbx_dist.Items.Clear();
            dt_dist = accesoDB.ConsultaGIS("NM_DIST", layerDist, "CD_DIST <> 99 AND NM_DEPA='" +
                                                                  cbx_dpto.SelectedItem.ToString() + "' AND NM_PROV='" +
                                                                  cbx_prov.SelectedItem.ToString() + "' ORDER BY CASE WHEN NM_DIST IS NOT NULL THEN NM_DIST ELSE CD_DIST END ASC");
            foreach (DataRow row in dt_dist.Rows)
            {
                cbx_dist.Items.Add(row["NM_DIST"]); // Agrega el valor de la columna deseada al ComboBox
            }
            _ = ApplyQueryWithZoomLayersUG(nameLyrCartografiaBase, "Provincias");
            _ = ApplyQueryDefinitionWithZoomAsync(nameLyrCartografiaBase, null);
            _ = AddGraphicWithOutlineStyle(nameLyrCartografiaBase, "Provincias");
        }

        private void cbx_dist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_dist.SelectedItem == null)
            {
                return;
            }
            _ = ApplyQueryWithZoomLayersUG(nameLyrCartografiaBase, "Distritos");
            _ = ApplyQueryDefinitionWithZoomAsync(nameLyrCartografiaBase, null);
            _ = AddGraphicWithOutlineStyle(nameLyrCartografiaBase, "Distritos");
        }

        private async Task OnOffLayers(string option, string[] layersName, string groupLayers)
        {
            //Parameter option: "On" or "Off"
            await QueuedTask.Run(() =>
            {
                Map activeMap = MapView.Active.Map;
                GroupLayer layerGroup = activeMap.GetLayersAsFlattenedList().OfType<GroupLayer>().FirstOrDefault(group => group.Name == groupLayers);
                if (layerGroup != null)
                {
                    foreach (string layerName in layersName)
                    {
                        // Buscar la capa por su nombre dentro del grupo de capas
                        FeatureLayer featureLayer = layerGroup.FindLayers(layerName).FirstOrDefault() as FeatureLayer;
                        if (featureLayer != null)
                        {
                            if (option == "On")
                                featureLayer.SetVisibility(true);
                            else if (option == "Off")
                                featureLayer.SetVisibility(false);
                        }
                    }
                }
            }
            );
        }
        public async void SetMapSpatialReferenceToUTM(string zonaUTM, Map activeMap = null)
        {
            await QueuedTask.Run(() =>
            {
                // Obtener el mapa activo
                if (activeMap == null)
                {
                    activeMap = MapView.Active.Map;
                }
                // Obtener el sistema de referencia espacial actual del mapa
                var currentSpatialReference = activeMap.SpatialReference;
                // Si el sistema de coordenadas actual ya es UTM 32718, salir y no hacer nada
                if (currentSpatialReference.Wkid == int.Parse(string.Format("327{0}", zonaUTM)))
                    return;
                string[] UTMarray = { "17", "18", "19" };
                if (Array.Exists(UTMarray, element => element == zonaUTM))
                {
                    // Definir el nuevo sistema de coordenadas espaciales (UTM)
                    SpatialReference utm = SpatialReferenceBuilder.CreateSpatialReference(int.Parse(string.Format("327{0}", zonaUTM)));
                    // Establecer el sistema de coordenadas espaciales del mapa activo
                    activeMap.SetSpatialReference(utm);
                }
                else
                {
                    SpatialReference utm = SpatialReferenceBuilder.CreateSpatialReference(int.Parse(zonaUTM));
                    // Establecer el sistema de coordenadas espaciales del mapa activo
                    activeMap.SetSpatialReference(utm);
                }
            });
        }

        private void rbtn_area_estudio_Checked(object sender, RoutedEventArgs e)
        {
            gbox_area_estudio.Visibility = Visibility.Visible;
            gbox_datosIGN.Visibility = Visibility.Collapsed;
            gbox_coordenadas.Visibility = Visibility.Collapsed;
        }

        private void rbtn_punto_xy_Checked(object sender, RoutedEventArgs e)
        {
            gbox_coordenadas.Visibility = Visibility.Visible;
            gbox_area_estudio.Visibility = Visibility.Collapsed;
            gbox_datosIGN.Visibility = Visibility.Collapsed;
            Map activeMap = MapView.Active.Map;
            SpatialReference scr = activeMap.SpatialReference;
            Dictionary<int, string> a = new Dictionary<int, string>
            {
            { 4326, "Geograficas WGS84" },
            { 32717, "UTM 17S" },
            { 32718, "UTM 18S" },
            { 32719, "UTM 19S" },
            { 3857, "Web Mercator (Auxiliary Sphere)" },
            { 102100, "Web Mercator (Auxiliary Sphere)" }
            };
            try
            {
                lbl_scrMap.Content = a[scr.Wkid];
            }
            catch ( Exception ex)
            {
                MessageBox.Show("Ocurrio un error con sistema de referencia \n" + ex);
            }
        }
        private void rbtn_punto_xy_Unchecked(object sender, RoutedEventArgs e)
        {
            gbox_coordenadas.Visibility = Visibility.Collapsed;
        }

        private void btn_ae_loadShp_Click(object sender, RoutedEventArgs e)
        {
            AddShapefileToMap();
        }

        public async void AddShapefileToMap()
        {
            var openFileDialog1 = new ArcGIS.Desktop.Catalog.OpenItemDialog
            {
                // Agregamos titulo y filtro 
                Title = "Seleccionar un Shapefile",
                BrowseFilter = BrowseProjectFilter.GetFilter(ArcGIS.Desktop.Catalog.ItemFilters.Shapefiles)
            };
            bool? ok = openFileDialog1.ShowDialog();
            if (ok == true)
            {
                var filename = openFileDialog1.Items.First();
                tbx_shpPath.Text = filename.Path;
                Uri uri = new Uri(filename.Path);
                await QueuedTask.Run(() =>
                {
                    // Crear la capa
                    Layer addedLayer = LayerFactory.Instance.CreateLayer(uri, MapView.Active.Map);
                    // Centrar la vista en la capa
                    MapView.Active.ZoomToAsync(addedLayer);
                    outlineBlackShp(addedLayer as FeatureLayer);
                });
            }
        }

        public async void outlineBlackShp(FeatureLayer featureLayer)
        {
            var colorIn = CIMColor.CreateRGBColor(255, 0, 0);  // Rojo
            await QueuedTask.Run(() =>
            {
                var trans = 75.0;//semi transparent
                // Linea de brode negra con grosor 2 y de estilo punteado
                CIMStroke InLine = SymbolFactory.Instance.ConstructStroke(CIMColor.CreateRGBColor(0, 0, 0, 50.0), 1.5, SimpleLineStyle.Dash);
                CIMStroke OutLine = SymbolFactory.Instance.ConstructStroke(CIMColor.CreateRGBColor(0, 255, 255, trans), 2.5, SimpleLineStyle.Dash);
                var symbol = featureLayer.GetRenderer() as CIMSimpleRenderer;
                // Crear una nueva simbología con relleno de color transparente
                var newFillSymbol = SymbolFactory.Instance.ConstructPolygonSymbol(
                    ColorFactory.Instance.CreateRGBColor(255, 0, 0, 0), SimpleFillStyle.Solid, InLine);
                newFillSymbol.SymbolLayers = newFillSymbol.SymbolLayers.Concat(new[] { OutLine }).ToArray();
                symbol.Symbol = newFillSymbol.MakeSymbolReference();
                // Actualiza la simbologia 
                featureLayer.SetRenderer(symbol);
            });
        }

        private IDisposable pointOverlay { get; set; }
        private async void btn_consult_xy_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_coord_x.Text == string.Empty || tbx_coord_y.Text == string.Empty)
            {
                MessageBox.Show("Ingresar longitud y latitud en el sistema correcto \n");
                return;
            }
            double longitud = double.Parse(tbx_coord_x.Text);
            double latitud = double.Parse(tbx_coord_y.Text);
            if (pointOverlay != null)
            {
                pointOverlay.Dispose();
                pointOverlay= null;
            }
            await QueuedTask.Run(() =>
            {
                try
                {
                    // Obtener el mapa activo
                    Map activeMap = MapView.Active.Map;
                    // Obtener el sistema de coordenadas del mapa
                    SpatialReference spatialReference = activeMap.SpatialReference;
                    // Crear un punto geográfico con las coordenadas (cambia estas según tus necesidades)
                    MapPoint mapPoint = MapPointBuilder.CreateMapPoint(longitud, latitud, spatialReference);
                    // Crear una simbología para el marcador
                    var marker = SymbolFactory.Instance.ConstructPointSymbol(
                        ColorFactory.Instance.RedRGB, 12.0, SimpleMarkerStyle.X);
                    // Añadir el Graphic al mapa
                    pointOverlay = MapView.Active.AddOverlay(mapPoint,marker.MakeSymbolReference());
                    // Centrar la vista en el punto y aplicar un nivel de zoom
                    double zoomScale = 20000;  // Ajusta esto según tus necesidades
                    Camera camera = new Camera
                    {
                        X = mapPoint.X,
                        Y = mapPoint.Y,
                        Z = mapPoint.Z, // O establecer una altitud específica si es necesario
                        Scale = zoomScale,  // Mantener la escala actual o establecer una nueva
                        Heading = 0,  // Otra opción podría ser mantener el encabezado actual de la cámara
                        Pitch = 0  // Otra opción podría ser mantener el tono actual de la cámara
                    };
                    MapView.Active.ZoomTo(camera, new TimeSpan(0, 0, 2));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error al ingresar la coordenada: \n" + ex);
                }
            });
        }

        private void rbtn_area_estudio_Unchecked(object sender, RoutedEventArgs e)
        {
            gbox_area_estudio.Visibility = Visibility.Collapsed;
        }

        private void rbtn_ign250k_Unchecked(object sender, RoutedEventArgs e)
        {
            string[] layers = { "Carta250" };
            _ = OnOffLayers("Off", layers, nameLyrCartaIGN);
        }

        private void rbtn_UG_Unchecked(object sender, RoutedEventArgs e)
        {
            grid_UG.Visibility = Visibility.Collapsed;
        }

        public async Task AddGraphicWithOutlineStyle(string groupCartaIGN, string Layer)
        {
            var dpto = cbx_dpto.SelectedItem;
            var prov = cbx_prov.SelectedItem;
            var dist = cbx_dist.SelectedItem;
            await QueuedTask.Run(() =>
            {
            Map activeMap = MapView.Active.Map;
            GroupLayer layerGroup = activeMap.GetLayersAsFlattenedList().OfType<GroupLayer>().FirstOrDefault(group => group.Name == groupCartaIGN);
            Geometry GeomReturn= null;
                if (layerGroup != null)
                {
                    // Buscar la capa por su nombre dentro del grupo de capas
                    FeatureLayer featureLayer = layerGroup.FindLayers(Layer).FirstOrDefault() as FeatureLayer;
                    if (featureLayer != null)
                    {
                        featureLayer.SetVisibility(true);
                        string query = "";
                        if (Layer == "Departamentos")
                        {
                            string query_dpto = string.Format("NM_DEPA='{0}' ", dpto.ToString());
                            query = query_dpto;
                        }
                        if (Layer == "Provincias")
                        {
                            string query_prov = string.Format("NM_DEPA='{0}' AND NM_PROV='{1}'", dpto.ToString(), prov.ToString());
                            query = query_prov;
                        }
                        if (Layer == "Distritos")
                        {
                            string query_dist = string.Format("NM_DEPA='{0}' AND NM_PROV='{1}' AND NM_DIST='{2}' ", dpto.ToString(), prov.ToString(), dist.ToString());
                            query = query_dist;
                        }
                        featureLayer.SetDefinitionQuery(query);
                        using (var rowCursor = featureLayer.Search())
                        {
                            if (rowCursor.MoveNext())
                            {
                                using (var feature = rowCursor.Current as Feature)
                                {
                                    GeomReturn = feature.GetShape();
                                }
                            }
                        }
                        var trans = 75.0; // Semi transparente
                                          // Línea de borde negra con grosor 2 y de estilo punteado
                        CIMStroke InLine = SymbolFactory.Instance.ConstructStroke(CIMColor.CreateRGBColor(0, 0, 0, 50.0), 1.5, SimpleLineStyle.Dash);
                        CIMStroke OutLine = SymbolFactory.Instance.ConstructStroke(CIMColor.CreateRGBColor(0, 255, 255, trans), 2.5, SimpleLineStyle.Dash);
                        // Crear una nueva simbología con relleno de color transparente y ambos bordes
                        var fillSymbol = SymbolFactory.Instance.ConstructPolygonSymbol(
                            ColorFactory.Instance.CreateRGBColor(255, 0, 0, 0), SimpleFillStyle.Solid, InLine);
                        // Agregar la capa de borde cyan al símbolo
                        fillSymbol.SymbolLayers = fillSymbol.SymbolLayers.Concat(new[] { OutLine }).ToArray();

                        var cimSymbol = fillSymbol.MakeSymbolReference();

                        // Crear y añadir el gráfico
                        var cimGraphicElement = new CIMPolygonGraphic
                        {
                            Polygon = GeomReturn as Polygon,
                            Symbol = cimSymbol,
                        };
                        MapView.Active.AddOverlay(cimGraphicElement);
                    }
                }
            });
        }

        private void chbx_cartaIGN_Checked(object sender, RoutedEventArgs e)
        {
            QueuedTask.Run(() =>
            {
                var activeMap = MapView.Active.Map;
                // Recorre todas y las capas enciende
                foreach (var layer in activeMap.GetLayersAsFlattenedList())
                {
                    layer.SetVisibility(true);
                }
            });
        }

        private void chbx_cartaIGN_Unchecked(object sender, RoutedEventArgs e)
        {
            QueuedTask.Run(() =>
            {
                var activeMap = MapView.Active.Map;
                // Recorre todas y las capas enciende
                foreach (var layer in activeMap.GetLayersAsFlattenedList())
                {
                    layer.SetVisibility(false);
                }
            });
        }

        private void rbtn_geocat_catastro_Checked(object sender, RoutedEventArgs e)
        {
            gbox_coordenadas.Visibility = Visibility.Collapsed;
            gbox_area_estudio.Visibility = Visibility.Collapsed;
            gbox_datosIGN.Visibility = Visibility.Collapsed;
            gbox_geocat_catastro.Visibility = Visibility.Visible;
            Map activeMap = MapView.Active.Map;
            SpatialReference scr = activeMap.SpatialReference;
            Dictionary<int, string> a = new Dictionary<int, string>
            {
            { 4326, "Geograficas WGS84" },
            { 32717, "UTM 17S" },
            { 32718, "UTM 18S" },
            { 32719, "UTM 19S" },
            { 3857, "Web Mercator (Auxiliary Sphere)" },
            { 102100, "Web Mercator (Auxiliary Sphere)" }
            };
            try
            {
                lbl_scrMap.Content = a[scr.Wkid];
                _ = AddLayersToMap(Path.Combine(fileLayers, @"TEMATICOS\\Catastro Minero.lyrx"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error con sistema de referencia \n" + ex);
            }
        }

        private void rbtn_geocat_catastro_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                gbox_geocat_catastro.Visibility = Visibility.Collapsed;
                Map activeMap = MapView.Active.Map;
                var LayerOk = activeMap.FindLayers(catastroMinero).FirstOrDefault();
                LayerOk.SetVisibility(false);
            }
            catch {; }
        }

        public async Task AddLayersToMap(string pathLyrBase)
        {
            await QueuedTask.Run(() =>
            {
                try
                {
                    if (!File.Exists(pathLyrBase))
                    {
                        throw new FileNotFoundException("El archivo .lyr no existe.");
                    }
                    Map activeMap = MapView.Active.Map;
                    var listaTOC = activeMap.GetLayersAsFlattenedList().Select(layer => layer.Name).ToList();
                    //string nameLayerOk = SearchLayersInDatoIGN.FindMostSimilarWord(catastroMinero, listaTOC);
                    var LayerOk = activeMap.FindLayers(catastroMinero).FirstOrDefault();
                    if (LayerOk != null) return;
                    else
                    {
                        // Cargar el archivo .lyr como una capa en el mapa activo
                        Layer layerBase = LayerFactory.Instance.CreateLayer(new Uri(pathLyrBase), activeMap, 0);
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Agregue un Mapa al Proyecto \n" + ex);
                }
            });
        }

        private void btn_geocat_clean_Click(object sender, RoutedEventArgs e)
        {
            tbx_geocat_cod_dm.Clear();
            tbx_geocat_nombre_dm.Clear();
            tbx_geocat_titular_dm.Clear();
        }

        private async void btn_geocat_searchDM_Click(object sender, RoutedEventArgs e)
        {
            string code_dm = tbx_geocat_cod_dm.Text;
            string nombre_dm = tbx_geocat_nombre_dm.Text;
            string titular_dm = tbx_geocat_titular_dm.Text;
            string query = String.Format("CODIGOU LIKE '%{0}%' AND CONCESION LIKE '%{1}%' AND TIT_CONCES LIKE '%{2}%'", code_dm, nombre_dm, titular_dm);
            //QueryShowTable(query);
            await SelectFeaturesByAttribute(catastroMinero, query);
            QueryShowTable(query);
        }

        public void QueryShowTable(string whereClause)
        {
            try
            {
                Map activeMap = MapView.Active.Map;
                var lyrs = MapView.Active.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>();
                var LayerOk = activeMap.FindLayers(catastroMinero).OfType<FeatureLayer>();
                MapView.Active.SelectLayers(LayerOk.ToList());
                if (LayerOk != null)
                {
                    // Cargar el archivo .lyr como una capa en el mapa activo
                    //var openTableBtnCmd = FrameworkApplication.GetPlugInWrapper("esri_editing_table_openTablePaneButton") as ICommand;
                    //if (openTableBtnCmd != null)
                    //{
                    //    // Let ArcGIS Pro do the work for us
                    //    if (openTableBtnCmd.CanExecute(null))
                    //    {
                    //        openTableBtnCmd.Execute(null);
                    //    }
                    //}
                        var mapMember = activeMap.GetLayersAsFlattenedList().OfType<MapMember>().FirstOrDefault();
                        //Gets or creates the CIMMapTableView for a MapMember.
                        var tableView = FrameworkApplication.Panes.GetMapTableView(mapMember);
                        //Configure the table view
                        tableView.DisplaySubtypeDomainDescriptions = false;
                        tableView.SelectionMode = true;
                        tableView.ShowOnlyContingentValueFields = true;
                        tableView.HighlightInvalidContingentValueFields = true;
                        //Open the table pane using the configured tableView. If a table pane is already open it will be activated.
                        //You must be on the UI thread to call this function.
                        var tablePane = FrameworkApplication.Panes.OpenTablePane(tableView);

                    }
                    else {; };
                }
            catch (Exception ex)
            {
                MessageBox.Show("Agregue un Mapa al Proyecto \n" + ex);
            } 
        }

        public async Task SelectFeaturesByAttribute(string layerName, string whereClause)
        {
            await QueuedTask.Run(() =>
            {
                var mapView = MapView.Active;
                if (mapView == null) return;
                var mapActive = MapView.Active.Map;
                // Deseleccionamos todas las capas antes de realizar el query
                var layers = mapActive.FindLayers(layerName);
                foreach (var lyr in mapActive.Layers.OfType<FeatureLayer>())
                {
                    lyr.ClearSelection();
                }
                // Encuentra la capa en la tabla de contenido por nombre
                Layer layer = layers.FirstOrDefault();

                if (layer != null)
                {
                    // Asegúrate de que la capa sea una FeatureLayer
                    if (layer is FeatureLayer featureLayer)
                    {
                        // Crea un nuevo QueryFilter y define el criterio de selección
                        QueryFilter queryFilter = new QueryFilter
                        {
                            WhereClause = whereClause
                        };
                        // Realiza la selección
                        featureLayer.Select(queryFilter);
                        //Asegúrate de que la vista esté actualizada con la nueva selección
                        MapView.Active.ZoomToSelected();//.ZoomToAsync(layer);
                    }
                }
            });
        }

        public void EnableRadioButtonsStartingWithRbtn()
            {
                // Recorre todos los elementos visuales en la ventana
                foreach (var control in FindVisualChildren<RadioButton>(this))
                {
                    if (control.Name.StartsWith("rbtn_"))
                    {
                        control.IsEnabled = true; // Habilita el RadioButton
                    }
                }
            }

        // Método auxiliar para encontrar todos los controles de un tipo específico en un contenedor
        //public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        //{
        //    if (depObj != null)
        //    {
        //        for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
        //        {
        //            DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(depObj, i);
        //            if (child != null && child is T)
        //            {
        //                yield return (T)child;
        //            }

        //            foreach (T childOfChild in FindVisualChildren<T>(child))
        //            {
        //                yield return childOfChild;
        //            }
        //        }
        //    }
        //}
    }
}


