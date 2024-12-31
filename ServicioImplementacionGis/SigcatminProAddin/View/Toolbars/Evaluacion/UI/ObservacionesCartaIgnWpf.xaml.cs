using CommonUtilities;
using DatabaseConnector;
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

namespace SigcatminProAddin.View.Toolbars.Evaluacion.UI
{
    /// <summary>
    /// Lógica de interacción para ObservacionesCartaIgnWpf.xaml
    /// </summary>
    public partial class ObservacionesCartaIgnWpf : Window
    {
        public ObservacionesCartaIgnWpf()
        {
            InitializeComponent();
            dataBaseHandler = new DatabaseHandler();
        }
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        // Se asume que dataChecked1..dataChecked20 provienen de algún UI o propiedades de clase
        // Variables que antes eran dataChecked1..dataChecked20:
        public string dataChecked1 { get; set; }
        public string dataChecked2 { get; set; }
        public string dataChecked3 { get; set; }
        public string dataChecked4 { get; set; }
        public string dataChecked5 { get; set; }
        public string dataChecked6 { get; set; }
        public string dataChecked7 { get; set; }
        public string dataChecked8 { get; set; }
        public string dataChecked9 { get; set; }
        public string dataChecked10 { get; set; }
        public string dataChecked11 { get; set; }
        public string dataChecked12 { get; set; }
        public string dataChecked13 { get; set; }
        public string dataChecked14 { get; set; }
        public string dataChecked15 { get; set; }
        public string dataChecked16 { get; set; }
        public string dataChecked17 { get; set; }
        public string dataChecked18 { get; set; }
        public string dataChecked19 { get; set; }
        public string dataChecked20 { get; set; }
        public string v_codigo { get; set; }
        public string gstrCodigo_Usuario { get; set; }
        public string v_observa_carta { get; set; }

        // Colecciones que se usaban en el VB6 / ArcObjects
        private List<string> colecciones_obs = new List<string>();
        private List<string> colecciones_obs_update = new List<string>();
        private List<string> colecciones_obs_upd_borrar = new List<string>();

        // Valores de coordenadas que usabas en tu layout
        private double posi_y_m = 18.0;
        private double posi_y1_m = 17.4;

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //ObservacionesCartaIgnWpf observacionesCartaIgnWpf = this;
            await GeneraObservacionesCartaAsync(this);
        }

        private void checked11_Checked(object sender, RoutedEventArgs e)
        {
            txtRio.Visibility = Visibility.Visible;
        }

        private void checked11_Unchecked(object sender, RoutedEventArgs e)
        {
            txtRio.Visibility = Visibility.Collapsed;
        }

        private void checked13_Checked(object sender, RoutedEventArgs e)
        {
            txtLaguna.Visibility = Visibility.Visible;
        }

        private void checked13_Unchecked(object sender, RoutedEventArgs e)
        {
            txtLaguna.Visibility = Visibility.Collapsed;
        }

        private void checked20_Checked(object sender, RoutedEventArgs e)
        {
            txtAreaUrbana.Visibility = Visibility.Visible;
        }

