using DevExpress.Xpf.Grid;
using SigcatminProAddin.Models.Constants;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para LimiteDistrital.xaml
    /// </summary>
    public partial class LimiteDistrital : Page
    {
        //private void ConfigureDataGridResultColumns()
        //{
        //    // Obtener la vista principal del GridControl
        //    var tableView = DataGridResult.View as DevExpress.Xpf.Grid.TableView;

        //    // Limpiar columnas existentes
        //    DataGridResult.Columns.Clear();

        //    if (tableView != null)
        //    {
        //        tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
        //    }

        //    // Crear y configurar columnas

        //    // Columna de índice (número de fila)
        //    GridColumn indexColumn = new GridColumn
        //    {
        //        Header = DataGridResultsDepartamentoConstants.Headers.Index, // Encabezado
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Index,
        //        UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
        //        AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
        //        VisibleIndex = 0, // Mostrar como la primera columna
        //        Width = DataGridResultsDepartamentoConstants.Widths.Index
        //    };

        //    GridColumn codigoColumn = new GridColumn
        //    {
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Codigo, // Nombre del campo en el DataTable
        //        Header = DataGridResultsDepartamentoConstants.Headers.Codigo,    // Encabezado visible
        //        Width = DataGridResultsDepartamentoConstants.Widths.Codigo            // Ancho de la columna
        //    };

        //    GridColumn departamentoColumn = new GridColumn
        //    {
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Departamento,
        //        Header = DataGridResultsDepartamentoConstants.Headers.Departamento,
        //        Width = DataGridResultsDepartamentoConstants.Widths.Departamento
        //    };

        //    GridColumn provinciaColumn = new GridColumn
        //    {
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Provincia,
        //        Header = DataGridResultsDepartamentoConstants.Headers.Provincia,
        //        Width = DataGridResultsDepartamentoConstants.Widths.Provincia
        //    };

        //    GridColumn distritoColumn = new GridColumn
        //    {
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Distrito,
        //        Header = DataGridResultsDepartamentoConstants.Headers.Distrito,
        //        Width = DataGridResultsDepartamentoConstants.Widths.Distrito
        //    };
        //    GridColumn capitalDistritalColumn = new GridColumn
        //    {
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Capital,
        //        Header = DataGridResultsDepartamentoConstants.Headers.Capital,
        //        Width = DataGridResultsDepartamentoConstants.Widths.Capital
        //    };
        //    GridColumn zonaColumn = new GridColumn
        //    {
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Zona,
        //        Header = DataGridResultsDepartamentoConstants.Headers.Zona,
        //        Width = DataGridResultsDepartamentoConstants.Widths.Zona
        //    };
        //    GridColumn ubigeoColumn = new GridColumn
        //    {
        //        FieldName = DataGridResultsDepartamentoConstants.ColumNames.Ubigeo,
        //        Header = DataGridResultsDepartamentoConstants.Headers.Ubigeo,
        //        Width = DataGridResultsDepartamentoConstants.Widths.Ubigeo
        //    };


        //    // Agregar columnas al GridControl
        //    DataGridResult.Columns.Add(indexColumn);
        //    DataGridResult.Columns.Add(codigoColumn);
        //    DataGridResult.Columns.Add(departamentoColumn);
        //    DataGridResult.Columns.Add(provinciaColumn);
        //    DataGridResult.Columns.Add(distritoColumn);
        //    DataGridResult.Columns.Add(capitalDistritalColumn);
        //    DataGridResult.Columns.Add(zonaColumn);
        //    DataGridResult.Columns.Add(ubigeoColumn);

        //}
        public LimiteDistrital()
        {
            InitializeComponent();
        }
    }
}
