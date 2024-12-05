using ArcGIS.Core.Data.Raster;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Core.Data;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Internal.CIM;
using QueryFilter = ArcGIS.Core.Data.QueryFilter;

namespace CommonUtilities.ArcgisProUtils
{
    public class RasterUtils
    {

        /// <summary>
        /// Obtiene un Mosaic Dataset desde una geodatabase.
        /// </summary>
        /// <param name="geodatabasePath">Ruta a la geodatabase.</param>
        /// <param name="mosaicDatasetName">Nombre del Mosaic Dataset.</param>
        /// <returns>El objeto MosaicDataset si se encuentra; de lo contrario, null.</returns>
        public static async Task<MosaicDataset> GetMosaicDatasetAsync(string geodatabasePath, string mosaicDatasetName)
        {
            return await QueuedTask.Run(() =>
            {
                // Crear la conexión a la geodatabase
                using (Geodatabase geodatabase = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri(geodatabasePath))))
                {
                    // Abrir el Mosaic Dataset
                    MosaicDataset mosaicDataset = geodatabase.OpenDataset<MosaicDataset>(mosaicDatasetName);
                    return mosaicDataset;
                }
            });
        }

        /// <summary>
        /// Agrega un Mosaic Dataset como capa al mapa activo.
        /// </summary>
        /// <param name="mosaicDataset">El Mosaic Dataset a agregar.</param>
        /// <returns></returns>
        public static async Task AddRasterCartaIGNLayerAsync(string mosaicDatasetName, Geodatabase geodatabase, Map map, string codeHoja)
        {
#pragma warning disable CA1416 // Validar la compatibilidad de la plataforma
            await QueuedTask.Run(() =>
            {
                // Obtener el mapa activo
                //Map map = MapView.Active.Map;

                if (map == null)
                {
                    System.Windows.MessageBox.Show("No hay un mapa activo.");
                    return;
                }
                string workspaceConnectionString = geodatabase.GetConnectionString();
                CIMStandardDataConnection dataConnection = new CIMStandardDataConnection();
                //QueryDescription qds = geodatabase..GetQueryDescription(" select GEF_OBJECTID, GEF_OBJKEY, OBJNR, ERNAM, ERDAT, KTEXT, IDAT2, AENAM, ARTPR, GEF_SHAPE from schema.tablename", "MySelect");
                //Table pTab = db.OpenTable(qds);
                // Setup the data connection object.
                dataConnection.WorkspaceFactory = WorkspaceFactory.SDE;
                dataConnection.WorkspaceConnectionString = workspaceConnectionString;

                dataConnection.DatasetType = esriDatasetType.esriDTMosaicDataset;
                dataConnection.Dataset = mosaicDatasetName;

                // Crear la capa a partir del Mosaic Dataset
                // Crear los parámetros de creación de la capa con el whereClause
                DefinitionQuery query = new DefinitionQuery { WhereClause = $"NAME='{codeHoja}'.ecw" };
                MosaicLayerCreationParams layerCreationParams = new MosaicLayerCreationParams(dataConnection)
                {
                    Name = $"Hoja_{codeHoja}", // Nombre descriptivo para la capa
                    // = query // Aplicar el filtro WHERE
                };
                //var rasterLayer = LayerFactory.Instance.CreateLayer<MosaicLayer>(new LayerCreationParams(dataConnection), map);
                var rasterLayer = LayerFactory.Instance.CreateLayer<MosaicLayer>(layerCreationParams, map);
                //rasterLayer.SetDefinitionQuery($"NAME='{codeHoja}'.ecw");    
                // Opcionalmente, puedes personalizar la capa después de agregarla
            });
#pragma warning restore CA1416 // Validar la compatibilidad de la plataforma
        }

        //public static async Task AddRasterCartaIGNLayerAsync1(string mosaicDatasetName, Geodatabase geodatabase, Map map, string codeHoja)
        //{

        //    await QueuedTask.Run(() =>
        //    {
        //        //// Ruta de conexión SDE
        //        //using (DatabaseConnectionProperties connectionProperties = new DatabaseConnectionProperties(EnterpriseDatabaseType.SQLServer)
        //        //{
        //        //    AuthenticationMode = AuthenticationMode.DBMS,
        //        //    Instance = "your_server",
        //        //    Database = "your_database",
        //        //    User = "your_user",
        //        //    Password = "your_password"
        //        //})
        //        //using (Geodatabase gdb = new Geodatabase(connectionProperties))
        //        using (MosaicDataset rasterCatalog = geodatabase.OpenDataset<MosaicDataset>(mosaicDatasetName))
        //        {
        //            // Filtro para seleccionar el ráster
        //            QueryFilter queryFilter = new QueryFilter { WhereClause = $"NAME='{codeHoja}'.ecw" }; // Ajusta según tus necesidades

        //            using (RowCursor cursor = rasterCatalog.GetCatalog())//.Select(queryFilter, SelectionType.ObjectID, SelectionOption.Normal))
        //            {

        //            }
        //        }
        //    });
        //}
    }
}
