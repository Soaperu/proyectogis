using AutoMapper;
using Sigcatmin.pro.Application.Dtos.Response;
using Sigcatmin.pro.Domain.Dtos;

namespace Sigcatmin.pro.Application.Mappers
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            CreateMap<CoordinateDto, CoordinatedResponseDto>();
        }

    }
}
