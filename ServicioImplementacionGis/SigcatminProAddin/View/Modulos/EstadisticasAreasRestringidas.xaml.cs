using CommonUtilities;
using DatabaseConnector;
using DevExpress.Office.NumberConverters;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para EstadisticasAreasRestringidas.xaml
    /// </summary>
    public partial class EstadisticasAreasRestringidas : Page
    {
        public DatabaseHandler dataBaseHandler;
        public EstadisticasAreasRestringidas()
        {
            InitializeComponent();
            CurrentUser();
            dataBaseHandler = new DatabaseHandler();
        }
        public class ComboBoxPairs
        {
            public string _Key { get; set; }
            public int _Value { get; set; }

            public ComboBoxPairs(string _key, int _value)
            {
                _Key = _key;
                _Value = _value;
            }

        }
        public class ComboBoxPairsString
        {
            public string _Key { get; set; }
            public string _Value { get; set; }

            public ComboBoxPairsString(string _key, string _value)
            {
                _Key = _key;
                _Value = _value;
            }

        }

        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
        }
        private void CbxTypeConsult_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("--Seleccionar--", 0));
            cbp.Add(new ComboBoxPairs("MINERIA A NIVEL NACIONAL", 1));
            cbp.Add(new ComboBoxPairs("PERU CONTINENTAL", 2));

            // Asignar la lista al ComboBox
            CbxTypeConsult.DisplayMemberPath = "_Key";
            CbxTypeConsult.SelectedValuePath = "_Value";
            CbxTypeConsult.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxTypeConsult.SelectedIndex = 0;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void CbxDatum_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("WGS-84", 2));
            cbp.Add(new ComboBoxPairs("PSAD-56", 1));

            // Asignar la lista al ComboBox
            CbxDatum.DisplayMemberPath = "_Key";
            CbxDatum.SelectedValuePath = "_Value";
            CbxDatum.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxDatum.SelectedIndex = 0;
            GlobalVariables.CurrentDatumDm = "2";
        }

        private void CbxDatum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbxDatum.SelectedValue?.ToString() == "2")
            {
                GlobalVariables.CurrentDatumDm = "2";
            }
            else
            {
                GlobalVariables.CurrentDatumDm = "1";
            }
        }

        private void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reseteamos los controles de detalle
            //cbodetalle.IsEnabled = false;
            BtnMineNo.IsEnabled = false;

            // Verificamos si hay un ítem seleccionado
            if (CbxTypeConsult.SelectedIndex < 0)
            {
                MessageBox.Show("No ha seleccionado ningún tipo de consulta",
                                "OBSERVACIÓN",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }

            // Obtenemos el texto seleccionado en el ComboBox
            int? selectedValue = CbxTypeConsult.SelectedValue as int?;
            //if (string.IsNullOrEmpty(selectedValue)) return;

            // Dependiendo del tipo seleccionado, ejecutar la lógica correspondiente
            switch (selectedValue)
            {
                case 2:
                    // Guardamos el tipo en una variable de clase (si lo necesitas)
                    // tipo_selec_catnomin = "SEGUN DEPARTAMENTO";

                    // Habilitamos/deshabilitamos controles
                    //cbodetalle.IsEnabled = true;
                    //cboZona.IsEnabled = false;
                    //BtnLoadData.IsEnabled = true;

                    //lblZona.Visibility = Visibility.Hidden;
                    //cboZona.Visibility = Visibility.Hidden;

                    //// Limpiamos los ítems anteriores
                    //cbodetalle.Items.Clear();
                    //cboZona.Items.Clear();

                    // Ejemplo de la llamada a tu método de carga
                    // cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", false);
                    // pFeatureClass = pFeatureLayer_depa.FeatureClass;
                    // pFeatureCursor = pFeatureClass.Search(null, false);

                    // Lógica de lectura de features y llenado de cbodetalle
                    // try
                    // {
                    //     IFeature pFeature = pFeatureCursor.NextFeature();
                    //     while (pFeature != null)
                    //     {
                    //         string v_nm_depa = pFeature.get_Value(pFeatureCursor.FindField("NM_DEPA")).ToString();
                    //         if (!EsLagunaONombreEspecial(v_nm_depa))
                    //         {
                    //             cbodetalle.Items.Add(v_nm_depa);
                    //         }
                    //         pFeature = pFeatureCursor.NextFeature();
                    //     }
                    // }
                    // catch (Exception ex)
                    // {
                    //     // Manejo de errores
                    // }

                    // Asignamos un índice por defecto
                    //if (cbodetalle.Items.Count > 0)
                    //    cbodetalle.SelectedIndex = 0;

                    //// Cargar tipos de reservas (ejemplo)
                    //CargarTiposReserva();

                    //break;

                case 3:
                    // tipo_selec_catnomin = "TIPO DE RESERVA";
                    //cbodetalle.IsEnabled = true;
                    //cbodetalle.Items.Clear();

                    //lblZona.Visibility = Visibility.Hidden;
                    //cboZona.Visibility = Visibility.Hidden;
                    //cboZona.Items.Clear();

                    //BtnLoadData.IsEnabled = true;

                    //// Llamada a la base de datos o clase que devuelve el listado de reservas
                    //// Ejemplo:
                    //try
                    //{
                    //    DataTable lodbtExiste_tipo = dataBaseHandler.GetRestrictedAreaType()//cls_Oracle.F_Obtiene_Tipo_AreaRestringida();
                    //    if (lodbtExiste_tipo != null)
                    //    {
                    //        foreach (DataRow row in lodbtExiste_tipo.Rows)
                    //        {
                    //            string v_nm_depa = row["TN_DESTIP"].ToString();
                    //            cbodetalle.Items.Add(v_nm_depa);
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Manejo de excepción, log, etc.
                    //}

                    //if (cbodetalle.Items.Count > 0)
                    //    cbodetalle.SelectedIndex = 0;

                    //// Dependiendo de tu lógica, habilitar también cbotiporese, etc.
                    //CbxTypeMineNo.IsEnabled = true;
                    break;

                case 1: // "MINERIA A NIVEL NACIONAL":
                    // tipo_selec_catnomin = "MINERIA A NIVEL NACIONAL";

                    BtnMineNo.IsEnabled = true;
                    BtnLoadData.IsEnabled = true;

                    // Ajuste de visibilidad y ubicación de controles
                    //cboZona.Visibility = Visibility.Hidden;
                    //lblZona.Visibility = Visibility.Hidden;
                    //lblregion.Visibility = Visibility.Hidden;
                    //cbodetalle.Visibility = Visibility.Hidden;

                    // Lógica para cargar combos de reserva minera
                    CbxTypeMineNo.IsEnabled = true;
                    CbxTypeMineNo.Items.Clear();

                    // Si tienes un cbotiporesemin también
                    CbxTypeMineYes.Items.Clear();

                    try
                    {
                        DataTable lodbtExiste_tipo = dataBaseHandler.GetRestrictedAreaType(); //cls_Oracle.F_Obtiene_Tipo_AreaRestringida();
                        if (lodbtExiste_tipo != null)
                        {
                            foreach (DataRow row in lodbtExiste_tipo.Rows)
                            {
                                string v_nm_depa = row["TN_DESTIP"].ToString();
                                // Filtrado (excluir algunos tipos)
                                if (EsValidoParaMineria(v_nm_depa))
                                {
                                    // Agregar según la clasificación propia
                                    if (v_nm_depa == "AREA NATURAL")
                                    {
                                        CbxTypeMineNo.Items.Add("AREA NATURAL - USO INDIRECTO");
                                        CbxTypeMineYes.Items.Add("AREA NATURAL - USO DIRECTO");
                                        CbxTypeMineYes.Items.Add("AREA NATURAL - AMORTIGUAMIENTO");
                                        CbxTypeMineYes.Items.Add("CLASIFICACION DIVERSA");
                                        CbxTypeMineNo.Items.Add("CLASIFICACION DIVERSA");
                                        CbxTypeMineYes.Items.Add("AREA DE CONSERVACION PRIVADA");
                                        CbxTypeMineYes.Items.Add("AREA DE CONSERVACION MUNICIPAL Y OTROS");
                                    }
                                    else if (v_nm_depa == "PROYECTO ESPECIAL")
                                    {
                                        CbxTypeMineNo.Items.Add("PROYECTO ESPECIAL - HIDRAULICOS");
                                        CbxTypeMineYes.Items.Add("PROYECTO ESPECIAL (no hidráulicos)");
                                    }
                                    else if (v_nm_depa == "PROPUESTA DE AREA NATURAL")
                                    {
                                        CbxTypeMineYes.Items.Add("PROPUESTA DE AREA NATURAL");
                                    }
                                    else if (v_nm_depa == "POSIBLE ZONA URBANA")
                                    {
                                        CbxTypeMineYes.Items.Add("POSIBLE ZONA URBANA");
                                        CbxTypeMineYes.Items.Add("AREA DE EXPANSION URBANA");
                                    }
                                    else
                                    {
                                        CbxTypeMineNo.Items.Add(v_nm_depa);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                    }

                    if (CbxTypeMineNo.Items.Count > 0)
                        CbxTypeMineNo.SelectedIndex = 0;

                    if (CbxTypeMineYes.Items.Count > 0)
                        CbxTypeMineYes.SelectedIndex = 0;

                    break;
                case 0: break;
                default:
                    // No coincide con ninguno de los casos anteriores
                    MessageBox.Show("Tipo de consulta no manejado.",
                                    "OBSERVACIÓN",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                    break;
            }
        }

        private bool EsValidoParaMineria(string tipoReserva)
        {
            if (string.IsNullOrEmpty(tipoReserva)) return false;

            // Lista de exclusiones
            var exclusiones = new[]
            {
                "EXPEDIENTE DE CATASTRO",
                "OTRA AREA RESTRINGIDA",
                "RESERVA TURISTICA",
                "CONCESION DE LABOR GENERAL",
                "ANAD",
                "INFRAESTRUCTURA DEL ESTADO",
                "PRODUCTOR MINERO ARTESANAL",
                "CONCESION DE TRANSPORTE MINERO",
                "CONCESION DE LABOR GENERAL"
            };

            foreach (var ex in exclusiones)
            {
                if (tipoReserva.Equals(ex, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            return true;
        }

    }
}
