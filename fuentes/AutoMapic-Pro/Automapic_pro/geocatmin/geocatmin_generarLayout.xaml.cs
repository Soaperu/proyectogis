using ArcGIS.Core.CIM;
using ArcGIS.Core.Geometry;
using ICIM = ArcGIS.Core.Internal.CIM;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Automapic_pro.GlobalVariables;
using ArcGIS.Core.Internal.CIM;
using Envelope = ArcGIS.Core.Geometry.Envelope;
using SpatialReference = ArcGIS.Core.Geometry.SpatialReference;
using System.Collections;
using TextElement = ArcGIS.Desktop.Layouts.TextElement;
using ArcGIS.Desktop.Internal.Mapping.CommonControls;

namespace Automapic_pro.modulos
{
    /// <summary>
    /// Lógica de interacción para geocatmin_generarLayout.xaml
    /// </summary>
    public partial class geocatmin_generarLayout : Window
    {
        private geocatmin_cartaIGN Geocatmin_cartaIGN;
        
        public geocatmin_generarLayout()
        {
            InitializeComponent();
            if (sheetChecked=="250k")
            {
                rbtn_Scale250k.IsChecked = true;
                rbtn_Scale100k.IsEnabled = false;
                rbtn_Scale50k.IsEnabled = false;
            }
            else if (sheetChecked=="100k")
            {
                rbtn_Scale250k.IsEnabled = false;
                rbtn_Scale100k.IsChecked = true;
                rbtn_Scale50k.IsEnabled = false;
            }
            else if (sheetChecked=="50k")
            {
                rbtn_Scale250k.IsEnabled = false;
                rbtn_Scale100k.IsEnabled = false;
                rbtn_Scale50k.IsChecked = true;
            }
        }

        public static Envelope extentMap = null;

        private void btn_aceptarLayout_Click(object sender, RoutedEventArgs e)
        {
            //extentMap = MapView.Active.Extent;
            if  (tbx_titleMap.Text == "")
            {
                MessageBox.Show("Escriba: Nombre del mapa...");
                return;
            }
            else if ( tbx_checkedMap.Text == "")
            {
                MessageBox.Show("Escriba: Elaborado por...");
                return;
            }
            else if (tbx_madeMap.Text == "")
            {
                MessageBox.Show("Escriba: Revisado por...");
                return; 
            }

            if (rbtn_Scale50k.IsChecked == true) 
            {
                RemoveMapsAndLayoutsAsync("Layers", "Ubicacion", "Carta_IGN_50");
                ImportMXD(System.IO.Path.Combine(fileTemplates, template50k));
                copyLayersToMapLayoutAsync("Carta_IGN_50", "Carta50");
                ZoomToMapLayoutAsync("Carta_IGN_50", ExtentMain50k, utm, 50000);
                SetElementsTextAsync(tbx_titleMap.Text, tbx_madeMap.Text, "Carta_IGN_50");
            }
            else if (rbtn_Scale100k.IsChecked == true)
            {
                RemoveMapsAndLayoutsAsync("Layers", "Ubicacion", "Carta_IGN_100");
                ImportMXD(System.IO.Path.Combine(fileTemplates, template100k));
                copyLayersToMapLayoutAsync("Carta_IGN_100", "Carta");
                ZoomToMapLayoutAsync("Carta_IGN_100", ExtentMain100k, utm, 100000);
                SetElementsTextAsync(tbx_titleMap.Text, tbx_madeMap.Text, "Carta_IGN_100");
            }
            else if (rbtn_Scale250k.IsChecked == true)
            {
                RemoveMapsAndLayoutsAsync("Layers", "Ubicacion", "Carta_IGN_250k_v1");
                ImportMXD(System.IO.Path.Combine(fileTemplates, template250k));
                copyLayersToMapLayoutAsync("Carta_IGN_250k_v1", "Carta250");
                ZoomToMapLayoutAsync("Carta_IGN_250", ExtentMain250k, utm, 250000);
                SetElementsTextAsync(tbx_titleMap.Text, tbx_madeMap.Text, "Carta_IGN_250k_v1");
            }
            this.Close();
        }

