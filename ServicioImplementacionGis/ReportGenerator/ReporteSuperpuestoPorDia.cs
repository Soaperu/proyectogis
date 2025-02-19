using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;
using System.Data;
using System.Windows.Forms;
using DevExpress.Xpf.Grid;

namespace ReportGenerator
{
    public class ReporteSuperpuestoPorDia
    {
        public void ExportXLSReporte(DataTable dataTable, string titulo, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Crear un nuevo archivo de Excel
            using (var package = new ExcelPackage())
            {
                // Crear una hoja de trabajo
                var worksheet = package.Workbook.Worksheets.Add("Datos");

                // Encabezado principal
                worksheet.Cells["A1:L1"].Merge = true;
                worksheet.Cells["A1"].Value = "INGEMMET";
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 16;

                // Título
                worksheet.Cells["A2:L2"].Merge = true;
                worksheet.Cells["A2"].Value = titulo;
                worksheet.Cells["A2"].Style.Font.Bold = true;
                worksheet.Cells["A2"].Style.Font.Size = 10;

                

                // Escribir los encabezados de las columnas
                int startRow = 6; // Fila inicial para los datos
                int col = 1; // Columna inicial (A = 1, B = 2, ..., Z = 26, AA = 27, etc.)

                foreach (DataColumn column in dataTable.Columns)
                {
                    worksheet.Cells[startRow, col].Value = column.ColumnName;
                    worksheet.Cells[startRow, col].Style.Font.Bold = true;
                    worksheet.Cells[startRow, col].Style.Font.Size = 10;

                    if (column.DataType == typeof(decimal) || column.DataType == typeof(double))
                    {
                        worksheet.Column(col).Style.Numberformat.Format = "#,##0.00";
                    }
                    col++;
                }

                // Escribir los datos del DataGridView
                int row = startRow + 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    col = 1;
                    foreach (var item in dataRow.ItemArray)
                    {
                        worksheet.Cells[row, col].Value = item ?? "";
                        col++;
                    }
                    row++;
                }

                // Ajustar el ancho de las columnas automáticamente
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                // Estilo de la fuente para el rango
                worksheet.Cells[$"A1:P{row}"].Style.Font.Name = "Tahoma";

                // Guardar el archivo
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }

            MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
