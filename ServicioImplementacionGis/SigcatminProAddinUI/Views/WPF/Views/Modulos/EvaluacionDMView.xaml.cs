using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using Sigcatmin.pro.Application.Contracts.Requests;
using Sigcatmin.pro.Application.UsesCases;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Domain.Enums;
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
        private readonly GraficarDerechoMineroUseCase _graficarDerechoMineroUseCase;

        private string _seletecdRowCode = string.Empty;
        public EvaluacionDMView()
        {
            InitializeComponent();
            _evaluacionDMViewModel = new EvaluacionDMViewModel();
            _getDerechoMineroUseCase = Program.GetService<GetDerechoMineroUseCase>();
            _countRowsGISUseCase = Program.GetService<CountRowsGISUseCase>();
            _getCoordenadasDMUseCase = Program.GetService<GetCoordenadasDMUseCase>();
            _graficarDerechoMineroUseCase = Program.GetService<GraficarDerechoMineroUseCase>();

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
        private void LayerListBox_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string layerText in _evaluacionDMViewModel.LayersText)
            {
                var checkbox = CheckboxHelper.GenerateChexbox(layerText, false);
                LayersListBox.Items.Add(checkbox);
            }
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
               
            }
            catch(Exception ex)
            {
                MessageBoxHelper.ShowError(ErrorMessage.UnexpectedError, TitlesMessage.Error);
            }
        }
        private async void DataGridResultTableView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            int currentDatum = (int)CbxSistema.SelectedValue;
            UpdateFromSelectedRow(sender);

            ClearCanvas();

            _evaluacionDMViewModel.Coordinates = await _getCoordenadasDMUseCase.Execute(_seletecdRowCode);
            var coordinates = _evaluacionDMViewModel.GetCoordinatesByTypeSystem(currentDatum);

            DataGridDetails.ItemsSource = coordinates;

            if (coordinates == null || !coordinates.Any())
            {  
                ToggleGraphButton(false);
                return;
            }
            ToggleGraphButton(true);
            GraphCoordinates();    
        }
        private void UpdateFromSelectedRow(object sender)
        {
            _seletecdRowCode = DataGridResult.GetSelectedRow<string>(sender, "Codigo");
            string zona = DataGridResult.GetSelectedRow<string>(sender, "Zona");
            string nombre = DataGridResult.GetSelectedRow<string>(sender, "Nombre");
            string hectarea = DataGridResult.GetSelectedRow<string>(sender, "Hectarea");

            CbxZona.SelectedValue = zona;
            TbxArea.Text = hectarea;
            TbxArea.IsReadOnly = true;
        }
        private void ToggleGraphButton(bool isEnable)
        {
            BtnGraficar.IsEnabled = isEnable;
        }
        private void GraphCoordinates()
        {
            if (DataGridDetails.ItemsSource is List<CoordinateModel> coordinates)
            {
                if (coordinates == null || !coordinates.Any())
                {
                    MessageBoxHelper.ShowInfo("No se encontraron coordenadas para graficar", "Sin coordenadas");
                    return;
                }

                ImagenPoligono.Visibility = Visibility.Collapsed;

                double canvasWidth = PolygonCanvas.ActualWidth;
                double canvasHeight = PolygonCanvas.ActualHeight;

                var elements = _evaluacionDMViewModel.GetPolygonElements(coordinates, canvasWidth, canvasHeight);

                PolygonCanvas.Children.Clear();
                foreach (var element in elements)
                {
                    PolygonCanvas.Children.Add(element);
                }
            }
        }
        private void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(_seletecdRowCode)) return;

            if (CbxSistema.SelectedItem is ComboBoxItemGeneric<int> selectedItem)
            {
                DataGridDetails.ItemsSource = _evaluacionDMViewModel.GetCoordinatesByTypeSystem(selectedItem.Id);
                GraphCoordinates();
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
            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                MessageBoxHelper.ShowInfo("Por favor ingrese un valor de radio", "Atención");
               return;
            }

            var derechoMineroselected = DataGridResult.GetFocusedRow() as DerechoMineroDto;
            var graficarDto = new GraficarDerechoMineroRequest
            {
                MapName = "Catastro Minero",
                Codigo = derechoMineroselected.Codigo,
                Radio = int.Parse(TbxRadio.Text),
                Zona = derechoMineroselected.Zona, 
                StateGraph = derechoMineroselected.Naturaleza,
                Datum = (CoordinateSystem)CbxSistema.SelectedValue,
                IsDMGraphVisible = ChkGraficarDmY.IsChecked.GetValueOrDefault()
            }; 

            await _graficarDerechoMineroUseCase.Execute(graficarDto);

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
