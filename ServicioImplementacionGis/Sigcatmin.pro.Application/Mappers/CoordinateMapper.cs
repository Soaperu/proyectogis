using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Application.Dtos.Response;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Domain.Enums;

namespace Sigcatmin.pro.Application.Mappers
{
    public static class CoordinateMapper
    {
        public static IEnumerable<CoordinatedResponseDto> MapToResponse(IEnumerable<CoordinateDto> source, int typeSystem)
        {

            return source.Select(x => new CoordinatedResponseDto
            {
                Norte = typeSystem switch
                {
                    (int)CoordinateSystem.WGS84 => x.NorteWGS84,
                    (int)CoordinateSystem.PSAD56 => x.NortePSAD56,
                    _ => throw new NotImplementedException()
                },
                Este = typeSystem switch
                {
                    (int)CoordinateSystem.WGS84 => x.EsteWGS84,
                    (int)CoordinateSystem.PSAD56 => x.EstePSAD56,
                    _ => throw new NotImplementedException()
                },

                Vertice = x.Vertice,
            });
        }

    }
}
