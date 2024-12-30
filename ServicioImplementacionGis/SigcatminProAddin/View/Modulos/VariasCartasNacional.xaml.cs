﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ArcGIS.Core.Data;
using ArcGIS.Core.Events;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Mapping.Events;
using CommonUtilities;
using CommonUtilities.ArcgisProUtils;
using DatabaseConnector;
using DevExpress.Xpf.Grid;
using SigcatminProAddin.Utils.UIUtils;
using FlowDirection = System.Windows.FlowDirection;
using System.Text.RegularExpressions;
using SigcatminProAddin.Models;
using SigcatminProAddin.Models.Constants;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.CIM;
using DevExpress.Xpf.Grid.GroupRowLayout;
using System.Security.Policy;
using DevExpress.Mvvm.Native;
using static SigcatminProAddin.View.Modulos.EvaluacionDM;
using DevExpress.Utils;
using DevExpress.XtraPrinting.Native;

using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Core.Geometry;
using System.Collections.ObjectModel;
using ArcGIS.Core.Data.UtilityNetwork.Trace;






namespace SigcatminProAddin.View.Modulos
{
    /// <summary>
    /// Lógica de interacción para VariasCartasNacional.xaml
    /// </summary>
    public partial class VariasCartasNacional : Page
    {
        private DatabaseConnection dbconn;
        public DatabaseHandler dataBaseHandler;
        bool sele_denu;
        int datumwgs84 = 2;
        int datumpsad56 = 1;
        string ResultadoCarta = "";

        //public ObservableCollection<CartaIGN> CartaIGNs { get; set; }


        public class CartaIGN
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public int Zona { get; set; }
            public int EsteMin { get; set; }
            public int NorteMin { get; set; }
            public int EsteMax { get; set; }
            public int NorteMax { get; set; }
            public override string ToString()
            {
                return $"Codigo: {Codigo}, Nombre: {Nombre}, Zona: {Zona}, EsteMin: {EsteMin}, NorteMin: {NorteMin}, EsteMax: {EsteMax}, NorteMax: {NorteMax}";
            }
        }

        public class Vertice
        {
            public string Nombre { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return $"Nombre: {Nombre}, X: {X}, Y: {Y}";
            }
        }



        public VariasCartasNacional()
        {
            InitializeComponent();
            AddCheckBoxesToListBox();
            CurrentUser();
            ConfigureDataGridResultColumns();
            ConfigureDataGridDetailsColumns();
            dataBaseHandler = new DatabaseHandler();
            CbxTypeConsult.SelectedIndex = 0;
            TbxRadio.Text = "5";
            BtnGraficar.IsEnabled = true;
        }

