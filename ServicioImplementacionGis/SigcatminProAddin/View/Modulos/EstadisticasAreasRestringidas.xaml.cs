using ArcGIS.Core.Data;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.Office.NumberConverters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
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


namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para EstadisticasAreasRestringidas.xaml
    /// </summary>
    public partial class EstadisticasAreasRestringidas : Page
    {
        public DatabaseHandler dataBaseHandler;
        private string _typeConsult = "";
        private string globalPath = GlobalVariables.pathFileTemp;
        private string vZona;
        private string vDatum;
        private string vCodigo;
        DataTable lodbtExiste_SupAR = new DataTable();
        double areasup_rese_local = 0.0; // v_areasup_rese
        double areaini_depa_local = 0.0; // v_areaini_depa
        double cantidad_local = 0.0; // v_cantidad
        string nombre_depa = "";   // vCodigo_depa
        Geodatabase geodatabase;
        SdeConnectionGIS sdeHelper = new SdeConnectionGIS();

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

        private async Task InitConectionGdb()
        {
            geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                            , AppConfig.userName
                                                                            , AppConfig.password);

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
                vDatum = "2";
            }
            else
            {
                GlobalVariables.CurrentDatumDm = "1";
                vDatum = "1";
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
                case 0: break;

                case 1: // "MINERIA A NIVEL NACIONAL":
                    _typeConsult = "MINERIA A NIVEL NACIONAL";

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

                case 2:
                    // Guardamos el tipo en una variable de clase (si lo necesitas)
                    _typeConsult = "SEGUN DEPARTAMENTO";

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

                    break;

                case 3:
                    _typeConsult = "TIPO DE RESERVA";
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

        private async void BtnMineNo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Por buenas prácticas, la mayor parte de la lógica GIS 
                // se ejecuta dentro de un QueuedTask:
                await QueuedTask.Run(async () =>
                {
                    await CalcularAreasAsync();
                });

                // Mensaje final
                MessageBox.Show("El proceso ha finalizado satisfactoriamente.",
                                "Estadísticas de Áreas Restringidas",
                                System.Windows.MessageBoxButton.OK,
                                System.Windows.MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task CalcularAreasAsync()
        {
            // Ejemplo: en tu VB original, se inicializaban pMap, pFeature, etc.
            // En ArcGIS Pro no se usa pMap/pMxDoc de ArcObjects, se usa Mapping APIs.
            // Supongamos que tienes un FeatureClass a partir de una capa:

            // var myLayer = MapView.Active.Map.FindLayers("NombreDeCapa").FirstOrDefault() as FeatureLayer;
            // if (myLayer == null) return;

            // Obtenemos el FeatureClass
            // FeatureClass featureClass = await QueuedTask.Run(() => myLayer.GetFeatureClass());

            // Prepara DataTable para resultados:
            DataTable lodtTabla = new DataTable();
            lodtTabla.Columns.Add("SELEC", typeof(string));
            lodtTabla.Columns.Add("CODIGO", typeof(string));
            lodtTabla.Columns.Add("NOMBRE", typeof(string));
            lodtTabla.Columns.Add("TP_RESE", typeof(string));
            lodtTabla.Columns.Add("NM_TPRESE", typeof(string));
            lodtTabla.Columns.Add("AREA", typeof(double));
            lodtTabla.Columns.Add("CANTI", typeof(double));
            lodtTabla.Columns.Add("AREA_NETA", typeof(double));
            lodtTabla.Columns.Add("PORCEN", typeof(double));

            // Abre archivos de salida:
            string ruta;
            string ruta1;
            ruta = Path.Combine(globalPath, "reporte.txt");
            ruta1 = Path.Combine(globalPath, "reportearq.txt");

            using (StreamWriter sw = new StreamWriter(ruta))
            using (StreamWriter sw1 = new StreamWriter(ruta1))
            {
                // switch para manejar "tipo_selec_catnomin"
                switch (_typeConsult)
                {
                    case "SEGUN DEPARTAMENTO":
                        //await CalcularSegunDepartamento(sw, lodtTabla);
                        break;

                    case "MINERIA A NIVEL NACIONAL":
                        await CalcularMineriaNacional(sw, lodtTabla);
                        break;

                    case "TIPO DE RESERVA":
                        //await CalcularTipoReserva(sw, lodtTabla);
                        break;

                    // ... y así sucesivamente (migrar cada ElseIf de tu VB.NET)

                    default:
                        // Si no coincide con ninguno
                        sw.WriteLine("No se ha identificado tipo de cálculo.");
                        break;
                }
            }

            // Luego del switch, 
            // podrías cargar "lodtTabla" en algún control, exportar a XML, etc.
            // Ejemplo de exportar a XML:
            string xmlPath = Path.Combine(globalPath, "arestringida.xml");
            DataSet ds = new DataSet();
            ds.Tables.Add(lodtTabla.Copy());
            ds.WriteXml(xmlPath);

            // O mostrar totales, etc.
        }

        private async Task CalcularMineriaNacional(StreamWriter sw, DataTable lodtTabla)
        {
            // Muchas de las operaciones se deben ejecutar en un QueuedTask
            // para cumplir con el modelo de ArcGIS Pro.
            await QueuedTask.Run(async () =>
            {
                try
                {
                    // Variables iniciales, según la lógica original:
                    vZona = "18";       // Se fija en "18"
                    vCodigo = "PERU";   // Se asigna "PERU"
                                        // vDatum    "WGS_84"
                    try
                    {
                        // Este es un "stub" de lo que harías en realidad
                        // cls_Oracle.FT_OBTIENE_TIPORESE(...) 
                        //DataTable lolodbExiste = dataBaseHandler.GetRestrictedAreaType();
                    }
                    catch (Exception)
                    {
                        // En VB era:
                        // MsgBox("Error en Obtener los tipos de reservas ...")
                        // En C# + ArcGIS Pro se puede usar:
                        // ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(...)
                    }


                    // Recorremos los ítems de CbxTypeMineNo.
                    for (int contador2 = 0; contador2 < CbxTypeMineNo.Items.Count; contador2++)
                    {
                        string sele_elemento = CbxTypeMineNo.Items[contador2].ToString();
                                                
                        lodbtExiste_SupAR = FeatureClassLoader.GetDataForElemento(sele_elemento, vDatum, vZona, vCodigo);

                        // Emulamos esa lógica:
                        if (sele_elemento == "ZONA ARQUEOLOGICA")
                        {
                            // 'Se hace por proceso python (se omite la parte interna)
                            // Podrías poner un continue o algo similar.
                        }
                        else if (sele_elemento == "RED VIAL NACIONAL")
                        {
                            // 'Se toma el área del total
                            // (El original no hacía mucho en este else if,
                            //  sino que hacía la lógica más abajo.)
                        }
                        else
                        {
                            // Recorrer las filas en "lodbtExiste_SupAR"
                            // y llenar 'lodtTabla' y 'sw'.
                            for (int contador1 = 0; contador1 < lodbtExiste_SupAR.Rows.Count; contador1++)
                            {
                                DataRow fila = lodbtExiste_SupAR.Rows[contador1];

                                // SIMULAMOS lo que hacías en VB:
                                areasup_rese_local = 0.0; // v_areasup_rese
                                areaini_depa_local = 0.0; // v_areaini_depa
                                cantidad_local = 0.0; // v_cantidad
                                nombre_depa = "";   // vCodigo_depa

                                // Asigna con conversiones seguras
                                if (fila.Table.Columns.Contains("AREASUPER"))
                                    areasup_rese_local = Convert.ToDouble(fila["AREASUPER"]);
                                if (fila.Table.Columns.Contains("AREAINI"))
                                    areaini_depa_local = Convert.ToDouble(fila["AREAINI"]);
                                if (fila.Table.Columns.Contains("CANTIDAD"))
                                    cantidad_local = Convert.ToDouble(fila["CANTIDAD"]);
                                if (fila.Table.Columns.Contains("NOMBRE"))
                                    nombre_depa = fila["NOMBRE"].ToString();

                                if (cantidad_local > 0)
                                {
                                    // Creas una nueva fila en lodtTabla
                                    DataRow dRow = lodtTabla.NewRow();
                                    dRow["CODIGO"] = nombre_depa; // vCodigo_depa
                                    dRow["NOMBRE"] = nombre_depa; // vCodigo_depa
                                    dRow["TP_RESE"] = sele_elemento;
                                    dRow["NM_TPRESE"] = sele_elemento;
                                    dRow["AREA"] = Math.Round(areaini_depa_local, 4).ToString("###,###.0000");
                                    dRow["CANTI"] = cantidad_local;
                                    dRow["AREA_NETA"] = Math.Round(areasup_rese_local, 4).ToString("###,###.0000");

                                    double porc = 0.0;
                                    if (areaini_depa_local != 0)
                                        porc = (areasup_rese_local / areaini_depa_local) * 100;
                                    double porcRedondeado = Math.Round(porc, 2);
                                    dRow["PORCEN"] = porcRedondeado.ToString("###,###.00");

                                    lodtTabla.Rows.Add(dRow);

                                    // Luego escribes en el StreamWriter
                                    // sw.WriteLine("8P") (si la zona es 18), etc...
                                    if (vZona == "17") sw.WriteLine("7P");
                                    else if (vZona == "18") sw.WriteLine("8P");
                                    else if (vZona == "19") sw.WriteLine("9P");

                                    sw.WriteLine(vDatum);
                                    sw.WriteLine(nombre_depa);
                                    sw.WriteLine(sele_elemento);
                                    sw.WriteLine(Math.Round(areaini_depa_local, 4).ToString("###,###.0000"));
                                    sw.WriteLine(cantidad_local);
                                    sw.WriteLine(Math.Round(areasup_rese_local, 4).ToString("###,###.0000"));
                                    sw.WriteLine(porcRedondeado.ToString("###,###.00"));
                                }
                                else
                                {
                                    // Cuando la cantidad es 0
                                    areasup_rese_local = 0;
                                    double porc = 0;

                                    if (vZona == "17") sw.WriteLine("7P");
                                    else if (vZona == "18") sw.WriteLine("8P");
                                    else if (vZona == "19") sw.WriteLine("9P");

                                    sw.WriteLine(vDatum);
                                    //sw.WriteLine(v_nm_depa1);
                                    sw.WriteLine(sele_elemento);
                                    sw.WriteLine(Math.Round(areaini_depa_local, 4).ToString("###,###.0000"));
                                    sw.WriteLine(cantidad_local);
                                    sw.WriteLine(Math.Round(areasup_rese_local, 4).ToString("###,###.0000"));
                                    sw.WriteLine(Math.Round(porc, 2).ToString("###,###.00"));
                                }
                            }
                        }
                    }

                    await MapUtils.CreateMapAsync(GlobalVariables.mapNameCatastro);
                    await InitConectionGdb();
                    Map map = await MapUtils.EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro);
                    // -----------------------------
                    // Ahora la parte de "ZONA ARQUEOLOGICA"
                    // El original corre un .bat (Shell) y luego añade un shapefile "za",
                    // y hace un FeatureCursor en pFeatureLayer_tmp, sumando F_AREA, etc.
                    // Lo emulamos a grandes rasgos:
                    if (vDatum == "2")
                    {
                        string sele_elemento = "ZONA ARQUEOLOGICA";
                        lodbtExiste_SupAR = FeatureClassLoader.GetDataForElemento(sele_elemento, vDatum, vZona, vCodigo);
                    }

                    for (int contador1 = 0; contador1 < lodbtExiste_SupAR.Rows.Count; contador1++)
                    {
                        cantidad_local = Convert.ToDouble(lodbtExiste_SupAR.Rows[contador1]["CANTIDAD"]);
                    }
                    
                    var featureClassLoader = new FeatureClassLoader(geodatabase, map, vZona, "99");
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_DepNacional + vZona, false);
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Caram84 + vZona, false);

                    var Params = Geoprocessing.MakeValueArray("Departamento_Nacional", "Caram");
                    var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetMineNoZa, Params);

                    var fLayer = MapView.Active.Map.FindLayers("za").FirstOrDefault() as FeatureLayer;

                    // 1) Obtener la FeatureClass
                    FeatureClass tmpFeatureClass = fLayer.GetFeatureClass();
                    // 2) Contar cuántas entidades hay
                    long count = tmpFeatureClass.GetCount();
                    double areaValue = 0.0;
                    double v_area_f1 = 0.0;
                    if (count > 0)
                    {
                        // 3) Recorrer las features y sumar "F_AREA"
                        using (RowCursor cursor = tmpFeatureClass.Search(null, false))
                        {
                            while (cursor.MoveNext())
                            {
                                using (Feature feature = (Feature)cursor.Current)
                                {
                                    // Obtener el valor del campo "F_AREA"
                                    // Nota: asumiendo que "F_AREA" es un campo double/float:
                                    areaValue = Convert.ToDouble(feature["F_AREA"]);
                                    // Redondeamos a 4 decimales 
                                    areaValue = Math.Round(areaValue, 4);

                                    // Acumulamos en v_area_f1
                                    v_area_f1 += areaValue;
                                }
                            }
                        }

                        // 4) Convertir la suma a hectáreas dividiendo entre 10_000
                        v_area_f1 = v_area_f1 / 10000.0;

                        // (La variable v_area_total no se usa en tu ejemplo, 
                        //  pero se menciona en comentarios)

                        // 5) Crear la fila en "lodtTabla"
                        DataRow dRow = lodtTabla.NewRow();
                        dRow["CODIGO"] = nombre_depa;
                        dRow["NOMBRE"] = nombre_depa;
                        dRow["TP_RESE"] = "ZONA ARQUEOLOGICA";
                        dRow["NM_TPRESE"] = "ZONA ARQUEOLOGICA";

                        // Ejemplo: si ya tienes "v_areaini_depa", 
                        // usas Math.Round(...) y Formateo como en VB:
                        dRow["AREA"] = Math.Round(areaini_depa_local, 4).ToString("###,###.0000");
                        dRow["CANTI"] = cantidad_local;  // si tienes v_cantidad en tu clase

                        // "AREA_NETA" = v_area_f1 (ya en hectáreas)
                        dRow["AREA_NETA"] = Math.Round(v_area_f1, 4).ToString("###,###.0000");

                        // "PORCEN" = (v_area_f1 / areaini_depa_local)*100
                        double porcentaje = 0.0;
                        if (areaini_depa_local != 0)
                            porcentaje = (v_area_f1 / areaini_depa_local) * 100.0;

                        dRow["PORCEN"] = Math.Round(porcentaje, 2).ToString("###,###.00");

                        lodtTabla.Rows.Add(dRow);

                        // 6) Escribir en tu StreamWriter (sw), tal como en VB:
                        sw.WriteLine("8P");
                        sw.WriteLine(vDatum);
                        sw.WriteLine(nombre_depa);
                        sw.WriteLine("ZONA ARQUEOLOGICA");
                        sw.WriteLine(Math.Round(areaini_depa_local, 4).ToString("###,###.0000"));
                        sw.WriteLine(cantidad_local);
                        sw.WriteLine(Math.Round(v_area_f1, 4).ToString("###,###.0000"));
                        sw.WriteLine(Math.Round(porcentaje, 2).ToString("###,###.00"));
                    }
                    else
                    {
                        // Si no hay features en esa capa shapefile,
                        // tu VB ejecuta:
                        //   v_area_f1 = 0
                        //   v_porcen_f1 = 0
                        //   v_area_total = areaini_depa_local
                        //   sw.WriteLine("8P")
                        //   ...

                        v_area_f1 = 0.0;
                        //v_porcen_f1 = 0.0;
                        //v_area_total = areaini_depa_local;

                        // Escribes en sw
                        sw.WriteLine("8P");
                        sw.WriteLine(vDatum);
                        sw.WriteLine(nombre_depa);
                        sw.WriteLine("ZONA ARQUEOLOGICA");
                        sw.WriteLine(Math.Round(areaini_depa_local, 4).ToString("###,###.0000"));
                        sw.WriteLine(cantidad_local);
                        sw.WriteLine(Math.Round(v_area_f1, 4).ToString("###,###.0000"));
                        // (v_area_f1 / areaini_depa_local) => 0/loQueSea = 0
                        double porcentaje = 0.0;
                        if (areaini_depa_local != 0)
                            porcentaje = (v_area_f1 / areaini_depa_local) * 100.0;
                        sw.WriteLine(Math.Round(porcentaje, 2).ToString("###,###.00"));

                        // Ejemplo de final con un MessageBox:
                        // En VB hacías: MsgBox("El proceso ha finalizado satisfactoriamente..1.")
                        // En C# ArcGIS Pro:
                        // ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("El proceso ha finalizado Satisfactoriamente..1.", "ESTADÍSTICAS...");

                        // Y al final, sw.Close() -> pero ojo, si lo cierras aquí, 
                        // ya no se escribe nada más fuera. Asegúrate de dónde cierras tu StreamWriter.
                    }

                    if (vDatum == "2")
                    {
                        string sele_elemento = "RED VIAL NACIONAL";
                        lodbtExiste_SupAR = FeatureClassLoader.GetDataForElemento(sele_elemento, vDatum, vZona, vCodigo);
                    }

                    for (int contador1 = 0; contador1 < lodbtExiste_SupAR.Rows.Count; contador1++)
                    {
                        cantidad_local = Convert.ToDouble(lodbtExiste_SupAR.Rows[contador1]["CANTIDAD"]);
                    }


                    Params = Geoprocessing.MakeValueArray("Departamento_Nacional", "Caram");
                    response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetMineNoRd, Params);

                    fLayer = MapView.Active.Map.FindLayers("rd").FirstOrDefault() as FeatureLayer;

                    // 1) Obtener la FeatureClass
                    tmpFeatureClass = fLayer.GetFeatureClass();
                    // 2) Contar cuántas entidades hay
                    count = tmpFeatureClass.GetCount();
                    areaValue = 0.0;
                    v_area_f1 = 0.0;
                    if (count > 0)
                    {
                        // 3) Recorrer las features y sumar "F_AREA"
                        using (RowCursor cursor = tmpFeatureClass.Search(null, false))
                        {
                            while (cursor.MoveNext())
                            {
                                using (Feature feature = (Feature)cursor.Current)
                                {
                                    // Obtener el valor del campo "F_AREA"
                                    // Nota: asumiendo que "F_AREA" es un campo double/float:
                                    areaValue = Convert.ToDouble(feature["F_AREA"]);
                                    // Redondeamos a 4 decimales 
                                    areaValue = Math.Round(areaValue, 4);

                                    // Acumulamos en v_area_f1
                                    v_area_f1 += areaValue;
                                }
                            }
                        }

                        // 4) Convertir la suma a hectáreas dividiendo entre 10_000
                        v_area_f1 = v_area_f1 / 10000.0;

                        // (La variable v_area_total no se usa en tu ejemplo, 
                        //  pero se menciona en comentarios)

                        // 5) Crear la fila en "lodtTabla"
                        DataRow dRow = lodtTabla.NewRow();
                        dRow["CODIGO"] = nombre_depa;
                        dRow["NOMBRE"] = nombre_depa;
                        dRow["TP_RESE"] = "RED VIAL NACIONAL";
                        dRow["NM_TPRESE"] = "RED VIAL NACIONAL";

                        // Ejemplo: si ya tienes "v_areaini_depa", 
                        // usas Math.Round(...) y Formateo como en VB:
                        dRow["AREA"] = Math.Round(areaini_depa_local, 4).ToString("###,###.0000");
                        dRow["CANTI"] = cantidad_local;  // si tienes v_cantidad en tu clase

                        // "AREA_NETA" = v_area_f1 (ya en hectáreas)
                        dRow["AREA_NETA"] = Math.Round(v_area_f1, 4).ToString("###,###.0000");

                        // "PORCEN" = (v_area_f1 / areaini_depa_local)*100
                        double porcentaje = 0.0;
                        if (areaini_depa_local != 0)
                            porcentaje = (v_area_f1 / areaini_depa_local) * 100.0;

                        dRow["PORCEN"] = Math.Round(porcentaje, 2).ToString("###,###.00");

                        lodtTabla.Rows.Add(dRow);

                        // 6) Escribir en tu StreamWriter (sw), tal como en VB:
                        sw.WriteLine("8P");
                        sw.WriteLine(vDatum);
                        sw.WriteLine(nombre_depa);
                        sw.WriteLine("RED VIAL NACIONAL");
                        sw.WriteLine(Math.Round(areaini_depa_local, 4).ToString("###,###.0000"));
                        sw.WriteLine(cantidad_local);
                        sw.WriteLine(Math.Round(v_area_f1, 4).ToString("###,###.0000"));
                        sw.WriteLine(Math.Round(porcentaje, 2).ToString("###,###.00"));
                    }
                    else
                    {
                        // Si no hay features en esa capa shapefile,
                        // tu VB ejecuta:
                        //   v_area_f1 = 0
                        //   v_porcen_f1 = 0
                        //   v_area_total = areaini_depa_local
                        //   sw.WriteLine("8P")
                        //   ...

                        v_area_f1 = 0.0;
                        //v_porcen_f1 = 0.0;
                        //v_area_total = areaini_depa_local;

                        // Escribes en sw
                        sw.WriteLine("8P");
                        sw.WriteLine(vDatum);
                        sw.WriteLine(nombre_depa);
                        sw.WriteLine("ZONA ARQUEOLOGICA");
                        sw.WriteLine(Math.Round(areaini_depa_local, 4).ToString("###,###.0000"));
                        sw.WriteLine(cantidad_local);
                        sw.WriteLine(Math.Round(v_area_f1, 4).ToString("###,###.0000"));
                        // (v_area_f1 / areaini_depa_local) => 0/loQueSea = 0
                        double porcentaje = 0.0;
                        if (areaini_depa_local != 0)
                            porcentaje = (v_area_f1 / areaini_depa_local) * 100.0;
                        sw.WriteLine(Math.Round(porcentaje, 2).ToString("###,###.00"));

                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Debug.WriteLine($"Error en CalcularMineriaNacional: {ex.Message}");
                }
            });
        }

    }
}
