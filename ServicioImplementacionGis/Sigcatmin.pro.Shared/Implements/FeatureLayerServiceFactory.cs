using ArcGIS.Core.Data;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;

namespace Sigcatmin.pro.Shared.Implements
{
    public class FeatureLayerServiceFactory : IFeatureLayerServiceFactory
    {
        public IFeatureLayerService CreateFeatureLayerService(Geodatabase geodatabase, Map activeMap)
        {
            return new FeatureLayerService(geodatabase, activeMap);
        }
    }
}