        private void CbxTypeConsult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbxSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



        }

        private void ConfigureDataGridDetailsColumns()
        {
            var tableView = DataGridDetails.View as DevExpress.Xpf.Grid.TableView;
            DataGridDetails.Columns.Clear();
            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            GridColumn verticeColumn = new GridColumn
            {
                FieldName = DatagridDetailsConstants.ColumnNames.Vertice,
                Header = DatagridDetailsConstants.Headers.Vertice,
                Width = DatagridDetailsConstants.Widths.VerticeWidth
            };

            GridColumn esteColumn = new GridColumn
            {
                FieldName = DatagridDetailsConstants.ColumnNames.Este,
                Header = DatagridDetailsConstants.Headers.Este,
                Width = new GridColumnWidth(DatagridDetailsConstants.Widths.StarWidthRatio, GridColumnUnitType.Star)
            };

            GridColumn norteColumn = new GridColumn
            {
                FieldName = DatagridDetailsConstants.ColumnNames.Norte,
                Header = DatagridDetailsConstants.Headers.Norte,
                Width = new GridColumnWidth(DatagridDetailsConstants.Widths.StarWidthRatio, GridColumnUnitType.Star)
            };

            // Agregar columnas al GridControl
            DataGridDetails.Columns.Add(verticeColumn);
            DataGridDetails.Columns.Add(esteColumn);
            DataGridDetails.Columns.Add(norteColumn);

        }
        private void ConfigureDataGridResultColumns()
        {
            // Obtener la vista principal del GridControl
            var tableView = DataGridResult.View as DevExpress.Xpf.Grid.TableView;

            // Limpiar columnas existentes
            DataGridResult.Columns.Clear();

            if (tableView != null)
            {
                tableView.AllowEditing = false; // Bloquea la edición a nivel de vista
            }

            // Crear y configurar columnas

            // Columna de índice (número de fila)
            GridColumn indexCdHoja = new GridColumn
            {
                Header = DatagridResultConstants.HeadersCarta.CdHoja, // Encabezado
                FieldName = DatagridResultConstants.ColumNamesCarta.CdHoja,
                UnboundType = DevExpress.Data.UnboundColumnType.Integer, // Tipo de columna no vinculada
                AllowEditing = DevExpress.Utils.DefaultBoolean.False, // Bloquear edición
                VisibleIndex = 0, // Mostrar como la primera columna
                Width = DatagridResultConstants.WidthsCarta.CdHoja
            };

            GridColumn indexNmHoja = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.NmHoja, // Nombre del campo en el DataTable
                Header = DatagridResultConstants.HeadersCarta.NmHoja,    // Encabezado visible
                Width = DatagridResultConstants.WidthsCarta.NmHoja            // Ancho de la columna
            };

            GridColumn indexZona = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.Zona,
                Header = DatagridResultConstants.HeadersCarta.Zona,
                Width = DatagridResultConstants.WidthsCarta.Zona
            };

            GridColumn indexEsteMin = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.EsteMin,
                Header = DatagridResultConstants.HeadersCarta.EsteMin,
                Width = DatagridResultConstants.WidthsCarta.EsteMin
            };

            GridColumn indexEsteMax = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.EsteMax,
                Header = DatagridResultConstants.HeadersCarta.EsteMax,
                Width = DatagridResultConstants.WidthsCarta.EsteMax
            };
            GridColumn indexNorteMin = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.NorteMin,
                Header = DatagridResultConstants.HeadersCarta.NorteMin,
                Width = DatagridResultConstants.WidthsCarta.NorteMin
            };
            GridColumn indexNorteMax = new GridColumn
            {
                FieldName = DatagridResultConstants.ColumNamesCarta.NorteMax,
                Header = DatagridResultConstants.HeadersCarta.NorteMax,
                Width = DatagridResultConstants.WidthsCarta.NorteMax
            };
            //GridColumn naturalezaColumn = new GridColumn
            //{
            //    FieldName = DatagridResultConstants.ColumNames.Naturaleza,
            //    Header = DatagridResultConstants.Headers.Naturaleza,
            //    Width = DatagridResultConstants.Widths.Naturaleza
            //};
            //GridColumn cartaColumn = new GridColumn
            //{
            //    FieldName = DatagridResultConstants.ColumNames.Carta,
            //    Header = DatagridResultConstants.Headers.Carta,
            //    Width = DatagridResultConstants.Widths.Carta
            //};
            //GridColumn hectareaColumn = new GridColumn
            //{
            //    FieldName = DatagridResultConstants.ColumNames.Hectarea,
            //    Header = DatagridResultConstants.Headers.Hectarea,
            //    Width = DatagridResultConstants.Widths.Hectarea
            //};

            // Agregar columnas al GridControl
            DataGridResult.Columns.Add(indexCdHoja);
            DataGridResult.Columns.Add(indexNmHoja);
            DataGridResult.Columns.Add(indexZona);
            DataGridResult.Columns.Add(indexEsteMin);
            DataGridResult.Columns.Add(indexEsteMax);
            DataGridResult.Columns.Add(indexNorteMin);
            DataGridResult.Columns.Add(indexNorteMax);
            //DataGridResult.Columns.Add(naturalezaColumn);
            //DataGridResult.Columns.Add(cartaColumn);
            //DataGridResult.Columns.Add(hectareaColumn);

        }

        private void CbxSistema_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("WGS-84", 2));
            cbp.Add(new ComboBoxPairs("PSAD-56", 1));

            // Asignar la lista al ComboBox
            CbxSistema.DisplayMemberPath = "_Key";
            CbxSistema.SelectedValuePath = "_Value";
            CbxSistema.ItemsSource = cbp;

            // Seleccionar la primera opción por defecto
            CbxSistema.SelectedIndex = 0;
            GlobalVariables.CurrentDatumDm = "2";

        }

        private void CbxZona_Loaded(object sender, RoutedEventArgs e)
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>();

            cbp.Add(new ComboBoxPairs("17", 17));
            cbp.Add(new ComboBoxPairs("18", 18));
            cbp.Add(new ComboBoxPairs("19", 19));

            // Asignar la lista al ComboBox
            CbxZona.DisplayMemberPath = "_Key";
            CbxZona.SelectedValuePath = "_Value";
            CbxZona.ItemsSource = cbp;

            // Seleccionar la opción 18 por defecto
            CbxZona.SelectedIndex = 1;
        }

        private void AddCheckBoxesToListBox()
        {
            string[] items = {
                                "Caram",
                                "Catastro Forestal",
                                "Predio Rural",
                                "Limite Departamental",
                                "Limite Provincial",
                                "Limite Distrital",
                                "Centros Poblados",
                                "Red Hidrografica",
                                "Red Vial"
                            };

            // Agrega cada elemento como un CheckBox al ListBox
            for (int i = 0; i < items.Length; i++)
            {
                var checkBox = new System.Windows.Controls.CheckBox
                {
                    Content = items[i],
                    Margin = new Thickness(2),
                    Style = (Style)FindResource("Esri_CheckboxToggleSwitch"),

                    FlowDirection = FlowDirection.RightToLeft,
                    IsThreeState = true // Permite el estado Indeterminado
                };

                // Establece el estado Indeterminado para los dos primeros elementos
                if (i == 0 || i == 1)
                {
                    checkBox.IsChecked = true; // Estado Indeterminado
                    checkBox.IsEnabled = false; // Desahibilidato
                }

                LayersListBox.Items.Add(checkBox);
            }

        }

        private void CurrentUser()
        {
            CurrentUserLabel.Text = GlobalVariables.ToTitleCase(AppConfig.fullUserName);
        }
        private void ClearCanvas()
        {
            PolygonCanvas.Children.Clear();
        }

        private void ClearDatagrids()
        {
            //DataGridResult.ItemsSource = null;
            DataGridDetails.ItemsSource = null;
        }

        public void ClearControls()
        {
            var functions = new PageCommonFunctions();
            functions.ClearControls(this);
            ClearCanvas();
            ClearDatagrids();
        }

        private void BtnOtraConsulta_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            CbxSistema.SelectedIndex = 0;
            CbxTypeConsult.SelectedIndex = 0;
            CbxZona.SelectedIndex = 1;
            BtnGraficar.IsEnabled = false;
        }


        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

            //var seleccionados = DataGridResult.GetSelectedRowHandles()
            //                          .Select(handle => DataGridResult.GetRow(handle) as Registro)
            //                          .ToList();

            //foreach (var registro in seleccionados)
            //{
            //    Registros.Remove(registro);
            //}


            // Verificar si hay un elemento seleccionado
            if (listBoxVertices.SelectedItem != null)
            {
                // Obtener el elemento seleccionado
                string elementoSeleccionado = listBoxVertices.SelectedItem as string;
                listBoxVertices.Items.Remove(elementoSeleccionado);
                //BtnAgregarHoja_Click_1(sender, e);
                datos_grilla();


            }
            else
            {
                // Informar al usuario que no hay ningún elemento seleccionado
                MessageBox.Show(
                    "Por favor, selecciona un elemento para eliminar.",
                    "Sin Selección",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxVertices.Items.Count > 0)
            {
                // Confirmar la eliminación (opcional)
                MessageBoxResult resultado = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar todos los elementos?",
                    "Confirmar Eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    // Eliminar todos los elementos del ListBox
                    listBoxVertices.Items.Clear();
                    datos_grilla();

                }
            }
            else
            {
                // Informar al usuario que el ListBox ya está vacío
                MessageBox.Show(
                    "El ListBox ya está vacío.",
                    "Información",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            BtnGraficar.IsEnabled = false;

        }

        private void BtnAgregarHoja_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxValue.Text))
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(MessageConstants.Errors.EmptySearchValue,
                                                                 MessageConstants.Titles.MissingValue,
                                                                 MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Crear la cadena valor con el número de punto y las coordenadas
            //string valor = "Carta " + (listBoxVertices.Items.Count + 1) + ":  " + TbxValue.Text.TrimEnd().ToUpper();
            string valor = "Carta " + ":  " + TbxValue.Text.TrimEnd().ToUpper();
            listBoxVertices.Items.Add(valor);
            BtnGraficar.IsEnabled = true;
            datos_grilla();


        }

        private void datos_grilla()
        {
            ResultadoCarta = "";
            for (int i = 0; i < listBoxVertices.Items.Count; i++)
            {
                string item = listBoxVertices.Items[i].ToString();
                // Dividir el texto por el delimitador
                string[] partes = item.Split(":");

                string dato = partes[1];
                dato = dato.Trim();
                if (i == listBoxVertices.Items.Count - 1)
                {
                    ResultadoCarta = ResultadoCarta + "'" + dato + "'";
                }
                else
                {
                    ResultadoCarta = ResultadoCarta + "'" + dato + "'" + ",";
                }
            }


            try
            {
                string value = "CODIGO";// (string)CbxTypeConsult.SelectedValue.ToString();
                string datumStr = CbxSistema.Text;
                //var dmrRecords = dataBaseHandler.GetOfficialCarta(value, TbxValue.Text.TrimEnd(), datumStr);
                var dmrRecords = dataBaseHandler.GetOfficialCartaIn(value, ResultadoCarta.TrimEnd(), datumStr);
                //calculatedIndex(DataGridResult, dmrRecords, DatagridResultConstants.ColumNames.Index);
                DataGridResult.ItemsSource = dmrRecords.DefaultView;
                int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
                string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
                CbxZona.SelectedValue = zoneDm;
            }
            catch (Exception ex)
            {
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format(MessageConstants.Errors.UnexpectedError, ex.Message),
                                                                    MessageConstants.Titles.Error,
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Error);
                DataGridResult.ItemsSource = null;
                return;
            }

            TbxValue.Text = "";

        }
        private async Task<Map> EnsureMapViewIsActiveAsync(string mapName)
        {
            if (MapView.Active != null)
            {
                return MapView.Active.Map;
            }

            // Esperar hasta que MapView.Active esté disponible
            TaskCompletionSource<Map> tcs = new TaskCompletionSource<Map>();

            SubscriptionToken eventToken = null;
            eventToken = DrawCompleteEvent.Subscribe(async args =>
            {
                // Desuscribirse del evento
                // Desuscribirse del evento
                if (eventToken != null)
                {
                    DrawCompleteEvent.Unsubscribe(eventToken);
                }
                // Activar el mapa "CATASTRO MINERO"
                Map map = await CommonUtilities.ArcgisProUtils.MapUtils.FindMapByNameAsync(mapName);
                await CommonUtilities.ArcgisProUtils.MapUtils.ActivateMapAsync(map);

                // Completar la tarea con el mapa activo
                //tcs.SetResult(MapView.Active.Map);
                tcs.SetResult(map);
            });

            // Esperar hasta que el evento se complete
            return await tcs.Task;
        }

        public async Task UpdateValueAsync(string capa, string codigoValue)
        {
            await QueuedTask.Run(() =>
            {
                try
                {
                    // Obtener el documento del mapa y la capa
                    Map pMap = MapView.Active.Map;
                    FeatureLayer pFeatLayer1 = null;
                    foreach (var layer in pMap.Layers)
                    {
                        if (layer.Name.ToUpper() == capa.ToUpper())
                        {
                            pFeatLayer1 = layer as FeatureLayer;
                            break;
                        }
                    }

                    if (pFeatLayer1 == null)
                    {
                        System.Windows.MessageBox.Show("No se encuentra el Layer");
                        return;
                    }

                    // Obtener la clase de entidades de la capa
                    FeatureClass pFeatureClas1 = pFeatLayer1.GetTable() as FeatureClass;

                    // Preparar la fecha y hora
                    string fecha = DateTime.Now.ToString("yyyy/MM/dd");
                    string v_fec_denun = fecha + " 00:00";
                    string v_hor_denun = DateTime.Now.ToString("HH:mm:ss");

                    // Comenzar la transacción
                    using (RowCursor pUpdateFeatures = pFeatureClas1.Search(null, false))
                    {
                        int contador = 0;
                        while (pUpdateFeatures.MoveNext())
                        {
                            contador++;
                            using (Row row = pUpdateFeatures.Current)
                            {
                                string v_codigo_dm = row["CODIGOU"].ToString();

                                // Llamar al procedimiento para obtener datos de Datum y bloquear estado
                                DataTable lodtbDatos_dm = dataBaseHandler.ObtenerDatumDm(v_codigo_dm);
                                if (lodtbDatos_dm.Rows.Count > 0)
                                {
                                    row["DATUM"] = lodtbDatos_dm.Rows[0]["ESTADO"].ToString();
                                }

                                // Llamar a otros procedimientos para obtener situación y estado
                                DataTable lodtbDatos1 = dataBaseHandler.ObtenerBloqueadoDm(v_codigo_dm);
                                if (lodtbDatos1.Rows.Count > 0)
                                {
                                    if (lodtbDatos1.Rows[0]["CODIGO"].ToString() == "1")
                                    {
                                        row["BLOQUEO"] = "1";
                                        row["CASO"] = "D.M. - ANAP";
                                    }

                                }
                                else
                                {
                                    row["BLOQUEO"] = "0";
                                }

                                row["CONTADOR"] = contador;

                                DataTable lodtbDatos2 = dataBaseHandler.ObtenerDatosGeneralesDM(v_codigo_dm);
                                if (lodtbDatos2.Rows.Count > 0)
                                {
                                    row["SITUACION"] = lodtbDatos2.Rows[0]["SITUACION"].ToString();
                                }
                                else
                                {
                                    row["SITUACION"] = "X";
                                }

                                // Lógica de asignación de leyenda dependiendo del estado
                                string estado = row["ESTADO"].ToString();
                                string leyenda = string.Empty;
                                switch (estado)
                                {
                                    case " ":
                                        leyenda = "G4";  // Denuncios Extinguidos
                                        break;
                                    case "P":
                                        leyenda = "G1";  // Petitorio Tramite
                                        break;
                                    case "D":
                                        leyenda = "G2";  // Denuncio Tramite
                                        break;
                                    case "E":
                                    case "N":
                                    case "Q":
                                    case "T":
                                        leyenda = "G3";  // Denuncios Titulados
                                        break;
                                    case "F":
                                    case "J":
                                    case "L":
                                    case "H":
                                    case "Y":
                                    case "9":
                                    case "X":
                                        leyenda = "G4";  // Denuncios Extinguidos
                                        break;
                                    case "C":
                                        DataTable lodtbDatos3 = dataBaseHandler.ObtenerDatosDM(v_codigo_dm);
                                        string v_situacion_dm = "";
                                        string v_estado_dm = "";
                                        if (lodtbDatos3.Rows.Count > 0)
                                        {
                                            v_situacion_dm = lodtbDatos3.Rows[0]["SITUACION"].ToString();
                                            v_estado_dm = lodtbDatos3.Rows[0]["ESTADO"].ToString();
                                        }
                                        if (v_situacion_dm == "V" && v_estado_dm == "T")
                                        {
                                            leyenda = "G3";
                                        }
                                        else if (v_situacion_dm == "V" && v_estado_dm == "R")
                                        {
                                            leyenda = "G1";
                                        }
                                        else
                                        {
                                            leyenda = "G4";
                                        }
                                        break;
                                    case "A":
                                    case "B":
                                    case "S":
                                    case "M":
                                    case "G":
                                    case "R":
                                    case "Z":
                                    case "K":
                                    case "V":

                                        leyenda = "G5";  // Otros
                                        break;
                                    default:
                                        row["LEYENDA"] = "";
                                        row["EVAL"] = "EV";
                                        row["TIPO_EX"] = "PE";
                                        row["CONCESION"] = "Dm_Simulado";
                                        row["FEC_DENU"] = v_fec_denun;
                                        row["HOR_DENU"] = v_hor_denun;
                                        row["CARTA"] = "CARTA";

                                        break;

                                }
                                if (row["BLOQUEO"].ToString() == "1")
                                {
                                    leyenda = "G7";
                                }

                                // Actualizar los valores de departamento, provincia y distrito
                                DataTable lodtbDemarca = dataBaseHandler.ObtenerDatosUbigeo(row["DEMAGIS"].ToString().Substring(0, 6));
                                if (lodtbDemarca.Rows.Count > 0)
                                {
                                    row["DPTO"] = lodtbDemarca.Rows[0]["DPTO"].ToString();
                                    row["PROV"] = lodtbDemarca.Rows[0]["PROV"].ToString();
                                    row["DIST"] = lodtbDemarca.Rows[0]["DIST"].ToString();
                                }
                                if (codigoValue == row["CODIGOU"].ToString())
                                {
                                    leyenda = "G6";
                                }

                                row["LEYENDA"] = leyenda;

                                row.Store();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error en UpdateValue: " + ex.Message);
                }
            });

        }

        private async void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            if (ChkGraficarDmY.IsChecked == true)
            {
                GlobalVariables.stateDmY = true;
            }
            else
            {
                GlobalVariables.stateDmY = false;
            }

            int datum = (int)CbxSistema.SelectedValue;
            string datumStr = CbxSistema.Text;
            int radio = int.Parse(TbxRadio.Text);
            string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);

            await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync("CATASTRO MINERO");
            //int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
            //string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
            //GlobalVariables.CurrentCodeDm = codigoValue;
            //string stateGraphic = DataGridResult.GetCellValue(focusedRowHandle, "PE_VIGCAT")?.ToString();
            //string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
            //GlobalVariables.CurrentZoneDm = zoneDm;

            string zoneDm = CbxZona.SelectedValue.ToString();
            GlobalVariables.CurrentZoneDm = zoneDm;
            var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
                                                                                        , AppConfig.userName
                                                                                        , AppConfig.password);
            //var v_zona_dm = dataBaseHandler.VerifyDatumDM(codigoValue);
            string fechaArchi = DateTime.Now.Ticks.ToString();
            GlobalVariables.idExport = fechaArchi;
            string catastroShpName = "Catastro" + fechaArchi;
            GlobalVariables.CurrentShpName = catastroShpName;
            string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            string dmShpName = "DM" + fechaArchi;
            string dmShpNamePath = "DM" + fechaArchi + ".shp";
            try
            {
                // Obtener el mapa Catastro//

                Map map = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
                // Crear instancia de FeatureClassLoader y cargar las capas necesarias
                var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");

                //Carga capa Catastro
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
                }

                ////Carga capa Distrito
                //if (datum == datumwgs84)
                //{
                //    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_WGS + zoneDm, false);
                //}
                //else
                //{
                //    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_Distrito_Z + zoneDm, false);
                //}
                //await CommonUtilities.ArcgisProUtils.SymbologyUtils.ColorPolygonSimple(featureClassLoader.pFeatureLayer_dist);
                //Carga capa Zona Urbana
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaWgs84 + zoneDm, false); //"DATA_GIS.GPO_ZUR_ZONA_URBANA_WGS_"
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_ZUrbanaPsad56 + zoneDm, false);
                }
                //var resultado = "'17-I','18-i'";

                int este_min = 0;
                int norte_min = 0;
                int este_max = 0;
                int norte_max = 0;
                DataTable coordenadasTable = dataBaseHandler.GetOfficialCartaLimite("CODIGO", ResultadoCarta, datumStr);
                if (coordenadasTable.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    foreach (DataRow row in coordenadasTable.Rows)
                    {
                        este_min = Convert.ToInt32(row["XMIN"]);
                        norte_min = Convert.ToInt32(row["YMIN"]);
                        este_max = Convert.ToInt32(row["XMAX"]);
                        norte_max = Convert.ToInt32(row["YMAX"]);
                    }
                };

                int Tbx_EsteMin = este_min;// int.Parse(TbxEsteMin.Text);
                int Tbx_EsteMax = este_max; // int.Parse(TbxEsteMax.Text);
                int Tbx_NorteMin = norte_min; // int.Parse(TbxNorteMin.Text);
                int Tbx_NorteMax = norte_max; // int.Parse(TbxNorteMax.Text);

                var extentDmRadio = ObtenerExtent(Tbx_EsteMin, Tbx_NorteMin, Tbx_EsteMax, Tbx_NorteMax, datum, radio);
                var extentDm = ObtenerExtent(Tbx_EsteMin, Tbx_NorteMin, Tbx_EsteMax, Tbx_NorteMax, datum);
                GlobalVariables.currentExtentDM = extentDm;

                // Llamar al método IntersectFeatureClassAsync desde la instancia
                string listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);
                // Encontrando Distritos superpuestos a DM con
                //DataTable intersectDist;
                //if (datum == datumwgs84)
                //{
                //    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_WGS_" + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectDist = dataBaseHandler.IntersectOracleFeatureClass("4", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, "DATA_GIS.GPO_DIS_DISTRITO_" + zoneDm, codigoValue);
                //}
                //CommonUtilities.DataProcessorUtils.ProcessorDataAreaAdminstrative(intersectDist);
                //DataTable orderUbigeosDM;
                //orderUbigeosDM = dataBaseHandler.GetUbigeoData(codigoValue);

                //Carga capa Hojas IGN
                if (datum == datumwgs84)
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
                }
                else
                {
                    await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta56, false);
                }
                string listHojas = await featureClassLoader.IntersectFeatureClassAsync("Carta IGN", extentDm.xmin, extentDm.ymin, extentDm.xmax, extentDm.ymax);
                //GlobalVariables.CurrentPagesDm = listHojas;
                // Encontrando Caram superpuestos a DM con

                //DataTable intersectCaram;
                //if (datum == datumwgs84)
                //{
                //    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, FeatureClassConstants.gstrFC_Caram84 + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectCaram = dataBaseHandler.IntersectOracleFeatureClass("81", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_Caram56 + zoneDm, codigoValue);
                //}
                //CommonUtilities.DataProcessorUtils.ProcessorDataCaramIntersect(intersectCaram);

                //DataTable intersectCForestal;
                //if (datum == datumwgs84)
                //{
                //    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, FeatureClassConstants.gstrFC_Cforestal + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectCForestal = dataBaseHandler.IntersectOracleFeatureClass("93", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_forestal + zoneDm, codigoValue);
                //}
                //CommonUtilities.DataProcessorUtils.ProcessorDataCforestalIntersect(intersectCForestal);

                //DataTable intersectDm;
                //if (datum == datumwgs84)
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroWGS84, FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, codigoValue);
                //}
                //else
                //{
                //    intersectDm = dataBaseHandler.IntersectOracleFeatureClass("24", FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, codigoValue);
                //}
                //DataTable distBorder;
                //var distBorder = dataBaseHandler.CalculateDistanceToBorder(codigoValue, zoneDm, datumStr);
                //GlobalVariables.DistBorder = Math.Round(Convert.ToDouble(distBorder.Rows[0][0]) / 1000.0, 3);
                //CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listCodigoColin, out string listCodigoSup, out List<string> colectionsAreaSup);
                //await CommonUtilities.ArcgisProUtils.LayerUtils.AddLayerAsync(map,Path.Combine(outputFolder, catastroShpNamePath));
                await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.AgregarCampoTemaTpm(catastroShpName, "Catastro");
                await UpdateValueAsync(catastroShpName, "");

                //CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.ProcessOverlapAreaDm(intersectDm, out string listaCodigoColin, out string listaCodigoSup, out List<string> coleccionesAareaSup);
                //await CommonUtilities.ArcgisProUtils.FeatureProcessorUtils.UpdateRecordsDmAsync(catastroShpName, listaCodigoColin, listaCodigoSup, coleccionesAareaSup);
                //await featureClassLoader.ExportAttributesTemaAsync(catastroShpName, GlobalVariables.stateDmY, dmShpName, $"CODIGOU='{codigoValue}'");
                string styleCat = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleCatastro);
                await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(catastroShpName, styleCat, "LEYENDA", StyleItemType.PolygonSymbol);

                //var Params = Geoprocessing.MakeValueArray(catastroShpNamePath, codigoValue);
                //var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetEval, Params);
                CommonUtilities.ArcgisProUtils.LayerUtils.SelectSetAndZoomByNameAsync(catastroShpName, false);
                List<string> layersToRemove = new List<string>() { "Catastro", "Carta IGN", dmShpName, "Zona Urbana" };
                await CommonUtilities.ArcgisProUtils.LayerUtils.RemoveLayersFromActiveMapAsync(layersToRemove);
                await CommonUtilities.ArcgisProUtils.LayerUtils.ChangeLayerNameAsync(catastroShpName, "Catastro");
                GlobalVariables.CurrentShpName = "Catastro";
                MapUtils.AnnotateLayerbyName("Catastro", "CONTADOR", "DM_Anotaciones");
                UTMGridGenerator uTMGridGenerator = new UTMGridGenerator();
                var (gridLayer, pointLayer) = await uTMGridGenerator.GenerateUTMGridAsync(extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, "Malla", zoneDm);
                await uTMGridGenerator.AnnotateGridLayer(pointLayer, "VALOR");
                await uTMGridGenerator.RemoveGridLayer("Malla", zoneDm);
                string styleGrid = Path.Combine(GlobalVariables.stylePath, GlobalVariables.styleMalla);
                await CommonUtilities.ArcgisProUtils.SymbologyUtils.ApplySymbologyFromStyleAsync(gridLayer.Name, styleGrid, "CLASE", StyleItemType.LineSymbol);
                try
                {
                    // Itera todos items seleccionados en el ListBox de WPF
                    foreach (var item in LayersListBox.Items)
                    {
                        if (item is CheckBox checkBox && checkBox.IsChecked == true)
                        {
                            string capaSeleccionada = checkBox.Content.ToString();
                            await LayerUtils.AddLayerCheckedListBox(capaSeleccionada, zoneDm, featureClassLoader, datum, extentDmRadio);
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error en capa de listado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex) { }
            BtnGraficar.IsEnabled = true;

            //BtnGraficar.IsEnabled = false;
            //int FlagL = listBoxVertices.Items.Count;
            //if (FlagL == 0)
            //{
            //    MessageBox.Show(
            //       "Por favor, escribir nombre de la carta. ejm: 19-i.",
            //       "Sin Selección",
            //       MessageBoxButton.OK,
            //       MessageBoxImage.Information);
            //    return;
            //}
            //if (ChkGraficarDmY.IsChecked == true)
            //{
            //    GlobalVariables.stateDmY = true;
            //}
            //else
            //{
            //    GlobalVariables.stateDmY = false;
            //}
            //int datum = (int)CbxSistema.SelectedValue;
            //string datumStr = CbxSistema.Text;
            //int radio = int.Parse(TbxRadio.Text);
            //string outputFolder = Path.Combine(GlobalVariables.pathFileContainerOut, GlobalVariables.fileTemp);

            //await CommonUtilities.ArcgisProUtils.MapUtils.CreateMapAsync("CATASTRO MINERO");
            ////int focusedRowHandle = DataGridResult.GetSelectedRowHandles()[0];
            ////string codigoValue = DataGridResult.GetCellValue(focusedRowHandle, "CODIGO")?.ToString();
            ////GlobalVariables.CurrentCodeDm = codigoValue;
            ////string stateGraphic = DataGridResult.GetCellValue(focusedRowHandle, "PE_VIGCAT")?.ToString();
            ////string zoneDm = DataGridResult.GetCellValue(focusedRowHandle, "ZONA")?.ToString();
            ////GlobalVariables.CurrentZoneDm = zoneDm;
            //var sdeHelper = new DatabaseConnector.SdeConnectionGIS();
            //Geodatabase geodatabase = await sdeHelper.ConnectToOracleGeodatabaseAsync(AppConfig.serviceNameGis
            //                                                                            , AppConfig.userName
            //                                                                            , AppConfig.password);
            ////var v_zona_dm = "";
            //string zoneDm = CbxZona.SelectedValue.ToString();
            ////string fechaArchi = DateTime.Now.Ticks.ToString();
            ////GlobalVariables.idExport = fechaArchi;
            ////string catastroShpName = "Catastro" + fechaArchi;
            ////GlobalVariables.CurrentShpName = catastroShpName;
            ////string catastroShpNamePath = "Catastro" + fechaArchi + ".shp";
            ////string dmShpName = "DM" + fechaArchi;
            ////string dmShpNamePath = "DM" + fechaArchi + ".shp";
            //try
            //{
            //    // Obtener el mapa Catastro//

            //    Map map = await EnsureMapViewIsActiveAsync(GlobalVariables.mapNameCatastro); // "CATASTRO MINERO"
            //    // Crear instancia de FeatureClassLoader y cargar las capas necesarias
            //    var featureClassLoader = new FeatureClassLoader(geodatabase, map, zoneDm, "99");

            //    //Carga capa Catastro
            //    if (datum == datumwgs84)
            //    {
            //        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta84, false);
            //    }
            //    else
            //    {
            //        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_HCarta56, false);
            //    }


            //    BtnGraficar.IsEnabled = true;

            //    string resultado = "";
            //    // Recorrer el ListBox con un bucle for
            //    for (int i = 0; i < listBoxVertices.Items.Count; i++)
            //    {
            //        string item = listBoxVertices.Items[i].ToString();
            //        // Dividir el texto por el delimitador
            //        string[] partes = item.Split(":");

            //        string dato = partes[1];
            //        dato = dato.Trim();
            //        if (i == listBoxVertices.Items.Count - 1)
            //        {
            //            resultado = resultado + "'" + dato + "'";
            //        }
            //        else
            //        {
            //            resultado = resultado + "'" + dato + "'" + ",";
            //        }

            //        // Mostrar las partes
            //        //foreach (string parte in partes)
            //        //{
            //        //    Console.WriteLine(parte);
            //        //}
            //    }
            //    MessageBox.Show(resultado);
            //    //FeatureInfo selectedFeature = await GetFeatureInfobyQuery($"{resultado}'", "Carta IGN");
            //    FeatureInfo selectedFeature = await GetFeatureInfobyQuery($"CD_HOJA IN ({resultado})", "Carta IGN");


            //    //Carga capa Catastro
            //    if (datum == datumwgs84)
            //    {
            //        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroWGS84 + zoneDm, false);
            //    }
            //    else
            //    {
            //        await featureClassLoader.LoadFeatureClassAsync(FeatureClassConstants.gstrFC_CatastroPSAD56 + zoneDm, false);
            //    }

            //    //var extentDmRadio = ObtenerExtent(codigoValue, datum, radio);
            //    //var extentDm = ObtenerExtent(codigoValue, datum);
            //    //GlobalVariables.currentExtentDM = extentDm;
            //    //// Llamar al método IntersectFeatureClassAsync desde la instancia
            //    //string listDms = await featureClassLoader.IntersectFeatureClassAsync("Catastro", extentDmRadio.xmin, extentDmRadio.ymin, extentDmRadio.xmax, extentDmRadio.ymax, catastroShpName);
            //    //// Encontrando Distritos superpuestos a DM con

            //    //var Params = Geoprocessing.MakeValueArray(layerName, folderName, id, 1);
            //    //var response = await GlobalVariables.ExecuteGPAsync(GlobalVariables.toolBoxPathEval, GlobalVariables.toolGetAreasOverlay, Params);



            //}




            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show("Error en UpdateValue: " + ex.Message);
            //}
        }
        private ExtentModel ObtenerExtent(int XMin, int YMin, int XMax, int YMax, int datum, int radioKm = 0)
        {
            //// Obtener las coordenadas usando la función ObtenerCoordenadas
            //DataTable coordenadasTable = ObtenerCoordenadas(codigoValue, datum);

            //// Asegurarse de que la tabla contiene filas
            //if (coordenadasTable.Rows.Count == 0)
            //{
            //    throw new Exception("No se encontraron coordenadas para calcular el extent.");
            //}
            int radioMeters = radioKm * 1000;
            // Inicializar las variables para almacenar los valores extremos
            int xmin = XMin; // int.MaxValue;
            int xmax = XMax; // int.MinValue;
            int ymin = YMin; // int.MaxValue;
            int ymax = YMax; // int.MinValue;

            // Iterar sobre las filas para calcular los valores extremos
            //foreach (DataRow row in coordenadasTable.Rows)
            //{
            //    int este = Convert.ToInt32(row["ESTE"]);
            //    int norte = Convert.ToInt32(row["NORTE"]);

            //    if (este < xmin) xmin = este;
            //    if (este > xmax) xmax = este;
            //    if (norte < ymin) ymin = norte;
            //    if (norte > ymax) ymax = norte;
            //}

            // Crear el objeto ExtentModel con los valores calculados
            ExtentModel extent = new ExtentModel
            {
                xmin = xmin - radioMeters,
                xmax = xmax + radioMeters,
                ymin = ymin - radioMeters,
                ymax = ymax + radioMeters
            };

            return extent;
        }

        public DataTable FilterColumns(DataTable originalTable, params string[] columnNames)
        {
            // Crear un nuevo DataTable para las columnas seleccionadas
            DataTable filteredTable = new DataTable();

            // Agregar las columnas seleccionadas al nuevo DataTable
            foreach (string columnName in columnNames)
            {
                if (originalTable.Columns.Contains(columnName))
                {
                    filteredTable.Columns.Add(columnName, originalTable.Columns[columnName].DataType);
                }
                else
                {
                    throw new ArgumentException($"La columna '{columnName}' no existe en el DataTable original.");
                }
            }

            // Copiar filas con los valores de las columnas seleccionadas
            foreach (DataRow row in originalTable.Rows)
            {
                DataRow newRow = filteredTable.NewRow();
                foreach (string columnName in columnNames)
                {
                    newRow[columnName] = row[columnName];
                }
                filteredTable.Rows.Add(newRow);
            }

            return filteredTable;
        }
        private DataTable ObtenerCoordenadas(string codigoValue, int datum)
        {
            DataTable filteredTable;
            string[] requiredColumns = {
                    DatagridDetailsConstants.RawColumNames.Vertice,
                    DatagridDetailsConstants.RawColumNames.CoorEsteE,
                    DatagridDetailsConstants.RawColumNames.CoorNorteE };

            var dmrRecords = dataBaseHandler.GetDMDataWGS84_IN(codigoValue);

            if (datum == datumwgs84)
            {
                requiredColumns = new string[] {
                    DatagridDetailsConstants.RawColumNames.Vertice,
                    DatagridDetailsConstants.RawColumNames.CoorEste,
                    DatagridDetailsConstants.RawColumNames.CoorNorte };
            }
            filteredTable = FilterColumns(dmrRecords, requiredColumns);
            // Renombrar las columnas
            filteredTable.Columns[DatagridDetailsConstants.RawColumNames.Vertice].ColumnName = DatagridDetailsConstants.ColumnNames.Vertice;
            filteredTable.Columns[requiredColumns[1]].ColumnName = DatagridDetailsConstants.ColumnNames.Este;
            filteredTable.Columns[requiredColumns[2]].ColumnName = DatagridDetailsConstants.ColumnNames.Norte;

            return filteredTable;
        }



        private ExtentModel ObtenerExtent(string codigoValue, int datum, int radioKm = 0)
        {
            // Obtener las coordenadas usando la función ObtenerCoordenadas
            DataTable coordenadasTable = ObtenerCoordenadas(codigoValue, datum);

            // Asegurarse de que la tabla contiene filas
            if (coordenadasTable.Rows.Count == 0)
            {
                throw new Exception("No se encontraron coordenadas para calcular el extent.");
            }
            int radioMeters = radioKm * 1000;
            // Inicializar las variables para almacenar los valores extremos
            int xmin = int.MaxValue;
            int xmax = int.MinValue;
            int ymin = int.MaxValue;
            int ymax = int.MinValue;

            // Iterar sobre las filas para calcular los valores extremos
            foreach (DataRow row in coordenadasTable.Rows)
            {
                int este = Convert.ToInt32(row["ESTE"]);
                int norte = Convert.ToInt32(row["NORTE"]);

                if (este < xmin) xmin = este;
                if (este > xmax) xmax = este;
                if (norte < ymin) ymin = norte;
                if (norte > ymax) ymax = norte;
            }

            // Crear el objeto ExtentModel con los valores calculados
            ExtentModel extent = new ExtentModel
            {
                xmin = xmin - radioMeters,
                xmax = xmax + radioMeters,
                ymin = ymin - radioMeters,
                ymax = ymax + radioMeters
            };

            return extent;
        }

        public async Task<FeatureInfo> GetFeatureInfobyQuery(string p_Filtro, string layerName)
        {
            Feature selectedFeature = null;
            FeatureInfo featureInfo = new FeatureInfo();

            await QueuedTask.Run(() =>
            {
                // Obtener la capa por nombre
                var layer = MapView.Active.Map.Layers.FirstOrDefault(l => l.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase)) as FeatureLayer;

                if (layer == null)
                {
                    MessageBox.Show("No se encuentra el Layer");
                    return;
                }

                // Crear un query filter
                var queryFilter = new ArcGIS.Core.Data.QueryFilter
                {
                    WhereClause = p_Filtro
                    //WhereClause = $"CD_HOJA IN ({p_Filtro})"//p_Filtro
                };

                using (var rowCursor = layer.Search(queryFilter))
                {
                    if (rowCursor.MoveNext())
                    {
                        selectedFeature = rowCursor.Current as Feature;
                    }
                }

                if (selectedFeature == null)
                {
                    MessageBox.Show("No hay ninguna Selección");
                }
                else
                {
                    layer.Select(queryFilter);
                };


                //featureInfo.Codigo = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                //featureInfo.Nombre = selectedFeature.GetOriginalValue(selectedFeature.FindField("CONCESION")).ToString();
                //featureInfo.Fecha = selectedFeature.GetOriginalValue(selectedFeature.FindField("FEC_DENU")).ToString();
                //featureInfo.Area = selectedFeature.GetOriginalValue(selectedFeature.FindField("HECTAREA")).ToString();
                //featureInfo.Titular = selectedFeature.GetOriginalValue(selectedFeature.FindField("TIT_CONCES")).ToString();
                //featureInfo.TipoDM = selectedFeature.GetOriginalValue(selectedFeature.FindField("D_ESTADO")).ToString();
                ////featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                ////featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                ////featureInfo.TbxCodigo.Text = selectedFeature.GetOriginalValue(selectedFeature.FindField("CODIGOU")).ToString();
                //featureInfo.Contador = selectedFeature.GetOriginalValue(selectedFeature.FindField("CONTADOR")).ToString();
                //featureInfo.Hora = selectedFeature.GetOriginalValue(selectedFeature.FindField("HOR_DENU")).ToString();
                //featureInfo.Prioridad = selectedFeature.GetOriginalValue(selectedFeature.FindField("EVAL")).ToString();
            });

            return featureInfo;
        }

        public class FeatureInfo
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Fecha { get; set; }
            public string Area { get; set; }
            public string Titular { get; set; }
            public string TipoDM { get; set; }
            public string Contador { get; set; }
            public string Hora { get; set; }
            public string Prioridad { get; set; }
        }

        private void CbxZona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

