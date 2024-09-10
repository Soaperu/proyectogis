using ArcGIS.Desktop.Core;
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



namespace Automapic_pro
{
    /// <summary>
    /// Interaction logic for dockpane_mainView.xaml
    /// </summary>
    public partial class dockpane_mainView : UserControl
    {
        public dockpane_mainView()
        {
            InitializeComponent();
            //Frame frame = new Frame();
            login loginkey = new login(mainFrame);
            //UserControl_login login = new UserControl_login(mainFrame);
            //Window_login login = new Window_login();
            //frame.Content = login;
            mainFrame.Navigate(loginkey);
            //DataContext = ;
        }

        public void ClearView()
        {
            contentControl_main.Content = null;
        }

        private void mainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }
    }


}
