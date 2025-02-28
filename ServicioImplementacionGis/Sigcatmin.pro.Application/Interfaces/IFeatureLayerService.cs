using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Mapping;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IFeatureLayerService
    {
        Task<FeatureLayer> LoadFeatureLayerAsync(string featureClassName, bool isVisible, string queryClause = "1=1");
    }
}
