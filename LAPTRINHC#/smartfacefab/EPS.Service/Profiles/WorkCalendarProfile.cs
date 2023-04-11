using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.WorkCalendar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class WorkCalendarDtoToEntity : Profile
    {
        public WorkCalendarDtoToEntity()
        {
            CreateMap<WorkCalendarCreateDto, WorkCalendar>();
            CreateMap<WorkCalendarUpdateDto, WorkCalendar>();
            CreateMap<WCalendarDetailCreateDto, WorkCalendarDetail>();
            CreateMap<WCalendarDetailUpdateDto, WorkCalendarDetail>();
            CreateMap<WorkCalendar, WorkCalendarDetail>();
        }
    }

    public class WorkCalendarEntityToDto : Profile
    {
        public WorkCalendarEntityToDto()
        {
            CreateMap<WorkCalendar, WorkCalendarGridDto>()
                 .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                   .ForMember(dest => dest.DateFromFormat, mo => mo.MapFrom(src => FormatDate(src.DateFrom)))
                   .ForMember(dest => dest.DateToFormat, mo => mo.MapFrom(src => FormatDate(src.DateTo)))
            ;
            CreateMap<WorkCalendar, WorkCalendarDetailDto>()
                  .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                   .ForMember(dest => dest.DateFromFormat, mo => mo.MapFrom(src => FormatDate(src.DateFrom)))
                   .ForMember(dest => dest.DateToFormat, mo => mo.MapFrom(src => FormatDate(src.DateTo)))

            ;
            CreateMap<WorkCalendar, WorkCalendarCreateDto>();

            ;
            CreateMap<WorkCalendar, WorkCalendarUpdateDto>();


            ;

            //detail
            CreateMap<WorkCalendarDetail, WCalendarDetailGridDto>();

            ;
            CreateMap<WorkCalendarDetail, WCalendarDetailDetailDto>();

            ;
            CreateMap<WorkCalendarDetail, WCalendarDetailCreateDto>();


            ;
            CreateMap<WorkCalendarDetail, WCalendarDetailUpdateDto>();

            ;

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
