using Dapper.FluentMap.Mapping;
using Sigcatmin.pro.Domain.Dtos;

namespace Sigcatmin.pro.Persistence.Mappings
{
    public class DerechoMineroDtoMap : EntityMap<DerechoMineroDto>
    {
        public DerechoMineroDtoMap()
        {
            Map(p => p.PeVigCat)
              .ToColumn("PE_VIGCAT");
        }


    }


}