        private async void ImportMXD(string mxdFilePath)
        {
            var addItem = ItemFactory.Instance.Create(mxdFilePath, ItemFactory.ItemType.PathItem) as IProjectItem;
            await QueuedTask.Run(() =>
            {
                // Agregar el proyecto actual
                Project.Current.AddItem(addItem);
            });
        }

        private async void copyLayersToMapLayoutAsync(string nameLayout, string nameCarta)
        {
            await QueuedTask.Run(() =>
            {
                Map myMap = Project.Current.GetItems<MapProjectItem>().First().GetMap();
                // Accede al Layout deseado
                LayoutProjectItem layoutItem = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(l => l.Name == nameLayout);
                if (layoutItem == null)
                    return;
                Layout layout = layoutItem.GetLayout();
                // Busca el MapFrame en el Layout.
                MapFrame mapFrameLayout1 = layout.Elements.OfType<MapFrame>().FirstOrDefault(n => n.Name == "Layers Map Frame");
                MapFrame mapFrameLayout2 = layout.Elements.OfType<MapFrame>().FirstOrDefault(n => n.Name == "Ubicacion Map Frame");
                if (mapFrameLayout1 == null)
                    return;
                // Una vez que tienes el MapFrame, puedes acceder al Map asociado.
                Map mapLayout1 = mapFrameLayout1.Map;
                SetMapSpatialReferenceToUTM(utm,mapLayout1);
                Map mapLayout2 = mapFrameLayout2.Map;
                GroupLayer layerGroupIGN = myMap.GetLayersAsFlattenedList().OfType<GroupLayer>().FirstOrDefault(group => group.Name == nameLyrCartografiaBase);
                GroupLayer layerGroupCB = myMap.GetLayersAsFlattenedList().OfType<GroupLayer>().FirstOrDefault(group => group.Name == nameLyrCartaIGN);
                //GroupLayer newGroupLayer = LayerFactory.Instance.CreateGroupLayer(groupLayer.Name) as GroupLayer;

                foreach (var layer in myMap.Layers.Reverse().Where(l => l.IsVisible))
                {
                    if (layer is GroupLayer groupLayer)
                    {
                        // Si el nombre del grupo de capas es "xxxxxx", se omíte.
                        if (groupLayer.Name == nameLyrCartaIGN)
                            continue;

                        // Si es un grupo de capas, crea un nuevo grupo en el mapa destino.
                        GroupLayer newGroupLayer = LayerFactory.Instance.CreateGroupLayer(mapLayout1, 0, groupLayer.Name);

                        // Agrega solo las capas visibles dentro del grupo al nuevo grupo en el mapa destino.
                        foreach (var innerLayer in groupLayer.Layers.Reverse().Where(l => l.IsVisible))
                        {
                            Layer copyLayer = LayerFactory.Instance.CopyLayer(innerLayer,newGroupLayer, 0);
                        }
                    }
                    else if (layer is Layer regularLayer)
                    {
                        if (layer.IsVisible == true)
                        {
                            Layer copyLayer = LayerFactory.Instance.CopyLayer(layer, mapLayout1, 0);
                        }
                    }
                }

                if (layerGroupCB != null)
                {
                    foreach (var layer2 in layerGroupCB.Layers)
                    {
                        if (layer2.IsVisible == true)
                        {
                            Layer copyLayer = LayerFactory.Instance.CopyLayer(layer2, mapLayout2, 0);
                            FeatureLayer toFeatureLayercopy = copyLayer as FeatureLayer;
                            var cimLayerDef = toFeatureLayercopy.GetDefinition() as CIMFeatureLayer;
                            cimLayerDef.MinScale = 0; // "Out Beyond" (Por ejemplo: No mostrar la capa cuando el mapa esté más alejado que 1:10000)
                            cimLayerDef.MaxScale = 0; // "Out Beyond" (Por ejemplo: No mostrar la capa cuando el mapa esté más alejado que 1:1000)
                            toFeatureLayercopy.SetDefinition(cimLayerDef);
                            if (layer2.Name.Contains(nameCarta))
                            {
                                Layer copyLayer2 = LayerFactory.Instance.CopyLayer(layer2, mapLayout2, 0);
                                copyLayer2.SetName(layer2.Name + "_s");
                                // Obtener la definición de símbolo actual
                                FeatureLayer toFeatureLayer = copyLayer2 as FeatureLayer;
                                toFeatureLayer.SetLabelVisibility(false);
                                var symbol = toFeatureLayer.GetRenderer() as CIMSimpleRenderer;
                                // Crear una nueva simbología con relleno de color amarillo y transparencia del 40%
                                var newFillSymbol = SymbolFactory.Instance.ConstructPolygonSymbol(
                                    ColorFactory.Instance.CreateRGBColor(255, 255, 0, 40), SimpleFillStyle.Solid,null);
                                symbol.Symbol = newFillSymbol.MakeSymbolReference();
                                // Actualiza la simbologia 
                                toFeatureLayer.SetRenderer(symbol);
                                //Creamos un query para solo resaltar la carta del mapa principal
                                toFeatureLayer.SetDefinitionQuery(GeneralQuery);
                                var cimLayerDef2 = toFeatureLayer.GetDefinition() as CIMFeatureLayer;
                                cimLayerDef2.MinScale = 0; 
                                cimLayerDef2.MaxScale = 0; 
                                toFeatureLayercopy.SetDefinition(cimLayerDef2);
                            };
                            if (layer2.Name == "Provincias")
                            {
                                FeatureLayer toFeatureLayer = copyLayer as FeatureLayer;
                                toFeatureLayer.SetLabelVisibility(false);
                            }
                            
                        }
                    }
                }
            });
        }

