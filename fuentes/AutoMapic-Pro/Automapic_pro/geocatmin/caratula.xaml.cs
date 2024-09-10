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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Automapic_pro.modulos
{
    /// <summary>
    /// Lógica de interacción para caratula.xaml
    /// </summary>
    public partial class caratula : Window
    {
        public caratula()
        {
            InitializeComponent();
        }

        private void RichTBX_MouseEnter(object sender, MouseEventArgs e)
        {
            Color customColor = (Color)ColorConverter.ConvertFromString("#006DA0");
            SolidColorBrush customBrush = new SolidColorBrush(customColor);
            rtbx_warning.Background = customBrush;
        }

        private void btn_caratulaOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.To = 1.0;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1)); // 1 segundos duracion 
            this.BeginAnimation(Window.OpacityProperty, animation);
        }
    }
}
