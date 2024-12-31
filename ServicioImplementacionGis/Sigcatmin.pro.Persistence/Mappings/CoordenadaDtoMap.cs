using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.FluentMap.Mapping;
using Sigcatmin.pro.Domain.Dtos;

namespace Sigcatmin.pro.Persistence.Mappings
{
    public class CoordenadaDtoMap : EntityMap<CoordenadaDto>
    {
        public CoordenadaDtoMap()
        {
            Map(p => p.Vertice)
              .ToColumn("CD_NUMVER");

            Map(p => p.Norte)
              .ToColumn("CD_CORNOR");
            Map(p => p.Este)
              .ToColumn("CD_COREST");
        }

    }
}
