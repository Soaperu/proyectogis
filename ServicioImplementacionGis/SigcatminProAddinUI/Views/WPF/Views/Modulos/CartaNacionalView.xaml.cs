﻿//using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace SigcatminProAddinUI.Views.WPF.Views.Modulos
{
    /// <summary>
    /// Lógica de interacción para CartaNacionalView.xaml
    /// </summary>
    public partial class CartaNacionalView : Page
    {
        public CartaNacionalView()
        {
            InitializeComponent();
        }
        private void CbxTypeConsult_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            TbxValue.Clear();
        }
        private void DataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
        }
        private void TbxValue_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            }
        }
        private void CbxTypeConsult_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            //if (string.IsNullOrEmpty(TbxValue.Text))
            //{
            //    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
            //                                                     MessageConstants.Titles.MissingValue,
            //                                                     MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
        }

        private void DataGridResult_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            // Verificar si la columna es la columna de índice
            if (e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Integer && e.IsGetData)
            {
                // Asignar el índice de la fila
                e.Value = e.ListSourceRowIndex + 1; // Los índices son base 0, así que sumamos 1
            }
        }

        private void TbxRadio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Agregar el nuevo texto al existente en el TextBox
            string currentText = (sender as System.Windows.Controls.TextBox)?.Text ?? string.Empty;
            string newText = currentText.Insert(
                (sender as System.Windows.Controls.TextBox)?.SelectionStart ?? 0, e.Text);

            // Validar si el texto es un número válido
            //e.Handled = !NumberRegex.IsMatch(newText);
        }

        private void CbxZona_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            //ClearControls();
            //CbxSistema.SelectedIndex = 0;
            //CbxTypeConsult.SelectedIndex = 0;
            //CbxZona.SelectedIndex = 1;
        }

        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TbxRadio_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Permitir teclas específicas (como Backspace, Delete, flechas, etc.)
            if (e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab ||
                e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Enter || e.Key == Key.Escape)
            {
                e.Handled = false;
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

    }
}
