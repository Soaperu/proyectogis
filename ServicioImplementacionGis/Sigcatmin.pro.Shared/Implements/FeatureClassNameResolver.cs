using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Application.Contracts.Dtos;
using Sigcatmin.pro.Application.Contracts.Models;
using Sigcatmin.pro.Application.Interfaces;

namespace Sigcatmin.pro.Shared.Implements
{
    public class FeatureClassNameResolver : IFeatureClassNameResolver
    {
        private readonly List<FeatureClassInfo> _featureClassMappings;

        public FeatureClassNameResolver(List<FeatureClassInfo> featureClassMappings)
        {
            _featureClassMappings = featureClassMappings;
        }

        public FeatureClassNameDto ResolveFeatureClassName(string requestedFeatureClassName, string zona, string regionSelected)
        {
            var featureClassInfo = _featureClassMappings.FirstOrDefault(f =>
                string.Equals(f.FeatureClassName, requestedFeatureClassName, StringComparison.OrdinalIgnoreCase) ||
                (f.FeatureClassNameGenerator != null && string.Equals(f.FeatureClassNameGenerator(zona), requestedFeatureClassName, StringComparison.OrdinalIgnoreCase))
            );

            if (featureClassInfo == null)
            {
                // Si no se encuentra, usar un nombre genérico
                featureClassInfo = new FeatureClassInfo
                {
                    FeatureClassName = requestedFeatureClassName,
                    LayerName = requestedFeatureClassName,
                    VariableName = null
                };
            }

            string layerName = featureClassInfo.LayerName ?? featureClassInfo.LayerNameGenerator?.Invoke(regionSelected);

            string featureName = featureClassInfo?.FeatureClassName ?? featureClassInfo?.FeatureClassNameGenerator?.Invoke(zona) ?? requestedFeatureClassName;

            return new FeatureClassNameDto(featureName, layerName);
        }

    }
}
