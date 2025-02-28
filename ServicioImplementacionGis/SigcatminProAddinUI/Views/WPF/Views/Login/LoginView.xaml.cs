using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SigcatminProAddinUI.Views.WPF.ViewModel;

namespace SigcatminProAddinUI.Views.WPF.Views.Login
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           
           
        }

        private void tbxUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            waterMarkUser.Visibility = string.IsNullOrEmpty(tbxUser.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                var error = viewModel[nameof(viewModel.Username)];
                ErrorUserNameBlock.Text = error;
            
            }
        }
        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                var error = viewModel[nameof(viewModel.Password)];
                ErrorPasswordBlock.Text = error;
                var binding = pwdPassword.GetBindingExpression(PasswordBox.TagProperty);
                binding?.UpdateSource();
            }
        }

        private void pwdPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            waterMarkPass.Visibility = string.IsNullOrEmpty(pwdPassword.Password) ? Visibility.Visible : Visibility.Collapsed;

            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = pwdPassword.Password;
                var binding = pwdPassword.GetBindingExpression(PasswordBox.TagProperty);
                binding?.UpdateSource();
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

        //private void pwdPassword_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    ttipMayusculas.IsOpen = (Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Down) == KeyStates.Down;
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Al abrir el login podra escribir directamente el usuario
            tbxUser.Focus();
        }

        //private void tbxUser_KeyDown(object sender, KeyEventArgs e)
        //{
        //    // Si se presiona TAB, mueve el enfoque al PasswordBox
        //    if (e.Key == Key.Tab)
        //    {
        //        e.Handled = true; // Evita el comportamiento predeterminado de TAB
        //        pwdPassword.Focus();
        //    }
        //}
    }
}
