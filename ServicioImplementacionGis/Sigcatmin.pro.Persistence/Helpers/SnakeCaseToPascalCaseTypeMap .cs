using Dapper.FluentMap.Mapping;
using Sigcatmin.pro.Domain.Dtos;

namespace Sigcatmin.pro.Persistence.Helpers
{
    public class DerechoMineroDtoMap : EntityMap<DerechoMineroDto>
    {
        public DerechoMineroDtoMap()
        {
            Map(p => p.Codigo)
                .ToColumn("CODIGO");

            Map(p => p.Nombre)
              .ToColumn("NOMBRE");
        }


    }

    
}
