using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.ReportWorkingTime;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class EventDtoToEntity : Profile
    {
        public EventDtoToEntity()
        {
            CreateMap<EventCreateDto, Event>();
            CreateMap<CustomerEventCreateDto, CustomerEvent>();
            CreateMap<CustomerEventUpdateDto, CustomerEvent>();
        }        
    }

    public class SupplierEntityToDto : Profile
    {
        public SupplierEntityToDto()
        {
            CreateMap<Event, EventGridDto>();

            CreateMap<View_ListEvent, EventGridDto>()
                .ForMember(dest => dest.Machine, mo => mo.MapFrom(src => src.Device.DeviceName))
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => ConvertToStatus(src.Status)))
                .ForMember(dest => dest.GenderStr, mo => mo.MapFrom(src => ConverterGender(src.Gender)))
                .ForMember(dest => dest.PersonTypeStr, mo => mo.MapFrom(src => ConverterPersonType(src.PersonType)))
                .ForMember(dest => dest.ErrorCodeName, mo => mo.MapFrom(src => ConvertToErrorCode(Int32.Parse(src.ErrorCode))))
                .ForMember(dest => dest.AccessDateDayFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                .ForMember(dest => dest.AccessDateTimeFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("HH:mm:ss") : ""))
                .ForMember(dest => dest.TemperatureFormat, mo => mo.MapFrom(src => FormatTemperature(src.Temperature)))
                .ForMember(dest => dest.WearMaskName, mo => mo.MapFrom(src => Converter(src.WearMask)))
                //.ForMember(dest => dest.AreaName, mo => mo.MapFrom(src => src.Areas.Name))
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.HiddenParentCompanyField, mo => mo.MapFrom(src => src.Company.HiddenParentField))
                ;

            CreateMap<View_ListEvent_RealTime, EventGridDto>()
                .ForMember(dest => dest.Machine, mo => mo.MapFrom(src => src.Machine.DeviceName))
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => ConvertToStatus(src.Status)))
                .ForMember(dest => dest.GenderStr, mo => mo.MapFrom(src => ConverterGender(src.Gender)))
                .ForMember(dest => dest.PersonTypeStr, mo => mo.MapFrom(src => ConverterPersonType(src.PersonType)))
                .ForMember(dest => dest.PersonType, mo => mo.MapFrom(src => src.PersonType))
                .ForMember(dest => dest.AreaName, mo => mo.MapFrom(src => src.Areas.Name))
                .ForMember(dest => dest.ErrorCodeName, mo => mo.MapFrom(src => ConvertToErrorCode(Int32.Parse(src.ErrorCode))))
                .ForMember(dest => dest.AccessDateDayFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                .ForMember(dest => dest.AccessDateTimeFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("HH:mm:ss") : ""))
                .ForMember(dest => dest.TemperatureFormat, mo => mo.MapFrom(src => FormatTemperature(src.Temperature)))
                .ForMember(dest => dest.WearMaskName, mo => mo.MapFrom(src => Converter(src.WearMask)))
                .ForMember(dest => dest.IsHighTemp, mo => mo.MapFrom(src => CheckHighTemp(src.Temperature, src.Machine.TemperatureThreshold)))
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                ;

            CreateMap<Event, EventDetailDto>()
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => ConvertToStatus(src.Status)))
                .ForMember(dest => dest.ErrorCodeName, mo => mo.MapFrom(src => ConvertToErrorCode(Int32.Parse(src.ErrorCode))))
                .ForMember(dest => dest.WearMaskName, mo => mo.MapFrom(src => Converter(src.WearMask)))
                .ForMember(dest => dest.ScoreMatchFormat, mo => mo.MapFrom(src => src.ScoreMatch.HasValue ? src.ScoreMatch.Value.ToString("F") : ""))
                .ForMember(dest => dest.AccessDateDayFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("dd/MM/yyyy") : ""))
                .ForMember(dest => dest.AccessDateTimeFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("HH:mm:ss") : ""))
                .ForMember(dest => dest.TemperatureFormat, mo => mo.MapFrom(src => FormatTemperature(src.Temperature)))
                .ForMember(dest => dest.IsHighTemp, mo => mo.MapFrom(src => CheckHighTemp(src.Temperature, src.Machine.TemperatureThreshold)))
                ;

            CreateMap<View_ListEvent, EventDetailDto>()
                .ForMember(dest => dest.BirthdayStr, mo => mo.MapFrom(src => formatTimeToDayFormat(src.Birthday)))
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => ConvertToStatus(src.Status)))
                .ForMember(dest => dest.PersonTypeStr, mo => mo.MapFrom(src => ConverterPersonType(src.PersonType)))
                .ForMember(dest => dest.ErrorCodeName, mo => mo.MapFrom(src => ConvertToErrorCode(Int32.Parse(src.ErrorCode))))
                .ForMember(dest => dest.GenderStr, mo => mo.MapFrom(src => ConverterGender(src.Gender)))
                .ForMember(dest => dest.WearMaskName, mo => mo.MapFrom(src => Converter(src.WearMask)))
                .ForMember(dest => dest.ScoreMatchFormat, mo => mo.MapFrom(src => src.ScoreMatch.HasValue ? src.ScoreMatch.Value.ToString("F") : ""))
                .ForMember(dest => dest.AccessDateDayFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                .ForMember(dest => dest.AccessDateTimeFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("HH:mm:ss") : ""))
                .ForMember(dest => dest.TemperatureFormat, mo => mo.MapFrom(src => FormatTemperature(src.Temperature)))
                ;

            CreateMap<v_ReportWorkingTime, ReportWorkingTimeGridDto>()
                .ForMember(dest => dest.DayFormat, mo => mo.MapFrom(src => formatTimeToDayFormat(src.Day)))
                .ForMember(dest => dest.DayOfWeekInVietnamese, mo => mo.MapFrom(src => getDayOfWeekInVietnamese(src.Day)))
                .ForMember(dest => dest.StartAccessTimeFormat, mo => mo.MapFrom(src => formatTimeToTimeOnly(src.StartAccessTime)))
                .ForMember(dest => dest.EndAccessTimeFormat, mo => mo.MapFrom(src => formatTimeToTimeOnly(src.EndAccessTime)))
                .ForMember(dest => dest.EarlyTime, mo => mo.MapFrom(src => CalculateEarlyTime(src.DiffEndTime)))
                .ForMember(dest => dest.LateTime, mo => mo.MapFrom(src => CalculateLateTime(src.DiffStartTime)))
                .ForMember(dest => dest.TotalTimeFormat, mo => mo.MapFrom(src => CheckFormatTotalTime(src.TotalTime)))
                .ForMember(dest => dest.OverTime, mo => mo.MapFrom(src => CalculateOverTime(src.DiffEndTime)));

            CreateMap<CustomerEvent, CustomerEventGridDto>();

        }

        private static bool CheckHighTemp(double? temparature, double? temperatureThreshold)
        {
            if (temparature.HasValue)
            {
                //Ko có thì lấy môc 37.5
                double threshHold = temperatureThreshold.HasValue ? temperatureThreshold.Value : 37.5;
                if (temparature.Value > threshHold)
                {
                    return true;
                }
            }
            return false;
        }

        private static string formatTimeToDayFormat(DateTime? time)
        {
            if (time.HasValue)
            {
                return time.Value.ToString("dd/MM/yyyy");
            }
            return "";
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

        private static string formatTimeToTimeOnly(DateTime? time)
        {
            if (time.HasValue)
            {
                return time.Value.ToString("HH:mm:ss");
            }
            return "";
        }

        private static string CheckFormatTotalTime(double? totalTime)
        {
            if (totalTime != null)
            {
                //convert to minute
                int totalTimeInMinute = (int)(totalTime.Value * 60);
                return formatMinuteToHHMM(totalTimeInMinute);
            }
            return "";
        }

        private static string formatMinuteToHHMM(int timeInMinute)
        {
            string strHour = "00";
            int hour = timeInMinute / 60;
            if (hour < 10)
            {
                strHour = "0" + hour.ToString();
            }
            else
            {
                strHour = hour.ToString();
            }
            int minute = timeInMinute % 60;
            string strMinute = "00";
            if (minute < 10)
            {
                strMinute = "0" + minute.ToString();
            }
            else
            {
                strMinute = minute.ToString();
            }

            return strHour + ":" + strMinute;
        }

        private static string CalculateEarlyTime(int? diffEndTime)
        {
            if (diffEndTime.HasValue)
            {
                if (diffEndTime.Value <= 0)
                {
                    return "-";
                }
                int ealryInMinute = Math.Abs(diffEndTime.Value);
                return formatMinuteToHHMM(ealryInMinute);
            }
            return "";
        }

        private static string CalculateOverTime(int? diffEndTime)
        {
            if (diffEndTime.HasValue)
            {
                if (diffEndTime.Value >= 0)
                {
                    return "-";
                }
                int overTimeInMinute = Math.Abs(diffEndTime.Value);
                return formatMinuteToHHMM(overTimeInMinute);
            }
            return "-";
        }

        private static string CalculateLateTime(int? diffStartTime)
        {
            if (diffStartTime.HasValue)
            {
                if (diffStartTime.Value <= 0)
                {
                    return "-";
                }
                int lateInMinute = Math.Abs(diffStartTime.Value);
                return formatMinuteToHHMM(lateInMinute);
            }
            return "";
        }

        private static string FormatTemperature(double? temp)
        {
            if (temp.HasValue)
            {
                string rs = temp.Value.ToString("F");
                rs = rs + "°C";
                return rs;
            }
            return "";
        }

        private static string Converter(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 1:
                        return "Guess.Detail.Form.Yes";
                        break;
                    case 0:
                        return "Guess.Detail.Form.No";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "";
        }

        private static string ConvertToStatus(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 1:
                        return "Reporting.Event.List.Table.Status0";
                        break;
                    case 0:
                        return "Reporting.Event.List.Table.Status1";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "";
        }

        private static string ConverterPersonType(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 1:
                        return "Reporting.Event.List.Table.Employee";
                        break;
                    case 2:
                        return "Reporting.Event.List.Table.Unregistered";
                        break;
                    case 4:
                    case 7:
                        return "Reporting.Event.List.Table.Suspect";
                        break;
                    case 5:
                        return "Reporting.Event.List.Table.Thief";
                        break;
                    case 6:
                        return "Reporting.Event.List.Table.Vandalize";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "Reporting.Event.List.Table.Employee";
        }

        private static string ConverterGender(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 0:
                        return "Guess.Detail.Form.Male";
                        break;
                    case 1:
                        return "Guess.Detail.Form.Female";
                        break;
                    case -1:
                        return "Guess.Detail.Form.Unknown";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "N/A";
        }

        private static string ConvertToErrorCode(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 0012:
                        return "Reporting.Event.List.Table.ListHD";
                        break;
                    case 1001:
                        return "Reporting.Event.List.Table.ListHD1";
                        break;
                    case 1002:
                        return "Reporting.Event.List.Table.ListHD2";
                        break;
                    case 1003:
                        return "Reporting.Event.List.Table.ListHD3";
                        break;
                    case 1004:
                        return "Reporting.Event.List.Table.ListHD4";
                        break;
                    case 2001:
                        return "Reporting.Event.List.Table.ListHD7";
                        break;
                    case 2002:
                        return "Reporting.Event.List.Table.ListHD2";
                        break;
                    case 2003:
                        return "Reporting.Event.List.Table.ListHD3";
                        break;
                    case 2004:
                        return "Reporting.Event.List.Table.ListHD4";
                        break;
                    case 3001:
                        return "Reporting.Event.List.Table.ListHD5";
                        break;
                    case 3002:
                        return "Reporting.Event.List.Table.ListHD2";
                        break;
                    case 3003:
                        return "Reporting.Event.List.Table.ListHD3";
                        break;
                    case 3004:
                        return "Reporting.Event.List.Table.ListHD4";
                        break;
                    case 0001:
                        return "Reporting.Event.List.Table.ListHD4";
                        break;
                    case 0002:
                        return "Reporting.Event.List.Table.ListHD6";
                        break;
                    case 0003:
                        return "Reporting.Event.List.Table.ListHD8";
                        break;
                    case 0004:
                        return "Reporting.Event.List.Table.ListHD2";
                        break;
                    case 0005:
                        return "Reporting.Event.List.Table.ListHD3";
                        break;
                    case 0006:
                        return "Reporting.Event.List.Table.ListHD3";
                        break;
                    case 0007:
                        return "Reporting.Event.List.Table.ListHD3";
                        break;
                    case 0018:
                        return "Reporting.Event.List.Table.List0018";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "";
        }
    }
}
