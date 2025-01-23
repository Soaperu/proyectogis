using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Dialogs;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using CommonUtilities.ArcgisProUtils.Models;


//using ClosedXML.Excel;
using DevExpress.ClipboardSource.SpreadsheetML;
using OfficeOpenXml;

namespace SigcatminProAddin.View.Botones.UI
{
    /// <summary>
    /// Lógica de interacción para GenerarPlanosMasivosWpf.xaml
    /// </summary>
    public partial class GenerarPlanosMasivosWpf : Window
    {
        string filePath = string.Empty;
        string excelFilePath = string.Empty;
        Dictionary<string, List<string>> columnData = new Dictionary<string, List<string>>();
        public List<string> SelectedColumnData { get; private set; }
        List<string> columnHeaders = new List<string>();
        public GenerarPlanosMasivosWpf()
        {
            InitializeComponent();
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

        private void btnCarga_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Crea objeto de filtro
                var xlsxFilter = BrowseProjectFilter.GetFilter("esri_browseDialogFilters_browseFiles");
                xlsxFilter.FileExtension = "*.xlsx;*.xls";//restringir a extensiones específicas según sea necesario
                xlsxFilter.BrowsingFilesMode = true;
                // Especifique un nombre para mostrar en el cuadro combinado desplegable del filtro; de lo contrario, el nombre se mostrará como "Predeterminado"
                xlsxFilter.Name = "Archivos de Excel (*.xlsx;*.xls)";
                // Configuración del diálogo para seleccionar archivo
                OpenItemDialog openFileDialog = new OpenItemDialog
                {
                    Title = "Seleccionar archivo Excel",
                    MultiSelect = false,
                    BrowseFilter = xlsxFilter
                };

                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    excelFilePath = openFileDialog.Items[0].Path;
                    tbxArchivo.Text = excelFilePath;
                    cbxField.IsEnabled = true;
                }
                cbxField.Items.Clear();
                //var columnHeaders = GetColumnHeaders(excelFilePath);
                columnHeaders = GetColumnHeadersWithEPPlus(excelFilePath);
                foreach (var header in columnHeaders)
                {
                    cbxField.Items.Add(header);
                }
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"Error al cargar el archivo Excel: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private List<string> GetColumnHeaders(string filePath)
        //{
        //    var columnHeaders = new List<string>();

        //    using (var workbook = new XLWorkbook(filePath))
        //    {
        //        var worksheet = workbook.Worksheet(1); // Leer la primera hoja
        //        var firstRow = worksheet.Row(1); // Leer la primera fila

        //        foreach (var cell in firstRow.CellsUsed())
        //        {
        //            columnHeaders.Add(cell.GetValue<string>()); // Agregar el valor de la celda a la lista
        //        }
        //    }

        //    return columnHeaders;
        //}

        private void cbxField_DropDownOpened(object sender, EventArgs e)
        {
            //cbxField.Items.Clear();
            ////var columnHeaders = GetColumnHeaders(excelFilePath);
            //var columnHeaders = GetColumnHeadersWithEPPlus(excelFilePath);
            //foreach (var header in columnHeaders)
            //{
            //    cbxField.Items.Add(header);
            //}
        }

        public List<string> GetColumnHeadersWithEPPlus(string filePath)
        {
            var headers = new List<string>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Primera hoja
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    headers.Add(worksheet.Cells[1, col].Text);
                }
            }

