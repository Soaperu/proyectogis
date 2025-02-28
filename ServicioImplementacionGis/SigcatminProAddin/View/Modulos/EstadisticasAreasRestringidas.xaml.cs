using ArcGIS.Core.Data;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using OfficeOpenXml;
using SigcatminProAddin.Models.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        string v_codigo_depa = "";
        string v_nm_laguna = "";
        Geodatabase geodatabase;
        SdeConnectionGIS sdeHelper = new SdeConnectionGIS();

        public EstadisticasAreasRestringidas()
        {
            InitializeComponent();
            CurrentUser();
            ConfigureDataGridResultColumns();
            dataBaseHandler = new DatabaseHandler();

        }
        public class ComboBoxPairs
        {
            public string _Key { get; set; }
            public string _Value { get; set; }

            public ComboBoxPairs(string _key, string _value)
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

            cbp.Add(new ComboBoxPairs("--Seleccionar--", "0"));
            cbp.Add(new ComboBoxPairs("MINERIA A NIVEL NACIONAL", "1"));
            cbp.Add(new ComboBoxPairs("SEGUN DEPARTAMENTO", "2"));
            cbp.Add(new ComboBoxPairs("SEGUN PROVINCIA", "3"));

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

            cbp.Add(new ComboBoxPairs("WGS-84", "2"));
            cbp.Add(new ComboBoxPairs("PSAD-56", "1"));

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

        private async void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Reseteamos los controles de detalle
            //CbxRegion.IsEnabled = false;
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
            string selectedValue = CbxTypeConsult.SelectedValue as string;
            //if (string.IsNullOrEmpty(selectedValue)) return;

            // Dependiendo del tipo seleccionado, ejecutar la lógica correspondiente
            switch (selectedValue)
            {
                case "0": break;

                case "1": // "MINERIA A NIVEL NACIONAL":
                    _typeConsult = "MINERIA A NIVEL NACIONAL";

                    BtnMineNo.IsEnabled = true;
                    BtnLoadData.IsEnabled = true;


                    // Lógica para cargar combos de reserva minera
                    CbxTypeMineNo.IsEnabled = true;
                    CbxTypeMineNo.Items.Clear();

                    // Si tienes un CbxTypeMineNomin también
                    CbxTypeMineYes.Items.Clear();

                    try
                    {
                        LoadComboBoxYesNo();
                        //DataTable lodbtExiste_tipo = dataBaseHandler.GetRestrictedAreaType(); //cls_Oracle.F_Obtiene_Tipo_AreaRestringida();
                        //if (lodbtExiste_tipo != null)
                        //{
                        //    foreach (DataRow row in lodbtExiste_tipo.Rows)
                        //    {
                        //        string v_nm_depa = row["TN_DESTIP"].ToString();
                        //        // Filtrado (excluir algunos tipos)
                        //        if (EsValidoParaMineria(v_nm_depa))
                        //        {
                        //            // Agregar según la clasificación propia
                        //            if (v_nm_depa == "AREA NATURAL")
                        //            {
                        //                CbxTypeMineNo.Items.Add("AREA NATURAL - USO INDIRECTO");
                        //                CbxTypeMineYes.Items.Add("AREA NATURAL - USO DIRECTO");
                        //                CbxTypeMineYes.Items.Add("AREA NATURAL - AMORTIGUAMIENTO");
                        //                CbxTypeMineYes.Items.Add("CLASIFICACION DIVERSA");
                        //                CbxTypeMineNo.Items.Add("CLASIFICACION DIVERSA");
                        //                CbxTypeMineYes.Items.Add("AREA DE CONSERVACION PRIVADA");
                        //                CbxTypeMineYes.Items.Add("AREA DE CONSERVACION MUNICIPAL Y OTROS");
                        //            }
                        //            else if (v_nm_depa == "PROYECTO ESPECIAL")
                        //            {
                        //                CbxTypeMineNo.Items.Add("PROYECTO ESPECIAL - HIDRAULICOS");
                        //                CbxTypeMineYes.Items.Add("PROYECTO ESPECIAL (no hidráulicos)");
                        //            }
                        //            else if (v_nm_depa == "PROPUESTA DE AREA NATURAL")
                        //            {
                        //                CbxTypeMineYes.Items.Add("PROPUESTA DE AREA NATURAL");
                        //            }
                        //            else if (v_nm_depa == "POSIBLE ZONA URBANA")
                        //            {
                        //                CbxTypeMineYes.Items.Add("POSIBLE ZONA URBANA");
                        //                CbxTypeMineYes.Items.Add("AREA DE EXPANSION URBANA");
                        //            }
                        //            else
                        //            {
                        //                CbxTypeMineNo.Items.Add(v_nm_depa);
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        MessageBox.Show($"Error al obtener los tipos de reserva minera: {ex.Message}");
                    }

                    if (CbxTypeMineNo.Items.Count > 0)
                        CbxTypeMineNo.SelectedIndex = 0;

                    if (CbxTypeMineYes.Items.Count > 0)
                        CbxTypeMineYes.SelectedIndex = 0;

                    break;

                case "2":
                    // Guardamos el tipo en una variable de clase (si lo necesitas)
                    _typeConsult = "SEGUN DEPARTAMENTO";
                    try
                    {
                        LoadComboBoxYesNo();
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        MessageBox.Show($"Error al obtener los tipos de reserva minera: {ex.Message}");
                    }
                    // Habilitamos/deshabilitamos controles

                    await InitConectionGdb();
                    var pFeatureClass = await LayerUtils.GetFeatureClass(geodatabase, FeatureClassConstants.gstrFC_Departamento_WGS + 18);
                    var listItems = await QueuedTask.Run(() =>
                    {
                        List<string> items = new List<string>();
                        // Lógica de lectura de features y llenado de CbxRegion
                        try
                        {
                            using (RowCursor cursor = pFeatureClass.Search(null, false))
                            {
                                while (cursor.MoveNext())
                                {
                                    using (Row row = cursor.Current)
                                    {
                                        object nmValue = row["NM_DEPA"];
                                        if (nmValue != null) // Verificas si es distinto de null
                                        {
                                            string v_nm_depa = row["NM_DEPA"].ToString();
                                            if (!EsLagunaONombreEspecial(v_nm_depa))
                                            {
                                                items.Add(v_nm_depa);
                                            }
                                        }
                                    }
                                }
                            }
                            items.Sort();
                            return items;
                        }
                        catch (Exception ex)
                        {
                            // Manejo de errores
                            MessageBox.Show($"Error al obtener los departamentos: {ex.Message}");
                            return items;
                        }
                    });

                    foreach (string item in listItems)
                    {
                        CbxRegion.Items.Add(item);
                    }
                    // Asignamos un índice por defecto
                    if (CbxRegion.Items.Count > 0)
                        CbxRegion.SelectedIndex = 0;

                    //// Cargar tipos de reservas (ejemplo)
                    //CargarTiposReserva();
                    if (CbxTypeMineNo.Items.Count > 0)
                        CbxTypeMineNo.SelectedIndex = 0;

                    if (CbxTypeMineYes.Items.Count > 0)
                        CbxTypeMineYes.SelectedIndex = 0;
                    break;

                case "3":
                    _typeConsult = "SEGUN PROVINCIA";
                    try
                    {
                        LoadComboBoxYesNo();
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        MessageBox.Show($"Error al obtener los tipos de reserva minera: {ex.Message}");
                    }
                    //CbxRegion.IsEnabled = true;
                    //CbxRegion.Items.Clear();

                    //TbxZone.Visibility = Visibility.Hidden;
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
                    //            CbxRegion.Items.Add(v_nm_depa);
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Manejo de excepción, log, etc.
                    //}

                    //if (CbxRegion.Items.Count > 0)
                    //    CbxRegion.SelectedIndex = 0;

                    //// Dependiendo de tu lógica, habilitar también CbxTypeMineNo, etc.
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

        private bool EsLagunaONombreEspecial(string nombre)
        {
            // Ajustar la lógica a tu preferencia
            if (string.IsNullOrEmpty(nombre)) return false;

            // Ejemplo de comparaciones
            var especiales = new[] {
                "LAGUNA UMAYO",
                "LAGO DE ARAPA",
                "LAGUNA LANGUILAYO",
                "LAGUNA DE PARINACOCHAS",
                "LAGUNA DE JUNIN",
                "LAGUNA SALINAS",
                "LAGO TITICACA",
                "FUERA DEL PERU",
                "MAR"
            };

            foreach (var e in especiales)
            {
                if (nombre.Equals(e, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
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
            ProgressBarUtils progressBar = new ProgressBarUtils("Calculando estadisticas de Mineria No...");
            progressBar.Show();
            try
            {

                BtnMineNo.IsEnabled = false;
                // Por buenas prácticas, la mayor parte de la lógica GIS 
                // se ejecuta dentro de un QueuedTask:
                await QueuedTask.Run(async () =>
                {
                    await CalcularAreasMinNoAsync();
                });

                // Mensaje final
                MessageBox.Show("El proceso ha finalizado satisfactoriamente.",
                                "Estadísticas de Áreas Restringidas",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                progressBar.Dispose();
                MessageBox.Show($"Error: {ex.Message}");
            }
            progressBar.Dispose();
        }

        /// <summary>
        /// Crea y devuelve un DataTable preconfigurado 
        /// con las columnas necesarias para los resultados.
        /// </summary>
        private DataTable CrearDataTableResultados()
        {
            DataTable lodtTabla = new DataTable();
            //lodtTabla.Columns.Add("SELEC", typeof(string));
            lodtTabla.Columns.Add("CODIGO", typeof(string));
            lodtTabla.Columns.Add("NOMBRE", typeof(string));
            lodtTabla.Columns.Add("TP_RESE", typeof(string));
            lodtTabla.Columns.Add("NM_TPRESE", typeof(string));
            lodtTabla.Columns.Add("AREA", typeof(double));
            lodtTabla.Columns.Add("CANTI", typeof(double));
            lodtTabla.Columns.Add("AREA_NETA", typeof(double));
            lodtTabla.Columns.Add("PORCEN", typeof(double));

            return lodtTabla;
        }


        private async Task CalcularAreasMinNoAsync()
        {
            // Prepara DataTable para resultados:
            DataTable lodtTabla = CrearDataTableResultados();

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
                        await CalcularSegunDepartamentoNo(sw, lodtTabla);
                        break;

                    case "MINERIA A NIVEL NACIONAL":
                        await CalcularMineriaNacionalNo(sw, lodtTabla);
                        break;

                    case "SEGUN PROVINCIA":
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
            // Guardar a XML
            ds.WriteXml(xmlPath);
            Dispatcher.Invoke(() =>
            {
                calculatedIndex(DataGridResult, lodtTabla.Rows.Count, DatagridResultConstantsDM.ColumNames.Index);
                DataGridResult.ItemsSource = lodtTabla.Copy().DefaultView;
            });

        }
        private void calculatedIndex(GridControl table, int records, string columnName)
        {
            int newRowHandle = DataControlBase.NewItemRowHandle;

            // Agregar registros de ejemplo con índice
            for (int i = 1; i <= records; i++)
            {
                table.SetCellValue(newRowHandle, columnName, i);
            }
        }

        private async Task CalcularSegunDepartamentoNo(StreamWriter sw, DataTable lodtTabla)
        {
            // Capturar los valores en el hilo de UI antes de entrar a `QueuedTask.Run`
            string v_nm_depa = string.Empty;
            string v_Zona = string.Empty;
            string v_sistema = vDatum;
            await QueuedTask.Run(async () =>
            {
                // Variables que en VB estaban definidas como globales o locales
                string seleElemento = string.Empty;
                string v_tipo_rese = string.Empty;

                // Cargar la feature class
                //cls_Catastro.PT_CargarFeatureClass_SDE(gstrFC_Departamento, m_application, "2", false);
                // pFeatureClass = pFeatureLayer_depa.FeatureClass;
                await InitConectionGdb();
                var pFeatureClass = await LayerUtils.GetFeatureClass(geodatabase, FeatureClassConstants.gstrFC_Departamento_WGS + 18);

                // Tomar valores de combos (ajusta si usas WPF: CbxTypeMineNo.SelectedItem as string, etc.)
                Dispatcher.Invoke(() =>
                {
                    v_nm_depa = CbxRegion.SelectedItem?.ToString();
                    v_Zona = CbxZone.SelectedItem?.ToString();
                    //v_sistema = CbxDatum.SelectedItem?.ToString();
                });

                // Ejemplo de obtener “tipo de reserva”. En VB quedaba comentado:
                // sele_elemento = CbxTypeMineNo.SelectedItem
                // Supongo que lo usarás más adelante.
                // string seleElemento = CbxTypeMineNo.SelectedItem?.ToString();

                // lodbtExiste_tipo = cls_Oracle.FT_OBTIENE_TIPORESE(seleElemento);
                // Manejo de excepciones
                try
                {
                    DataTable lodbtExiste_tipo = dataBaseHandler.GetRestrictedAreaType();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en Obtener los tipos de reservas de la base de datos: " + ex.Message,
                        "CONSULTA BASE DE DATOS...",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                // Construimos un QueryFilter (ArcObjects) o un WhereClause
                QueryFilter pqueryfilter =
                                           new QueryFilter()
                                           {
                                               WhereClause = $"NM_DEPA = '{v_nm_depa}'"
                                           };
                //IQueryFilter pqueryfilter = new QueryFilterClass();  // Si usas ArcObjects
                //pqueryfilter.WhereClause = $"NM_DEPA = '{v_nm_depa}'";

                // Buscamos features en la clase
                //var pFeatureCursor = pFeatureClass.Search(pqueryfilter, true);
                //IFeature pFeature = pFeatureCursor.NextFeature();
                using (RowCursor cursor = pFeatureClass.Search(pqueryfilter, false))
                {
                    while (cursor.MoveNext())
                    {
                        //// Iteramos
                        using (Feature feature = (Feature)cursor.Current)
                        {
                            string v_codigo = feature["CD_DEPA"].ToString();
                            string v_nm_depa1 = feature["NM_DEPA"].ToString();

                            // Si v_codigo = "99" => MAR o FRONTERA
                            if (v_codigo == "99")
                            {
                                if (v_nm_depa1 == "MAR" || v_nm_depa1 == "FUERA DEL PERU")
                                {
                                    // Recorremos cbotiporese (sus Items)
                                    for (int i = 0; i < CbxTypeMineNo.Items.Count; i++)
                                    {
                                        seleElemento = CbxTypeMineNo.Items[i].ToString();

                                        // Filtrar ciertos “no deseados”
                                        if (seleElemento == "EXPEDIENTE DE CATASTRO"
                                         || v_nm_depa == "RESERVA TURISTICA"
                                         || v_nm_depa == "CONCESION DE LABOR GENERAL"
                                         || v_nm_depa == "ANAD"
                                         || v_nm_depa == "INFRAESTRUCTURA DEL ESTADO")
                                        {
                                            continue;
                                        }

                                        // Llamada a la base (cls_Oracle)
                                        if (v_sistema == "2")
                                        {
                                            lodbtExiste_SupAR = dataBaseHandler.GetStatisticalIntersection(
                                                "3",
                                                $"DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_{v_Zona}",
                                                $"DATA_GIS.GPO_CAR_CARAM_WGS_{v_Zona}",
                                                v_codigo,
                                                seleElemento
                                            );
                                        }
                                        else
                                        {
                                            lodbtExiste_SupAR = dataBaseHandler.GetStatisticalIntersection(
                                                "3",
                                                $"DATA_GIS.GPO_DEP_DEPARTAMENTO_{v_Zona}",
                                                $"DATA_GIS.GPO_CAR_CARAM_{v_Zona}",
                                                v_codigo,
                                                seleElemento
                                            );
                                        }

                                        if (lodbtExiste_SupAR.Rows.Count == 0)
                                        {
                                            // No hay nada
                                            continue;
                                        }
                                        else
                                        {
                                            // Tomamos la última fila (o iteramos)
                                            double v_areasup_rese = 0.0;
                                            double v_areaini_depa = 0.0;
                                            double v_cantidad = 0.0;

                                            for (int j = 0; j < lodbtExiste_SupAR.Rows.Count; j++)
                                            {
                                                v_areasup_rese = Convert.ToDouble(lodbtExiste_SupAR.Rows[j]["AREASUPER"]);
                                                //v_codigo_depa = lodbtExiste_SupAR.Rows[j]["CODIGO"].ToString();
                                                v_areaini_depa = Convert.ToDouble(lodbtExiste_SupAR.Rows[j]["AREAINI"]);
                                                v_cantidad = Convert.ToDouble(lodbtExiste_SupAR.Rows[j]["CANTIDAD"]);
                                            }

                                            if (v_cantidad > 0)
                                            {
                                                // Añadimos fila a lodtTabla
                                                DataRow dRow = lodtTabla.NewRow();
                                                dRow["CODIGO"] = v_codigo;
                                                dRow["NOMBRE"] = v_nm_depa1;
                                                dRow["TP_RESE"] = seleElemento;
                                                dRow["NM_TPRESE"] = seleElemento;
                                                dRow["AREA"] = Math.Round(v_areaini_depa, 4).ToString("###,###.0000");
                                                dRow["CANTI"] = v_cantidad;
                                                dRow["AREA_NETA"] = Math.Round(v_areasup_rese, 4).ToString("###,###.0000");
                                                double porc = 0.0;
                                                if (v_areaini_depa != 0.0)
                                                    porc = (v_areasup_rese / v_areaini_depa) * 100.0;
                                                dRow["PORCEN"] = Math.Round(porc, 2).ToString("###,###.00");
                                                lodtTabla.Rows.Add(dRow);

                                                // Imprimir en sw
                                                switch (v_Zona)
                                                {
                                                    case "17": sw.WriteLine("7P"); break;
                                                    case "18": sw.WriteLine("8P"); break;
                                                    case "19": sw.WriteLine("9P"); break;
                                                }

                                                sw.WriteLine(v_sistema);
                                                sw.WriteLine(v_nm_depa1);
                                                sw.WriteLine(seleElemento);
                                                sw.WriteLine(Math.Round(v_areaini_depa, 4).ToString("###,###.0000"));
                                                sw.WriteLine(v_cantidad);
                                                sw.WriteLine(Math.Round(v_areasup_rese, 4).ToString("###,###.0000"));
                                                sw.WriteLine(Math.Round(porc, 2).ToString("###,###.00"));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // v_codigo != "99" => caso NORMAL
                                try
                                {
                                    for (int i = 0; i < CbxTypeMineNo.Items.Count; i++)
                                    {
                                        seleElemento = CbxTypeMineNo.Items[i].ToString();

                                        // Filtros
                                        if (seleElemento == "EXPEDIENTE DE CATASTRO"
                                         || v_nm_depa == "RESERVA TURISTICA"
                                         || v_nm_depa == "CONCESION DE LABOR GENERAL"
                                         || v_nm_depa == "ANAD"
                                         || v_nm_depa == "INFRAESTRUCTURA DEL ESTADO")
                                        {
                                            continue;
                                        }

                                        // Llamadas cls_Oracle segun v_sistema
                                        if (v_sistema == "2")
                                        {
                                            if (seleElemento == "AREA DE DEFENSA NACIONAL")
                                            {
                                                lodbtExiste_SupAR = dataBaseHandler.GetStatisticalIntersection(
                                                    "19",
                                                    $"DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_{v_Zona}",
                                                    $"DATA_GIS.GPO_CAR_CARAM_WGS_{v_Zona}",
                                                    v_codigo,
                                                    seleElemento
                                                );
                                            }
                                            else
                                            {
                                                lodbtExiste_SupAR = dataBaseHandler.GetStatisticalIntersection(
                                                    "3",
                                                    $"DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_{v_Zona}",
                                                    $"DATA_GIS.GPO_CAR_CARAM_WGS_{v_Zona}",
                                                    v_codigo,
                                                    seleElemento
                                                );
                                            }
                                        }
                                        else
                                        {
                                            lodbtExiste_SupAR = dataBaseHandler.GetStatisticalIntersection(
                                                "3",
                                                $"DATA_GIS.GPO_DEP_DEPARTAMENTO_{v_Zona}",
                                                $"DATA_GIS.GPO_CAR_CARAM_{v_Zona}",
                                                v_codigo,
                                                seleElemento
                                            );
                                        }

                                        if (lodbtExiste_SupAR.Rows.Count == 0)
                                        {
                                            // nada
                                            continue;
                                        }
                                        else
                                        {
                                            // En tu VB: If v_codigo = "04" or "12" or "21" or "05" or "08" => LAGUNA
                                            // ...
                                            // De aquí en adelante se anida mucha lógica. 
                                            // Lo ideal es separarla en métodos, 
                                            // pero la mantendremos igual para no romper la lógica.

                                            // (1) Departamentos que tienen lagunas
                                            if (v_codigo == "04" || v_codigo == "12" || v_codigo == "21"
                                             || v_codigo == "05" || v_codigo == "08")
                                            {
                                                // Lógica lagunas (PUNO, etc.)
                                                // ...
                                                // (Ajusta la migración de tu VB anidado)
                                                // (1) DEPARTAMENTOS QUE TIENEN LAGUNAS
                                                // Este bloque corresponde a la parte "If v_codigo = "04" Or ... Then"

                                                // Leemos la tabla "lodbtExiste_SupAR" (ya poblada con FT_Int_tiporesexdepa)
                                                double v_areaini_depa = 0.0;
                                                double v_areasup_rese = 0.0;
                                                double v_cantidad = 0.0;

                                                // Tomamos la última fila o iteramos para asignar v_areasup_rese, etc.
                                                // (En tu VB hacías un For que tomaba la misma variable repetidamente.)
                                                for (int idx = 0; idx < lodbtExiste_SupAR.Rows.Count; idx++)
                                                {
                                                    v_areasup_rese = Convert.ToDouble(lodbtExiste_SupAR.Rows[idx]["AREASUPER"]);
                                                    v_codigo_depa = lodbtExiste_SupAR.Rows[idx]["CODIGO"].ToString();
                                                    v_areaini_depa = Convert.ToDouble(lodbtExiste_SupAR.Rows[idx]["AREAINI"]);
                                                    v_cantidad = Convert.ToDouble(lodbtExiste_SupAR.Rows[idx]["CANTIDAD"]);
                                                }

                                                // Asignamos "v_nm_laguna" dependiendo del v_codigo
                                                // (Igual que en tu VB: If v_codigo = "05" Then v_nm_laguna="LAGUNA DE PARINACOCHAS", etc.)
                                                if (v_codigo == "05") v_nm_laguna = "LAGUNA DE PARINACOCHAS";
                                                else if (v_codigo == "04") v_nm_laguna = "LAGUNA SALINAS";
                                                else if (v_codigo == "08") v_nm_laguna = "LAGUNA LAGUILAYO";
                                                else if (v_codigo == "12") v_nm_laguna = "LAGUNA DE JUNIN";
                                                else if (v_codigo == "21") v_nm_laguna = "LAGUNAS DE PUNO";

                                                // Si es "LAGUNAS DE PUNO", sumamos UMAYO, ARAPA, TITICACA
                                                if (v_nm_laguna == "LAGUNAS DE PUNO")
                                                {
                                                    double v_areaini_depa2 = 0.0;
                                                    double v_areasup_rese2 = 0.0;

                                                    for (int j = 1; j <= 3; j++)
                                                    {
                                                        // Asignamos v_nm_laguna a la laguna concreta
                                                        if (j == 1) v_nm_laguna = "LAGUNA UMAYO";
                                                        else if (j == 2) v_nm_laguna = "LAGO DE ARAPA";
                                                        else if (j == 3) v_nm_laguna = "LAGO TITICACA";

                                                        // Volvemos a llamar FT_Int_tiporesexdepa con code=4, etc.
                                                        // En VB: lodbtExiste_SupAR = ...
                                                        DataTable tempSupAR;
                                                        if (v_sistema == "2")
                                                        {
                                                            tempSupAR = dataBaseHandler.GetStatisticalIntersection(
                                                                "4",
                                                                $"DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_{v_Zona}",
                                                                $"DATA_GIS.GPO_CAR_CARAM_WGS_{v_Zona}",
                                                                v_nm_laguna,
                                                                seleElemento
                                                            );
                                                        }
                                                        else
                                                        {
                                                            tempSupAR = dataBaseHandler.GetStatisticalIntersection(
                                                                "4",
                                                                $"DATA_GIS.GPO_DEP_DEPARTAMENTO_{v_Zona}",
                                                                $"DATA_GIS.GPO_CAR_CARAM_{v_Zona}",
                                                                v_nm_laguna,
                                                                seleElemento
                                                            );
                                                        }

                                                        // Sumamos v_areasup_rese1, v_areaini_depa1
                                                        double v_areasup_rese1 = 0.0;
                                                        double v_areaini_depa1 = 0.0;

                                                        foreach (DataRow rowLag in tempSupAR.Rows)
                                                        {
                                                            v_areasup_rese1 = Convert.ToDouble(rowLag["AREASUPER"]);
                                                            v_codigo_depa = rowLag["CODIGO"].ToString();
                                                            v_areaini_depa1 = Convert.ToDouble(rowLag["AREAINI"]);
                                                        }
                                                        v_areaini_depa2 += v_areaini_depa1;
                                                        v_areasup_rese2 += v_areasup_rese1;
                                                    }

                                                    // Una vez sumado, creamos la fila final
                                                    if (v_cantidad > 0)
                                                    {
                                                        DataRow dRow = lodtTabla.NewRow();
                                                        dRow["CODIGO"] = v_codigo_depa;
                                                        dRow["NOMBRE"] = v_nm_depa1;
                                                        dRow["TP_RESE"] = seleElemento;
                                                        dRow["NM_TPRESE"] = seleElemento;
                                                        double areaTotal = (v_areaini_depa + v_areaini_depa2);
                                                        double resaTotal = (v_areasup_rese + v_areasup_rese2);

                                                        dRow["AREA"] = Math.Round(areaTotal, 4).ToString("###,###.0000");
                                                        dRow["CANTI"] = v_cantidad;
                                                        dRow["AREA_NETA"] = Math.Round(resaTotal, 4).ToString("###,###.0000");

                                                        double porcen = 0.0;
                                                        if (areaTotal != 0.0)
                                                            porcen = (resaTotal / areaTotal) * 100.0;

                                                        dRow["PORCEN"] = Math.Round(porcen, 2).ToString("###,###.00");

                                                        lodtTabla.Rows.Add(dRow);

                                                        // Escribir en sw
                                                        if (v_Zona == "17") sw.WriteLine("7P");
                                                        else if (v_Zona == "18") sw.WriteLine("8P");
                                                        else if (v_Zona == "19") sw.WriteLine("9P");

                                                        sw.WriteLine(v_sistema);
                                                        sw.WriteLine(v_nm_depa1);
                                                        sw.WriteLine(seleElemento);
                                                        sw.WriteLine(Math.Round(areaTotal, 4).ToString("###,###.0000"));
                                                        sw.WriteLine(v_cantidad);
                                                        sw.WriteLine(Math.Round(resaTotal, 4).ToString("###,###.0000"));
                                                        sw.WriteLine(Math.Round(porcen, 2).ToString("###,###.00"));
                                                    }

                                                    // Se iguala para tener mismo valor de la area region
                                                    v_areaini_depa = v_areaini_depa2;
                                                }
                                                else
                                                {
                                                    // Dpto con LAGUNA “individual” (no es PUNO)
                                                    // Recorremos lodbtExiste_SupAR para v_areasup_rese1, v_areaini_depa1
                                                    double v_areasup_rese1 = 0.0;
                                                    double v_areaini_depa1 = 0.0;

                                                    foreach (DataRow rowLag in lodbtExiste_SupAR.Rows)
                                                    {
                                                        v_areasup_rese1 = Convert.ToDouble(rowLag["AREASUPER"]);
                                                        v_codigo_depa = rowLag["CODIGO"].ToString();
                                                        v_areaini_depa1 = Convert.ToDouble(rowLag["AREAINI"]);
                                                    }

                                                    // Sumando areas
                                                    if (v_areasup_rese > 0.0)
                                                    {
                                                        DataRow dRow = lodtTabla.NewRow();
                                                        double areaTotal = (v_areaini_depa + v_areaini_depa1);
                                                        double resaTotal = (v_areasup_rese + v_areasup_rese1);

                                                        dRow["CODIGO"] = v_codigo_depa;
                                                        dRow["NOMBRE"] = v_nm_depa1;
                                                        dRow["TP_RESE"] = seleElemento;
                                                        dRow["NM_TPRESE"] = seleElemento;
                                                        dRow["AREA"] = Math.Round(areaTotal, 4).ToString("###,###.0000");
                                                        dRow["CANTI"] = v_cantidad;
                                                        dRow["AREA_NETA"] = Math.Round(resaTotal, 4).ToString("###,###.0000");

                                                        double porcen = 0.0;
                                                        if (areaTotal != 0.0)
                                                            porcen = (resaTotal / areaTotal) * 100.0;

                                                        dRow["PORCEN"] = Math.Round(porcen, 2).ToString("###,###.00");
                                                        lodtTabla.Rows.Add(dRow);

                                                        // Imprimir en sw
                                                        if (v_Zona == "17") sw.WriteLine("7P");
                                                        else if (v_Zona == "18") sw.WriteLine("8P");
                                                        else if (v_Zona == "19") sw.WriteLine("9P");

                                                        sw.WriteLine(v_sistema);
                                                        sw.WriteLine(v_nm_depa1);
                                                        sw.WriteLine(seleElemento);
                                                        sw.WriteLine(Math.Round(areaTotal, 4).ToString("###,###.0000"));
                                                        sw.WriteLine(v_cantidad);
                                                        sw.WriteLine(Math.Round(resaTotal, 4).ToString("###,###.0000"));
                                                        sw.WriteLine(Math.Round(porcen, 2).ToString("###,###.00"));
                                                    }

                                                    // Sumar en la variable principal
                                                    v_areaini_depa += v_areaini_depa1;
                                                }
                                            }
                                            else
                                            {
                                                // (2) Departamentos que no tienen laguna
                                                // ...
                                                // Valida "POSIBLES ZONAS URBANAS", "xANAP INGEMMET", etc.
                                                // (Igual que tu VB, con for cont1 etc.)
                                                if (seleElemento == "POSIBLES ZONAS URBANAS")
                                                {
                                                    // Llamamos FT_Int_tiporesexdepa con code=5 y "ZONA URBANA" (tal como en tu VB)
                                                    DataTable tempSupAR;
                                                    if (v_sistema == "2")
                                                    {
                                                        tempSupAR = dataBaseHandler.GetStatisticalIntersection(
                                                            "5",
                                                            $"DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_{v_Zona}",
                                                            $"DATA_GIS.GPO_CAR_CARAM_WGS_{v_Zona}",
                                                            v_codigo,
                                                            "ZONA URBANA"
                                                        );
                                                    }
                                                    else
                                                    {
                                                        tempSupAR = dataBaseHandler.GetStatisticalIntersection(
                                                            "5",
                                                            $"DATA_GIS.GPO_DEP_DEPARTAMENTO_{v_Zona}",
                                                            $"DATA_GIS.GPO_CAR_CARAM_{v_Zona}",
                                                            v_codigo,
                                                            "ZONA URBANA"
                                                        );
                                                    }

                                                    // Iteramos sobre las filas resultantes
                                                    foreach (DataRow rowZona in tempSupAR.Rows)
                                                    {
                                                        double v_areasup_rese = Convert.ToDouble(rowZona["AREASUPER"]);
                                                        string v_codigo_depa = rowZona["CODIGO"].ToString();
                                                        double v_areaini_depa = Convert.ToDouble(rowZona["AREAINI"]);
                                                        double v_cantidad = Convert.ToDouble(rowZona["CANTIDAD"]);

                                                        if (v_cantidad > 0)
                                                        {
                                                            // Creamos la fila en lodtTabla
                                                            DataRow dRow = lodtTabla.NewRow();
                                                            dRow["CODIGO"] = v_codigo_depa;
                                                            dRow["NOMBRE"] = v_nm_depa1;
                                                            dRow["TP_RESE"] = "ZONA URBANA";
                                                            dRow["NM_TPRESE"] = seleElemento; // "POSIBLES ZONAS URBANAS" era el string en cbotiporese
                                                            dRow["AREA"] = Math.Round(v_areaini_depa, 4).ToString("###,###.0000");
                                                            dRow["CANTI"] = v_cantidad;
                                                            dRow["AREA_NETA"] = Math.Round(v_areasup_rese, 4).ToString("###,###.0000");

                                                            double porcen = 0.0;
                                                            if (v_areaini_depa != 0.0)
                                                                porcen = (v_areasup_rese / v_areaini_depa) * 100.0;

                                                            dRow["PORCEN"] = Math.Round(porcen, 2).ToString("###,###.00");
                                                            lodtTabla.Rows.Add(dRow);

                                                            // Escribimos en el StreamWriter
                                                            if (v_Zona == "17") sw.WriteLine("7P");
                                                            else if (v_Zona == "18") sw.WriteLine("8P");
                                                            else if (v_Zona == "19") sw.WriteLine("9P");

                                                            sw.WriteLine(v_sistema);
                                                            sw.WriteLine(v_nm_depa1);
                                                            sw.WriteLine(seleElemento);
                                                            sw.WriteLine(Math.Round(v_areaini_depa, 4).ToString("###,###.0000"));
                                                            sw.WriteLine(v_cantidad);
                                                            sw.WriteLine(Math.Round(v_areasup_rese, 4).ToString("###,###.0000"));
                                                            sw.WriteLine(Math.Round(porcen, 2).ToString("###,###.00"));
                                                        }
                                                    }
                                                }
                                                else if (seleElemento == "xANAP INGEMMET")
                                                {
                                                    // Llamada con code=7 y "ANAP"
                                                    DataTable tempSupAR;
                                                    if (v_sistema == "2")
                                                    {
                                                        tempSupAR = dataBaseHandler.GetStatisticalIntersection(
                                                            "7",
                                                            $"DATA_GIS.GPO_DEP_DEPARTAMENTO_WGS_{v_Zona}",
                                                            $"DATA_GIS.GPO_CAR_CARAM_WGS_{v_Zona}",
                                                            v_codigo,
                                                            "ANAP"
                                                        );
                                                    }
                                                    else
                                                    {
                                                        tempSupAR = dataBaseHandler.GetStatisticalIntersection(
                                                            "7",
                                                            $"DATA_GIS.GPO_DEP_DEPARTAMENTO_{v_Zona}",
                                                            $"DATA_GIS.GPO_CAR_CARAM_{v_Zona}",
                                                            v_codigo,
                                                            "ANAP"
                                                        );
                                                    }

                                                    foreach (DataRow rowInge in tempSupAR.Rows)
                                                    {
                                                        double v_areasup_rese_inge = Convert.ToDouble(rowInge["AREASUPER"]);
                                                        string v_codigo_depa = rowInge["CODIGO"].ToString();
                                                        double v_areaini_depa = Convert.ToDouble(rowInge["AREAINI"]);
                                                        double v_cantidad_inge = Convert.ToDouble(rowInge["CANTIDAD"]);

                                                        if (v_cantidad_inge > 0)
                                                        {
                                                            // Creamos la fila "ANAP"
                                                            DataRow dRow = lodtTabla.NewRow();
                                                            dRow["CODIGO"] = v_codigo_depa;
                                                            dRow["NOMBRE"] = v_nm_depa1;
                                                            dRow["TP_RESE"] = "ANAP";
                                                            dRow["NM_TPRESE"] = seleElemento; // "xANAP INGEMMET"
                                                            dRow["AREA"] = Math.Round(v_areaini_depa, 4).ToString("###,###.0000");
                                                            dRow["CANTI"] = v_cantidad_inge;
                                                            dRow["AREA_NETA"] = Math.Round(v_areasup_rese_inge, 4).ToString("###,###.0000");

                                                            double porcen = 0.0;
                                                            if (v_areaini_depa != 0.0)
                                                                porcen = (v_areasup_rese_inge / v_areaini_depa) * 100.0;

                                                            dRow["PORCEN"] = Math.Round(porcen, 2).ToString("###,###.00");
                                                            lodtTabla.Rows.Add(dRow);

                                                            // Escribimos en sw
                                                            if (v_Zona == "17") sw.WriteLine("7P");
                                                            else if (v_Zona == "18") sw.WriteLine("8P");
                                                            else if (v_Zona == "19") sw.WriteLine("9P");

                                                            sw.WriteLine(v_sistema);
                                                            sw.WriteLine(v_nm_depa1);
                                                            sw.WriteLine(seleElemento);
                                                            sw.WriteLine(Math.Round(v_areaini_depa, 4).ToString("###,###.0000"));
                                                            sw.WriteLine(v_cantidad_inge);
                                                            sw.WriteLine(Math.Round(v_areasup_rese_inge, 4).ToString("###,###.0000"));
                                                            sw.WriteLine(Math.Round(porcen, 2).ToString("###,###.00"));
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // (PROCESO NORMAL)
                                                    // Aquí, por ejemplo, "ZONA URBANA", "ANAP", o cualquier otra reserva sin laguna
                                                    // Recorremos lodbtExiste_SupAR y creamos filas con la misma lógica

                                                    // En tu VB: "If sele_elemento = 'ZONA URBANA' Then code=6" 
                                                    //           "If sele_elemento = 'ANAP' Then restar v_areasup_rese_inge"
                                                    // 
                                                    // Suponiendo que lodbtExiste_SupAR es la tabla que ya obtuviste:
                                                    foreach (DataRow rowNor in lodbtExiste_SupAR.Rows)
                                                    {
                                                        double v_areasup_rese = Convert.ToDouble(rowNor["AREASUPER"]);
                                                        string v_codigo_depa = rowNor["CODIGO"].ToString();
                                                        double v_areaini_depa = Convert.ToDouble(rowNor["AREAINI"]);
                                                        double v_cantidad = Convert.ToDouble(rowNor["CANTIDAD"]);

                                                        if (v_cantidad > 0)
                                                        {
                                                            // Si era "ANAP", se podía restar v_areasup_rese_inge, etc.
                                                            // (Solo si así lo definiste en tu VB)
                                                            //if (seleElemento == "ANAP")
                                                            //{
                                                            //    // Ajustar con variables globales v_areasup_rese_inge, v_cantidad_inge
                                                            //    v_areasup_rese = v_areasup_rese - v_areasup_rese_inge;
                                                            //    v_cantidad = v_cantidad - v_cantidad_inge;
                                                            //}

                                                            // Creamos la fila
                                                            DataRow dRow = lodtTabla.NewRow();
                                                            dRow["CODIGO"] = v_codigo_depa;
                                                            dRow["NOMBRE"] = v_nm_depa1;
                                                            dRow["TP_RESE"] = seleElemento;
                                                            dRow["NM_TPRESE"] = seleElemento;
                                                            dRow["AREA"] = Math.Round(v_areaini_depa, 4).ToString("###,###.0000");
                                                            dRow["CANTI"] = v_cantidad;
                                                            dRow["AREA_NETA"] = Math.Round(v_areasup_rese, 4).ToString("###,###.0000");

                                                            double porcen = 0.0;
                                                            if (v_areaini_depa != 0.0)
                                                                porcen = (v_areasup_rese / v_areaini_depa) * 100.0;

                                                            dRow["PORCEN"] = Math.Round(porcen, 2).ToString("###,###.00");
                                                            lodtTabla.Rows.Add(dRow);

                                                            // Escribimos en sw
                                                            if (v_Zona == "17") sw.WriteLine("7P");
                                                            else if (v_Zona == "18") sw.WriteLine("8P");
                                                            else if (v_Zona == "19") sw.WriteLine("9P");

                                                            sw.WriteLine(v_sistema);
                                                            sw.WriteLine(v_nm_depa1);
                                                            sw.WriteLine(seleElemento);
                                                            sw.WriteLine(Math.Round(v_areaini_depa, 4).ToString("###,###.0000"));
                                                            sw.WriteLine(v_cantidad);
                                                            sw.WriteLine(Math.Round(v_areasup_rese, 4).ToString("###,###.0000"));
                                                            sw.WriteLine(Math.Round(porcen, 2).ToString("###,###.00"));
                                                        }
                                                    }
                                                }
                                            }
                                        } // fin else
                                    } // fin for CbxTypeMineNo
                                }
                                catch (Exception ex)
                                {
                                    // VB: Catch ex As Exception
                                    // Se tragaba la excepción, 
                                    // al menos la imprimimos:
                                    Console.WriteLine("Error en bloque normal: " + ex.Message);
                                }
                            }
                        }
                    }
                } // fin while
            });
        }
        private async Task CalcularMineriaNacionalNo(StreamWriter sw, DataTable lodtTabla)
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


                    Params = Geoprocessing.MakeValueArray("Caram");
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
                        v_area_f1 = 0.0;
                        //v_porcen_f1 = 0.0;
                        //v_area_total = areaini_depa_local;

                        // Escribes en sw
                        sw.WriteLine("8P");
                        sw.WriteLine(vDatum);
                        sw.WriteLine(nombre_depa);
                        sw.WriteLine("RED VIAL NACIONAL");
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

        private void DataGridResult_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            // Verificar si la columna es la columna de índice
            if (e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Integer && e.IsGetData)
            {
                // Asignar el índice de la fila
                e.Value = e.ListSourceRowIndex + 1; // Los índices son base 0, así que sumamos 1
            }
        }

        private void ConfigureDataGridResultColumns()
        {
            // Obtener la vista principal del GridControl
            var tableView = DataGridResult.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DataGridResult.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = true; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn indexColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.Headers.Index, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNames.Index,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.Widths.Index
            };
            GridColumn selectColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesEstadisticasAR.Select,
                Header = DatagridResultConstantsDM.HeadersEstadisticasAR.Select,
                Width = DatagridResultConstantsDM.WidthsEstadisticasAR.Select,
                EditSettings = new CheckEditSettings
                {
                    IsThreeState = false,     // Permite un estado indeterminado (null)
                    AllowNullInput = true,   // Permite valores nulos (indeterminado)
                }

            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Codigo, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.Headers.Codigo,    // Encabezado visible
                Width = DatagridResultConstantsDM.Widths.Codigo,            // Ancho de la columna
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };

            GridColumn nombreColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNames.Nombre,
                Header = DatagridResultConstantsDM.Headers.Nombre,
                Width = DatagridResultConstantsDM.Widths.Nombre,
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };

            GridColumn tpResetColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesEstadisticasAR.TpReset,
                Header = DatagridResultConstantsDM.HeadersEstadisticasAR.TpReset,
                Width = DatagridResultConstantsDM.WidthsEstadisticasAR.TpReset,
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };

            GridColumn nmTpResecColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesEstadisticasAR.NmTprese,
                Header = DatagridResultConstantsDM.HeadersEstadisticasAR.NmTprese,
                Width = DatagridResultConstantsDM.WidthsEstadisticasAR.NmTprese,
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };

            GridColumn areaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesEstadisticasAR.Area,
                Header = DatagridResultConstantsDM.HeadersEstadisticasAR.Area,
                Width = DatagridResultConstantsDM.WidthsEstadisticasAR.Area,
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };
            GridColumn cantiColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesEstadisticasAR.Canti,
                Header = DatagridResultConstantsDM.HeadersEstadisticasAR.Canti,
                Width = DatagridResultConstantsDM.WidthsEstadisticasAR.Canti,
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };
            GridColumn areaNetaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesEstadisticasAR.AreaNeta,
                Header = DatagridResultConstantsDM.HeadersEstadisticasAR.AreaNeta,
                Width = DatagridResultConstantsDM.WidthsEstadisticasAR.AreaNeta,
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };
            GridColumn porcenColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesEstadisticasAR.Porcen,
                Header = DatagridResultConstantsDM.HeadersEstadisticasAR.Porcen,
                Width = DatagridResultConstantsDM.WidthsEstadisticasAR.Porcen,
                AllowEditing = DevExpress.Utils.DefaultBoolean.False
            };

            // Agregar columnas al GridControl
            DataGridResult.Columns.Add(indexColumn);
            DataGridResult.Columns.Add(selectColumn);
            DataGridResult.Columns.Add(codigoColumn);
            DataGridResult.Columns.Add(nombreColumn);
            DataGridResult.Columns.Add(tpResetColumn);
            DataGridResult.Columns.Add(nmTpResecColumn);
            DataGridResult.Columns.Add(areaColumn);
            DataGridResult.Columns.Add(cantiColumn);
            DataGridResult.Columns.Add(areaNetaColumn);
            DataGridResult.Columns.Add(porcenColumn);
        }

        private async void BtnMineSi_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarUtils progressBar = new ProgressBarUtils("Calculando estadisticas de Mineria Si...");
            progressBar.Show();
            try
            {
                // Por buenas prácticas, la mayor parte de la lógica GIS 
                // se ejecuta dentro de un QueuedTask:
                await QueuedTask.Run(async () =>
                {
                    await CalcularAreasMinSiAsync();
                });

                // Mensaje final
                MessageBox.Show("El proceso ha finalizado satisfactoriamente.",
                                "Estadísticas de Áreas Restringidas",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                progressBar.Dispose();
            }
            progressBar.Dispose();
        }

        private async Task CalcularAreasMinSiAsync()
        {

            // Prepara DataTable para resultados:
            DataTable lodtTabla = CrearDataTableResultados();

            // Abre archivos de salida:
            string ruta;
            string ruta1;
            ruta = Path.Combine(globalPath, "reporteMIN.txt");
            ruta1 = Path.Combine(globalPath, "reportearqMIN.txt");

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
                        await CalcularMineriaNacionalSi(sw, lodtTabla);
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
            string xmlPath = Path.Combine(globalPath, "arestringidaSi.xml");
            DataSet ds = new DataSet();
            ds.Tables.Add(lodtTabla.Copy());
            // Guardar a XML
            ds.WriteXml(xmlPath);
            Dispatcher.Invoke(() =>
            {
                calculatedIndex(DataGridResult, lodtTabla.Rows.Count, DatagridResultConstantsDM.ColumNames.Index);
                DataGridResult.ItemsSource = lodtTabla.Copy().DefaultView;
            });
        }

        private async Task CalcularMineriaNacionalSi(StreamWriter sw, DataTable lodtTabla)
        {
            await QueuedTask.Run(async () =>
            {
                try
                {
                    // Variables iniciales, según la lógica original:
                    vZona = "18";       // Se fija en "18"
                    vCodigo = "PERU";   // Se asigna "PERU"
                    //vDatum =  // vDatum    "WGS_84"

                    // Recorremos los ítems de CbxTypeMineNo.
                    for (int contador2 = 0; contador2 < CbxTypeMineYes.Items.Count; contador2++)
                    {
                        string sele_elemento = CbxTypeMineYes.Items[contador2].ToString();

                        lodbtExiste_SupAR = FeatureClassLoader.GetDataForElemento(sele_elemento, vDatum, vZona, vCodigo);
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
                        // Emulamos esa lógica:
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
                        string sele_elemento = "POSIBLE ZONA URBANA";
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
                    var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetMineSiPu, Params);

                    var fLayer = MapView.Active.Map.FindLayers("pu").FirstOrDefault() as FeatureLayer;

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

                        // 5) Crear la fila en "lodtTabla"
                        DataRow dRow = lodtTabla.NewRow();
                        dRow["CODIGO"] = nombre_depa;
                        dRow["NOMBRE"] = nombre_depa;
                        dRow["TP_RESE"] = "POSIBLE ZONA URBANA";
                        dRow["NM_TPRESE"] = "POSIBLE ZONA URBANA";

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
                        sw.WriteLine("POSIBLE ZONA URBANA");
                        sw.WriteLine(Math.Round(areaini_depa_local, 4).ToString("###,###.0000"));
                        sw.WriteLine(cantidad_local);
                        sw.WriteLine(Math.Round(v_area_f1, 4).ToString("###,###.0000"));
                        sw.WriteLine(Math.Round(porcentaje, 2).ToString("###,###.00"));
                    }
                    else
                    {
                        // Si no hay features en esa capa shapefile,

                        v_area_f1 = 0.0;
                        //v_porcen_f1 = 0.0;
                        //v_area_total = areaini_depa_local;

                        // Escribes en sw
                        sw.WriteLine("8P");
                        sw.WriteLine(vDatum);
                        sw.WriteLine(nombre_depa);
                        sw.WriteLine("POSIBLE ZONA URBANA");
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
                    Debug.WriteLine($"Error en CalcularMineriaNacionalSi: {ex.Message}");
                }
            });
        }

        private void BtnLoadData_Click(object sender, RoutedEventArgs e)
        {
            // Declaraciones usadas en VB
            decimal s1 = 0;
            decimal s2 = 0;
            DataTable dt = null;
            try
            {
                // Comprobamos si existe arestringida.xml
                string xmlPath = Path.Combine(globalPath, "arestringida.xml");
                if (!File.Exists(xmlPath))
                {
                    MessageBox.Show(
                        "NO existe información previa para cargar Áreas Restringidas, procesar...",
                        "Estadísticas..",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    return;
                }

                // Si existe, lo leemos con DataSet
                DataSet ds = new DataSet();
                ds.ReadXml(xmlPath);

                // Asumimos que ds.Tables[0] contiene la tabla
                dt = ds.Tables[0];

                // Asignamos al DataGrid
                // En WPF DataGrid = ItemsSource en lugar de DataSource
                DataGridResult.ItemsSource = dt.DefaultView;

                // Llamamos a funciones auxiliares (equivalentes en C#)
                //PT_Agregar_Funciones_EVAL();
                //PT_Forma_Grilla_EVAL();

                // chkEstado.Checked = False : chkEstado.Checked = True
                // En WPF => IsChecked
                //chkEstado.IsChecked = false;
                //chkEstado.IsChecked = true;

                // For i As Integer = 0 To Me.dgdDetalle.RowCount - 1
                // dgdDetalle.Item(i, "SELEC") = True
                // En WPF, para asignar, iteramos sobre dt Rows:
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dt.Rows[i]["SELEC"] = "True";
                }

                // No existe "AllowUpdate" en WPF DataGrid. 
                // Podrías usar dgdDetalle.IsReadOnly = false; si deseas edición
                // dgdDetalle.Focus();
                DataGridResult.Focus();

                // Sumamos valores (s1, s2)
                // En VB: s1 = dt.Rows(r).Item("AREA"), s2 = dt.Rows(r).Item("AREA_NETA")
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    // s1 = dt.Rows(r).Item("AREA")
                    // pero en el VB lo sobrescribía: s1 = dt.Rows(r).Item("AREA")
                    // y no lo sumaba. Ajusta la lógica si es un error en VB
                    s1 = Convert.ToDecimal(dt.Rows[r]["AREA"]);

                    // s2 = s2 + dt.Rows(r).Item("AREA_NETA")
                    s2 += Convert.ToDecimal(dt.Rows[r]["AREA_NETA"]);
                }

                // txtArea1.Text = Format(s1, "###,###,###,###.####")
                TbxAreaTotal.Text = String.Format("{0:###,###,###,###.####}", s1);

                // txtArea2.Text = Format(s2, "###,###,###,###.####")
                TbxAreaTotalAres.Text = String.Format("{0:###,###,###,###.####}", s2);

                // txtPorcentaje.Text = Format((s2 / s1) * 100, "###,###,###,###.##")
                if (s1 != 0)
                {
                    decimal porcentaje = (s2 / s1) * 100;
                    TbxAreaAres.Text = String.Format("{0:###,###,###,###.##}", porcentaje);
                }
                else
                {
                    TbxAreaAres.Text = "0";
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepción
                // En VB estaba vacío, aquí podemos mostrar un mensaje si deseas
                MessageBox.Show("Error: " + ex.Message, "Excepción",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Me.btnExcel.Enabled = True
            BtnExportExcel.IsEnabled = true;
        }

        private async void CbxRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Inhabilitamos el botón calcular.
            BtnMineNo.IsEnabled = false;

            // Limpiamos items del combo cboZona
            CbxZone.Items.Clear();

            // Cargamos la FeatureClass de departamento
            await InitConectionGdb();
            var pFeatureClass = await LayerUtils.GetFeatureClass(geodatabase, FeatureClassConstants.gstrFC_Departamento_WGS + 18);
            // Obtener el departamento seleccionado en un ComboBox WPF
            string v_nm_depa = CbxRegion.SelectedItem as string;
            if (string.IsNullOrEmpty(v_nm_depa))
            {
                // Si no se seleccionó nada, salir
                return;
            }

            DataTable lodbtZona = new DataTable();
            DataTable lodbtUbigeo = new DataTable();

            // Filtro
            string lo_Filtro_Ubigeo = $"NM_DEPA = '{v_nm_depa.ToUpper()}'";
            lodbtUbigeo = dataBaseHandler.SelectByUbigeo("1", v_nm_depa.ToUpper());

            if (lodbtUbigeo.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No existe registro en la capa, verificar datos",
                    "[SIGCATMIN]",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                return;
            }

            // Tomar el primer UBIGEO
            string ubigeo = lodbtUbigeo.Rows[0]["UBIGEO"].ToString();
            lo_Filtro_Ubigeo = $"CD_DEPA = '{ubigeo}'";

            // Consultar las Zonas
            lodbtZona = dataBaseHandler.SelectByZone(ubigeo + "0000");

            // Si hay 2 registros
            if (lodbtZona.Rows.Count == 2)
            {
                // - Si 0 => Muestra mensaje y return
                // - Si 2 => Lógica
                // - Si 3 => Lógica
                // (Si realmente solo hay 2 filas, no habrá 3, pero copiamos tu lógica)

                // a) Si 0 => Salir
                if (lodbtZona.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No existe la capa, ingrese nuevamente",
                        "[SIGCATMIN]",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    return;
                }
                // b) Si 2 => Ajustamos combo y Label
                else if (lodbtZona.Rows.Count == 2)
                {
                    // Habilitamos combo
                    CbxZone.IsEnabled = true;

                    TbxZone.Text = "Zona: ";

                    // Añadir items
                    for (int w = 0; w < lodbtZona.Rows.Count; w++)
                    {
                        string zona_sele = lodbtZona.Rows[w]["DESCRIPCION"].ToString();
                        CbxZone.Items.Add(zona_sele);
                    }

                    if (CbxZone.Items.Count > 1)
                    {
                        CbxZone.SelectedIndex = 1;
                        CbxZone.IsEnabled = false;
                    }
                    BtnMineNo.IsEnabled = true;
                    // Actualizar segun SelectedItem
                }
                // c) Si 3 => Ajustamos combo y Label
                else if (lodbtZona.Rows.Count == 3)
                {
                    //TbxZone.Margin = new Thickness(210, 60, 0, 0);
                    CbxZone.Visibility = Visibility.Visible;
                    CbxZone.IsEnabled = true;
                    TbxZone.Visibility = Visibility.Visible;

                    TbxZone.Text = "Departamento seleccionado se encuentra \r\nentre 2 ZONAS, seleccione una ZONA";

                    for (int w = 0; w < lodbtZona.Rows.Count; w++)
                    {
                        string zona_sele = lodbtZona.Rows[w]["DESCRIPCION"].ToString();
                        CbxZone.Items.Add(zona_sele);
                    }

                    CbxZone.IsEnabled = true;
                }
            }
            else
            {
                // else: Si lodbtZona.Rows.Count != 2
                // TbxZone.Margin = new Thickness(210, 60, 0, 0);
                CbxZone.Visibility = Visibility.Visible;
                CbxZone.IsEnabled = true;
                TbxZone.Visibility = Visibility.Visible;
                TbxZone.Text = "Departamento seleccionado se encuentra \r\nentre 2 ZONAS, seleccione una ZONA";

                for (int w = 0; w < lodbtZona.Rows.Count; w++)
                {
                    string zona_sele = lodbtZona.Rows[w]["DESCRIPCION"].ToString();
                    CbxZone.Items.Add(zona_sele);
                }

                if (CbxZone.Items.Count > 1)
                {
                    CbxZone.SelectedIndex = 1;
                    //cls_Catastro.Actualizar_DM(CbxZone.SelectedItem);
                }
                BtnMineNo.IsEnabled = true;
                // v_Zona = cboZona.SelectedItem?.ToString();
            }
        }

        private void LoadComboBoxYesNo()
        {
            CbxTypeMineNo.Items.Clear();
            CbxTypeMineYes.Items.Clear();
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

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportDataGridToExcel(DataGridResult, Path.Combine(GlobalVariables.ContaninerTemplatesReports, "Plantilla_ARES_Nacional_MineriaNo.xlsx"), Path.Combine(GlobalVariables.pathFileTemp,"ARES_Nacional_MineriaNo.xlsx"));
        }
        public static void ExportDataGridToExcel(GridControl dataGrid, string plantillaPath, string outputPath)
        {
            try
            {
                // Verifica si el DataGrid tiene elementos
                if (dataGrid.VisibleRowCount == 0)
                {
                    MessageBox.Show("El DataGrid está vacío. No hay datos para exportar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Cargar la plantilla de Excel
                FileInfo fileInfo = new FileInfo(plantillaPath);
                if (!fileInfo.Exists)
                {
                    MessageBox.Show("La plantilla de Excel no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Obtiene la primera hoja

                    int filaInicio = 6; // Comenzar desde la fila 2
                    int columnaInicio = 3; // Columna C (índice 2 en EPPlus)

                    // Lista de campos que se deben exportar
                    string[] columnasAExportar = { "TP_RESE", "CANTI", "AREA_NETA", "PORCEN" };
                    // Filtrar las columnas del GridControl que se van a exportar
                    var columnasFiltradas = dataGrid.Columns
                        .Where(col => columnasAExportar.Contains(col.FieldName))
                        .ToList();
                    // Obtener los datos del GridControl y almacenarlos en una lista de diccionarios
                    List<Dictionary<string, object>> datos = new List<Dictionary<string, object>>();
                    // Recorrer las filas del DataGrid
                    for (int i = 0; i < dataGrid.VisibleRowCount; i++)
                    {
                        var row = dataGrid.GetRowHandleByVisibleIndex(i);
                        if (!dataGrid.IsValidRowHandle(row)) continue;

                        int colIndex = columnaInicio;
                        var fila = new Dictionary<string, object>();
                        foreach (var column in columnasFiltradas)
                        {
                            fila[column.FieldName] = dataGrid.GetCellValue(row, column) ?? "";
                        }

                        datos.Add(fila);
                        //foreach (var column in columnasFiltradas)
                        //{
                        //    if (column.Visible)
                        //    {
                        //        // Obtener el valor de la celda
                        //        var cellValue = dataGrid.GetCellValue(row, column);
                        //        worksheet.Cells[filaInicio + i, colIndex].Value = cellValue?.ToString() ?? "";
                        //        colIndex++;
                        //    }
                        //}
                    }
                    // Ordenar los datos alfabéticamente por la columna "TP_RESET"
                    datos = datos.OrderBy(d => d["TP_RESE"].ToString()).ToList();

                    // Insertar los datos ordenados en la hoja de Excel
                    for (int i = 0; i < datos.Count; i++)
                    {
                        int colIndex = columnaInicio;

                        foreach (var column in columnasFiltradas)
                        {
                            worksheet.Cells[filaInicio + i, colIndex].Value = datos[i][column.FieldName].ToString();
                            colIndex++;
                        }
                    }

                    // Guardar el archivo en el outputPath
                    package.SaveAs(new FileInfo(outputPath));

                    MessageBox.Show("Datos exportados exitosamente a Excel.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
