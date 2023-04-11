using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.OverTimeHours;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class OverTimeHoursDtoToEntity : Profile
    {
        public OverTimeHoursDtoToEntity()
        {
            CreateMap<OverTimeHoursCreateDto, OverTimeHours>();
            CreateMap<OverTimeHoursUpdateDto, OverTimeHours>();
        }

        public class OverTimeHoursEntityToDto : Profile
        {
            public OverTimeHoursEntityToDto()
            {
                CreateMap<OverTimeHours, OverTimeHoursCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));

                CreateMap<OverTimeHours, OverTimeHoursGridDto>()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
                    .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                    .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                    .ForMember(dest => dest.HiddenParentField, mo => mo.MapFrom(src => src.Department.HiddenParentField))
                    .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Department.Name))
                    .ForMember(dest => dest.DayFormat, mo => mo.MapFrom(src => src.Day.HasValue ? String.Format("{0:dd/MM/yyyy}", src.Day.Value) : ""))
                    .ForMember(dest => dest.TimeFromFormat, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.TimeFrom)))
                    .ForMember(dest => dest.TimeToFormat, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.TimeTo)))
                    .ForMember(dest => dest.TotalFormat, mo => mo.MapFrom(src => FormatSumOT(src.Total)))
                    .ForMember(dest => dest.TotalRealFormat, mo => mo.MapFrom(src => FormatSumOT(src.TotalReal)))
                    .ForMember(dest => dest.TotalRegisterFormat, mo => mo.MapFrom(src => FormatSumOT(src.TotalRegister)))
                    .ForMember(dest => dest.WorkingShiftName, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name))
                    .ForMember(dest => dest.DayOfWeek, mo => mo.MapFrom(src => GetDayOfWeek(src.Day)));

                CreateMap<OverTimeHours, OverTimeHoursDetailDto>()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
                    .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                    .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                    .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Department.Name))
                    .ForMember(dest => dest.DayFormat, mo => mo.MapFrom(src => src.Day.HasValue ? String.Format("{0:dd/MM/yyyy}", src.Day.Value) : ""))
                    .ForMember(dest => dest.TimeFromFormat, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.TimeFrom)))
                    .ForMember(dest => dest.TimeToFormat, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.TimeTo)))
                    .ForMember(dest => dest.TotalFormat, mo => mo.MapFrom(src => FormatSumOT(src.Total)))
                    .ForMember(dest => dest.TotalRealFormat, mo => mo.MapFrom(src => FormatSumOT(src.TotalReal)))
                    .ForMember(dest => dest.TotalRegisterFormat, mo => mo.MapFrom(src => FormatSumOT(src.TotalRegister)))
                    .ForMember(dest => dest.WorkingShiftName, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name));

                CreateMap<OverTimeHours, OverTimeHoursUpdateDto>()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));
            }
        }

        private static string FormatTimeSpanToStr(DateTime? timeOT)
        {
            if (timeOT.HasValue)
            {
                return timeOT.Value.ToString("dd/MM/yyyy HH:mm:ss");
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

        public static int GetDayOfWeek(DateTime? date)
        {
            if (date.HasValue)
            {

                return ((int)date.Value.DayOfWeek);
            }
            return -1;
        }
    }
}
