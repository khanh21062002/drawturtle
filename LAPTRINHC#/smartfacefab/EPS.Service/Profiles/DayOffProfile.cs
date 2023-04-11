using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.DayOff;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class DayOffDtoToEntity : Profile
    {
        public DayOffDtoToEntity()
        {
            CreateMap<DayOffCreateDto, DayOff>();
            CreateMap<DayOffUpdateDto, DayOff>();
        }
    }

    public class DayOffEntityToDto : Profile
    {
        public DayOffEntityToDto()
        {
            CreateMap<DayOff, DayOffGridDto>()
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.FormatDateFrom, mo => mo.MapFrom(src => FormatDate(src.DateFrom)))
                .ForMember(dest => dest.FormatDateTo, mo => mo.MapFrom(src => FormatDate(src.DateTo)))
                .ForMember(dest => dest.HiddenParentField, mo => mo.MapFrom(src => src.Department.HiddenParentField))

                .ForMember(dest => dest.IsOutDate, mo => mo.MapFrom(src => OutDate(src)));

            CreateMap<DayOff, DayOffDetailDto>()
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.FormatDateFrom, mo => mo.MapFrom(src => FormatDate(src.DateFrom)))
                .ForMember(dest => dest.FormatDateTo, mo => mo.MapFrom(src => FormatDate(src.DateTo)));

            CreateMap<DayOff, DayOffCreateDto>().IgnoreAllNonExisting().ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));

            CreateMap<DayOff, DayOffUpdateDto>();
        }

        private static string FormatDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd/MM/yyyy");
            }
            return "";
        }

        private static bool OutDate(DayOff src)
        {
            DateTime now = DateTime.Now;
            if (src.DateTo.HasValue)
            {
                int result = DateTime.Compare(src.DateTo.Value, now);
                if (result > 0)
                    return true;
                else return false;
            }
            return false;
        }

    }
}
