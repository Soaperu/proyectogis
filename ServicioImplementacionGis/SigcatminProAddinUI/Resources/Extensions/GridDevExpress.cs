using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Grid;

namespace SigcatminProAddinUI.Resources.Extensions
{
    public static class GridDevExpress
    {
        public static T GetSelectedRow<T>(this GridControl grid, object sender, string field)
        {
            var tableView = sender as DevExpress.Xpf.Grid.TableView;
            if (tableView != null && tableView.Grid.VisibleRowCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                int focusedRowHandle = tableView.FocusedRowHandle;
                return (T)grid.GetCellValue(focusedRowHandle, field);
            }

            return default(T);

        }
    }
}
