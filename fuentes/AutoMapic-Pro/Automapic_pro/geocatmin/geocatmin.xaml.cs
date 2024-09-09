using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework;
//using ArcGIS.Desktop.Framework.Contracts;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Automapic_pro.modulos
{
    /// <summary>
    /// Lógica de interacción para geocatmin.xaml
    /// </summary>
    public partial class geocatmin : Page
    {
        private geocatmin_cartaIGN Geocatmin_cartaIGN;
        private geocatmin_base Geocatmin_base;
        private geocatmin_tematicos Geocatmin_tematicos;
        private geocatmin_imagenes Geocatmin_imagenes;
        private geocatmin_otras_fuentes Geocatmin_otrasfuentes;
        private geocatmin_BDGeocientifica Geocatmin_BDGeocientifica;
        private Frame currentframe = new Frame();
        private geocatmin_generarLayout generarLayout;
        private caratula viewCaratula;

        private enum modulosConsutlas
        {
            geocatmin_cartaIGN = 1,

        }
        public geocatmin()
        {
            InitializeComponent();
            viewCaratula = new caratula();
            viewCaratula.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            viewCaratula.ShowDialog();
            cbx_consultas.Items.Add("Carta - IGN");
            //cbx_consultas.Items.Add("Base");
            cbx_consultas.Items.Add("Temáticos");
            cbx_consultas.Items.Add("Imagenes");
            cbx_consultas.Items.Add("Otras fuentes");
            //cbx_consultas.Items.Add("BDGeocientifica");
            cbx_consultas.SelectedIndex = 0;
            lbl_usuario.Content = "Usuario: " + GlobalVariables.loginUser;
        }
        public geocatmin(Frame frame)
        {
            this.currentframe = frame;
            InitializeComponent();
            login foruser = new login();
            //"Usuario: " + foruser.tbx_user.Text.ToString();
        }
        private void cbx_consultas_Loaded(object sender, RoutedEventArgs e)
        {
            //cbx_consultas.Items.Clear();
            //cbx_consultas.Items.Add("--Seleccionar--");
            //cbx_consultas.Items.Add("Carta - IGN");
            //cbx_consultas.Items.Add("Base");
            //cbx_consultas.Items.Add("Temáticos");
            //cbx_consultas.Items.Add("Imagenes");
            //cbx_consultas.Items.Add("Otras fuentes");
            //cbx_consultas.Items.Add("BDGeocientifica");
            //cbx_consultas.SelectedIndex = 0;
            //lbl_usuario.Content = "Usuario: " + GlobalVariables.loginUser;
        }

        private void consultasFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        private void cbx_consultas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_consultas.SelectedItem.ToString() == "Carta - IGN")
            {
                if (Geocatmin_cartaIGN is null)
                {
                    Geocatmin_cartaIGN = new geocatmin_cartaIGN();
                }
                consultasFrame.Navigate(Geocatmin_cartaIGN);
            }
            else if (cbx_consultas.SelectedItem.ToString() == "Base")
            {
                if (Geocatmin_base is null)
                {
                    Geocatmin_base = new geocatmin_base();
                }
                consultasFrame.Navigate(Geocatmin_base);
            }
            else if (cbx_consultas.SelectedItem.ToString() == "Temáticos")
            {
                if (Geocatmin_tematicos is null)
                {
                    Geocatmin_tematicos = new geocatmin_tematicos();
                }
                consultasFrame.Navigate(Geocatmin_tematicos);
            }
            else if (cbx_consultas.SelectedItem.ToString() == "Imagenes")
            {
                if (Geocatmin_imagenes is null)
                {
                    Geocatmin_imagenes = new geocatmin_imagenes();
                }
                consultasFrame.Navigate(Geocatmin_imagenes);
            }
            else if (cbx_consultas.SelectedItem.ToString() == "Otras fuentes")
            {
                if (Geocatmin_otrasfuentes is null)
                {
                    Geocatmin_otrasfuentes = new geocatmin_otras_fuentes();
                }
                consultasFrame.Navigate(Geocatmin_otrasfuentes);
            }
            else if (cbx_consultas.SelectedItem.ToString() == "BDGeocientifica")
            {
                if (Geocatmin_BDGeocientifica is null)
                {
                    Geocatmin_BDGeocientifica = new geocatmin_BDGeocientifica();
                }
                consultasFrame.Navigate(Geocatmin_BDGeocientifica);
            }
        }

        private void btn_generarLayout_Click(object sender, RoutedEventArgs e)
        {
            if (generarLayout == null || !generarLayout.IsVisible)
            {
                generarLayout = new geocatmin_generarLayout();
                generarLayout.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                generarLayout.ShowDialog();
            }
            else
            {
                generarLayout.Activate();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var project = Project.Current;
            int mapCount = project.GetItems<MapProjectItem>().Count();
            int layoutCount = project.GetItems<LayoutProjectItem>().Count();

            if (mapCount > 0 && layoutCount > 0)
            {
                // Invoca el comando de exportación a PDF
                var pane = FrameworkApplication.DockPaneManager.Find("esri_layouts_exportDockPane");
                bool visible = pane.IsVisible;
                // Lo activamos
                pane.Activate();
            }
            else
            {
                MessageBox.Show("El proyecto no tiene mapas ni layouts ");
                //System.Diagnostics.Debug.WriteLine("El proyecto no tiene mapas ni layouts.");
            }

        }
    }
}
