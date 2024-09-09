using Automapic_pro.modulos;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Automapic_pro.firma_espectral;
using Automapic_pro.mapa_geologico_50k;
using Automapic_pro.bdgeocientifica;
using Automapic_pro.datos_campo_drme;

namespace Automapic_pro
{
    /// <summary>
    /// Lógica de interacción para select_modulos.xaml
    /// </summary>
    public partial class select_modulos : Page
    {
        private geocatmin Geocatmin;
        private busquedas firmasEspectrales;
        private mp_geologico_50k MpGeologico50k;
        private bd_geocientifica bdgeocientifica;
        private datos_campos_drme datosCampoDrme;
        private Frame currentframe = new Frame();
        public select_modulos()
        {
            InitializeComponent();
        }
        public select_modulos(Frame frame)
        {
            this.currentframe = frame;
            InitializeComponent();
            // Items agregados al comboBox de Modulos, esto cambiara por una tabla de en la base de datos
            cbx_selectModulos.Items.Add("-- Seleccionar Modulo --");
            cbx_selectModulos.Items.Add("Geocatmin Desktop - Consultas de mapas");
            cbx_selectModulos.Items.Add("Firmas Espectrales");
            cbx_selectModulos.Items.Add("Mapa geologico 50000");
            cbx_selectModulos.Items.Add("BD Geocientifica");
            cbx_selectModulos.Items.Add("Datos de Campo DRME");
            cbx_selectModulos.SelectedIndex = 0;
        }
        public select_modulos(mp_geologico_50k mpGeologico50k)
        {
            InitializeComponent();
            this.MpGeologico50k = mpGeologico50k;
            this.MpGeologico50k.change_state_progress += change_state_progressBar;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_selectModulos.SelectedItem.ToString() == "-- Seleccionar Modulo --")
            {
                return;
            }
            if (cbx_selectModulos.SelectedItem.ToString() == "Geocatmin Desktop - Consultas de mapas")
            {
                if (Geocatmin is null)
                { 
                 Geocatmin = new geocatmin();
                }
                secondFrame.Navigate(Geocatmin);
            }
            else if (cbx_selectModulos.SelectedItem.ToString() == "Firmas Espectrales")
            {
                if (firmasEspectrales is null)
                {
                    firmasEspectrales = new busquedas();
                }
                secondFrame.Navigate(firmasEspectrales);
            }
            else if (cbx_selectModulos.SelectedItem.ToString() == "Mapa geologico 50000")
            {
                if (MpGeologico50k is null)
                {
                    MpGeologico50k = new mp_geologico_50k();
                }
                secondFrame.Navigate(MpGeologico50k);
            }
            else if (cbx_selectModulos.SelectedItem.ToString() == "BD Geocientifica")
            {
                if (bdgeocientifica is null)
                {
                    bdgeocientifica = new bd_geocientifica();
                }
                secondFrame.Navigate(bdgeocientifica);
            }
            else if (cbx_selectModulos.SelectedItem.ToString() == "Datos de Campo DRME")
            {
                if (datosCampoDrme is null)
                {
                    datosCampoDrme = new datos_campos_drme();
                }
                secondFrame.Navigate(datosCampoDrme);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea cerrar sesión en Automapic Pro?", "Confirmacion" , MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                login loginKey = new login(this.currentframe);
                this.currentframe.Navigate(loginKey);
            }
            else { return; }
            
        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            //cbx_selectModulos.Items.Clear();
            //cbx_selectModulos.Items.Add("-- Seleccionar Modulo --");
            //cbx_selectModulos.Items.Add("Geocatmin Desktop - Consultas de mapas");
            //cbx_selectModulos.Items.Add("Firmas Espectrales");
            //cbx_selectModulos.SelectedIndex = 0;
        }

        private void secondFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        public void change_state_progressBar(bool indeterminado)
        {
            Dispatcher.Invoke(() =>
            {
                progressBarMain.IsIndeterminate = indeterminado;
            });
        }
        protected void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.MpGeologico50k.change_state_progress -= change_state_progressBar;
            //base.OnUnloaded(sender, e);
        }

    }
}
