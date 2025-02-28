using ArcGIS.Core.Data;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;

namespace Sigcatmin.pro.Shared.Implements
{
    public class FeatureLayerServiceFactory : IFeatureLayerServiceFactory
    {
        private readonly IFeatureClassNameResolver _featureClassNameResolver;

        public FeatureLayerServiceFactory(IFeatureClassNameResolver featureClassNameResolver)
        {
            _featureClassNameResolver = featureClassNameResolver;
        }
        public IFeatureLayerService CreateFeatureLayerService(Geodatabase geodatabase, Map activeMap, string zona, string regionSelected)
        {
            return new FeatureLayerService(geodatabase, activeMap, zona, regionSelected, _featureClassNameResolver);
        }

    }
}
