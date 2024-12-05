using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Internal.Core;
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
        private const string V = "eva1";

        public static async Task AddLayoutPath(string layoutFilePath)
        {
            // Verificar si el archivo existe
            if (!File.Exists(layoutFilePath))
            {
                MessageBox.Show($"No se encontró el archivo de layout: {layoutFilePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Ejecutar la tarea en el hilo principal de CIM
            await QueuedTask.Run(async () =>
            {
                try
                {
                    var addItem = ItemFactory.Instance.Create(layoutFilePath, ItemFactory.ItemType.PathItem) as IProjectItem;
                    // Agregar el layout al proyecto actual
                    Project.Current.AddItem(addItem);
                    LayoutProjectItem layoutItem = Project.Current.GetItems<LayoutProjectItem>().FirstOrDefault(l => l.Name == V);
                    // Verificar si la capa fue agregada correctamente
                    //LayoutProjectItem layout = layoutProjectItem as LayoutProjectItem;
                    Layout layout = layoutItem.GetLayout();
                    if (layout != null)
                    {
                        // Abrir la vista del layout
                        await ActivateLayoutAsync(layout);

                        // Mostrar un mensaje de éxito
                        MessageBox.Show($"Layout '{layout.Name}' agregado y abierto exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Manejar el caso en que la capa no es una LayoutProjectItem
                        MessageBox.Show("No se pudo agregar el layout. Asegúrate de que el archivo es un .pagx válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra durante el proceso
                    MessageBox.Show($"Ocurrió un error al agregar el layout: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public static async Task ActivateLayoutAsync(Layout layout )
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

    }
}
