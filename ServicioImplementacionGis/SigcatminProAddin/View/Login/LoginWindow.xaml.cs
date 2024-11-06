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
using System.Windows.Shapes;

namespace SigcatminProAddin.View.Login
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void tbxUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            waterMarkUser.Visibility = string.IsNullOrEmpty(tbxUser.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void tbxPasswordView_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pwdPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled)
            {
                mayusculas.IsOpen = true;
            }
            else
            {
                mayusculas.IsOpen = false;
            }
            if (e.Key == Key.Enter)
            {
                btnLogin.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void pwdPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            waterMarkPass.Visibility = string.IsNullOrEmpty(pwdPassword.Password) ? Visibility.Visible : Visibility.Collapsed;
            if (pwdPassword.Password.Length > 0)
            {
                tbxPasswordView.Text = pwdPassword.Password;
            }
        }

        private void btnViewPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbxPasswordView.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Collapsed;
            System.Windows.Media.Color customColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#006DA0");
            btnViewPassword.Background = new SolidColorBrush(customColor);
            ImgViewpassword.Source = new BitmapImage(new Uri("Images/Login/visible16_white.png", UriKind.Relative));
        }

        private void btnViewPassword_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tbxPasswordView.Visibility = Visibility.Collapsed;
            pwdPassword.Visibility = Visibility.Visible;
            btnViewPassword.Background = new SolidColorBrush(Colors.White);
            ImgViewpassword.Source = new BitmapImage(new Uri("Images/Login/visible16_blue.png", UriKind.Relative));
        }

        private void btnViewPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            tbxPasswordView.Visibility = Visibility.Collapsed;
            pwdPassword.Visibility = Visibility.Visible;
            btnViewPassword.Background = new SolidColorBrush(Colors.White);
            ImgViewpassword.Source = new BitmapImage(new Uri("Images/Login/visible16_blue.png", UriKind.Relative));
        }

        private void btnViewPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Color customColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#a2defa");
            btnViewPassword.Background = new SolidColorBrush(customColor);
            btnViewPassword.Background = new SolidColorBrush(customColor);
        }
    }
}
