using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Sigcatmin.pro.Application.Dtos.Response;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.Mappings;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Domain.Enums;
using Sigcatmin.pro.Domain.Interfaces.Repositories;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class GetCoordenadasDMUseCase
    {
        private readonly IEvaluacionGISRepository _evaluacionGISRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public GetCoordenadasDMUseCase(IEvaluacionGISRepository evaluacionGISRepository,
            ILoggerService loggerService,
            IMapper mapper)
        {
            _evaluacionGISRepository = evaluacionGISRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CoordinatedResponseDto>> Execute(string code, int typeSystem)
        {
            try
            {
                var coordinates = await _evaluacionGISRepository.GetcoordinatesByCode(code);
                return _mapper.Map<IEnumerable<CoordinatedResponseDto>>(coordinates, opts => opts.Items["coordinateSystem"] = typeSystem);
                //if (typeSystem == (int)CoordinateSystem.WGS84)
                //{
                //    return coordinates.Select(x => new CoordinatedResponseDto(
                //        x.Vertice, 
                //        x.NorteWGS84,
                //        x.EsteWGS84
                //     ));
                //}

                //return coordinates.Select(x => new CoordinatedResponseDto(
                //      x.Vertice,
                //      x.NortePSAD56,
                //      x.EstePSAD56
                //   ));

                return typeSystem switch
                {
                    (int)CoordinateSystem.WGS84 => _mapper.Map<IEnumerable<CoordinatedResponseDto>>(coordinates),
                    (int)CoordinateSystem.PSAD56 => _mapper.Map<IEnumerable<CoordinatedResponseDto>>(coordinates),
                    _ => throw new ArgumentException("Invalid coordinate system type.")
                };

            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                throw;
            }
        }
    }
}
