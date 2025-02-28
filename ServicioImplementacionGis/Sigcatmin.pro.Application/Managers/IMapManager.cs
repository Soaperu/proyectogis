using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Mapping;


namespace Sigcatmin.pro.Application.Managers
{
    public interface IMapManager
    {
        Task<Map> EnsureMapViewIsActiveAsync(string mapName);
    }
}
