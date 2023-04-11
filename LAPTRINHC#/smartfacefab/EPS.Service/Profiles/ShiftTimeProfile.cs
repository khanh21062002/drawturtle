using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.ShiftTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class ShiftTimeDtoToEntity : Profile
    {
        public ShiftTimeDtoToEntity()
        {
            CreateMap<ShiftTimeCreateDto, ShiftTime>();
            CreateMap<ShiftTimeUpdateDto, ShiftTime>();
        }

    }

    public class ShiftTimeEntityToDto : Profile
    {
        public ShiftTimeEntityToDto()
        {
            CreateMap<ShiftTime, ShiftTimeCreateDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto;
            CreateMap<ShiftTime, ShiftTimeUpdateDto>();
            CreateMap<ShiftTime, ShiftTimeGridDto>()
                 .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name));
            CreateMap<ShiftTime, ShiftTimeDetailDto>()
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name));
        }

    }

}
