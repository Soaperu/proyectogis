using ArcGIS.Core.Data;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Internal.Core;
using ArcGIS.Desktop.Internal.Mapping.Controls.TimeSlider;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using ArcGIS.Core.Data.UtilityNetwork.Trace;
using ArcGIS.Core.Internal.CIM;
using QueryFilter = ArcGIS.Core.Data.QueryFilter;
using Path = System.IO.Path;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CommonUtilities.ArcgisProUtils
{
    public class LayoutUtils
    {
        //private const string V = "Plantilla_evd_84";
        private  LayoutConfiguration? _config;
        public LayoutUtils(LayoutConfiguration config)
        {
            _config = config;
        }
        public static async Task<LayoutProjectItem> AddLayoutPath(string layoutFilePath, string nameLayer, string mapName, string layoutName, int scale = 1)
        {
            // Verificar si el archivo existe
            if (!File.Exists(layoutFilePath))
            {
                MessageBox.Show($"No se encontró el archivo de layout: {layoutFilePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Ejecutar la tarea en el hilo principal de CIM
            #pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            #pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return await QueuedTask.Run(async () =>
            {
                try
                {
                    // Obtener la lista actual de mapas antes de agregar el layout
                    var mapsBefore = Project.Current.GetItems<MapProjectItem>().Select(m => m.Name).ToList();

                    var addItem = ItemFactory.Instance.Create(layoutFilePath, ItemFactory.ItemType.PathItem) as IProjectItem;
                    // Agregar el layout al proyecto actual
                    Project.Current.AddItem(addItem);
                    LayoutProjectItem layoutItem = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(l => string.Equals(l.Name, layoutName, StringComparison.OrdinalIgnoreCase));
                    // Verificar si la capa fue agregada correctamente
                    //LayoutProjectItem layout = layoutProjectItem as LayoutProjectItem;
                    Layout layout = layoutItem.GetLayout();
                    if (layout != null)
                    {
                        // Abrir la vista del layout
                        //await ActivateLayoutAsync(layout);

                        // Mostrar un mensaje de éxito
                        //MessageBox.Show($"Layout '{layout.Name}' agregado y abierto exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        //layout.SetName(mapName);
                        MapFrame mfrm = layout.FindElement( mapName + " Map Frame") as MapFrame;
                        Map mapCatastro = await MapUtils.FindMapByNameAsync(mapName);
                        mfrm.SetMap(mapCatastro);
                        var zoomNameLayer = mapCatastro.GetLayersAsFlattenedList().OfType<Layer>().FirstOrDefault(l => l.Name == nameLayer);
                        var fLayer = (FeatureLayer)zoomNameLayer;
                        FeatureClass featureClass = fLayer.GetFeatureClass();
                        // Obtener la definición del FeatureClass para acceder a su extensión
                        FeatureClassDefinition fcDef = featureClass.GetDefinition() as FeatureClassDefinition;
                        if (fcDef != null)
                        {
                            ArcGIS.Core.Geometry.Envelope layerExtent = fcDef.GetExtent();
                            mfrm.SetCamera(layerExtent);
                            if (scale > 1)
                            {
                                Camera cam = mfrm.Camera;
                                cam.Scale = scale;
                                mfrm.SetCamera(cam);
                            }
                        }
                        // Obtener la lista de mapas después de agregar el layout
                        var mapsAfter = Project.Current.GetItems<MapProjectItem>().Select(m => m.Name).ToList();

                        // Identificar el mapa nuevo que se ha agregado
                        var newMaps = mapsAfter.Except(mapsBefore).ToList();
                        foreach (var newMapName in newMaps)
                        {
                            // Evitar eliminar el mapa que deseas mantener
                            if (!string.Equals(newMapName, mapName, StringComparison.OrdinalIgnoreCase))
                            {
                                var mapToRemove = Project.Current.GetItems<MapProjectItem>().FirstOrDefault(m => m.Name == newMapName);
                                if (mapToRemove != null)
                                {
                                    Project.Current.RemoveItem(mapToRemove);
                                }
                            }
                        }

                        return layoutItem;
                    }
                    else
                    {
                        // Manejar el caso en que la capa no es una LayoutProjectItem
                        MessageBox.Show("No se pudo agregar el layout. Asegúrate de que el archivo es un .pagx válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                    // Manejar cualquier excepción que ocurra durante el proceso
                    MessageBox.Show($"Ocurrió un error al agregar el layout: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            #pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
            #pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }

        public static async Task ActivateLayoutAsync(Layout layout)
        {
            await QueuedTask.Run(() =>
            {
                var layoutProjItem = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(mpi => mpi.Name == layout.Name);
                if (layoutProjItem != null)
                {
                    // Activa el layout en la vista
                    //MappingModule.ActiveMapView = MapView.Active;
                    var mapPane = ProApp.Panes.OfType<ILayoutPane>().FirstOrDefault(mPane => (mPane as Pane).ContentID == layoutProjItem.Path);
                    if (mapPane != null)
                    {
                        var pane = mapPane as Pane;
                        pane.Activate();
                    }
                }
            });
        }
        /// <summary>
        /// Elimina los diseños (layouts) del proyecto actual cuyos nombres coincidan con la lista proporcionada.
        /// Si la lista está vacía o es nula, pregunta al usuario si desea eliminar todos los layouts.
        /// </summary>
        /// <param name="layoutNamesToDelete">
        /// Lista de nombres de layouts a eliminar. Puede ser nula o vacía.
        /// </param>
        public static async Task DeleteSpecifiedLayoutsAsync(List<string>? layoutNamesToDelete = null)
        {
            // Verificar si no se proporcionó lista o está vacía
            if (layoutNamesToDelete == null || layoutNamesToDelete.Count == 0)
            {
                var result = MessageBox.Show(
                    "No se proporcionaron nombres de layouts para eliminar. ¿Desea eliminar todos los layouts del proyecto?",
                    "Advertencia",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    return; // Usuario canceló la operación
                }
            }

            // Ejecuta la operación en el contexto de ArcGIS Pro
            await QueuedTask.Run(() =>
            {
                // Obtiene todos los layouts del proyecto actual
                var allLayouts = Project.Current.GetItems<LayoutProjectItem>();

                // Para evitar problemas al eliminar mientras iteramos, usamos .ToList()
                foreach (var layoutItem in allLayouts.ToList())
                {
                    // Si layoutNamesToDelete es null o vacío, eliminamos todos
                    // De lo contrario, eliminamos solo los que coincidan por nombre (ignorando mayúsc/minúsc)
                    if ((layoutNamesToDelete == null || layoutNamesToDelete.Count == 0) ||
                         layoutNamesToDelete.Contains(layoutItem.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        Project.Current.RemoveItem(layoutItem);
                    }
                }
            });
        }

        /// <summary>
        /// Exporta un layout a PDF dado su nombre y la ruta de salida.
        /// </summary>
        /// <param name="layoutName">El nombre del layout a exportar.</param>
        /// <param name="outputPdfPath">Ruta completa (incluyendo el archivo) donde se guardará el PDF.</param>
        /// <returns>Tarea asíncrona.</returns>
        public static async Task ExportLayoutToPdfAsync(string layoutName, string outputPdfPath)
        {
            // Verificar que la ruta no sea nula o vacía
            if (string.IsNullOrEmpty(outputPdfPath))
            {
                throw new ArgumentException("La ruta de salida para el PDF es inválida.", nameof(outputPdfPath));
            }

            // Asegurarnos de que el directorio exista
            string directory = Path.GetDirectoryName(outputPdfPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Ejecutar en el hilo especializado de ArcGIS Pro
            await QueuedTask.Run(() =>
            {
                // 1. Buscar el LayoutProjectItem con el nombre especificado
                var layoutItem = Project.Current
                                        .GetItems<LayoutProjectItem>()
                                        .FirstOrDefault(item =>
                                                string.Equals(item.Name, layoutName, StringComparison.OrdinalIgnoreCase));
                if (layoutItem == null)
                {
                    throw new InvalidOperationException($"No se encontró el layout con nombre '{layoutName}'.");
                }

                // 2. Obtener el objeto Layout a partir del LayoutProjectItem
                Layout layout = layoutItem.GetLayout();

                // 3. Configurar los parámetros de exportación a PDF
                //    Puedes ajustar la resolución, la calidad de compresión, etc.
                ExportFormat pdfFormat = new PDFFormat
                {
                    OutputFileName = outputPdfPath,
                    Resolution = 300,             // DPI (puntos por pulgada)
                    DoCompressVectorGraphics = true,
                    ImageCompression = ImageCompression.Adaptive,
                    ImageCompressionQuality = 80, // Calidad de compresión de imágenes (0-100)
                };

                // 4. Exportar el layout a PDF
                layout.Export(pdfFormat);

                // (Opcional) Si quieres mostrar un mensaje de éxito o log
                //MessageBox.Show($"Layout exportado correctamente a: {outputPdfPath}");
            });
        }

        /// <summary>
        /// Determina la ruta de la plantilla basada en el tipo de plano y otras configuraciones.
        /// </summary>
        /// <param name="tipoPlano">Tipo de plano.</param>
        /// <returns>Ruta completa de la plantilla.</returns>
        public string DeterminarRutaPlantilla(string tipoPlano)
        {
            string rutaPlantilla = string.Empty;

            switch (tipoPlano)
            {
                case "Reporte previo":
                    rutaPlantilla = Path.Combine(_config.BasePath, "Plantilla_listadodm1.pagx");
                    break;

                case "Plano reporte A4":
                    // Define la ruta para "Plano reporte A4"
                    rutaPlantilla = Path.Combine(_config.BasePath, "Plantilla_reporte_A4.pagx");
                    break;

                case "Plano catastral":
                    if (_config.Sistema.Equals("PSAD-56", StringComparison.OrdinalIgnoreCase))
                    {
                        rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                            ? Path.Combine(_config.BasePath, "Plantilla_evd_56_urb.pagx")
                            : Path.Combine(_config.BasePath, "Plantilla_evd_56.pagx");
                    }
                    else if (_config.Sistema.Equals("WGS-84", StringComparison.OrdinalIgnoreCase))
                    {
                        rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                            ? Path.Combine(_config.BasePath, "Plantilla_evd_84_urb.pagx")
                            : Path.Combine(_config.BasePath, "Plantilla_evd_84.pagx");
                    }
                    break;

                case "Plano Masivo":
                    rutaPlantilla = _config.CasoLdMasivo.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "Plantilla_LD_masivo_84.pagx")
                        : Path.Combine(_config.BasePath, "Plantilla_ev_masivo_84.pagx");
                    break;

                case "Plano Masivo Cuadricula":
                    rutaPlantilla = Path.Combine(_config.BasePath, "Plantilla_ev_masivo_cuad_84.pagx");
                    break;

                case "Plano Libredenu":
                    rutaPlantilla = Path.Combine(_config.BasePath, "Plantilla_libredenu.pagx");
                    break;

                case "Plano Area superpuesta":
                    rutaPlantilla = Path.Combine(_config.BasePath, $"Plantilla_sup{_config.ContaHojaSup}.pagx");
                    break;

                case "Plano Area Restringida":
                    rutaPlantilla = Path.Combine(_config.BasePath, "Plantilla_area_restringida2.pagx");
                    break;

                case "Plano Acumulacion":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_acumulacion.pagx");
                    break;

                case "Plano Integrantes_Acumulacion":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_integrantes_acumulacion.pagx");
                    break;

                case "Plano Acumulacion_Afectacion":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_acumulacion_afectacion.pagx");
                    break;

                case "Plano Acumulacion_Beneficio":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_acumulacion_beneficio.pagx");
                    break;

                case "Plano Acumulacion_Arestringida":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_acumulacion_arestringida.pagx");
                    break;

                case "Plano Division":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_division_coordenadas.pagx");
                    break;

                case "Plano Renuncia":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_renuncia_coordenadas.pagx");
                    break;

                case "Plano Renuncia_Arestringida":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_renuncia_arestringida.pagx");
                    break;

                case "Plano Integrantes_UEA":
                case "Plano integrado_UEA":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_integrantes_uea.pagx");
                    break;

                case "Plano inclusion_exclusion_UEA":
                    rutaPlantilla = _config.TablaIntegrantesCount < 30
                        ? Path.Combine(_config.BasePath, "plantilla_uea.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_uea_incluido_excluido.pagx");
                    break;

                case "Plano Inclusion_UEA":
                    rutaPlantilla = _config.TablaIntegrantesCount < 30
                        ? Path.Combine(_config.BasePath, "plantilla_uea.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_uea_incluido.pagx");
                    break;

                case "Plano Exclusion_UEA":
                    rutaPlantilla = _config.TablaIntegrantesCount < 30
                        ? Path.Combine(_config.BasePath, "plantilla_uea.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_uea_excluido.pagx");
                    break;

                case "Plano Analisis_UEA":
                case "Plano dm_circulo":
                case "Plano petitorios_PMA":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_uea.pagx");
                    break;

                case "Plano Demarca":
                    rutaPlantilla = _config.Sistema.Equals("PSAD-56", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_demarca_56.pagx")
                        : _config.Sistema.Equals("WGS-84", StringComparison.OrdinalIgnoreCase)
                            ? Path.Combine(_config.BasePath, "plantilla_demarca_84.pagx")
                            : string.Empty;
                    break;

                case "Plano Geologico":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_Geologico.pagx");
                    break;

                case "Plano Cuadricula":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_cuadricula.pagx");
                    break;

                case "Plano carta":
                    rutaPlantilla = _config.Sistema.Equals("PSAD-56", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_cartaign_56.pagx")
                        : _config.Sistema.Equals("WGS-84", StringComparison.OrdinalIgnoreCase)
                            ? Path.Combine(_config.BasePath, "plantilla_cartaign_84.pagx")
                            : string.Empty;
                    break;

                case "Plano Simultaneo":
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_sim.pagx");
                    break;

                case "Plano Venta":
                    rutaPlantilla = DetermineSalesPlanTemplate();
                    break;

                case "Plano_variado":
                    rutaPlantilla = DetermineTemplateVariablePlane();
                    break;

                default:
                    // Tipo de plano no reconocido
                    rutaPlantilla = string.Empty;
                    break;
            }

            return rutaPlantilla;
        }

        /// <summary>
        /// Determina la plantilla para el tipo de plano "Plano Venta".
        /// </summary>
        /// <returns>Ruta completa de la plantilla.</returns>
        private string DetermineSalesPlanTemplate()
        {
            string rutaPlantilla = string.Empty;

            if (_config.SelePlano.Equals("Formato A4", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_pv_A4_m_Urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_A4_m.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_A4_m_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Formato A3", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_pv_A3_m_Urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_A3_m.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_A3_m_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Formato A2", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_pv_A2_Urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_A2.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_A2_m_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Formato A0", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_pv_A0_Urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_A0.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_A0_m_situacion.pagx");
                }
            }

            return rutaPlantilla;
        }

        /// <summary>
        /// Determina la plantilla para el tipo de plano "Plano_variado".
        /// </summary>
        /// <returns>Ruta completa de la plantilla.</returns>
        private string DetermineTemplateVariablePlane()
        {
            string rutaPlantilla = string.Empty;

            if (_config.SelePlano.Equals("Plano A4 Vertical", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_VA4_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_VA4.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_VA4_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A4 Horizontal", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "Plantilla_PV_HA4_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_HA4.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_HA4_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A3 Vertical", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_VA3_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_VA3.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_VA3_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A3 Horizontal", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_HA3_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_HA3.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_HA3_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A2 Vertical", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_VA2_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_VA2.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_VA2_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A2 Horizontal", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_HA2_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_HA2.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_HA2_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A1 Vertical", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_VA1_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_VA1.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_VA1_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A1 Horizontal", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_HA1_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_HA1.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_HA1_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A0 Vertical", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_VA0_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_VA0.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_VA0_situacion.pagx");
                }
            }
            else if (_config.SelePlano.Equals("Plano A0 Horizontal", StringComparison.OrdinalIgnoreCase))
            {
                if (_config.SeleccionPlanoSi.Equals("Plano Catastral Publico", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = _config.ValidaUrbShp.Equals("1", StringComparison.OrdinalIgnoreCase)
                        ? Path.Combine(_config.BasePath, "plantilla_PV_HA0_urb.pagx")
                        : Path.Combine(_config.BasePath, "plantilla_pv_HA0.pagx");
                }
                else if (_config.SeleccionPlanoSi.Equals("Plano Catastral Situacion DM", StringComparison.OrdinalIgnoreCase))
                {
                    rutaPlantilla = Path.Combine(_config.BasePath, "plantilla_pv_HA0_situacion.pagx");
                }
            }

            return rutaPlantilla;
        }

        public static Task<string> GeneratorTextListVerticesDecrease(RowCursor rowCursor)
        {
            string texto = "";

            return QueuedTask.Run(() =>
            {
                using (Row row = rowCursor.Current)
                {
                    var geometry = row["SHAPE"] as ArcGIS.Core.Geometry.Geometry;
                    var polygon = geometry as ArcGIS.Core.Geometry.Polygon;
                    texto += $"Nombre = {row["RESULTADO"]} - {row["CONCATENAT"]}\n";

                    texto += "--------------------------------------------------\n";
                    texto += new string(' ', 3) + " Vert." + new string(' ', 10) + "Norte" + new string(' ', 10) + "Este\n";
                    texto += "--------------------------------------------------\n";
                    for (int i = 0; i < polygon.PointCount - 1; i++)
                        {
                            var vertex = polygon.Points[i];

                            var este = Math.Round(vertex.X, 3);
                            var norte = Math.Round(vertex.Y, 3);
                            var esteFormateado = string.Format("{0:### ###.#0}", este);
                            var norteFormateado = string.Format("{0:# ### ###.#0}", norte);
                            texto += new string(' ', 5) + (i + 1).ToString().PadLeft(3, '0');
                            texto += new string(' ', 5) + esteFormateado;
                            texto += new string(' ', 5) + norteFormateado + "\n";
                        }
                    texto += "--------------------------------------------------\n";
                    texto += new string(' ', 10) + $"Área UTM = {Math.Round(polygon.Area / 10000,2)} (Ha)\n";
                    texto += "--------------------------------------------------\n";

                    return texto;
                }
                    //}
                //}
            });
        }

        public static Task<string> GeneratorTextListVerticesSimul(RowCursor rowCursor)
        {
            string texto = "";

            return QueuedTask.Run(() =>
            {
                using (Row row = rowCursor.Current)
                {
                    var geometry = row["SHAPE"] as ArcGIS.Core.Geometry.Geometry;
                    var polygon = geometry as ArcGIS.Core.Geometry.Polygon;
                    //texto += $"Nombre = {row["RESULTADO"]} - {row["CONCATENAT"]}\n";

                    //texto += "--------------------------------------------------\n";
                    //texto += new string(' ', 3) + " Vert." + new string(' ', 10) + "Norte" + new string(' ', 10) + "Este\n";
                    //texto += "--------------------------------------------------\n";
                    for (int i = 0; i < polygon.PointCount - 1; i++)
                    {
                        var vertex = polygon.Points[i];

                        var este = Math.Round(vertex.X, 3);
                        var norte = Math.Round(vertex.Y, 3);
                        var esteFormateado = string.Format("{0:### ###.#0}", este);
                        var norteFormateado = string.Format("{0:# ### ###.#0}", norte);
                        texto += new string(' ', 5) + (i + 1).ToString().PadLeft(3, '0');
                        texto += new string(' ', 5) + esteFormateado;
                        texto += new string(' ', 5) + norteFormateado + "\n";
                    }
                    texto += "--------------------------------------------------\n";
                    texto += new string(' ', 10) + $"Área UTM = {Math.Round(polygon.Area / 10000, 4)} Ha.\n";
                    texto += "--------------------------------------------------\n";

                    return texto;
                }
                //}
                //}
            });
        }

        public static async Task AddTextListVerticesToLayoutSim(FeatureLayer featureLayer, LayoutProjectItem layoutItem, string filter = "1=1")
        {
            double x = 20.0;
            double y = 12.8;
            await QueuedTask.Run(async () =>
            {
                var layer = featureLayer.GetFeatureClass();
                if (layer == null)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Por favor, selecciona una capa en el panel de contenido.", "Advertencia");
                    return;
                }
                // Definir un filtro de consulta (actualmente recupera todas las funciones, ajustar según sea necesario)
                ArcGIS.Core.Data.QueryFilter queryFilter = new ArcGIS.Core.Data.QueryFilter { WhereClause = filter };
                // Crear un cursor para iterar sobre las características de la capa
                using (var rowCursor = layer.Search(queryFilter, false))
                {
                    while (rowCursor.MoveNext())
                    {
                        var textList = await GeneratorTextListVerticesSimul(rowCursor);
                        var color = new CIMRGBColor { R = 0, G = 0, B = 0, Alpha = 100 };
                        var _layout = layoutItem.GetLayout();
                        Coordinate2D coord = new Coordinate2D(x, y);
                        var mapPoint = MapPointBuilderEx.CreateMapPoint(coord);
                        CIMTextSymbol textSymbol = SymbolFactory.Instance.ConstructTextSymbol(color, 8, "Tahoma", "Regular");
                        ElementFactory.Instance.CreateTextGraphicElement(_layout, TextType.PointText, mapPoint, textSymbol, textList);
                        y -= 1.5;
                    }
                }
            });
        }


    }
    public class LayoutConfiguration
    {
        public string BasePath { get; set; } // Equivalente a glo_pathMXT
        public string Sistema { get; set; } // Equivalente a v_sistema
        public string ValidaUrbShp { get; set; } // Equivalente a valida_urb_shp
        public string CasoLdMasivo { get; set; } // Equivalente a caso_ldmasivo
        public string SelePlano { get; set; } // Equivalente a sele_plano - formato de plano
        public string SeleccionPlanoSi { get; set; } // Equivalente a seleccion_plano_si
        public int TablaIntegrantesCount { get; set; } // Equivalente a tabla_integrantes.Rows.Count
        public int ContaHojaSup { get; set; } // Equivalente a conta_hoja_sup
    }
}
