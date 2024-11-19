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
        //EvaluacionDM _EvaluacionDM;
        private MainViewModel _mainViewModel;
        private Dictionary<string, Page> _loadedModules;
        private static MainContainer _instance;
        public MainContainer()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(frameContainer);
            _loadedModules = new Dictionary<string, Page>();
            //_EvaluacionDM = new EvaluacionDM();
            //frameContainer.Navigate(_EvaluacionDM);
        }
        public static MainContainer Instance
        {
            get
            {
                if (_instance == null || !_instance.IsVisible)
                {
                    _instance = new MainContainer();
                }
                return _instance;
            }
        }

        private void frameContainer_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        // Método para cargar módulos en el frameContainer
        public void LoadModule(string moduleName, Page modulePage)
        {
            if (!_loadedModules.ContainsKey(moduleName))
            {
                // Si el módulo no está cargado, agregarlo al diccionario
                _loadedModules[moduleName] = modulePage;
            }

            // Navegar al módulo en el FrameContainer
            frameContainer.Navigate(_loadedModules[moduleName]);
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

        private void btnCloseContainer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
