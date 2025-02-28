using ArcGIS.Core.Data;
using ArcGIS.Desktop.Mapping;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IFeatureLayerServiceFactory
    {
        IFeatureLayerService CreateFeatureLayerService(Geodatabase geodatabase, Map activeMap,string zona, string regionSelected);
    }
}