            return headers;
        }

        //private Dictionary<string, List<string>> GetColumnData(string filePath, List<string> columnHeaders)
        //{

        //    using (var workbook = new XLWorkbook(filePath))
        //    {
        //        var worksheet = workbook.Worksheet(1); // Leer la primera hoja

        //        foreach (var header in columnHeaders)
        //        {
        //            columnData[header] = new List<string>();
        //        }

        //        // Leer las filas comenzando desde la segunda fila
        //        foreach (var row in worksheet.RowsUsed().Skip(1))
        //        {
        //            int columnIndex = 1;
        //            foreach (var header in columnHeaders)
        //            {
        //                var cellValue = row.Cell(columnIndex).GetValue<string>();
        //                columnData[header].Add(cellValue);
        //                columnIndex++;
        //            }
        //        }
        //    }
        //    return columnData;
        //}

        private Dictionary<string, List<string>> GetColumnDataWithEPPlus(string filePath, List<string> columnHeaders)
        {
            //var columnData = new Dictionary<string, List<string>>();

            // Configurar la licencia de EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Leer la primera hoja

                // Inicializar el diccionario con las columnas
                foreach (var header in columnHeaders)
                {
                    columnData[header] = new List<string>();
                }

                // Leer las filas comenzando desde la segunda fila
                int rowCount = worksheet.Dimension.End.Row; // Total de filas
                int colCount = worksheet.Dimension.End.Column; // Total de columnas

                for (int row = 2; row <= rowCount; row++) // Inicia desde la fila 2
                {
                    for (int col = 1; col <= colCount; col++) // Recorre cada columna
                    {
                        string header = columnHeaders[col - 1]; // Obtiene el header correspondiente
                        var cellValue = worksheet.Cells[row, col].Text; // Lee el valor de la celda
                        columnData[header].Add(cellValue);
                    }
                }
            }

            return columnData;
        }

        private void cbxField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            columnData = GetColumnDataWithEPPlus(excelFilePath, columnHeaders);
            if (cbxField.SelectedItem != null && columnData != null)
            {
                string selectedHeader = cbxField.SelectedItem.ToString();
                if (columnData.ContainsKey(selectedHeader))
                {
                    SelectedColumnData = columnData[selectedHeader];
                }
                btnFolder.IsEnabled = true;
                tbxFolder.IsEnabled = true;
            }
        }

        public async void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            if (tbxArchivo.Text == string.Empty || tbxFolder.Text == string.Empty || cbxField.SelectedItem == null)
            { 
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, complete todos los campos.", "Advertencia",
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            foreach (KeyValuePair<string, List<string>> entry in columnData)
            {
                string key = entry.Key;
                List<string> values = entry.Value;

                //Console.WriteLine($"Clave: {key}");

                // Iterar a través de la lista de valores
                foreach (string value in values)
                {

                    double x;
                    double y;
                    string planeEval;
                    string pathLayout;
                    pathLayout = System.IO.Path.Combine(GlobalVariables.ContaninerTemplatesReport, GlobalVariables.planeEval);
                    planeEval = GlobalVariables.planeEval.Split('.')[0];
                    List<string> layoutsToDelete = new List<string>()
                    {
                        planeEval,
                    };
                    await LayoutUtils.DeleteSpecifiedLayoutsAsync(layoutsToDelete);
                    var table = ComplementaryProcessesUtils.SearchCodeValue(value);
                    System.Data.DataRow row = table.Rows[0];

                    ResultadoEvaluacionModel res = new ResultadoEvaluacionModel();
                    res = await  ComplementaryProcessesUtils.EvaluationDmByCode(value, row);
                    GlobalVariables.resultadoEvaluacion.ListaResultadosCriterio = res.ListaResultadosCriterio;
                    GlobalVariables.resultadoEvaluacion.areaDisponible = res.areaDisponible;
                    GlobalVariables.resultadoEvaluacion.codigo = res.codigo;
                    GlobalVariables.resultadoEvaluacion.nombre = res.nombre;
                    GlobalVariables.resultadoEvaluacion.distanciaFrontera = res.distanciaFrontera;
                    GlobalVariables.resultadoEvaluacion.isCompleted = res.isCompleted;

                    await LayoutUtils.DeleteSpecifiedLayoutsAsync(layoutsToDelete);
                    string mapName = GlobalVariables.mapNameCatastro;
                    string nameLayer = GlobalVariables.CurrentShpName;
                    var layoutItem = await LayoutUtils.AddLayoutPath(pathLayout, nameLayer, mapName, planeEval);
                    ElementsLayoutUtils elementsLayoutUtils = new ElementsLayoutUtils();
                    (x, y) = await elementsLayoutUtils.TextElementsEvalAsync(layoutItem);
                    y = await elementsLayoutUtils.AgregarTextosLayoutAsync("Evaluacion", layoutItem, 15.2);
                    await elementsLayoutUtils.GeneralistaDmPlanoEvaAsync(y);
                    string outPathPdf = System.IO.Path.Combine(filePath, "Evaluacion" + "_" + value + ".pdf");
                    await LayoutUtils.ExportLayoutToPdfAsync(planeEval, outPathPdf);
                }
            }
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenItemDialog openFileDialog = new OpenItemDialog
            {
                Title = "Seleccionar carpeta para planos",
                MultiSelect = false,
                BrowseFilter = BrowseProjectFilter.GetFilter(ItemFilters.Folders)
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                filePath = openFileDialog.Items[0].Path;
                tbxFolder.Text = filePath;
                cbxField.IsEnabled = true;
            }
            btnGraficar.IsEnabled = true;
        }
    }
}
