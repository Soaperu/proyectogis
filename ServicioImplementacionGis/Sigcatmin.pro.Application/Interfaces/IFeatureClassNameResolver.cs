using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IFeatureClassNameResolver
    {
        string ResolveFeatureClassName(string requestedFeatureClassName, string zona);
    }
}
