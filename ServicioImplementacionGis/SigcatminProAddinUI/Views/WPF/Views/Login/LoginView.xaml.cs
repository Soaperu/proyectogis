using Sigcatmin.pro.Application.Dtos;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.UsesCases;
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
using System.Windows.Shapes;

namespace SigcatminProAddinUI.Views.WPF.Views.Login
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly ILoggerService _loggerService;
        public LoginView()
        {
            InitializeComponent();
            _loginUseCase = Program.GetService<LoginUseCase>();
            _loggerService = Program.GetService<ILoggerService>();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
      
            try
            {
                string username = tbxUser.Text;
                string password = pwdPassword.Password;
            }
            catch (Exception ex)
            {
                _loggerService.LogError("",ex);
            }
            //var loginService = Program.GetService<LoginUseCase>();
            _loginUseCase.Execute(new LoginRequestDto() { UserName = "pava2778", Password = "ingemmet" });
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
                ttipMayusculas.IsOpen = true;
            }
            else
            {
                ttipMayusculas.IsOpen = false;
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
            ImgViewpassword.Source = new BitmapImage(new Uri("/SigcatminProAddin;component/Images/Login/visible16_white.png", UriKind.Relative));
        }

        private void btnViewPassword_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tbxPasswordView.Visibility = Visibility.Collapsed;
            pwdPassword.Visibility = Visibility.Visible;
            System.Windows.Media.Color customColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#F3F3F3");
            btnViewPassword.Background = new SolidColorBrush(customColor);
            ImgViewpassword.Source = new BitmapImage(new Uri("/SigcatminProAddin;component/Images/Login/visible16_blue.png", UriKind.Relative));
            pwdPassword.Focus();
        }

        private void btnViewPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            tbxPasswordView.Visibility = Visibility.Collapsed;
            pwdPassword.Visibility = Visibility.Visible;
            System.Windows.Media.Color customColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#F3F3F3");
            btnViewPassword.Background = new SolidColorBrush(customColor);
            ImgViewpassword.Source = new BitmapImage(new Uri("/SigcatminProAddin;component/Images/Login/visible16_blue.png", UriKind.Relative));
        }

        private void btnViewPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Color customColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#a2defa");
            btnViewPassword.Background = new SolidColorBrush(customColor);
            //btnViewPassword.Background = new SolidColorBrush(customColor);
        }

        private void gridHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // El botón izquierdo del ratón esté presionado
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Permite el arrastre de la ventana
                this.DragMove();
            }
        }

        private void pwdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            ttipMayusculas.IsOpen = (Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Down) == KeyStates.Down;
        }
        private void showTemporaryMessage(string message, Color color)
        {
            // Muestra mensajes de errores en el login
            lblLoginError.Content = message;
            lblLoginError.Foreground = new SolidColorBrush(color);
            lblLoginError.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Al abrir el login podra escribir directamente el usuario
            tbxUser.Focus();
        }

        private void tbxUser_KeyDown(object sender, KeyEventArgs e)
        {
            // Si se presiona TAB, mueve el enfoque al PasswordBox
            if (e.Key == Key.Tab)
            {
                e.Handled = true; // Evita el comportamiento predeterminado de TAB
                pwdPassword.Focus();
            }
        }
    }
}
