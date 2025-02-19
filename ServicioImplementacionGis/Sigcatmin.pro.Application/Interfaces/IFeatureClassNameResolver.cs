using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Application.Contracts.Dtos;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface IFeatureClassNameResolver
    {
        FeatureClassNameDto ResolveFeatureClassName(string requestedFeatureClassName, string zona, string regionSelected);
    }
}
