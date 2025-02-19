using ArcGIS.Core.Data;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using Sigcatmin.pro.Application.Interfaces;

namespace Sigcatmin.pro.Shared.Implements
{
    public class FeatureLayerService : IFeatureLayerService
    {
        private Geodatabase _geodatabase;
        private Map _activeMap;
        private string _zona;
        private string _regionSelected;
        private readonly IFeatureClassNameResolver _featureClassNameResolver;
        public FeatureLayerService(
            Geodatabase geodatabase,
            Map activeMap,
            string zona,
            string regionSelected,
            IFeatureClassNameResolver featureClassNameResolver)
        {
            _geodatabase = geodatabase;
            _activeMap = activeMap;
            _zona = zona;
            _regionSelected = regionSelected;
            _featureClassNameResolver = featureClassNameResolver;
        }
        public async Task<FeatureLayer> LoadFeatureLayerAsync(string featureClassName, bool isVisible, string queryClause = "1=1")
        {
            if (_geodatabase == null || _activeMap == null)
                throw new InvalidOperationException("Geodatabase y Map deben estar configurados antes de usar este servicio.");

#pragma warning disable CA1416
            return await QueuedTask.Run(() =>
            {

                var featureClassNames = _featureClassNameResolver.ResolveFeatureClassName(featureClassName, _zona, _regionSelected);

                using var featureClass = _geodatabase.OpenDataset<FeatureClass>(featureClassNames.ClassName);
                if (featureClass == null)
                    return null;


                FeatureLayerCreationParams flParams = new FeatureLayerCreationParams(featureClass)
                {
                    Name = featureClassNames.LayerName,
                    IsVisible = isVisible,
                    DefinitionQuery = new DefinitionQuery(whereClause: queryClause, name: "Filtro dema")
                };

                return LayerFactory.Instance.CreateLayer<FeatureLayer>(flParams, _activeMap);
            });
#pragma warning restore CA1416
        }

    }
}
