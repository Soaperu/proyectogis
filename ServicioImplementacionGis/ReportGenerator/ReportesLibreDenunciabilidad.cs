using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ReportGenerator
{
    public class ReportesLibreDenunciabilidad
    {
        public void ExportXLSReporte(DataTable dataTable, string XLSPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Crear el archivo Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte");

                // Encabezados
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataTable.Columns[i].ColumnName;
                }

                // Formato de encabezados en negrita y centrado
                using (var headerRange = worksheet.Cells[1, 1, 1, dataTable.Columns.Count])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                // Llenar los datos en las celdas
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dataTable.Rows[i][j];

                        // Cambiar el color de la celda según el valor de "Edad"
                        //if (dataTable.Columns[j].ColumnName == "Edad")
                        //{
                        //    int edad = Convert.ToInt32(dataTable.Rows[i][j]);
                        //    var cell = worksheet.Cells[i + 2, j + 1];

                        //    if (edad < 18)
                        //    {
                        //        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //        cell.Style.Fill.BackgroundColor.SetColor(Color.LightBlue); // Menores de edad
                        //    }
                        //    else if (edad >= 18 && edad <= 60)
                        //    {
                        //        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //        cell.Style.Fill.BackgroundColor.SetColor(Color.LightGreen); // Adultos
                        //    }
                        //    else
                        //    {
                        //        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //        cell.Style.Fill.BackgroundColor.SetColor(Color.LightCoral); // Adultos mayores
                        //    }
                        //}
                    }
                }

                // Ajustar ancho de columnas automáticamente
                worksheet.Cells.AutoFitColumns();

                // Guardar el archivo Excel
                File.WriteAllBytes(XLSPath, package.GetAsByteArray());

                Console.WriteLine($"Reporte generado exitosamente: {XLSPath}");
            }
        }
    }
}
