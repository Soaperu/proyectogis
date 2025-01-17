using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Sigcatmin.pro.Application.Dtos.Response;
using Sigcatmin.pro.Domain.Dtos;
using Sigcatmin.pro.Domain.Enums;

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