        private void checked20_Unchecked(object sender, RoutedEventArgs e)
        {
            txtAreaUrbana.Visibility = Visibility.Collapsed;
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

        public void CargarVariablesDesdeRadioButtons(Window container)
        {
            // Obtenemos todos los RadioButton en "container"
            var controls = FindVisualChildren<Control>(container).Where(c => c is RadioButton || c is CheckBox);

            // Recorremos cada uno, y si está "Checked", asignamos su Content a la variable correspondiente
            // según su Name o GroupName
            foreach (var control in controls)
            {
                bool isChecked = false;
                string content = "";
                // Determinar si el control está "Checked" y su contenido
                if (control is RadioButton rb)
                {
                    isChecked = rb.IsChecked == true;
                    content = rb.Content?.ToString() ?? "";
                }
                else if (control is CheckBox cb)
                {
                    isChecked = cb.IsChecked == true;
                    content = cb.Content?.ToString() ?? "";
                }

                if (isChecked)
                {
                    // Asignar el contenido
                    string controlName = control.Name; // ej. "RB_1_opcionA"
                    
                    // Extraer el número (1..20) del nombre
                    
                    int numero = -1;
                    var partes = controlName.Replace("checked", "");
                    if (partes.Length > 1)
                    {
                        int.TryParse(partes, out numero);
                    }

                    // según 'numero', asignamos la variable v_checkboxX
                    switch (numero)
                    {
                        case 1: dataChecked1 = content; break;
                        case 2: dataChecked2 = content; break;
                        case 3: dataChecked3 = content; break;
                        case 4: dataChecked4 = content; break;
                        case 5: dataChecked5 = content; break;
                        case 6: dataChecked6 = content; break;
                        case 7: dataChecked7 = content; break;
                        case 8: dataChecked8 = content; break;
                        case 9: dataChecked9 = content; break;
                        case 10: dataChecked10 = content; break;
                        case 11: dataChecked11 = content; break;
                        case 12: dataChecked12 = content; break;
                        case 13: dataChecked13 = content; break;
                        case 14: dataChecked14 = content; break;
                        case 15: dataChecked15 = content; break;
                        case 16: dataChecked16 = content; break;
                        case 17: dataChecked17 = content; break;
                        case 18: dataChecked18 = content; break;
                        case 19: dataChecked19 = content; break;
                        case 20: dataChecked20 = content; break;
                    }
                }
            }
        }

        /// <summary>
        /// Helper para buscar recursivamente todos los elementos de tipo T dentro de un contenedor WPF.
        /// </summary>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T t)
                    yield return t;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        public async Task GeneraObservacionesCartaAsync(Window containerConRadioButtons)
        {
            try
            {
                // 0) Cargar variables (v_checkbox1..v_checkbox20) desde los RadioButtons
                //    Solo los que estén Checked.
                CargarVariablesDesdeRadioButtons(containerConRadioButtons);

                // 1) LÓGICA DE NEGOCIO: crear la lista de observaciones 
                //    (igual que antes, pero usando las variables ya asignadas).
                //List<string> colecciones_obs = new List<string>();

                AgregarObservacionSiCorresponde(dataChecked1, "GT", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked2, "GP", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked3, "MT", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked4, "MP", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked5, "CS", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked6, "CF", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked7, "BT", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked8, "BP", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked9, "RT", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked10, "RP", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked11, "RH", colecciones_obs); // etc.
                AgregarObservacionSiCorresponde(dataChecked12, "CL", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked13, "LG", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked14, "RS", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked15, "FE", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked16, "TE", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked17, "RQ", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked18, "TL", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked19, "FR", colecciones_obs);
                AgregarObservacionSiCorresponde(dataChecked20, "AU", colecciones_obs);

                // 2) LÓGICA DE BDD (con cls_Oracle) - Insert, Update, Delete
                // --------------------------------------------------------------------
                // Aqui reproduces tu lógica:
                // - Consultar el estado con FT_Estado_Observacion(...)
                // - Dependiendo del caso, "INSERTA" o "UPDATE"
                // - Confirmar con MessageBox (en WPF quizás uses un Dialog) etc.
                string codeDM = GlobalVariables.CurrentCodeDm;
                string lostrEstado_Observacion = dataBaseHandler.CheckObservationStatus(codeDM);
                string lostrEstado = "";
                if (lostrEstado_Observacion == "0")
                {
                    // Insertar
                    lostrEstado = "INSERTA";
                    // *Si vas a mostrar un Dialog, en ArcGIS Pro usa `MessageBox.Show` de System.Windows
                    // O un diálogo propio con ArcGIS.Desktop.Framework.Dialogs.MessageBox
                    // Ejemplo:
                    var result = ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                        "¿Desea Grabar la Información?",
                        "BDGeocatmin",
                        System.Windows.MessageBoxButton.YesNo);

                    if (result == System.Windows.MessageBoxResult.No)
                        return;
                }
                else if (lostrEstado_Observacion == "1")
                {
                    // Update
                    lostrEstado = "UPDATE";
                    var result = ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(
                        "¿Información Existe, desea Actualizar?",
                        "BDGeocatmin",
                        System.Windows.MessageBoxButton.YesNo);

                    if (result == System.Windows.MessageBoxResult.No)
                        return;
                }
                // Primero, borramos indicadores no seleccionados:
                if (colecciones_obs_upd_borrar.Count > 0)
                {
                    foreach (var obsBorrar in colecciones_obs_upd_borrar)
                    {
                        dataBaseHandler.ManageObservationCartaDM(codeDM, codeDM, obsBorrar,
                            "OB", "", AppConfig.userName, "DELETE");
                    }
                }

                // Insertar/Actualizar las observaciones que quedan en colecciones_obs
                // (La lógica original anidaba muchos IF; aquí podemos mapear la descripción
                //  si es necesario, p. ej. con un diccionario.)
                foreach (string codObs in colecciones_obs)
                {
                    // Asignamos la descripción en base al checkbox (si aplica)
                    string descripcion = GetDescripcionAdicional(codObs);

                    // Llamada final a tu método de persistencia
                    // (Ej. "INSERTA" o "UPDATE")
                    string retorno = dataBaseHandler.ManageObservationCartaDM(v_codigo, v_codigo,
                        codObs, AppConfig.userName, descripcion, AppConfig.userName, lostrEstado);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    "Error al generar observaciones: " + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarObservacionSiCorresponde(string valorCheckbox, string codigoObservacion,
                                                    List<string> lista)
        {
            if (!string.IsNullOrEmpty(valorCheckbox))
            {
                lista.Add(codigoObservacion);
            }
        }

        /// <summary>
        /// Retorna la descripción adicional para la observación (por ejemplo "Rio de {v_checkbox11}", etc.).
        /// </summary>
        private string GetDescripcionAdicional(string codObs)
        {
            // Según tu código, hay algunos casos especiales:
            switch (codObs)
            {
                case "GT":
                    return dataChecked1;
                case "GP":
                    return dataChecked2;
                case "MT":
                    return dataChecked3;
                case "MP":
                    return dataChecked4;
                case "CS":
                    return dataChecked5;
                case "CF":
                    return dataChecked6;
                case "BT":
                    return dataChecked7;
                case "BP":
                    return dataChecked8;
                case "RT":
                    return dataChecked9;
                case "RP":
                    return dataChecked10;
                case "RH":
                    return dataChecked11 + " " + txtRio.Text;
                case "CL":
                    return dataChecked12;
                case "LG":
                    return dataChecked13 + " " + txtLago.Text;
                case "RS":
                    return dataChecked14;
                case "FE":
                    return dataChecked15;
                case "TE":
                    return dataChecked16;
                case "RQ":
                    return dataChecked17;
                case "TL":
                    return dataChecked18;
                case "FR":
                    return dataChecked19;
                case "AU":
                    return dataChecked20 + " " + txtAreaUrbana.Text;
                // ... etc. 
                default:
                    return string.Empty;
            }
        }
    }
}
