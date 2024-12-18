using ArcGIS.Core.Data;
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

namespace CommonUtilities.ArcgisProUtils
{
    public class LayoutUtils
    {
        //private const string V = "Plantilla_evd_84";
        private readonly LayoutConfiguration _config;
        public LayoutUtils(LayoutConfiguration config)
        {
            _config = config;
        }
        public static async Task<LayoutProjectItem> AddLayoutPath(string layoutFilePath, string nameLayer, string mapName, string layoutName)
        {
            // Verificar si el archivo existe
            if (!File.Exists(layoutFilePath))
            {
                MessageBox.Show($"No se encontró el archivo de layout: {layoutFilePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Ejecutar la tarea en el hilo principal de CIM
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            return await QueuedTask.Run(async () =>
            {
                try
                {
                    // Obtener la lista actual de mapas antes de agregar el layout
                    var mapsBefore = Project.Current.GetItems<MapProjectItem>().Select(m => m.Name).ToList();

                    var addItem = ItemFactory.Instance.Create(layoutFilePath, ItemFactory.ItemType.PathItem) as IProjectItem;
                    // Agregar el layout al proyecto actual
                    Project.Current.AddItem(addItem);
                    LayoutProjectItem layoutItem = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(l => l.Name == layoutName);
                    // Verificar si la capa fue agregada correctamente
                    //LayoutProjectItem layout = layoutProjectItem as LayoutProjectItem;
                    Layout layout = layoutItem.GetLayout();
                    if (layout != null)
                    {
                        // Abrir la vista del layout
                        //await ActivateLayoutAsync(layout);

                        // Mostrar un mensaje de éxito
                        //MessageBox.Show($"Layout '{layout.Name}' agregado y abierto exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        layout.SetName(mapName);
                        MapFrame mfrm = layout.FindElement( mapName +" Map Frame") as MapFrame;
                        Map mapCatastro = await MapUtils.FindMapByNameAsync(mapName);
                        mfrm.SetMap(mapCatastro);
                        var zoomNameLayer = mapCatastro.GetLayersAsFlattenedList().OfType<Layer>().FirstOrDefault(l => l.Name == nameLayer);
                        var fLayer = (FeatureLayer)zoomNameLayer;
                        FeatureClass featureClass = fLayer.GetFeatureClass();
                        // Obtener la definición del FeatureClass para acceder a su extensión
                        FeatureClassDefinition fcDef = featureClass.GetDefinition() as FeatureClassDefinition;
                        if (fcDef != null)
                        {
                            Envelope layerExtent = fcDef.GetExtent();
                            mfrm.SetCamera(layerExtent);
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
        /// Genera y agrega un layout al proyecto de ArcGIS Pro basado en el tipo de plano.
        /// </summary>
        /// <param name="tipoPlano">Tipo de plano a generar.</param>
        /// <returns>True si se agregó exitosamente, de lo contrario False.</returns>
        //public async Task<bool> GeneraPlanoReporteAsync(string tipoPlano)
        //{
        //    string rutaPlantilla = DeterminarRutaPlantilla(tipoPlano);

        //    if (string.IsNullOrEmpty(rutaPlantilla))
        //    {
        //        MessageBox.Show($"No se encontró una plantilla para el tipo de plano: {tipoPlano}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }

        //    if (!File.Exists(rutaPlantilla))
        //    {
        //        MessageBox.Show($"No se encontró el archivo de plantilla: {rutaPlantilla}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }

        //    try
        //    {
        //        // Ejecutar en el hilo adecuado
        //        await QueuedTask.Run(async () =>
        //        {
        //            try
        //            {
        //                // Agregar la plantilla al proyecto
        //                var layoutProjectItem = await Project.Current.AddItemAsync(rutaPlantilla, ProjectItemType.Layout);

        //                // Verificar si la plantilla se agregó correctamente
        //                if (layoutProjectItem is LayoutProjectItem layout)
        //                {
        //                    // Abrir la vista del layout
        //                    await layout.OpenAsync();

        //                    // Notificar al usuario
        //                    MessageBox.Show($"Layout '{layout.Name}' agregado y abierto exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        //                }
        //                else
        //                {
        //                    MessageBox.Show("No se pudo agregar el layout. Asegúrate de que el archivo es un .pagx válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Ocurrió un error al agregar el layout: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        });

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }
        //}

        /// <summary>
        /// Determina la ruta de la plantilla basada en el tipo de plano y otras configuraciones.
        /// </summary>
        /// <param name="tipoPlano">Tipo de plano.</param>
        /// <returns>Ruta completa de la plantilla.</returns>
        private string DeterminarRutaPlantilla(string tipoPlano)
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
       

    }
    public class LayoutConfiguration
    {
        public string BasePath { get; set; } // Equivalente a glo_pathMXT
        public string Sistema { get; set; } // Equivalente a v_sistema
        public string ValidaUrbShp { get; set; } // Equivalente a valida_urb_shp
        public string CasoLdMasivo { get; set; } // Equivalente a caso_ldmasivo
        public string SelePlano { get; set; } // Equivalente a sele_plano
        public string SeleccionPlanoSi { get; set; } // Equivalente a seleccion_plano_si
        public int TablaIntegrantesCount { get; set; } // Equivalente a tabla_integrantes.Rows.Count
        public int ContaHojaSup { get; set; } // Equivalente a conta_hoja_sup
    }
}
