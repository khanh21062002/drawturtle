using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Holidays;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class HolidaysDtoToEntity : Profile
    {
        public HolidaysDtoToEntity()
        {
            CreateMap<HolidaysUpdateDto, Holidays>();
            CreateMap<HolidaysCreateDto, Holidays>();
        }

    }

    public class HolidaysEntityToDto : Profile
    {
        public HolidaysEntityToDto()
        {
            CreateMap<Holidays, HolidaysGridDto>()
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                 .ForMember(dest => dest.FormatDate, mo => mo.MapFrom(src => FormatDate(src.Date)))
            ;
            CreateMap<Holidays, HolidaysDetailDto>()
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                 .ForMember(dest => dest.FormatDate, mo => mo.MapFrom(src => FormatDate(src.Date)));
            CreateMap<Holidays, HolidaysCreateDto>().IgnoreAllNonExisting().ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));
            CreateMap<Holidays, HolidaysUpdateDto>();
        }
        private static string FormatDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd/MM/yyyy");
            }
            return "";

        }
    }
}
