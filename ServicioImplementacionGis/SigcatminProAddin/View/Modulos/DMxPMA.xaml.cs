using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Mapping;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para DMxPMA.xaml
    /// </summary>
    public partial class DMxPMA : Page
    {
        DatabaseHandler dataBaseHandler;
        public DMxPMA()
        {
            InitializeComponent();
            CurrentUser();
            dataBaseHandler = new DatabaseHandler();
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
        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
        }
        private void CbxDatum_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("WGS-84", 2));
            cbp.Add(new ComboBoxPairs("PSAD-56", 1));

            // Asignar la lista al ComboBox
            CbxDatum.DisplayMemberPath = "_Key";
            CbxDatum.SelectedValuePath = "_Value";
            CbxDatum.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxDatum.SelectedIndex = 0;
            GlobalVariables.CurrentDatumDm = "2";
        }

        private async void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarUtils progressBar = new ProgressBarUtils("Procesando DM por PMA...");
            progressBar.Show();
            try
            {
                // Obtenemos los DM por PMA
                var dmxPMA = dataBaseHandler.GetDmxPma(CbxDatum.Text);
                int count1 = dmxPMA.Rows.Count;
                // Obtenemos los petitorios
                var petPMA = dataBaseHandler.GetPmaPetitions(CbxDatum.Text);
                int count2 = petPMA.Rows.Count;
                TittleLabelGrid1.Content = $"Relación de derechos mineros con calificación PMA: {count1}";
                TittleLabelGrid2.Content = $"Relación de derechos mineros con calificación PMA fuera de la demarcación calificada: {count2}";
                DataGridResultado1.ItemsSource = dmxPMA.DefaultView;
                DataGridResultado2.ItemsSource = petPMA.DefaultView;

                if (petPMA.Rows.Count == 0)
                {
                    MessageBox.Show("No Existe Petitorios PMA ubicados fuera de la Demarcación Calificada", "Observación...", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                string lista_codigo_pma = "";
                string lista_codigo_provg = "";
                string lista_codigo_provd = "";
                string lista_codigo_titular = "";
                string lista_codigo_prov = "";

                for (int contador = 0; contador < petPMA.Rows.Count; contador++)
                {
                    string codigo_eval = petPMA.Rows[contador]["CG_CODIGO"].ToString();
                    string codigo_prov = petPMA.Rows[contador]["CODDEM_G"].ToString();
                    string codigo_provd = petPMA.Rows[contador]["CODDEM"].ToString();
                    string codigo_titular = petPMA.Rows[contador]["TITULAR"].ToString();

                    if (contador == 0)
                    {
                        lista_codigo_pma = $"CODIGOU = '{codigo_eval}'";
                        lista_codigo_provg = $"CD_PROV = '{codigo_prov}'";
                        lista_codigo_provd = $"CD_PROV = '{codigo_provd}'";
                        lista_codigo_titular = $"TIT_CONCES = '{codigo_titular}'";
                    }
                    else
                    {
                        lista_codigo_pma += $" OR CODIGOU = '{codigo_eval}'";
                        lista_codigo_provg += $" OR CD_PROV = '{codigo_prov}'";
                        lista_codigo_provd += $" OR CD_PROV = '{codigo_provd}'";
                        lista_codigo_titular += $" OR TIT_CONCES = '{codigo_titular}'";
                    }
                }
                lista_codigo_prov = lista_codigo_provd + " OR " + lista_codigo_provg;

                // Creamos el mapa de Catastro Minero y sus capas

                var sdeHelper = new SdeConnectionGIS();
                Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                            , AppConfig.userName
                                                                                            , AppConfig.password);
                List<string> mapsToDelete = new List<string>()
                {
                    GlobalVariables.mapNameCatastro,
                };
                await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);
                //int datum=2;
                int datumwgs84 = 2;
                string datumStr = GlobalVariables.datumWGS;
                string zoneDm = "18";
                string fechaArchi = DateTime.Now.Ticks.ToString();
                GlobalVariables.idExport = fechaArchi;
                string provinciaShpName = "provin_s" + fechaArchi;
                string sortProvinciaShpName = "provins_" + fechaArchi;    
                string provinciaShpNamePath = "provin_s" + fechaArchi + ".shp";

                string catastroShpName = "tit_cata" + fechaArchi;
                string sortCatastroShpName = "titu_cata" + fechaArchi;
                string pmaCatastroShpName = "cata_pma" + fechaArchi;


                await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro);
                Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");
                if ((int)CbxDatum.SelectedValue == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Provincia_WGS + zoneDm, true);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Provincia_Z + zoneDm, true);
                }
                await SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_prov);
                //await LabelUtils.LabelFeatureLayer(featureClassLoader.pFeatureLayer_prov, "NM_PROV", 9, "#343434");
                await featureClassLoader.ExportAttributesTemaAsync("Provincia", true, provinciaShpNamePath, lista_codigo_prov);
                await FeatureProcessorUtils.AgregarCampoTemaTpm(provinciaShpName, "provincia_PMA");
                await FeatureProcessorUtils.UpdateValueLayerAsync(provinciaShpName, "provincia_PMA", petPMA);
                await FeatureProcessorUtils.LayerTableSortAsync(provinciaShpName, sortProvinciaShpName, "PROVIN", true);
                await FeatureProcessorUtils.UpdateValueLayerAsync(sortProvinciaShpName, "provincias_PMA", petPMA);
                var _featureLayer_provPMA = await LayerUtils.ChangeLayerNameAsync(sortProvinciaShpName, "provincias_PMA");
                //Carga capa Catastro
                if ((int)CbxDatum.SelectedValue == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }
                await featureClassLoader.ExportAttributesTemaAsync("Catastro", true, catastroShpName, lista_codigo_titular);
                await FeatureProcessorUtils.LayerTableSortAsync(catastroShpName, sortCatastroShpName, "TIT_CONCES", true);
                var _featureLayer_tit = await LayerUtils.ChangeLayerNameAsync(sortCatastroShpName, "titular_catastro");
                string stylePMA = System.IO.Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleProvinciaPMA);
                await SymbologyUtils.ApplySymbologyFromStyleAsync("provincias_PMA", stylePMA, "LEYEN", StyleItemType.PolygonSymbol, "");
                await featureClassLoader.ExportAttributesTemaAsync("Catastro", true, pmaCatastroShpName, lista_codigo_pma);
                var _featureLayer_cat_pma = await LayerUtils.ChangeLayerNameAsync(pmaCatastroShpName, "catastro_PMA");
                await LayerUtils.RemoveLayersFromMapNameAsync(GlobalVariables.mapNameCatastro, new List<string>() { "provincias_PMA", "titular_catastro", "catastro_PMA", "Provincia" }, false);
                List<string> fields = new List<string>() { "CD_PROV", "NM_PROV" };
                await LabelUtils.LabelFeatureLayerWithMultipleFields(_featureLayer_provPMA, fields, " ", 8, "#343434");
                await SymbologyUtils.ColorPolygonSimple(_featureLayer_tit);
                await SymbologyUtils.ColorPolygonSimple(_featureLayer_cat_pma);

                // Layout
                double y;

                var layoutConfiguration = new LayoutConfiguration();
                layoutConfiguration.BasePath = GlobalVariables.ContaninerTemplatesReport;
                var layoutUtils = new LayoutUtils(layoutConfiguration);
                var layoutPath = layoutUtils.DeterminarRutaPlantilla("Plano petitorios_PMA");
                string nameWithoutExtention = System.IO.Path.GetFileNameWithoutExtension(layoutPath);
                //int scale = (int)StringProcessorUtils.GetScaleFromFormatsString(SelectedEscala);
                GlobalVariables.currentTable = petPMA;
                var layoutProjectItem = await LayoutUtils.AddLayoutPath(layoutPath, "provincias_PMA", GlobalVariables.mapNameCatastro, nameWithoutExtention);
                ElementsLayoutUtils elementsLayoutUtils = new ElementsLayoutUtils();
                y = await elementsLayoutUtils.AgregarTextosLayoutAsync("petitorios_PMA", layoutProjectItem, 16.0);
                // Invoca el comando de exportación a PDF
                var pane = FrameworkApplication.DockPaneManager.Find("esri_layouts_exportDockPane");
                // Lo activamos
                pane.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                progressBar.Dispose();
            }
            progressBar.Dispose();
        }
    }
}
