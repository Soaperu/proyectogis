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
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;

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
                //procesamos archivo bat
            }

            string v_tipo = "1";

            //Codigo de Remate

            DataTable grupoccodigoRemate = databaseHandler.ObtenerGruposSimultaneidad(v_tipo, fechaSimultaneidadAsString);
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

            DataGridSimultaneidad.ItemsSource = grupoccodigoRemate;
        }
    }
}
