using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ServiceModel;
using ServiceActiveDirectory;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using ColorConverter = System.Windows.Media.ColorConverter;


namespace Automapic_pro
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    /// 
    public partial class login : Page
    {
        private Frame currentframe= new Frame();
        public login()
        {
            InitializeComponent();
            this.Loaded += Grid_Loaded;
        }

        public login(Frame frame)
        {
            this.currentframe = frame;
            InitializeComponent();
        }

        private string _url_SWLoginService = "https://srvstd.ingemmet.gob.pe/WS_Seguridad/SWLogin.svc";
        public string userlogin { get=> tbx_user.Text; } 
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            img_gif.Visibility = Visibility.Visible;
            Dictionary<string, object> responseJson = await Iniciar_sesion();
            select_modulos modulos = new select_modulos(this.currentframe);
            //this.currentframe.Navigate(modulos);
            if (settings.modeDev == "PROD")
            {
                if ((string)responseJson["status"] == "True")
                {
                    txbck_login.Text = "Bienvenido";
                    await Task.Delay(2500);
                    this.currentframe.Navigate(modulos);
                }
                else { MessageBox.Show("Usuario o contraseña incorrecta"); }
                GlobalVariables.loginUser = tbx_user.Text;
            }
            else this.currentframe.Navigate(modulos);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            waterMark_pass.Visibility = string.IsNullOrEmpty(passwordSource.Password) ? Visibility.Visible : Visibility.Collapsed;
            if(passwordSource.Password.Length > 0)
            {
                tbx_passwordView.Text = passwordSource.Password;
            }
        }

        private void tbx_user_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            waterMark_user.Visibility = string.IsNullOrEmpty(tbx_user.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            tbx_user.Focus();
            img_gif.Visibility= Visibility.Collapsed;
        }

        private void passwordSource_KeyDown(object sender, KeyEventArgs e)
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
                btn_login.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private async System.Threading.Tasks.Task<Dictionary<string, object>> Iniciar_sesion()
        {
            txbck_login.Text = "Validando Usuario";
            txbck_login.Visibility = Visibility.Visible;
            
            //Validacion de usuario por servicio web
            Dictionary<string, object> responseJson = new Dictionary<string, object>();
            BasicHttpsBinding binding = new BasicHttpsBinding();
            EndpointAddress endpoint = new EndpointAddress(_url_SWLoginService);
            SWLoginClient client = new SWLoginClient(binding, endpoint);
            string user = tbx_user.Text;
            string password = passwordSource.Password;
            
            RespuestaSeguridadU response = await client.UsuarioLoginADAsync(user, password);

            responseJson.Add("status", response.Usuario_Valido.ToString());
            client.Close();

            string _message= "¡Credenciales incorrectas!";
            if (!response.Auditoria.AUTORIZADO)
            {
                responseJson.Add("message", _message);
                img_gif.Visibility = Visibility.Collapsed;
                txbck_login.Visibility = Visibility.Collapsed;
                return responseJson;
            }
            if (!response.Usuario_Valido)
            {
                responseJson.Add("message", _message);
                img_gif.Visibility = Visibility.Collapsed;
                txbck_login.Visibility = Visibility.Collapsed;
                return responseJson;
            }
            return responseJson;
        }

        private void Btn_viewPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbx_passwordView.Visibility = Visibility.Visible;
            passwordSource.Visibility = Visibility.Collapsed;
            System.Windows.Media.Color customColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#006DA0");
            btn_viewPassword.Background = new SolidColorBrush(customColor);
            img_viewpassword.Source = new BitmapImage(new Uri("Images/visible16_white.png", UriKind.Relative));
            //tbx_passwordView.Focus();
        }
        private void Btn_viewPassword_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tbx_passwordView.Visibility = Visibility.Collapsed;
            passwordSource.Visibility = Visibility.Visible;
            btn_viewPassword.Background = new SolidColorBrush(Colors.White);
            img_viewpassword.Source = new BitmapImage(new Uri("Images/visible16_blue.png", UriKind.Relative));
            //passwordSource.Focus();
        }
        private void btn_viewPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            tbx_passwordView.Visibility = Visibility.Collapsed;
            passwordSource.Visibility = Visibility.Visible;
            btn_viewPassword.Background = new SolidColorBrush(Colors.White);
            img_viewpassword.Source = new BitmapImage(new Uri("Images/visible16_blue.png", UriKind.Relative));
        }
        private void btn_viewPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Color customColor = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#a2defa");
            btn_viewPassword.Background = new SolidColorBrush(customColor);
            btn_viewPassword.Background = new SolidColorBrush(customColor);
        }
        private void passwordView_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (tbx_passwordView.Text.Length > 0)
            //{
            //    passwordSource.Password = tbx_passwordView.Text;
            //}
            //tbx_passwordView.SelectionStart = passwordSource.Password.Length;
        }
    }
}
