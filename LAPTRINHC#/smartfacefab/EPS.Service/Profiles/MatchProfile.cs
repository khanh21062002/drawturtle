using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.FaceMatch;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class MatchDtoToEntity : Profile
    {
        public MatchDtoToEntity()
        {
            CreateMap<MatchCreateDto, Match>()
                    .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id));
        }

        public class MatchEntityToDto : Profile
        {
            public MatchEntityToDto()
            {
                CreateMap<Match, MatchCreateDto>()
                    .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id));
            }
        }
    }
}
