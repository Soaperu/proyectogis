using SigcatminProAddin.View.Modulos;
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

namespace SigcatminProAddin.View.Contenedor
{
    /// <summary>
    /// Lógica de interacción para MainContainer.xaml
    /// </summary>
    public partial class MainContainer : Window
    {
        EvaluacionDM _EvaluacionDM;
        public MainContainer()
        {
            InitializeComponent();
            _EvaluacionDM = new EvaluacionDM();
            frameContainer.Navigate(_EvaluacionDM);
        }

        private void frameContainer_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        private void gridHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Comprueba que el botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }
    }
}
