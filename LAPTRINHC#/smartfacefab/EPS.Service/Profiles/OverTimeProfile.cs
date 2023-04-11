using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.OverTime;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class OverTimeDtoToEntity : Profile
    {
        public OverTimeDtoToEntity()
        {
            CreateMap<OverTimeCreateDto, OverTime>();
            CreateMap<OverTimeUpdateDto, OverTime>();
        }

        public class OverTimeEntityToDto : Profile
        {
            public OverTimeEntityToDto()
            {
                CreateMap<OverTime, OverTimeCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));

                CreateMap<OverTime, OverTimeGridDto>()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
                    .ForMember(dest => dest.HiddenParentField, mo => mo.MapFrom(src => src.Department.HiddenParentField))
                    .ForMember(dest => dest.PERSON_NAME, mo => mo.MapFrom(src => src.Person.FullName))
                    .ForMember(dest => dest.PERSON_CODE, mo => mo.MapFrom(src => src.Person.PersonCode))
                    .ForMember(dest => dest.DEPARTMENT_NAME, mo => mo.MapFrom(src => src.Department.Name))
                    .ForMember(dest => dest.FORMAT_DATE_OT, mo => mo.MapFrom(src => src.DATE_OT.HasValue ? String.Format("{0:dd/MM/yyyy}", src.DATE_OT.Value) : ""))
                    .ForMember(dest => dest.FORMAT_OT_IN, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.OT_IN)))
                    .ForMember(dest => dest.FORMAT_OT_OUT, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.OT_OUT)))
                    .ForMember(dest => dest.FORMAT_SUM_OT, mo => mo.MapFrom(src => FormatSumOT(src.SUM_OT)))
                    .ForMember(dest => dest.SHIFT_NAME, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name))
                    .ForMember(dest => dest.COEFFICIENT, mo => mo.MapFrom(src => src.WorkingShiftTimes.OT))
                    .ForMember(dest => dest.TOTAL_TIME_OT, mo => mo.MapFrom(src => FormatTotalTime(src.WorkingShiftTimes.OT, src.SUM_OT)));

                CreateMap<OverTime, OverTimeDetailDto>()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
                    .ForMember(dest => dest.COMP_NAME, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.DEPARTMENT_NAME, mo => mo.MapFrom(src => src.Department.Name))
                    .ForMember(dest => dest.PERSON_NAME, mo => mo.MapFrom(src => src.Person.FullName))
                    .ForMember(dest => dest.FORMAT_DATE_OT, mo => mo.MapFrom(src => src.DATE_OT.HasValue ? String.Format("{0:dd/MM/yyyy}", src.DATE_OT.Value) : ""))
                    .ForMember(dest => dest.FORMAT_OT_IN, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.OT_IN)))
                    .ForMember(dest => dest.FORMAT_OT_OUT, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.OT_OUT)))
                    .ForMember(dest => dest.SHIFT_NAME, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name));

                CreateMap<OverTime, OverTimeUpdateDto>()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));
            }
        }

        private static string FormatTimeSpanToStr(TimeSpan? timeOT)
        {
            if (timeOT.HasValue)
            {
                return string.Format("{0:hh\\:mm}", timeOT);
            }
            return "";
        }

        private static string FormatTotalTime(double? coef, double? timeOT)
        {
            if (coef.HasValue && timeOT.HasValue)
            {
                double result = (double)(coef * timeOT);
                var rs = String.Format("{0:0.##}", result);
                return rs;
            }
            return "";
        }

        private static string FormatSumOT(double? timeOT)
        {
            if (timeOT.HasValue)
            {
                var rs = String.Format("{0:0.00}", timeOT);
                return rs;
            }
            return "";
        }
    }
}
