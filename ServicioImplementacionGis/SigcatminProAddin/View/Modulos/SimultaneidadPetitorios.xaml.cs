using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.DDL;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.CodeParser;
using DevExpress.Pdf.Native.BouncyCastle.Utilities;
using DevExpress.Xpf.Grid;
using DevExpress.XtraRichEdit.Tables.Native;
using SigcatminProAddin.Models;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para SimultaneidadPetitorios.xaml
    /// </summary>
    public partial class SimultaneidadPetitorios : Page
    {
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        private string fechaSimultaneidadAsString = "";
        private string v_sistema = "";
        private string currentDatum = "";
        private string gstrFC_Catastro_Minero = "";
        private string gstrFC_Cuadricula_Z = "";
        private string gstrFC_Carta = "";
        public SimultaneidadPetitorios()
        {
            InitializeComponent();
            CurrentUser();
        }
        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
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

        private async void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {
            DateTime fechaDatum = new DateTime(2016, 4, 7);
            DateTime fechaLibreDenunciabilidad = new DateTime(2020, 1, 1);
            

            List<string> mapsToDelete = new List<string>()
            {
                GlobalVariables.mapNameCatastro,

            };

            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);
            await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro);

            DateTime fechaSimultaneidad = DatePickerInicio.SelectedDate.Value;
            fechaSimultaneidadAsString = fechaSimultaneidad.ToString("dd/MM/yyyy");

            if (fechaSimultaneidad < fechaDatum)
            {
                v_sistema = "PSAD-56";
                currentDatum = "1";
                gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_";
                gstrFC_Cuadricula_Z = "GPO_CUA_CUADRICULAS_";
                gstrFC_Carta = "GPO_HOJ_HOJAS";
            }
            else
            {
                v_sistema = "WGS-84";
                currentDatum = "2";
                gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_WGS_";
                gstrFC_Cuadricula_Z = "GPO_CUA_CUADRICULAS_WGS_";
                gstrFC_Carta = "GPO_HOJ_HOJAS_18";
            }

            string verificaSimultaneidad = "0";

            verificaSimultaneidad = databaseHandler.VerificaSimultaneidad(fechaSimultaneidadAsString);

            if(verificaSimultaneidad == "0")
            {
                Simultaneidad simultaneidad = new Simultaneidad(fechaSimultaneidadAsString);
                simultaneidad.Main();

            }

            string v_tipo = "1";

            //Codigo de Remate

            DataTable grupoCodigoRemate = databaseHandler.ObtenerGruposSimultaneidad(v_tipo, fechaSimultaneidadAsString);
            DataTable dmxgrupo = databaseHandler.ObtenerDMyGruposSimultaneidad(fechaSimultaneidadAsString);
            DataTable ConcesionxGrupo = databaseHandler.ObtenerConcesionesyGruposSimultaneidad(fechaSimultaneidadAsString);

            //Actualizamos campo SI_CODREMATE
            string actualizaSimultaneidad = "0";
            if(fechaSimultaneidad > fechaLibreDenunciabilidad)
            {
                actualizaSimultaneidad = databaseHandler.UpdateDMSimultaneidad(fechaSimultaneidadAsString);
            }

            //Actaulizamos SG_D_DMHAGRSIMUL
            databaseHandler.InsertarDMxHaGRSimultaneidad(fechaSimultaneidadAsString);

            string fechaArchi = DateTime.Now.Ticks.ToString();
            string TablaDMSimultaneidad = "DMSimul" + fechaArchi;

            // Creamos tabla  DBf
            CrearTablaSimultaneidad(ConcesionxGrupo, TablaDMSimultaneidad);

            DataGridSimultaneidad.ItemsSource = grupoCodigoRemate;
        }


        private async void CrearTablaSimultaneidad(DataTable datos, string nombreTabla)
        {
            string glo_pathSIM = @"C:/bdgeocatmin/BDGEOCATMINPRO_84.gdb";

            try
            {
                await QueuedTask.Run(() =>
                {
                    using (Geodatabase geodatabase = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri(glo_pathSIM))))
                    {
                        SchemaBuilder schemaBuilder = new SchemaBuilder(geodatabase);
                        IReadOnlyList<TableDefinition> definitions = geodatabase.GetDefinitions<TableDefinition>();

                        foreach(TableDefinition definition in definitions)
                        {
                            if(definition.GetName().ToString().StartsWith("DMSimul"))
                            {
                                TableDescription table = new TableDescription(definition);
                                schemaBuilder.Delete(table);
                            }
                        }

                        // Definir campos
                        var fieldDescriptions =
                            new ArcGIS.Core.Data.DDL.FieldDescription[]
                            {
                                new ArcGIS.Core.Data.DDL.FieldDescription("CODIGO", FieldType.String) { Length = 13 },
                                new ArcGIS.Core.Data.DDL.FieldDescription("NOMBRE", FieldType.String) { Length = 50 },
                                new ArcGIS.Core.Data.DDL.FieldDescription("GRUPO", FieldType.String) { Length = 4 },
                                new ArcGIS.Core.Data.DDL.FieldDescription("COD_REMATE", FieldType.String) { Length = 20 },
                                new ArcGIS.Core.Data.DDL.FieldDescription("ZONA", FieldType.String) { Length = 2 },
                                new ArcGIS.Core.Data.DDL.FieldDescription("FEC_SIM", FieldType.Date),
                                new ArcGIS.Core.Data.DDL.FieldDescription("SUBGRUPO", FieldType.String) { Length = 5 }
                            };
                            var tableDescription = new TableDescription(nombreTabla, fieldDescriptions);
                            
                            schemaBuilder.Create(tableDescription);
                            bool success = schemaBuilder.Build();
                    }
                });

                // Llenar la tabla con datos del DataTable
                await QueuedTask.Run( async() =>
                {
                    using (Geodatabase geodatabase = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri(glo_pathSIM))))
                    using (ArcGIS.Core.Data.Table table = geodatabase.OpenDataset<ArcGIS.Core.Data.Table>(nombreTabla))
                    using (RowBuffer rowBuffer = table.CreateRowBuffer())
                    {
                        foreach (DataRow row in datos.Rows)
                        {
                            using (Row newRow = table.CreateRow(rowBuffer))
                            {
                                newRow["NOMBRE"] = row["CONCESION"].ToString();
                                newRow["FEC_SIM"] = Convert.ToDateTime(row["FEC_SIM"]);
                                newRow["GRUPO"] = row["GRUPO"].ToString();
                                newRow["SUBGRUPO"] = row["PS_SGRUPO"].ToString();
                                newRow["CODIGO"] = row["CODIGOU"].ToString();
                                newRow["ZONA"] = row["ZONA"].ToString();
                                newRow["COD_REMATE"] = row["COD_REMATE"].ToString();
                                newRow.Store();
                            }
                        }
                        ArcGIS.Desktop.Mapping.Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro);
                        ArcGIS.Desktop.Mapping.IStandaloneTableFactory tableFactory = ArcGIS.Desktop.Mapping.StandaloneTableFactory.Instance;
                        tableFactory.CreateStandaloneTable(new ArcGIS.Desktop.Mapping.StandaloneTableCreationParams(table), map);
                    }

                    
                });

              

                System.Windows.MessageBox.Show("Tabla generada y datos insertados correctamente.", "Éxito", System.Windows.MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error en generar tabla: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public DataTable ConvertirGridControlADataTable(GridControl gridControl)
        {
            DataTable dt = new DataTable();
            DataTable dataView = gridControl.ItemsSource as DataTable;
            
            dt = dataView;

            return dt;
        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtSimultaneidad = ConvertirGridControlADataTable(DataGridSimultaneidad);
            ConsultaDMSimultaneoc(dtSimultaneidad);
            generaplanosimultaneos();
        }

        public async void ConsultaDMSimultaneoc( DataTable p_ListBox)
        {
            string p_txtExiste;
            string esc_sim = "1";

            DataTable lodtbLeyenda = new DataTable();

            DataTable v_grusim = new DataTable();
            DataTable v_grusimf = new DataTable();
            DataTable v_coorsim = new DataTable();
            DataTable v_hojasim = new DataTable();
            int num = 0;

            string vnum_dm = "";
            string grusimf = "";
            string p_shapefile = "";
            string loStrShapefile = "Catastro" + DateTime.Now.Ticks.ToString();
            string tipo = "";
            string grusim = "";
            string v_zona_dm="";
            string sgrupo_sim ="";
            string v_cod_remate="";

            // Iterar sobre los elementos seleccionados en la lista
            for (int i = 0; i < p_ListBox.Rows.Count; i++)
            {
                if (Convert.ToBoolean(p_ListBox.Rows[i]["FLG_SEL"]))
                {
                    num++;
                    if (num > 2)
                    {
                        return;
                    }
                    else
                    {
                        grusim = p_ListBox.Rows[i]["GRUPO"].ToString();
                        vnum_dm = p_ListBox.Rows[i]["NUM_DM"].ToString();
                        v_zona_dm = p_ListBox.Rows[i]["ZONA"].ToString();
                        grusimf = p_ListBox.Rows[i]["GRUPOF"].ToString();
                        sgrupo_sim = p_ListBox.Rows[i]["SUB_GRUPO"].ToString();
                        v_cod_remate = p_ListBox.Rows[i]["COD_REMATE"].ToString();
                    }
                }
            }

            tipo = "1";
            v_grusimf = databaseHandler.ObtenercuadriculasSimultaneas(grusim, grusimf, tipo, fechaSimultaneidadAsString);
            string codigo_eval = "";
            string hectagis_sim = "";
            string hectagis_min = "";
            string cuad_eval = "";
            int num_cuasim = v_grusimf.Rows.Count;

            string lista_codigo_sim = "";
            string lista_dm_sim = "";
            string lista_cuad_sim = "";

            if (v_grusimf.Rows.Count > 0)
            {
                for (int contador = 0; contador < v_grusimf.Rows.Count; contador++)
                {
                    cuad_eval = v_grusimf.Rows[contador]["CD_CUAD"].ToString();
                    lista_cuad_sim += (contador == 0) ? $"CD_CUAD = '{cuad_eval}'" : $" OR CD_CUAD = '{cuad_eval}'";
                }
            }
            var sdeHelper = new SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);



            string datumwgs84 = "2";
            try
            {
                // Obtener el mapa Catastro//
                Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, v_zona_dm, "99");

                if (currentDatum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Cuadricula_WGS84 + v_zona_dm, false, lista_cuad_sim);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Cuadricula_PSAD56 + v_zona_dm, false, lista_cuad_sim);
                }

                


                //Obtener DMS Simultaneos
                v_grusim = databaseHandler.ObtenerDerechosSimultaneos(grusim, grusimf, tipo, fechaSimultaneidadAsString);

                if (v_grusim.Rows.Count > 0)
                {
                    for (int contador = 0; contador < v_grusimf.Rows.Count; contador++)
                    {
                        codigo_eval = v_grusim.Rows[contador]["CODIGOU"].ToString();
                        hectagis_sim = v_grusim.Rows[contador]["HECTAGIS"].ToString();

                        if(contador == 0)
                        {
                            hectagis_min = hectagis_sim;
                            lista_dm_sim = $"CODIGOU = '{codigo_eval}'";
                        }
                        else
                        {
                            if (Convert.ToDouble(hectagis_sim) < Convert.ToDouble(hectagis_min))
                            {
                                hectagis_min = hectagis_sim;
                            }
                            lista_dm_sim += $" OR CODIGOU = '{codigo_eval}'";
                        }
                    }
                }

                if (currentDatum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + v_zona_dm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + v_zona_dm, false);
                }

                await featureClassLoader.ExportAttributesTemaAsync("Catastro", false, "Cata_sim", lista_dm_sim);
                await FeatureProcessorUtils.AgregarCampoTemaTpm("Cata_sim", "Cata_sim");

                await featureClassLoader.ExportAttributesTemaAsync("Cuadriculas", false, "Cuadri_sim", lista_cuad_sim);
                await FeatureProcessorUtils.AgregarCampoTemaTpm("Cuadri_sim", "Cuadri_sim");
                await FeatureProcessorUtils.UpdateValueSimultaneoAsync("Cuadri_sim", sgrupo_sim, v_cod_remate);

                if (currentDatum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta56, false);
                }

                ArcGIS.Core.Geometry.Envelope envelope = null;
                envelope = featureClassLoader.pFeatureLayer_cuadriculas.QueryExtent();

                ExtentModel extentCuad = new ExtentModel {xmin= envelope.XMin, ymin= envelope.YMin, xmax= envelope.XMax, ymax= envelope.YMax };
                string listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", extentCuad.xmin, extentCuad.ymin, extentCuad.xmax, extentCuad.ymax);


                if(num_cuasim == 1)
                {
                    MapUtils.AnnotateVerticesofLayer("Cata_sim");
                }
                else
                {
                    string tipod = "1";
                    string inputFeatureClass = "Cuadri_sim";
                    string outputFeatureClass = "Cuadri_dsim";
                    string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);
                    string outputPath = Path.Combine(outputFolder, outputFeatureClass + ".shp");
                    List<string> dissolveFields = new List<string> { "Zona" };

                    //Realiza dissolve

                    List<string> parameters = new List<string>
                        {
                            inputFeatureClass,           // Capa de entrada
                            outputFeatureClass,          // Capa de salida
                            string.Join(";", dissolveFields),  // Campos de disolución
                            "Dissolve.Shape, Sum.Shape_area",                          // Estadísticas opcionales (dejar vacío si no se necesitan)
                            "MULTI_PART",                // MULTI_PART o SINGLE_PART (elige según necesidad)
                            "DISSOLVE_LINES"             // "DISSOLVE_LINES" o "UNSPLIT_LINES"
                        };

                    // Ejecuta el geoproceso Dissolve
                    var dissolveResult = await QueuedTask.Run(() =>
                        Geoprocessing.ExecuteToolAsync("management.Dissolve", parameters));

                    // Verificar si la herramienta se ejecutó correctamente
                    if (dissolveResult.IsFailed)
                    {
                        Console.WriteLine("Error en Dissolve: " + dissolveResult.ErrorMessages);
                    }
                    else
                    {
                        Console.WriteLine("Dissolve ejecutado con éxito.");
                    }

                    await FeatureProcessorUtils.AgregarCampoTemaTpm("Cuadri_dsim", "Cuadri_dsim");
                    await FeatureProcessorUtils.UpdateValueSimultaneoAsync("Cuadri_dsim", sgrupo_sim, v_cod_remate);
                    MapUtils.AnnotateVerticesofLayer("Cata_sim");
                    MapUtils.AnnotateLayerbyName("Cata_sim", "CONCESION", "Catastro_Anotaciones");
                    

                    if (num_cuasim == 1)
                    {
                        MapUtils.AnnotateLayerbyName("Cuadri_sim", "COD_REMATE", "Cuadriculas_Anotaciones");
                        await LayerUtils.ChangeLayerNameAsync("Cuadri_sim", "Cuadricula_sim");

                    }
                    else
                    {
                        MapUtils.AnnotateLayerbyName("Cuadri_dsim", "COD_REMATE", "Cuadriculas_Anotaciones");
                        //await LayerUtils.ChangeLayerNameAsync("Cuadri_dsim", "Cuadricula_sim");

                    }

                }

                //anota textos


            }
            catch
            {

            }
            
            

        }

        public async void generaplanosimultaneos()
        {

        }

    }
}
