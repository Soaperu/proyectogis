using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using CommonUtilities.ArcgisProUtils.Models;
using DatabaseConnector;
using Newtonsoft.Json;
using ReportGenerator;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ArcGIS.Desktop.Internal.Mapping.Views.PropertyPages.Map.TransformationViewModel;
using ArcGIS.Desktop.Core;
using DevExpress.Xpf.Grid;

namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para LibreDenunciabilidad.xaml
    /// </summary>
    public partial class LibreDenunciabilidad : Page
    {
        private DatabaseHandler _dataBaseHandler = new DatabaseHandler();
        private ReportesLibreDenunciabilidad _reportesLibreDenunciabilidad = new ReportesLibreDenunciabilidad();
        private DataTable dtTotalOfDms;
        private DataTable dtDMsToProcess;
        public LibreDenunciabilidad()
        {
            InitializeComponent();
            CurrentUser();
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

        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("Seleccione Datum", 0));
            cbp.Add(new ComboBoxPairs("WGS-84", 2));
            cbp.Add(new ComboBoxPairs("PSAD-56", 1));
            cbp.Add(new ComboBoxPairs("Ambos", 3));

            // Asignar la lista al ComboBox
            CbxSistema.DisplayMemberPath = "_Key";
            CbxSistema.SelectedValuePath = "_Value";
            CbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxSistema.SelectedIndex = 0;
            GlobalVariables.CurrentDatumDm = "0";
            dtTotalOfDms = _dataBaseHandler.GetCodigosLibreDenu();
        }

        private void DataGridRecordsToProcess_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "RowNumber" && e.IsGetData)
            {
                // Obtén el índice de la fila y asígnalo como número de registro
                e.Value = e.ListSourceRowIndex + 1;
            }
        }


        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana contenedora y cerrarla
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        public static async Task<string> GetDefaultScratchPath()
        {
            return await QueuedTask.Run(() =>
            {
                return Project.Current.HomeFolderPath ;
            });
        }

        private async void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {

            foreach (DataRow dtRecord in dtDMsToProcess.Rows)
            {
                var codigo = dtRecord["CODIGO"].ToString();
                string datum = dtRecord["DATUM"].ToString();

                DataTable dmrRecords = _dataBaseHandler.GetUniqueDM(codigo, 1);
                DataRow row = dmrRecords.Rows[0];
                string zona = row["ZONA"].ToString();
                //List<ResultadoEvaluacionModel> res = new List<ResultadoEvaluacionModel>();
                var Params = Geoprocessing.MakeValueArray(codigo, datum, zona);
                var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEvalLibreDenu, Params);
                List<ResultadoEval> resultadoEvalDenu = JsonConvert.DeserializeObject<List<ResultadoEval>>(response.ReturnValue);

                // Insertamos en Base De Datos
                _dataBaseHandler.EliminarRegistroEvaluacionTecnicaLD(codigo);
                foreach (ResultadoEval r in resultadoEvalDenu)
                {
                    _dataBaseHandler.InsertarEvaluacionTecnicaLD(codigo, r.CodigoU, r.Eval, r.Hectarea, r.Concesion, r.Clase);
                }

            }
        }

        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            string scratch = await GetDefaultScratchPath();
            System.Windows.MessageBox.Show(scratch);
        }

        private void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbxSistema.SelectedValue != null)
            {
                int selectedValue = (int)CbxSistema.SelectedValue;

                // Si el valor seleccionado es distinto de 0, activamos el botón
                BtnProcesar.IsEnabled = selectedValue != 0;
                if (selectedValue == 1 || selectedValue == 2)
                {
                    // Filtrar la columna "datum" según el valor seleccionado
                    DataRow[] filteredRows = dtTotalOfDms.Select($"datum = {selectedValue}");

                    // Crear un nuevo DataTable con los datos filtrados
                    if (filteredRows.Length > 0)
                    {
                        dtDMsToProcess = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        dtDMsToProcess = dtTotalOfDms.Clone(); // Mantiene la estructura sin datos
                    }
                }

                else if(selectedValue == 3)
                
                {
                    // Si es "Ambos", tomamos todos los datos
                    dtDMsToProcess = dtTotalOfDms.Copy();
                }
                else
                {
                    dtDMsToProcess = null;

                }
                if (dtDMsToProcess != null)
                {
                    LblNumRegistros.Content = dtDMsToProcess.Rows.Count.ToString();
                    DataGridRecordsToProcess.ItemsSource = dtDMsToProcess;
                }
                else
                {
                    LblNumRegistros.Content = "0";
                    DataGridRecordsToProcess.ItemsSource = null;
                }



            }
            else
            {
                // Si no hay selección, desactivamos el botón
                BtnProcesar.IsEnabled = false;
            }
        }

        private void BtnReporteAvisoRetiro_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls",
                Title = "Guardar Archivo de Excel",
                DefaultExt = "xlsx",
                FileName = "AvisoRetiro",
                AddExtension= true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                DataTable dataTable = _dataBaseHandler.GetAvisoRetiroXLSValues();

                string fraseborrar = "NO PUBLICAR NI ELIMINAR";
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["OPINION TECNICA"] != DBNull.Value) // Verifica que no sea nulo
                    {
                        string opinion = row["OPINION TECNICA"].ToString();
                        row["OPINION TECNICA"] = opinion.Replace(fraseborrar, "PUBLICAR");
                    }
                }

                _reportesLibreDenunciabilidad.ExportXLSReporte(dataTable, filePath);
            }
                
        }

        private void BtnReporteLibreDenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls",
                Title = "Guardar Archivo de Excel",
                DefaultExt = "xlsx",
                FileName = "LibreDenunciabilidad",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                DataTable dataTable = _dataBaseHandler.GetLibreDenunciabilidadXLSValues();                

                _reportesLibreDenunciabilidad.ExportXLSReporte(dataTable, filePath);
            }
        }

       
    }
}
