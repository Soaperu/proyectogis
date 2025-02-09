using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ArcGIS.Core.Data;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Mapping.Events;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.Xpf.Grid;
using SigcatminProAddin.Utils.UIUtils;
using FlowDirection = System.Windows.FlowDirection;
using System.Text.RegularExpressions;
using SigcatminProAddin.Models;
using SigcatminProAddin.Models.Constants;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.CIM;
using DevExpress.Xpf.Grid.GroupRowLayout;
using DevExpress.DataAccess.Native.Web;
using Newtonsoft.Json;
using CommonUtilities.ArcgisProUtils.Models;

//using DevExpress.CodeParser;
//using DevExpress.XtraRichEdit.Commands;
//using DevExpress.XtraCharts.Native;
using ArcGIS.Desktop.Core.Utilities;
using DevExpress.XtraCharts.Native;
using DevExpress.Charts.Native;
using DevExpress.Utils.CommonDialogs;



namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para MantoAreaRestringida.xaml
    /// </summary>
    public partial class MantoAreaRestringida : Page
    {
        private FeatureClassLoader featureClassLoader;
        public Geodatabase geodatabase;
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;

        // Definición de nombres de campos
        int opt;
        private string _FIELD_CG_CODIGO = "CODIGO";
        private string _FIELD_PROCESAR = "PROC";
        private string _FIELD_RE_CODEST = "CODEST";
        private string _FIELD_RE_SECUEN = "ID";
        private string _FIELD_RE_ARCGRA = "ARCHIVO";
        private string _FIELD_RE_MODREG = "MODREG";
        private string _FIELD_RE_INDICA = "MINERIA";
        private string _FIELD_RE_ZONA = "ZONA";
        private string _FIELD_RE_SELECT = "SEL.";
        private string _FIELD_RE_CLASE = "CLASE";

        // Valores de campos
        private string _ANDE = "DELETE";

        // Nombres de botones
        private string _BT_NAME_PROC = "Procesar";
        private string _BT_NAME_CORR = "Corregido";

        // Estados
        private int _EST_NOPROCTEMP = 0;
        private int _EST_PROCTEMP = 1;
        private int _EST_NOPROCPROD = 2;
        private int _EST_PROCPROD = 3;
        private int _EST_PROCESADO = 4;
        private int _EST_ERRTEMP = 96;
        private int _EST_ERRPROD = 97;
        private int _EST_LOADTEMP = 98;
        private int _EST_LOADPROD = 99;

        // Opciones de filtros
        private int _FILT_PROCTEMP = 1;
        private int _FILT_PROCPROD = 2;
        private int _FILT_PROCESADO = 3;
        private int _FILT_ERRORES = 4;
        private int _FILT_DATUM_WGS = 2;
        private int _FILT_ARE_RESE = 1;
        private int _FILT_ARE_URBA = 2;

        // Filtros
        public int _OPT_DATUM = 1;
        public int _OPT_ZONA = 2;
        public int _OPT_FILTRO = 3;
        public int _OPT_FILTRO_CREG = 4;
        public int _OPT_FILTRO_ENV = 5;
        public int _OPT_FILTRO_FEATURES = 6;

        // Tags para radio buttons de configuración
        public int _TAG_CURRENT = 3;
        public int _TAG_EXEENDDAY = 4;
        public int _TAG_NOEXE = 5;

        public MantoAreaRestringida()
        {
            InitializeComponent();
            ConfigureDataGridResultColumns();
            ConfigureDataGridClassTemporales();
            ConfigureDataGridClassProduccion();
            ConfigureDataGridReseDifReg();
            ConfigureDataGridResHistorico();
            ConfigureDataGridAreaReservada();
            ConfigureDataGridDemarca();
            ConfigureDataGridCartaIGN();

            dataBaseHandler = new DatabaseHandler();
            cargar_combo();
        }

        //private void btnResePath_Click(object sender, RoutedEventArgs e)
        //{
        //    // Crear una nueva instancia del FolderBrowserDialog
        //    using (IFolderBrowserDialog folderDialog = new FolderBrowserDialog())
        //    {
        //        // Establecer el título del diálogo
        //        folderDialog.Description = "Selecciona una Carpeta";

        //        // Mostrar el diálogo
        //        DialogResult result = folderDialog.ShowDialog();

        //        // Si el usuario selecciona una carpeta (no cancela)
        //        if (result == DialogResult.OK)
        //        {
        //            // Asignar la ruta de la carpeta seleccionada al TextBox
        //            txtResePath.Text = folderDialog.SelectedPath;
        //        }
        //    }
        //}





        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //CbxFeatures.SelectedIndex = 1;
            //CbxEnv.SelectedIndex = 1;
            var dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_FILTRO_CREG.ToString());
            GetOptionCBox(CbxFiltroControlReg, dmrRecords);

            dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_FILTRO_ENV.ToString());
            GetOptionCBox(CbxEnv, dmrRecords);
            cambiaOpcionesCboxFeatures();

            /**/
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
            cbp.Add(new ComboBoxPairs("--Seleccionar--", 0));
            cbp.Add(new ComboBoxPairs("FLAT1065", 1));
            cbp.Add(new ComboBoxPairs("CARI0213", 2));
            cbp.Add(new ComboBoxPairs("WVAL0398", 3));
            cbp.Add(new ComboBoxPairs("JFLO0589", 4));
            cbp.Add(new ComboBoxPairs("CQUI1819", 5));

            // Asignar la lista al ComboBox
            CbxUsuario.DisplayMemberPath = "_Key";
            CbxUsuario.SelectedValuePath = "_Value";
            CbxUsuario.ItemsSource = cbp;


            List<ComboBoxPairs> cbpF = new List<ComboBoxPairs>();
            cbpF.Add(new ComboBoxPairs("--Seleccionar--", 0));
            cbpF.Add(new ComboBoxPairs("Procesar Intermedia", 1));
            cbpF.Add(new ComboBoxPairs("Procesar Producción", 2));
            cbpF.Add(new ComboBoxPairs("Procesados", 3));
            cbpF.Add(new ComboBoxPairs("Errores", 4));
            // Asignar la lista al ComboBox
            CbxFiltro.DisplayMemberPath = "_Key";
            CbxFiltro.SelectedValuePath = "_Value";
            CbxFiltro.ItemsSource = cbpF;

            /**/


            CbxUsuario.SelectedIndex = 1;
            CbxFiltro.SelectedIndex = 1;

            CbxFiltroControlReg.SelectedIndex = 1;
            CbxEnv.SelectedIndex = 1;
            CbxFeatures.SelectedIndex = 1;
            CbxFiltroTipo.SelectedIndex = 1;


        }

        private void DataGridResult_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {

        }

        private void DataGridResultTableView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var tableView = sender as DevExpress.Xpf.Grid.TableView;
            if (tableView != null && tableView.Grid.VisibleRowCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                int focusedRowHandle = tableView.FocusedRowHandle;

                if (focusedRowHandle >= 0) // Verifica si hay una fila seleccionada
                {
                    // Obtener el valor de una columna específica (por ejemplo, "CODIGO")
                    if (DataGridResult.GetCellValue(focusedRowHandle, _FIELD_RE_MODREG)?.ToString() == _ANDE)
                    {

                    }
                    else
                    {

                    }
                    string idReg = DataGridResult.GetCellValue(focusedRowHandle, _FIELD_RE_SECUEN)?.ToString();
                    obtenerDetalle(idReg);

                    if (opt == _FILT_ERRORES)
                    {
                        var estado = int.Parse(DataGridResult.GetCellValue(focusedRowHandle, _FIELD_RE_SECUEN)?.ToString());
                        if (estado == _EST_ERRPROD)
                        {

                        }
                        else
                        {

                        }
                    }

                    ////'Si la columna PROCESAR no existe el evento debe finalizar
                    //if (!DataGridResult.Columns.Any(c => c.FieldName == _FIELD_PROCESAR))
                    //{
                    //    return;
                    //}
                    ///*****/


                    //var focusedRowData = e.NewRow as YourDataModel; // Asegúrate de que 'YourDataModel' es el tipo de datos que usas en la grilla

                    //if (focusedRowData != null)
                    //{
                    //    // Cambiar el color de la fila seleccionada
                    //    var focusedRowHandle = e.NewRowHandle;
                    //    DataGridResultTableView.SetRowCellValue(focusedRowHandle, "Background", new SolidColorBrush(Colors.Blue)); // Cambiar color a azul
                    //}





                    //'Si la columna a PROCESAR es seleccionada se debe esperar a que se cambie el checkbox para avanzar 
                    //if (e.Column != null && e.Column.FieldName == _FIELD_PROCESAR)

                    //{
                    //    DataGridResult.View.FocusedRowHandle = focusedRowHandle;
                    //    DataGridResult.View.PostEditor(); // Asegura que la edición termine
                    //    //obtenerDetalle(idreg);
                    //}
                }
            }
        }


        // Maneja el evento RowStyle del GridView
        private void DataGridResult_RowStyle(object sender, RowEventArgs e)
        {
            // Verificar que la fila no sea un encabezado (rowHandle >= 0)
            if (e.RowHandle >= 0)
            {
                // Obtener el valor de la celda en la columna "Status"
                var statusObj = DataGridResult.GetCellValue(e.RowHandle, "Status");

                // Asegurarse de que el valor no sea nulo o DBNull
                if (statusObj != null && statusObj != DBNull.Value)
                {
                    string status = statusObj.ToString();

                    // Aplicar el color de fondo dependiendo del valor de la celda "Status"
                    if (status.Equals("Active"))
                    {
                        // Pintar la fila de verde si el valor de "Status" es "Active"
                        //e.Appearance.BackColor = System.Windows.Media.Colors.Green;

                        //DataGridResult.Background = System.Windows.Media.Colors.LightGray;

                        //e.Appearance.Foreground = System.Windows.Media.Colors.White;  // Cambiar el color del texto a blanco
                    }
                    else if (status.Equals("Inactive"))
                    {
                        // Pintar la fila de gris si el valor de "Status" es "Inactive"
                        //e.Appearance.BackColor = System.Windows.Media.Colors.Gray;
                        //e.Appearance.Foreground = System.Windows.Media.Colors.White;  // Cambiar el color del texto a blanco
                    }
                }
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
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn idColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersMantoAR.Id, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Id,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsMantoAR.Id
            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Codigo, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.HeadersMantoAR.Codigo,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsMantoAR.Codigo            // Ancho de la columna
            };

            GridColumn zonaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Zona,
                Header = DatagridResultConstantsDM.HeadersMantoAR.Zona,
                Width = DatagridResultConstantsDM.WidthsMantoAR.Zona
            };

            GridColumn claseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Clase,
                Header = DatagridResultConstantsDM.HeadersMantoAR.Clase,
                Width = DatagridResultConstantsDM.WidthsMantoAR.Clase
            };

            GridColumn archivoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Archivo,
                Header = DatagridResultConstantsDM.HeadersMantoAR.Archivo,
                Width = DatagridResultConstantsDM.WidthsMantoAR.Archivo
            };
            GridColumn modRegColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.ModReg,
                Header = DatagridResultConstantsDM.HeadersMantoAR.ModReg,
                Width = DatagridResultConstantsDM.WidthsMantoAR.ModReg
            };
            GridColumn usuarioColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Usuario,
                Header = DatagridResultConstantsDM.HeadersMantoAR.Usuario,
                Width = DatagridResultConstantsDM.WidthsMantoAR.Usuario
            };
            GridColumn fechRegColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.FecReg,
                Header = DatagridResultConstantsDM.HeadersMantoAR.FecReg,
                Width = DatagridResultConstantsDM.WidthsMantoAR.FecReg
            };
            GridColumn codEstColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.CodEst,
                Header = DatagridResultConstantsDM.HeadersMantoAR.CodEst,
                Width = DatagridResultConstantsDM.WidthsMantoAR.CodEst
            };
            GridColumn mineriaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Mineria,
                Header = DatagridResultConstantsDM.HeadersMantoAR.Mineria,
                Width = DatagridResultConstantsDM.WidthsMantoAR.Mineria
            };
            GridColumn procColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesMantoAR.Proc,
                Header = DatagridResultConstantsDM.HeadersMantoAR.Proc,
                Width = DatagridResultConstantsDM.WidthsMantoAR.Proc
            };

            // Agregar columnas al GridControl
            DataGridResult.Columns.Add(idColumn);
            DataGridResult.Columns.Add(codigoColumn);
            DataGridResult.Columns.Add(zonaColumn);
            DataGridResult.Columns.Add(claseColumn);
            DataGridResult.Columns.Add(archivoColumn);
            DataGridResult.Columns.Add(modRegColumn);
            DataGridResult.Columns.Add(usuarioColumn);
            DataGridResult.Columns.Add(fechRegColumn);
            DataGridResult.Columns.Add(codEstColumn);
            DataGridResult.Columns.Add(mineriaColumn);
            DataGridResult.Columns.Add(procColumn);

        }

        private void ConfigureDataGridClassTemporales()
        {
            // Obtener la vista principal del GridControl
            var tableView = DGridRegistroTmp.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DGridRegistroTmp.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn totalColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Total, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Total,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Total
            };

            GridColumn psad17Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.PSad17, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.HeadersClassTemporales.PSad17,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsClassTemporales.PSad17            // Ancho de la columna
            };

            GridColumn psad18Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.PSad18,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.PSad18,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.PSad18
            };

            GridColumn psad19Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.PSad19,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.PSad19,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.PSad19
            };

            GridColumn wgs17Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Wgs17,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Wgs17,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Wgs17
            };
            GridColumn wgs18Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Wgs18,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Wgs18,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Wgs18
            };
            GridColumn wgs19Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Wgs19,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Wgs19,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Wgs19
            };
            GridColumn g56Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.G56,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.G56,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.G56
            };
            GridColumn g84Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.G84,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.G84,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.G84
            };

            // Agregar columnas al GridControl
            DGridRegistroTmp.Columns.Add(totalColumn);
            DGridRegistroTmp.Columns.Add(psad17Column);
            DGridRegistroTmp.Columns.Add(psad18Column);
            DGridRegistroTmp.Columns.Add(psad19Column);
            DGridRegistroTmp.Columns.Add(wgs17Column);
            DGridRegistroTmp.Columns.Add(wgs18Column);
            DGridRegistroTmp.Columns.Add(wgs19Column);
            DGridRegistroTmp.Columns.Add(g56Column);
            DGridRegistroTmp.Columns.Add(g84Column);

        }

        private void ConfigureDataGridClassProduccion()
        {
            // Obtener la vista principal del GridControl
            var tableView = DGridRegistroProd.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DGridRegistroProd.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn totalColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Total, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Total,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Total
            };

            GridColumn psad17Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.PSad17, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.HeadersClassTemporales.PSad17,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsClassTemporales.PSad17            // Ancho de la columna
            };

            GridColumn psad18Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.PSad18,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.PSad18,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.PSad18
            };

            GridColumn psad19Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.PSad19,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.PSad19,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.PSad19
            };

            GridColumn wgs17Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Wgs17,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Wgs17,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Wgs17
            };
            GridColumn wgs18Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Wgs18,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Wgs18,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Wgs18
            };
            GridColumn wgs19Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.Wgs19,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.Wgs19,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.Wgs19
            };
            GridColumn g56Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.G56,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.G56,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.G56
            };
            GridColumn g84Column = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesClassTemporales.G84,
                Header = DatagridResultConstantsDM.HeadersClassTemporales.G84,
                Width = DatagridResultConstantsDM.WidthsClassTemporales.G84
            };

            // Agregar columnas al GridControl
            DGridRegistroProd.Columns.Add(totalColumn);
            DGridRegistroProd.Columns.Add(psad17Column);
            DGridRegistroProd.Columns.Add(psad18Column);
            DGridRegistroProd.Columns.Add(psad19Column);
            DGridRegistroProd.Columns.Add(wgs17Column);
            DGridRegistroProd.Columns.Add(wgs18Column);
            DGridRegistroProd.Columns.Add(wgs19Column);
            DGridRegistroProd.Columns.Add(g56Column);
            DGridRegistroProd.Columns.Add(g84Column);
        }

        private void ConfigureDataGridReseDifReg()
        {
            // Obtener la vista principal del GridControl
            var tableView = DGridDifRegistro.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DGridDifRegistro.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn codigoColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersReseDifReg.Codigo, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesReseDifReg.Codigo,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsReseDifReg.Codigo
            };

            GridColumn claseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesReseDifReg.Clase, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.HeadersReseDifReg.Clase,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsReseDifReg.Clase            // Ancho de la columna
            };

            GridColumn regdbColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesReseDifReg.RegDB,
                Header = DatagridResultConstantsDM.HeadersReseDifReg.RegDB,
                Width = DatagridResultConstantsDM.WidthsReseDifReg.RegDB
            };

            GridColumn reggdbColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesReseDifReg.RegGDB,
                Header = DatagridResultConstantsDM.HeadersReseDifReg.RegGDB,
                Width = DatagridResultConstantsDM.WidthsReseDifReg.RegGDB
            };

            GridColumn observacionColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesReseDifReg.Observacion,
                Header = DatagridResultConstantsDM.HeadersReseDifReg.Observacion,
                Width = DatagridResultConstantsDM.WidthsReseDifReg.Observacion
            };

            // Agregar columnas al GridControl
            DGridDifRegistro.Columns.Add(codigoColumn);
            DGridDifRegistro.Columns.Add(claseColumn);
            DGridDifRegistro.Columns.Add(regdbColumn);
            DGridDifRegistro.Columns.Add(reggdbColumn);
            DGridDifRegistro.Columns.Add(observacionColumn);
        }

        private void ConfigureDataGridResHistorico()
        {
            // Obtener la vista principal del GridControl
            var tableView = DGResHistorico.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DGResHistorico.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn idColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersResHistorico.Id, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.Id,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsResHistorico.Id
            };

            GridColumn codigoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.Codigo, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.HeadersResHistorico.Codigo,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsResHistorico.Codigo            // Ancho de la columna
            };

            GridColumn archivoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.Archivo,
                Header = DatagridResultConstantsDM.HeadersResHistorico.Archivo,
                Width = DatagridResultConstantsDM.WidthsResHistorico.Archivo
            };

            GridColumn claseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.Clase,
                Header = DatagridResultConstantsDM.HeadersResHistorico.Clase,
                Width = DatagridResultConstantsDM.WidthsResHistorico.Clase
            };

            GridColumn zonaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.Zona,
                Header = DatagridResultConstantsDM.HeadersResHistorico.Zona,
                Width = DatagridResultConstantsDM.WidthsResHistorico.Zona
            };

            GridColumn modregColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.ModReg,
                Header = DatagridResultConstantsDM.HeadersResHistorico.ModReg,
                Width = DatagridResultConstantsDM.WidthsResHistorico.ModReg
            };

            GridColumn codestColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.CodEst,
                Header = DatagridResultConstantsDM.HeadersResHistorico.CodEst,
                Width = DatagridResultConstantsDM.WidthsResHistorico.CodEst
            };

            GridColumn fecregColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesResHistorico.FecReg,
                Header = DatagridResultConstantsDM.HeadersResHistorico.FecReg,
                Width = DatagridResultConstantsDM.WidthsResHistorico.FecReg
            };

            // Agregar columnas al GridControl
            DGResHistorico.Columns.Add(idColumn);
            DGResHistorico.Columns.Add(codigoColumn);
            DGResHistorico.Columns.Add(archivoColumn);
            DGResHistorico.Columns.Add(claseColumn);
            DGResHistorico.Columns.Add(zonaColumn);
            DGResHistorico.Columns.Add(modregColumn);
            DGResHistorico.Columns.Add(codestColumn);
            DGResHistorico.Columns.Add(fecregColumn);

        }

        private void ConfigureDataGridAreaReservada()
        {
            // Obtener la vista principal del GridControl
            var tableView = DGAreaReservada.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DGAreaReservada.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn idColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Id, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Id,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Id
            };

            GridColumn nombreColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Nombre, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Nombre,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Nombre            // Ancho de la columna
            };

            GridColumn nmreseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.NmRese,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.NmRese,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.NmRese
            };

            GridColumn tpreseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.TpRese,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.TpRese,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.TpRese
            };

            GridColumn categoriColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Categori,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Categori,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Categori
            };

            GridColumn claseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Clase,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Clase,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Clase
            };

            GridColumn zonaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Zona,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Zona,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Zona
            };

            GridColumn titularColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Titular,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Titular,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Titular
            };

            GridColumn hasColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Has,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Has,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Has
            };
            GridColumn zonexColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Zonex,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Zonex,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Zonex
            };
            GridColumn obsColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Obs,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Obs,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Obs
            };
            GridColumn normaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Norma,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Norma,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Norma
            };
            GridColumn archivoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Archivo,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Archivo,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Archivo
            };

            GridColumn fecinfColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.FecInf,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.FecInf,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.FecInf
            };
            GridColumn entidadColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Entidad,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Entidad,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Entidad
            };

            GridColumn usoColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Uso,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Uso,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Uso
            };
            GridColumn estgraftColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.EstGraf,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.EstGraf,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.EstGraf
            };
            GridColumn leyendaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Leyenda,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Leyenda,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Leyenda
            };
            GridColumn fecpubColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.FecPub,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.FecPub,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.FecPub
            };
            GridColumn envColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Env,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Env,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Env
            };
            GridColumn mineriaColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Mineria,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Mineria,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Mineria
            };
            GridColumn identitiColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.ColumNamesAreaReservada.Identiti,
                Header = DatagridResultConstantsDM.HeadersAreaReservada.Identiti,
                Width = DatagridResultConstantsDM.WidthsAreaReservada.Identiti
            };


            // Agregar columnas al GridControl
            DGAreaReservada.Columns.Add(idColumn);
            DGAreaReservada.Columns.Add(nombreColumn);
            DGAreaReservada.Columns.Add(nmreseColumn);
            DGAreaReservada.Columns.Add(tpreseColumn);
            DGAreaReservada.Columns.Add(categoriColumn);
            DGAreaReservada.Columns.Add(claseColumn);
            DGAreaReservada.Columns.Add(zonaColumn);
            DGAreaReservada.Columns.Add(titularColumn);
            DGAreaReservada.Columns.Add(hasColumn);
            DGAreaReservada.Columns.Add(zonexColumn);
            DGAreaReservada.Columns.Add(obsColumn);
            DGAreaReservada.Columns.Add(normaColumn);
            DGAreaReservada.Columns.Add(archivoColumn);
            DGAreaReservada.Columns.Add(fecinfColumn);
            DGAreaReservada.Columns.Add(entidadColumn);
            DGAreaReservada.Columns.Add(usoColumn);
            DGAreaReservada.Columns.Add(estgraftColumn);
            DGAreaReservada.Columns.Add(leyendaColumn);
            DGAreaReservada.Columns.Add(fecpubColumn);
            DGAreaReservada.Columns.Add(envColumn);
            DGAreaReservada.Columns.Add(mineriaColumn);
            DGAreaReservada.Columns.Add(identitiColumn);
        }

        private void ConfigureDataGridDemarca()
        {
            // Obtener la vista principal del GridControl
            var tableView = DGDemarca.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DGDemarca.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn cgcodigoColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersDemarca.CgCodigo, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesDemarca.CgCodigo,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsDemarca.CgCodigo
            };

            GridColumn decoddemColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.HeadersDemarca.DeCoddem, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.ColumNamesDemarca.DeCoddem,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsDemarca.DeCoddem            // Ancho de la columna
            };

            GridColumn usloguseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.HeadersDemarca.UsLoguse,
                Header = DatagridResultConstantsDM.ColumNamesDemarca.UsLoguse,
                Width = DatagridResultConstantsDM.WidthsDemarca.UsLoguse
            };

            GridColumn dgfecingColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.HeadersDemarca.DgFecing,
                Header = DatagridResultConstantsDM.ColumNamesDemarca.DgFecing,
                Width = DatagridResultConstantsDM.WidthsDemarca.DgFecing
            };


            // Agregar columnas al GridControl
            DGDemarca.Columns.Add(cgcodigoColumn);
            DGDemarca.Columns.Add(decoddemColumn);
            DGDemarca.Columns.Add(usloguseColumn);
            DGDemarca.Columns.Add(dgfecingColumn);
        }

        private void ConfigureDataGridCartaIGN()
        {
            // Obtener la vista principal del GridControl
            var tableView = DGCarta.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DGCarta.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn cgcodigoColumn = new GridColumn
            {
                Header = DatagridResultConstantsDM.HeadersCartaIGN.CgCodigo, // Encabezado
                FieldName = DatagridResultConstantsDM.ColumNamesCartaIGN.CgCodigo,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstantsDM.WidthsCartaIGN.CgCodigo
            };

            GridColumn cacodcarColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.HeadersCartaIGN.CaCodcar, // Nombre del campo en el DataTable
                Header = DatagridResultConstantsDM.ColumNamesCartaIGN.CaCodcar,    // Encabezado visible
                Width = DatagridResultConstantsDM.WidthsCartaIGN.CaCodcar            // Ancho de la columna
            };

            GridColumn usloguseColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.HeadersCartaIGN.UsLoguse,
                Header = DatagridResultConstantsDM.ColumNamesCartaIGN.UsLoguse,
                Width = DatagridResultConstantsDM.WidthsCartaIGN.UsLoguse
            };

            GridColumn catipcarColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.HeadersCartaIGN.CaTipcar,
                Header = DatagridResultConstantsDM.ColumNamesCartaIGN.CaTipcar,
                Width = DatagridResultConstantsDM.WidthsCartaIGN.CaTipcar
            };

            GridColumn cafecingColumn = new GridColumn
            {
                FieldName = DatagridResultConstantsDM.HeadersCartaIGN.CaFecing,
                Header = DatagridResultConstantsDM.ColumNamesCartaIGN.CaFecing,
                Width = DatagridResultConstantsDM.WidthsCartaIGN.CaFecing
            };

            // Agregar columnas al GridControl
            DGCarta.Columns.Add(cgcodigoColumn);
            DGCarta.Columns.Add(cacodcarColumn);
            DGCarta.Columns.Add(usloguseColumn);
            DGCarta.Columns.Add(catipcarColumn);
            DGCarta.Columns.Add(cafecingColumn);
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

        private void CbxFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void obtenerDetalle(string idreg)
        {
            //this.tb_detalle_are.ForeColor = System.Drawing.Color.DarkGreen;
            //string detalle = conn.P_SEL_DETALLE_REG(idreg);
            string detalle = dataBaseHandler.GetDetalleReg(idreg);
            string[] detalleArray = detalle.Split(',');

            string text = string.Empty;
            foreach (string i in detalleArray)
            {
                text += i + Environment.NewLine;
            }
            TbxDetalleAre.Text = text;

        }


        private void cargar_combo()
        {
            var dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_DATUM.ToString());
            GetOptionCBox(CbxDatum, dmrRecords);

            dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_ZONA.ToString());
            GetOptionCBox(CbxZona, dmrRecords);

            dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_FILTRO_CREG.ToString());
            GetOptionCBox(CbxFiltroTipo, dmrRecords);


            //dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_FILTRO.ToString());
            //GetOptionCBox(CbxFiltro, dmrRecords);

            //dmrRecords = dataBaseHandler.GetFiltroUsuario();
            //GetOptionCBox(CbxUsuario, dmrRecords);

            //CbxUsuario.SelectedValue = "FLAT1065";

        }

        public void GetOptionCBox(ComboBox cbox, DataTable dgrid)
        {
            DataView dgridView = new DataView(dgrid);
            cbox.ItemsSource = dgridView;
            cbox.SelectedValuePath = "CB_VALOPT";
            cbox.DisplayMemberPath = "CB_DESOPT";
        }

        private void CbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var opt = CbxDatum.SelectedValue;
                if (opt != null && int.TryParse(opt.ToString(), out int optValue))
                {
                    CbxZona.IsEnabled = optValue <= _FILT_DATUM_WGS;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void CbxDatum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Obtener el valor seleccionado en el ComboBox
                var opt = CbxDatum.SelectedValue;

                // Verificar si el valor es numérico antes de compararlo
                if (opt != null && int.TryParse(opt.ToString(), out int optValue))
                {
                    CbxZona.IsEnabled = optValue <= _FILT_DATUM_WGS;
                }
                else
                {
                    CbxZona.IsEnabled = false; // Deshabilitar si el valor no es válido
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DataGridResultTableView_RowUpdated(object sender, RowEventArgs e)
        {

        }

        private void MiTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = MiTabControl.SelectedIndex; // Obtiene el índice de la pestaña seleccionada

            switch (selectedIndex)
            {
                case 0:
                    break;

                case 11:
                    try
                    {
                        var dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_FILTRO_CREG.ToString());
                        GetOptionCBox(CbxFiltroControlReg, dmrRecords);

                        dmrRecords = dataBaseHandler.GetOpcionCBox(_OPT_FILTRO_ENV.ToString());
                        GetOptionCBox(CbxEnv, dmrRecords);
                        cambiaOpcionesCboxFeatures();
                        break;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                case 4:
                    try
                    {

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
                case 5:
                    try
                    {
                        string _pathrese = dataBaseHandler.GetObtienePathRese();
                        string _pathurba = dataBaseHandler.GetObtienePathUrba();
                        string _pathimage = dataBaseHandler.GetObtieneImage();
                        string _tipoexe   = dataBaseHandler.GetObtieneTipoExe();

                        txtResePath.Text = _pathrese;
                        txtUrbaPath.Text = _pathurba;
                        txtImagePath.Text = _pathimage;
                        //rbExeAhora.IsChecked= true;
                        

                    }
                    catch (Exception)
                    {
                        string message = "Error - No se pudo obtener la configuración establecida";
                        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                         "Advertencia",
                                                                         MessageBoxButton.OK, MessageBoxImage.Warning);
                        
                        return;
                        //throw;
                    }
                    break;
            }
        }

        private void cambiaOpcionesCboxFeatures()
        {
            int _tipo = Convert.ToInt32(CbxFiltroControlReg.SelectedValue);
            var _env = Convert.ToInt32(CbxEnv.SelectedValue);

            var dmrRecords = dataBaseHandler.GetObtieneFiltroFeatures(_env, _tipo);
            GetOptionCBox(CbxFeatures, dmrRecords);



        }

        DataTable _tableprod;
        DataTable _tabletemp;
        private void CbxFiltroControlReg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int opt = CbxFiltroControlReg.SelectedValue;
            int opt = Convert.ToInt32(CbxFiltroControlReg.SelectedValue);
            try
            {
                cambiaOpcionesCboxFeatures();

                if (opt == _FILT_ARE_RESE)
                {
                    _tableprod = dataBaseHandler.GetObtieneNumRegReseProd();
                    _tabletemp = dataBaseHandler.GetObtieneNumRegReseTemp();
                }
                else if (opt == _FILT_ARE_URBA)
                {
                    _tableprod = dataBaseHandler.GetObtieneNumRegUrbaProd();
                    _tabletemp = dataBaseHandler.GetObtieneNumRegUrbaTemp();
                }
                DGridRegistroProd.ItemsSource = _tableprod;
                DGridRegistroTmp.ItemsSource = _tabletemp;
                GetDiferenciaAnm();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GetDiferenciaAnm()
        {
            DataTable _table;
            try
            {
                int _tipo = Convert.ToInt32(CbxFiltroControlReg.SelectedValue);
                string _feature = CbxFeatures.SelectedValue.ToString();

                if (_tipo == _FILT_ARE_RESE)
                {
                    _table = dataBaseHandler.GetObtieneReseDifreg(_feature);
                }
                else
                {
                    _table = dataBaseHandler.GetObtieneUrbaDifreg(_feature);
                }

                LblCountRecords.Content = "Se encontraron " + _table.Rows.Count.ToString() + " registros";

                DGridDifRegistro.ItemsSource = _table;
            }
            catch (Exception ex)
            {
                // Handle exception if necessary (you can log it or show a message)
            }
        }

        private void CbxEnv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cambiaOpcionesCboxFeatures();
                GetDiferenciaAnm();

            }
            catch (Exception)
            {

                throw;
            }


        }

        private void CbxFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetDiferenciaAnm();

        }

        private void CbxUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetDiferenciaAnm();
        }

        private void CbxUsuario_Loaded(object sender, RoutedEventArgs e)
        {
            //List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
            //cbp.Add(new ComboBoxPairs("--Seleccionar--", 0));
            //cbp.Add(new ComboBoxPairs("FLAT1065", 1));
            //cbp.Add(new ComboBoxPairs("CARI0213", 2));
            //cbp.Add(new ComboBoxPairs("WVAL0398", 3));
            //cbp.Add(new ComboBoxPairs("JFLO0589", 4));
            //cbp.Add(new ComboBoxPairs("CQUI1819", 5));

            //// Asignar la lista al ComboBox
            //CbxUsuario.DisplayMemberPath = "_Key";
            //CbxUsuario.SelectedValuePath = "_Value";
            //CbxUsuario.ItemsSource = cbp;
            // Seleccionar la primera opción por defecto

        }

        private void CbxFiltro_Loaded(object sender, RoutedEventArgs e)
        {
            //List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();
            //cbp.Add(new ComboBoxPairs("--Seleccionar--", 0));
            //cbp.Add(new ComboBoxPairs("Procesar Intermedia", 1));
            //cbp.Add(new ComboBoxPairs("Procesar Producción", 2));
            //cbp.Add(new ComboBoxPairs("Procesados", 3));
            //cbp.Add(new ComboBoxPairs("Errores", 4));
            //// Asignar la lista al ComboBox
            //CbxFiltro.DisplayMemberPath = "_Key";
            //CbxFiltro.SelectedValuePath = "_Value";
            //CbxFiltro.ItemsSource = cbp;

        }

        private void btnPlano_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DGResultado_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {

        }


        private void btnBuscarHistorico_Click_1(object sender, RoutedEventArgs e)
        {

            string codigo = txtBuscaHistorico.Text;
            string fecIni = DtpFecIni.Text;
            string fecFin = DtpFecFin.Text;

            if (codigo == "")
            {
                codigo = "#";
            }
            if (fecIni == "")
            {
                fecIni = "#";
            }

            if (fecFin == "")
            {
                fecFin = "#";
            }

            DataTable _table;
            _table = dataBaseHandler.GetObtieneFiltroHistorico(codigo, fecIni, fecFin);
            DGResHistorico.ItemsSource = _table;
        }



        private void DGResHistorico_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {

        }

        private void btnBuscarAR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string _codigo = "";
                _codigo = txtBuscaAR.Text;
                if (_codigo == "")
                {

                    string message = "Por favor ingrese un código";
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                     "Advertencia",
                                                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DataTable _table;
                string vOpcion = "";
                string opt = "";
                opt = CbxFiltroTipo.SelectedValue.ToString();
                if (opt == "1")
                {
                    vOpcion = "1";
                }
                else
                {
                    vOpcion = "2";
                }

                _table = dataBaseHandler.GetObtieneDatosAR(vOpcion, _codigo);
                DGAreaReservada.ItemsSource = _table;



            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DGAreaReservada_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {

        }

        private void CbxFiltroTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void btnGuardarER_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscarH_Click(object sender, RoutedEventArgs e)
        {
            //DataTable dmrRecords;
            var rows = 0;
            if (CbxFiltro.SelectedItem is ComboBoxPairs selectedOpcion)
            {
                if (CbxUsuario.SelectedItem is ComboBoxPairs selectedUsuario)
                {
                    var dmrRecords = dataBaseHandler.GetProGisFiltro($"{selectedOpcion._Value}", $"{selectedUsuario._Key}");
                    rows = dmrRecords.Rows.Count;
                    DataGridResult.ItemsSource = dmrRecords.DefaultView;
                }
                else
                {
                    MessageBox.Show("Seleccione un valor válido usuario.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un valor válido opción.");
            }
            LblCountRecords.Content = "Se encontraron " + rows.ToString() + " registros";

            // Loop through the columns of the DataGridView (assuming it's a DevExpress control)
            //for (int i = 0; i < DataGridResult.Columns.Count; i++)
            //{
            //    // Disable sorting for all columns
            //    //DataGridResult.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;

            //    // Set ReadOnly property based on column name
            //    if (DataGridResult.Columns[i].Name == _FIELD_PROCESAR)
            //    {
            //        // Make this column editable
            //        //DataGridResult.Columns[i].OptionsColumn.ReadOnly = false;
            //    }
            //    else
            //    {
            //        // Make all other columns read-only
            //        //DataGridResult.Columns[i].OptionsColumn.ReadOnly = true;
            //    }
            //}

            //foreach (int rowHandle in DataGridResult.DataController.GetAllFilteredAndSortedRows())
            //{
            //    // Verificar si el rowHandle es válido
            //    if (!DataGridResult.IsValidRowHandle(rowHandle))
            //    {
            //        Console.WriteLine($"RowHandle inválido: {rowHandle}");
            //        continue;
            //    }

            //    // Verificar si el rowHandle corresponde a una fila de datos
            //    if (!DataGridResult.IsDataRow(rowHandle))
            //    {
            //        Console.WriteLine($"RowHandle {rowHandle} no es una fila de datos.");
            //        continue;
            //    }

            //    // Verificar que la columna _FIELD_RE_CODEST existe
            //    var column = DataGridResult.Columns.FirstOrDefault(c => c.FieldName == _FIELD_RE_CODEST);
            //    if (column == null)
            //    {
            //        Console.WriteLine($"Error: La columna {_FIELD_RE_CODEST} no existe en el GridControl.");
            //        continue;
            //    }

            //    // Obtener el valor de la celda
            //    var estadoObj = DataGridResult.GetRowCellValue(rowHandle, _FIELD_RE_CODEST);
            //    if (estadoObj == null || estadoObj == DBNull.Value)
            //    {
            //        Console.WriteLine($"Fila {rowHandle}: La celda {_FIELD_RE_CODEST} está vacía.");
            //        continue;
            //    }

            //    Console.WriteLine($"Fila {rowHandle}: {_FIELD_RE_CODEST} = {estadoObj} ({estadoObj.GetType()})");

            //    // Asegurarse de que el valor se pueda convertir a string para compararlo
            //    var estado = estadoObj.ToString();

            //    // Comparaciones y asignaciones
            //    if (estado.Equals(_EST_PROCTEMP) || estado.Equals(_EST_PROCPROD))
            //    {
            //        DataGridResult.SetRowCellValue(rowHandle, _FIELD_PROCESAR, true);
            //    }
            //    else if (estado.Equals(_EST_NOPROCTEMP) || estado.Equals(_EST_NOPROCPROD))
            //    {
            //        DataGridResult.SetRowCellValue(rowHandle, _FIELD_PROCESAR, false);
            //    }
            //    else if (estado.Equals(_EST_ERRTEMP) || estado.Equals(_EST_ERRPROD))
            //    {
            //        DataGridResult.SetRowCellValue(rowHandle, _FIELD_PROCESAR, false);
            //        DataGridResult.Columns[_FIELD_PROCESAR].OptionsColumn.ReadOnly = true;
            //        DataGridResult.Appearance.Row.BackColor = System.Drawing.Color.Pink;
            //    }
            //}

        }

        private void btnBuscarAux_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string _codigo = "";
                _codigo = txtBuscaAux.Text;
                if (_codigo == "")
                {

                    string message = "Por favor ingrese un código";
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(message,
                                                                     "Advertencia",
                                                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DataTable _table;
                _table = dataBaseHandler.GetObtieneConsultaDatoDB("1", _codigo);
                DGDemarca.ItemsSource = _table;

                _table = dataBaseHandler.GetObtieneConsultaDatoDB("2", _codigo);
                DGCarta.ItemsSource = _table;

            }
            catch (Exception)
            {

                throw;
            }


        }

        //private void rb_1_Checked(object sender, RoutedEventArgs e)
        //{
        //    RadioButton selectedRadioButton = sender as RadioButton;
        //    if (selectedRadioButton != null)
        //    {
        //        // Mostrar el contenido del RadioButton seleccionado en el TextBox
        //        string tb_selection = "Seleccionaste: " + selectedRadioButton.Name.ToString();
        //        string tb_s1election = "Seleccionaste: " + selectedRadioButton.Content.ToString();
        //    }
        //}

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Obtener el RadioButton que ha sido seleccionado
            RadioButton selectedRadioButton = sender as RadioButton;

            // Verificar si el RadioButton seleccionado no es nulo
            if (selectedRadioButton != null)
            {
                // Mostrar el contenido del RadioButton seleccionado en el TextBox
                string tb_selection = "Seleccionaste: " + selectedRadioButton.Content.ToString();
            }


        }

        private void btnPathAReserva_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPathZonaUrbana_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPathImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridResultTableView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == _FIELD_PROCESAR)
            {
                // Obtén el valor de la fila y columna modificada
                int focusedRowHandle = e.RowHandle;
                DataGridResult.View.PostEditor(); // Asegura que la edición se ha terminado

                // Puedes obtener el valor del campo 'idReg' o realizar otras acciones
                string idReg = DataGridResult.GetCellValue(focusedRowHandle, _FIELD_RE_SECUEN)?.ToString();
                if (!string.IsNullOrEmpty(idReg))
                {
                    obtenerDetalle(idReg);
                }



            }
        }

 
       





    }

}
