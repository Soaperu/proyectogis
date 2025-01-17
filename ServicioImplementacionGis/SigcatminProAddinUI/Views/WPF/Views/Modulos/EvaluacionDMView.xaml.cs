using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using Sigcatmin.pro.Application.UsesCases;
using SigcatminProAddinUI.Models;
using SigcatminProAddinUI.Resources.Extensions;
using SigcatminProAddinUI.Resources.Helpers;
using SigcatminProAddinUI.Resourecs.Constants;
using SigcatminProAddinUI.Views.WPF.ViewModel;

namespace SigcatminProAddinUI.Views.WPF.Views.Modulos
{
    /// <summary>
    /// Lógica de interacción para EvaluacionDMView.xaml
    /// </summary>
    public partial class EvaluacionDMView : Page
    {
        private EvaluacionDMViewModel _evaluacionDMViewModel;
        private readonly GetDerechoMineroUseCase _getDerechoMineroUseCase;
        private readonly CountRowsGISUseCase _countRowsGISUseCase;
        private readonly GetCoordenadasDMUseCase _getCoordenadasDMUseCase;

        private string _seletecdRowCode = string.Empty;
        public EvaluacionDMView()
        {
            InitializeComponent();
            _evaluacionDMViewModel = new EvaluacionDMViewModel();
            _getDerechoMineroUseCase = Program.GetService<GetDerechoMineroUseCase>();
            _countRowsGISUseCase = Program.GetService<CountRowsGISUseCase>();
            _getCoordenadasDMUseCase = Program.GetService<GetCoordenadasDMUseCase>();
            _evaluacionDMViewModel.LoadLayer(LayersListBox);

            TbxRadio.Text = _evaluacionDMViewModel.RadioDefaultValue.ToString();
        }
        private void CbxTypeConsult_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            TbxValue.Clear();
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
            var items = _evaluacionDMViewModel.GetItemsComboTypeConsult();
            ComboBoxHelper.LoadComboBox(CbxTypeConsult, items);
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxValue.Text))
            {
               MessageBoxHelper.ShowInfo(ErrorMessage.EmptySearchValue.FormatMessage(TbxValue.Text),
                   TitlesMessage.MissingValue);
                return;
            }
            try
            {

                string typeValue = CbxTypeConsult.SelectedValue.ToString();
                string searchValue = TbxValue.Text;
                var totalRows = await _countRowsGISUseCase.Execute(typeValue, searchValue);

                bool isValid = _evaluacionDMViewModel.ValidTotalRowsDerechosMineros(totalRows, searchValue);
                if (!isValid) return; 

                var derechosMineros = await _getDerechoMineroUseCase.Execute(searchValue, Convert.ToInt32(typeValue));

                DataGridResult.ItemsSource = derechosMineros;
                DataGridResult.CustomUnboundColumnData += DataGridResult_CustomUnboundColumnData;
                BtnGraficar.IsEnabled = true;
            }
            catch(Exception ex)
            {
                MessageBoxHelper.ShowError(ErrorMessage.UnexpectedError, TitlesMessage.Error);
            }
        }
        private async void DataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
         
            string zona = DataGridResult.GetSelectedRow<string>(sender, "Zona");
            string nombre = DataGridResult.GetSelectedRow<string>(sender, "Nombre");
            string hectarea = DataGridResult.GetSelectedRow<string>(sender, "Hectarea");
            int currentDatum = (int)CbxSistema.SelectedValue;
            _seletecdRowCode = DataGridResult.GetSelectedRow<string>(sender, "Codigo");

            CbxZona.SelectedValue = zona;
            TbxArea.Text = hectarea;
            TbxArea.IsReadOnly = true;
            ClearCanvas();

            var coordenadas = await _getCoordenadasDMUseCase.Execute(_seletecdRowCode, currentDatum);
            DataGridDetails.ItemsSource = coordenadas;
            GraphCoordinates(coordenadas);    
        }

        private void GraphCoordinates()
        {
            double canvasWidth = PolygonCanvas.ActualWidth;
            double canvasHeight = PolygonCanvas.ActualHeight;
        }

        private async void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(_seletecdRowCode)) return;

            if (CbxSistema.SelectedItem is ComboBoxItemGeneric<int> selectedItem)
            {
                var coordenadas = await _getCoordenadasDMUseCase.Execute(_seletecdRowCode, selectedItem.Id);
                DataGridDetails.ItemsSource = coordenadas;
            }
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
            var items = _evaluacionDMViewModel.GetItemsComboZona();
            ComboBoxHelper.LoadComboBox(CbxZona, items, 1);
        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            var items = _evaluacionDMViewModel.GetItemsComboSistema();
            ComboBoxHelper.LoadComboBox(CbxSistema, items);
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
        private void ClearCanvas()
        {
            PolygonCanvas.Children.Clear();
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
