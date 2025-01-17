using AutoMapper;
using Sigcatmin.pro.Application.Dtos.Response;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Domain.Enums;

namespace Sigcatmin.pro.Application.Mappings
{
    public class CoordinateMappingProfile : Profile
    {
        public CoordinateMappingProfile()
        {
      
            CreateMap<CoordinateDto, CoordinatedResponseDto>()
           //.ForMember(dest => dest.Norte, opt => opt.MapFrom<CoordinateResolver>())
           // .ForMember(dest => dest.Este, opt => opt.MapFrom<CoordinateResolver>())
            .ForMember(dest => dest.Vertice, opt => opt.MapFrom(src => src.Vertice));
        }
    }
    //public class CoordinateResolver : IValueResolver<CoordinateDto, CoordinatedResponseDto, decimal>
    //{
    //    private readonly int _coordinateSystem;

    //    public CoordinateResolver(int coordinateSystem)
    //    {
    //        _coordinateSystem = coordinateSystem;
    //    }

    //    public decimal Resolve(CoordinateDto source, CoordinatedResponseDto destination, decimal destMember, ResolutionContext context)
    //    {
    //        return _coordinateSystem switch
    //        {
    //            (int)CoordinateSystem.WGS84 => source.NorteWGS84, // o source.EsteWGS84 según el caso
    //            (int)CoordinateSystem.PSAD56 => source.NortePSAD56, // o source.EstePSAD56 según el caso
    //            _ => throw new ArgumentException("Invalid coordinate system")
    //        };
    //    }
    //}
}
