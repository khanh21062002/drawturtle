using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.DayOff;
using EPS.Service.Dtos.WorkingHours;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class WorkingHoursDtoToEntity : Profile
    {
        public WorkingHoursDtoToEntity()
        {
            CreateMap<WorkingHoursCreateDto, WorkingHours>();
            CreateMap<WorkingHoursUpdateDto, WorkingHours>();
        }
    }

    public class WorkingHoursEntityToDto : Profile
    {
        public WorkingHoursEntityToDto()
        {
            CreateMap<WorkingHours, WorkingHoursGridDto>()
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.DeptId, mo => mo.MapFrom(src => src.Person.DeptId))
                 .ForMember(dest => dest.PersonStatus, mo => mo.MapFrom(src => src.Person.Status))
                  .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Person.Department.Name))
                     .ForMember(dest => dest.HiddenParentField, mo => mo.MapFrom(src => src.Person.Department.HiddenParentField))
                .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                 .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                  .ForMember(dest => dest.WorkShiftName, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name))
                  .ForMember(dest => dest.DayFormat, mo => mo.MapFrom(src => FormatDate(src.Day)))
                    .ForMember(dest => dest.MinCheckInFormat, mo => mo.MapFrom(src => FormatDateCheckInOut(src.CheckIn)))
                     .ForMember(dest => dest.MaxCheckOutFormat, mo => mo.MapFrom(src => FormatDateCheckInOut(src.CheckOut)))
                       .ForMember(dest => dest.DayOfWeekInVietnamese, mo => mo.MapFrom(src => getDayOfWeekInVietnamese(src.Day)))
                       .ForMember(dest => dest.DayOfWeek, mo => mo.MapFrom(src => GetDayOfWeek(src.Day)))

                  .ForMember(dest => dest.TotalFormat, mo => mo.MapFrom(src => ConvertTotal(src.Total)))
                   .ForMember(dest => dest.EarlyStr, mo => mo.MapFrom(src => ConvertEarly(src.Early, src.WorkingShiftTimes.EarlyAccept)))
                   .ForMember(dest => dest.LateStr, mo => mo.MapFrom(src => ConvertLate(src.Late, src.WorkingShiftTimes.LateAccept)))
                ;

            CreateMap<WorkingHours, WorkingHoursDetailsDto>()
                 .ForMember(dest => dest.DayOfWeekInVietnamese, mo => mo.MapFrom(src => getDayOfWeekInVietnamese(src.Day)))
                 .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.DeptId, mo => mo.MapFrom(src => src.Person.DeptId))
                .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Person.Department.Name))
                .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                 .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                 .ForMember(dest => dest.TotalFormat, mo => mo.MapFrom(src => ConvertTotal(src.Total)))
                  .ForMember(dest => dest.WorkShiftName, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name))
                  .ForMember(dest => dest.DayFormat, mo => mo.MapFrom(src => FormatDate(src.Day)))
                    .ForMember(dest => dest.EarlyStr, mo => mo.MapFrom(src => ConvertEarly(src.Early, src.WorkingShiftTimes.EarlyAccept)))
                   .ForMember(dest => dest.LateStr, mo => mo.MapFrom(src => ConvertLate(src.Late, src.WorkingShiftTimes.LateAccept)))
               ;

            CreateMap<WorkingHours, WorkingHoursCreateDto>().IgnoreAllNonExisting().ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));

            CreateMap<WorkingHours, WorkingHoursUpdateDto>();
        }

        public static int GetDayOfWeek(DateTime? date)
        {
            if (date.HasValue)
            {

                return ((int)date.Value.DayOfWeek);
            }
            return -1;
        }
        private static string getDayOfWeekInVietnamese(DateTime? time)
        {
            if (time.HasValue)
            {
                int dayOfWeek = (int)(time.Value.DayOfWeek);
                switch (dayOfWeek)
                {
                    case 0:
                        return "Common.DayOfWeek.Sun";
                        break;
                    case 1:
                        return "Common.DayOfWeek.Mon";
                        break;
                    case 2:
                        return "Common.DayOfWeek.Tue";
                        break;
                    case 3:
                        return "Common.DayOfWeek.Wed";
                        break;
                    case 4:
                        return "Common.DayOfWeek.Thu";
                        break;
                    case 5:
                        return "Common.DayOfWeek.Fri";
                        break;
                    case 6:
                        return "Common.DayOfWeek.Sat";
                        break;
                    default:
                        return "";
                        break;
                }
            }
            return "";
        }
        private static string FormatDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd/MM/yyyy");
            }
            return "";
        }

        private static string FormatDateCheckInOut(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd/MM/yyyy HH:mm:ss");
            }
            return "";
        }
        private static string ConvertEarly(int? early, int? earlyAccept)
        {
            if (early.HasValue)
            {

                if (early < 0)
                {
                    if (earlyAccept.HasValue)
                    {
                        early = early - earlyAccept;
                    }
                    early = early * (-1);
                    return early.ToString();
                }
            }
            return "";
        }
        private static string ConvertLate(int? late, int? lateAccept)
        {
            if (late.HasValue && late > 0)
            {
                if (lateAccept.HasValue)
                {
                    late = late + lateAccept;
                }
                return late.ToString();
            }
            return "";
        }
        private static string ConvertTotal(double? total)
        {
            if (total.HasValue)
            {
                return String.Format("{0:0.##}", total);
            }
            return "";
        }

    }
}
