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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.DDL;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
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
            string v_sistema;
            string gstrFC_Catastro_Minero;
            string gstrFC_Cuadricula_Z;
            string gstrFC_Carta;

            List<string> mapsToDelete = new List<string>()
            {
                GlobalVariables.mapNameCatastro,

            };

            await MapUtils.DeleteSpecifiedMapsAsync(mapsToDelete);
            await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro);

            DateTime fechaSimultaneidad = DatePickerInicio.SelectedDate.Value;
            string fechaSimultaneidadAsString = fechaSimultaneidad.ToString("dd/MM/yyyy");

            if (fechaSimultaneidad < fechaDatum)
            {
                v_sistema = "PSAD-56";
                gstrFC_Catastro_Minero = "GPO_CMI_CATASTRO_MINERO_";
                gstrFC_Cuadricula_Z = "GPO_CUA_CUADRICULAS_";
                gstrFC_Carta = "GPO_HOJ_HOJAS";
            }
            else
            {
                v_sistema = "WGS-84";
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
                            new FieldDescription[]
                            {
                                new FieldDescription("CODIGO", FieldType.String) { Length = 13 },
                                new FieldDescription("NOMBRE", FieldType.String) { Length = 50 },
                                new FieldDescription("GRUPO", FieldType.String) { Length = 4 },
                                new FieldDescription("COD_REMATE", FieldType.String) { Length = 20 },
                                new FieldDescription("ZONA", FieldType.String) { Length = 2 },
                                new FieldDescription("FEC_SIM", FieldType.Date),
                                new FieldDescription("SUBGRUPO", FieldType.String) { Length = 5 }
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
    }
}
