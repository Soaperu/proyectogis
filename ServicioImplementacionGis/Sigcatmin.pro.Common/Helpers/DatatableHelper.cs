using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Common.Helpers
{
    public static class DatatableHelper
    {
            public static DataTable ConvertToDataTable<T>(List<T> data)
            {
                var dataTable = new DataTable(typeof(T).Name);

                // Crear las columnas del DataTable a partir de las propiedades del objeto
                foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                // Llenar las filas con los datos
                foreach (var item in data)
                {
                    var values = new object[dataTable.Columns.Count];
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        var propertyName = dataTable.Columns[i].ColumnName;
                        var propertyInfo = typeof(T).GetProperty(propertyName);
                        values[i] = propertyInfo?.GetValue(item, null) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        
}
