using CommonUtilities.LoginUtil;
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
using SigcatminProAddin.View.Constants;
using SigcatminProAddin.View.Contenedor;
using static DatabaseConnector.DatabaseConnection;
using DatabaseConnector;

namespace SigcatminProAddin.View.Login
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        private MainContainer _mainContainer;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUser.Text;
            string password = pwdPassword.Password;
            // Verificar si el usuario ha ingresado datos válidos
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                //MessageBox.Show("Por favor ingrese el usuario y la contraseña.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                string message = "Por favor ingrese el usuario y la contraseña.";
                showTemporaryMessage(message,Colors.Red);
                return;
            }
            else if (string.IsNullOrEmpty(username))
            {
                //MessageBox.Show("Por favor ingrese el usuario.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                string message = "Por favor ingrese el usuario.";
                showTemporaryMessage(message, Colors.Red);
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                //MessageBox.Show("Por favor ingrese la contraseña.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                string message = "Por favor ingrese la constraseña.";
                showTemporaryMessage(message, Colors.Red);
                return;
            }
            dbconn = new DatabaseConnection(username, password);
            string connectionString = dbconn.OracleConnectionString(); // O dbConnection.GetGisConnectionString()
            // Verifica la conexcion
            if (dbconn.TestConnection(connectionString))
            {
                showTemporaryMessage("Conexión exitosa", Colors.Blue);
                // Después del inicio de sesión exitoso
                AppConfig.userName = username;
                AppConfig.password = password;
                //MessageBox.Show("Conexión exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Procede con la lógica de la
                dataBaseHandler = new DatabaseHandler();
                string encryptedCredentials = CredentialManager.EncryptCredentials(tbxUser.Text, pwdPassword.Password, 5);
                SessionManager.SaveSession(encryptedCredentials);
                var result = dataBaseHandler.VerifyUser(username, password);
                var result2 = dataBaseHandler.GetUserRole("2", "ROL_CONSULTA_CM");
                _mainContainer = new MainContainer();
                _mainContainer.Show();
                StatesUtil.ActivateState(UIState.IsLogged);
                await Task.Delay(1500);
                this.Close();
            }
            else
            {
                //MessageBox.Show("Error en la conexión", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Maneja el fallo de autenticación
                string message = "Error en la conexión.";
                showTemporaryMessage(message, Colors.Red);
            }
            
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
            btnViewPassword.Background = new SolidColorBrush(Colors.White);
            ImgViewpassword.Source = new BitmapImage(new Uri("/SigcatminProAddin;component/Images/Login/visible16_blue.png", UriKind.Relative));
            pwdPassword.Focus();
        }

        private void btnViewPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            tbxPasswordView.Visibility = Visibility.Collapsed;
            pwdPassword.Visibility = Visibility.Visible;
            btnViewPassword.Background = new SolidColorBrush(Colors.White);
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