        private async void ZoomToMapLayoutAsync(string nameLayout, Envelope ExtentMapFrameMain, string zonaUTM, int getScale)
        {
            await QueuedTask.Run(() =>
            {
                // Accede al Layout deseado
                LayoutProjectItem layoutItem = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(l => l.Name == nameLayout);
                if (layoutItem == null)
                    return;
                Layout layout = layoutItem.GetLayout();
                // Busca el MapFrame en el Layout.
                MapFrame mapFrameLayout1 = layout.Elements.OfType<MapFrame>().FirstOrDefault(n => n.Name == "Layers Map Frame");
                MapFrame mapFrameLayout2 = layout.Elements.OfType<MapFrame>().FirstOrDefault(n => n.Name == "Ubicacion Map Frame");
                var srcMap1 = mapFrameLayout1.Map.SpatialReference;
                var srcMap2 = mapFrameLayout2.Map.SpatialReference;
                if (mapFrameLayout1 == null)
                    return;
                else
                {
                    if (zonaUTM == "18")
                    {
                        mapFrameLayout1.SetCamera(ExtentMapFrameMain);
                    }
                    else
                    {
                        var destSpatialReference = SpatialReferenceBuilder.CreateSpatialReference(int.Parse(string.Format("327{0}", zonaUTM))); // UTM 327+{17,19}
                        var srcSpatialReference = SpatialReferenceBuilder.CreateSpatialReference(32718);//SpatialReferences.WGS84; // WGS84
                        var transformation = ProjectionTransformation.Create(srcSpatialReference, destSpatialReference);
                        Envelope transformedEnvelope = (Envelope)GeometryEngine.Instance.ProjectEx(ExtentMapFrameMain, transformation);
                        mapFrameLayout1.SetCamera(transformedEnvelope);
                        var mapFrameDefinition = mapFrameLayout1.GetDefinition() as CIMMapFrame;
                        var grids = mapFrameDefinition.Grids;
                        CIMMeasuredGrid measuredGrid = (CIMMeasuredGrid)grids.FirstOrDefault(n => n.Name == "Measured Grid");
                        //firstgrid.ProjectedCoordinateSystem.WKID = int.Parse(string.Format("327{0}", zonaUTM));
                        measuredGrid.ProjectedCoordinateSystem = srcMap1.ToCIMSpatialReference() as ICIM.ProjectedCoordinateSystem;
                        grids[1]= measuredGrid;
                        mapFrameDefinition.Grids = grids;
                        mapFrameLayout1.SetDefinition(mapFrameDefinition);
                        Camera cam = mapFrameLayout1.Camera;
                        cam.Scale = getScale;
                        mapFrameLayout1.SetCamera(cam);
                    }
                }
                if (mapFrameLayout2 == null)
                    return;
                else
                {
                    var presrcSpatialReference = SpatialReferenceBuilder.CreateSpatialReference(32718);
                    var predestSpatialReference = SpatialReferenceBuilder.CreateSpatialReference(int.Parse(string.Format("327{0}", zonaUTM)));// UTM 327+{17,19}
                    var pretransformation = ProjectionTransformation.Create(presrcSpatialReference, predestSpatialReference);
                    Envelope pretransformedEnvelope = (Envelope)GeometryEngine.Instance.ProjectEx(ExtentMapFrameMain, pretransformation);
                    //var srcSpatialReference = SpatialReferenceBuilder.CreateSpatialReference(int.Parse(string.Format("327{0}", zonaUTM))); // UTM 327+{17,19}
                    var destSpatialReference = SpatialReferences.WGS84; // WGS84
                    var transformation = ProjectionTransformation.Create(predestSpatialReference, destSpatialReference);
                    Envelope transformedEnvelope = (Envelope)GeometryEngine.Instance.ProjectEx(pretransformedEnvelope, transformation);
                    mapFrameLayout2.SetCamera(transformedEnvelope);
                    Camera cam = mapFrameLayout2.Camera;
                    cam.Scale = 2000000;
                    mapFrameLayout2.SetCamera(cam);
                }

            });
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
                    SpatialReference Utm = SpatialReferenceBuilder.CreateSpatialReference(int.Parse(string.Format("327{0}", zonaUTM)));
                    // Establecer el sistema de coordenadas espaciales del mapa activo
                    activeMap.SetSpatialReference(Utm);
                }
                else
                {
                    SpatialReference Utm = SpatialReferenceBuilder.CreateSpatialReference(4236);
                    // Establecer el sistema de coordenadas espaciales del mapa activo
                    activeMap.SetSpatialReference(Utm);
                }
            });
        }
        public async void RemoveMapsAndLayoutsAsync(string mapName1, string mapName2, string layoutName)
        {
            await QueuedTask.Run(() =>
            {
                // Obtener los mapas del proyecto actual
                var maps = Project.Current.GetItems<MapProjectItem>();

                // Buscar mapas por nombre y eliminarlos si existen
                var map1 = maps.FirstOrDefault(m => m.Name == mapName1);
                var map2 = maps.FirstOrDefault(m => m.Name == mapName2);

                if (map1 != null) Project.Current.RemoveItem(map1);
                if (map2 != null) Project.Current.RemoveItem(map2);

                // Obtener los layouts del proyecto actual
                var layouts = Project.Current.GetItems<LayoutProjectItem>();

                // Buscar layout por nombre y eliminarlo si existe
                var layout = layouts.FirstOrDefault(l => l.Name == layoutName);
                if (layout != null) Project.Current.RemoveItem(layout);
            });
        }

        public async void SetElementsTextAsync(string titleMap, string madeMap, string nameLayout)
        {
            await QueuedTask.Run(() =>
            {
                // Accede al Layout deseado
                LayoutProjectItem layoutItem = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(l => l.Name == nameLayout);
                if (layoutItem == null)
                    return;
                Layout layout = layoutItem.GetLayout();
                TextElement txtElmTitle = layout.FindElement("title_text") as TextElement;
                TextElement txtElmCreatedby = layout.FindElement("createdby_text") as TextElement;
                TextElement txtElmScr = layout.FindElement("scr_text") as TextElement;
                TextProperties txtPropertiesTitle = new TextProperties(titleMap, "Arial", 13, "Regular");
                TextProperties txtPropertiesCreatedby = new TextProperties(madeMap, "Arial", 13, "Regular");
                txtElmTitle.SetTextProperties(txtPropertiesTitle);
                txtElmCreatedby.SetTextProperties(txtPropertiesCreatedby);
                MapFrame mapFrameLayout1 = layout.Elements.OfType<MapFrame>().FirstOrDefault(n => n.Name == "Layers Map Frame");
                var srcMap1 = mapFrameLayout1.Map.SpatialReference;
                TextProperties txtPropertiesScr = new TextProperties(srcMap1.Name, "Arial", 10, "Regular");
                txtElmScr.SetTextProperties(txtPropertiesScr);
            });
        }
    }
}