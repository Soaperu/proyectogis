using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.FluentMap.Mapping;
using Sigcatmin.pro.Domain.Dtos;

namespace Sigcatmin.pro.Persistence.Mappings
{
    public class CoordenadaDtoMap : EntityMap<CoordinateDto>
    {
        public CoordenadaDtoMap()
        {
            Map(p => p.Vertice)
              .ToColumn("CD_NUMVER");

            Map(p => p.NorteWGS84)
              .ToColumn("CD_CORNOR_E");

            Map(p => p.EsteWGS84)
              .ToColumn("CD_COREST_E");

            Map(p => p.NortePSAD56)
             .ToColumn("CD_CORNOR");

            Map(p => p.EstePSAD56)
              .ToColumn("CD_COREST");
        }

    }
}
