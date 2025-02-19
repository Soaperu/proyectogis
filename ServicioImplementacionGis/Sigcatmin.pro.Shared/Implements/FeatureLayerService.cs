using ArcGIS.Core.Data;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Sigcatmin.pro.Application.Interfaces;

namespace Sigcatmin.pro.Shared.Implements
{
    public class FeatureLayerService : IFeatureLayerService
    {
        private Geodatabase _geodatabase;
        private Map _activeMap;
        public FeatureLayerService(Geodatabase geodatabase, Map activeMap)
        {
            _geodatabase = geodatabase;
            _activeMap = activeMap;
        }
        public async Task<FeatureLayer> LoadFeatureLayerAsync(string featureClassName,string zona,  bool isVisible)
        {
            if (_geodatabase == null || _activeMap == null)
                throw new InvalidOperationException("Geodatabase y Map deben estar configurados antes de usar este servicio.");

            return await QueuedTask.Run(async () =>
            {
              using var featureClass = _geodatabase.OpenDataset<FeatureClass>(featureClassName);
                if (featureClass == null)
                    return null; // Manejo de error

                FeatureLayerCreationParams flParams = new FeatureLayerCreationParams(featureClass)
                {
                    Name = featureClassName,
                    IsVisible = isVisible
                };

                return LayerFactory.Instance.CreateLayer<FeatureLayer>(flParams, _activeMap);
            });
        }
    }
}
