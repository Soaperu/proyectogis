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
using System.Security.Policy;
using DevExpress.Mvvm.Native;
using static SigcatminProAddin.View.Modulos.EvaluacionDM;
using DevExpress.Utils;
using DevExpress.XtraPrinting.Native;

using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;






namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para LimiteAreaInteres.xaml
    /// </summary>
    public partial class LimiteAreaInteres : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        


        public LimiteAreaInteres()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();

            dataBaseHandler = new DatabaseHandler();
            CbxTypeConsult.SelectedIndex = 0;
            TbxRadio.Text = "5";

        }

        private void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("WGS-84", 1));
            cbp.Add(new ComboBoxPairs("PSAD-56", 2));

            // Asignar la lista al ComboBox
            CbxSistema.DisplayMemberPath = "_Key";
            CbxSistema.SelectedValuePath = "_Value";
            CbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxSistema.SelectedIndex = 0;

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

        
        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxEsteMin.Text))
            {
                string message = "Por favor ingrese un valor coordenada Este Mínimo";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertancia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TbxEsteMax.Text))
            {
                string message = "Por favor ingrese un valor coordenada Este Máximo";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertancia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TbxNorteMin.Text))
            {
                string message = "Por favor ingrese un valor coordenada Norte Mínimo";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertancia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(TbxNorteMax.Text))
            {
                string message = "Por favor ingrese un valor coordenada Norte Máximo";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                 "Advertancia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
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
            int datum = (int)CbxSistema.SelectedValue;
            string datumStr = CbxSistema.Text;
            int radio = int.Parse(TbxRadio.Text);
            string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
            //List<string> listMaps = new List<string> {"CATASTRO MINERO"};
            await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync("CATASTRO MINERO");
            //int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
            //string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
            //GlobalVariables.CurrentCodeDm = codigoValue;
            //string stateGraphic = DataGridResult.GetCellValue(focusedRowHandle, "PE_VIGCAT")?.ToString();
            //string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
            //GlobalVariables.CurrentZoneDm = zoneDm;

            string zoneDm = (string)CbxZona.SelectedValue;
            GlobalVariables.CurrentZoneDm = zoneDm;
            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            //var v_zona_dm = dataBaseHandler.VerifyDatumDM(codigoValue);
            string fechaArchi = DateTime.Now.Ticks.ToString();
            GlobalVariables.idExport = fechaArchi;
            string catastroShpName = "Catastro" + fechaArchi;
            GlobalVariables.CurrentShpName = catastroShpName;
            string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            string dmShpName = "DM" + fechaArchi;
            string dmShpNamePath = "DM" + fechaArchi + ".shp";

            try
            {
                // Obtener el mapa Catastro//

                Map map = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");

                //Carga capa Catastro
                if (datum == 1)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }

                ////Carga capa Distrito
                //if (datum == 1)
                //{
                //    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_WGS + zoneDm, false);
                //}
                //else
                //{
                //    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_Z + zoneDm, false);
                //}
                //await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_dist);

                //Carga capa Zona Urbana
                if (datum == 1)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaWgs84 + zoneDm, false); //"DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_"
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaPsad56 + zoneDm, false);
                }

                int Tbx_EsteMin = int.Parse(TbxEsteMin.Text);
                int Tbx_EsteMax = int.Parse(TbxEsteMax.Text);
                int Tbx_NorteMin = int.Parse(TbxNorteMin.Text);
                int Tbx_NorteMax = int.Parse(TbxNorteMax.Text);

                //var extentDmRadio = ObtenerExtent(codigoValue, datum, radio);
                //var extentDm = ObtenerExtent(codigoValue, datum);

                var extentDmRadio = ObtenerExtent(Tbx_EsteMin, Tbx_NorteMin, Tbx_EsteMax, Tbx_NorteMax, datum, radio);


                // Llamar al método IntersectFeatureClassAsync desde la instancia
                string listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);
                // Encontrando Distritos superpuestos a DM con

                //DataTable intersectDist;
                //if (datum == 1)
                //{
                //    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_WGS_" + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_" + zoneDm, codigoValue);
                //}
                //CommonUtilities.DataProcessorUtils.ProcessorDataAreaAdminstrative(intersectDist);
                //DataTable orderUbigeosDM;
                //orderUbigeosDM = dataBaseHandler.GetUbigeoData(codigoValue);


                //Carga capa Hojas IGN
                await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
                //string listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", extentDm.xmin, extentDm.ymin, extentDm.xmax, extentDm.ymax);
                string listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", Tbx_EsteMin, Tbx_NorteMin, Tbx_EsteMax, Tbx_NorteMax);
                GlobalVariables.CurrentPagesDm = listHojas;
                
                // Encontrando Caram superpuestos a DM con
                //DataTable intersectCaram;
                //if (datum == 1)
                //{
                //    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, FeatureClassConstants.gstrFC_Caram84 + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_Caram56 + zoneDm, codigoValue);
                //}
                //CommonUtilities.DataProcessorUtils.ProcessorDataCaramIntersect(intersectCaram);

                //DataTable intersectCForestal;
                //if (datum == 1)
                //{
                //    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, FeatureClassConstants.gstrFC_Cforestal + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_forestal + zoneDm, codigoValue);
                //}
                //CommonUtilities.DataProcessorUtils.ProcessorDataCforestalIntersect(intersectCForestal);

                //DataTable intersectDm;
                //if (datum == 1)
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroWGS84, FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, codigoValue);
                //}
                //DataTable distBorder;
               // var distBorder = dataBaseHandler.CalculateDistanceToBorder(codigoValue, zoneDm, datumStr);
                // GlobalVariables.DistBorder = Math.Round(Convert.ToDouble(distBorder.Rows[0][0]) / 1000.0, 3);
                //CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listCodigoColin, out string listCodigoSup, out List<string> colectionsAreaSup);
                //await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(map,Path.Combine(outputFolder, catastroShpNamePath));
                await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
                ///await UpdateValueAsync(catastroShpName, codigoValue);
                //CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listaCodigoColin, out string listaCodigoSup, out List<string> coleccionesAareaSup);
                //await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.UpdateRecordsDmAsync(catastroShpName, listaCodigoColin, listaCodigoSup, coleccionesAareaSup);
                ///await featureClassLoader.ExportAttributesTemaAsync(catastroShpName, GlobalVariables.stateDmY, dmShpName, $"CODIGOU='{codigoValue}'");
                ///await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, @"C:\bdgeocatmin\Estilos\CATASTRO.stylx", "LEYENDA", codigoValue);
                ///var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, codigoValue);
                /// var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEval, Params);
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
                CommonUtilities.ArcgisProUtils.LayerUtils.SelectSetAndZoomByNameAsync(catastroShpName);
                List<string> layers = new List<string>() { "Catastro", "Carta IGN", dmShpName, "Zona Urbana" };
                await CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(layers);
                await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameAsync(catastroShpName, "Catastro");
                GlobalVariables.CurrentShpName = "Catastro";
                MapUtils.AnnotateLayerbyName("Catastro", "CONTADOR", "Anotaciones");
                UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                await uTMGridGenerator.GenerateUTMGridAsync(extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, "Malla", zoneDm);
            }
            catch (Exception ex) { }



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

        private ExtentModel ObtenerExtent(int XMin, int YMin, int XMax, int YMax, int datum, int radioKm = 0)
        {
            //// Obtener las coordenadas usando la función ObtenerCoordenadas
            //DataTable coordenadasTable = ObtenerCoordenadas(codigoValue, datum);

            //// Asegurarse de que la tabla contiene filas
            //if (coordenadasTable.Rows.Count == 0)
            //{
            //    throw new Exception("No se encontraron coordenadas para calcular el extent.");
            //}
            int radioMeters = radioKm * 1000;
            // Inicializar las variables para almacenar los valores extremos
            int xmin = XMin; // int.MaxValue;
            int xmax = XMax; // int.MinValue;
            int ymin = YMin; // int.MaxValue;
            int ymax = YMax; // int.MinValue;

            // Iterar sobre las filas para calcular los valores extremos
            //foreach (DataRow row in coordenadasTable.Rows)
            //{
            //    int este = Convert.ToInt32(row["ESTE"]);
            //    int norte = Convert.ToInt32(row["NORTE"]);

            //    if (este < xmin) xmin = este;
            //    if (este > xmax) xmax = este;
            //    if (norte < ymin) ymin = norte;
            //    if (norte > ymax) ymax = norte;
            //}

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
        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        //public async void ObtenerLimitesShapefile()
        //{
        //    // Nombre de la capa Shapefile que se encuentra en el mapa
        //    string nombreShapefile = "NombreDeTuShapefile.shp";

        //    // Buscar la capa en el mapa
        //    var featureLayer = MapView.Active.Map.Layers
        //                            .FirstOrDefault(l => l.Name == nombreShapefile) as FeatureLayer;

        //    if (featureLayer != null)
        //    {
        //        // Usar QueuedTask para acceder a la capa y obtener la extensión
        //        var envelope = await QueuedTask.Run(() => featureLayer.GetExtentAsync());

        //        // Imprimir los límites
        //        Console.WriteLine($"Límite Mínimo: ({envelope.XMin}, {envelope.YMin})");
        //        Console.WriteLine($"Límite Máximo: ({envelope.XMax}, {envelope.YMax})");
        //    }
        //    else
        //    {
        //        Console.WriteLine("La capa no es de tipo FeatureLayer o no se encontró.");
        //    }
        //}

        public async Task ObtenerLimitesAsync()
        {
            // Buscar la capa en el mapa por nombre
            var featureLayer = MapView.Active.Map.Layers
                                .FirstOrDefault(l => l.Name == "NombreDeTuShapefile") as FeatureLayer;

            if (featureLayer != null)
            {
                // Usar QueryExtentAsync() para obtener la extensión de la capa
                var envelope = await QueuedTask.Run(() => featureLayer.QueryExtent());

                // Obtener las coordenadas mínimas y máximas de la extensión
                double minX = envelope.XMin;
                double minY = envelope.YMin;
                double maxX = envelope.XMax;
                double maxY = envelope.YMax;

                // Mostrar los límites en consola
                Console.WriteLine($"Límite Mínimo: ({minX}, {minY})");
                Console.WriteLine($"Límite Máximo: ({maxX}, {maxY})");
            }
            else
            {
                Console.WriteLine("La capa no se encontró en el mapa.");
            }
        }






    }
}
