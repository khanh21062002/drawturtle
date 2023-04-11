using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.WorkingShiftTimes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class WorkingShiftTimeDtoToEntity : Profile
    {
        public WorkingShiftTimeDtoToEntity()
        {
            CreateMap<WorkingShiftTimesCreateDto, WorkingShiftTimes>();
            CreateMap<WorkingShiftTimesUpdateDto, WorkingShiftTimes>();
        }

    }

    public class WorkingShiftTimeEntityToDto : Profile
    {
        public WorkingShiftTimeEntityToDto()
        {
            CreateMap<WorkingShiftTimes, WorkingShiftTimesCreateDto>()
                .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID)); // Get newly created identity Id back to Dto;
            CreateMap<WorkingShiftTimes, WorkingShiftTimesUpdateDto>();
            CreateMap<WorkingShiftTimes, WorkingShiftTimesGridDto>()
                .ForMember(dest => dest.FormatStartTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.StartTime)))
                .ForMember(dest => dest.FormatEndTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.EndTime)))
                  .ForMember(dest => dest.FormatEarlyAccept, mo => mo.MapFrom(src => FormatMinute(src.EarlyAccept)))
                    .ForMember(dest => dest.FormatLateAccept, mo => mo.MapFrom(src => FormatMinute(src.LateAccept)))
                     .ForMember(dest => dest.FormatStartCheckInOverTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.StartCheckInOverTime)))
                   .ForMember(dest => dest.FormatEndCheckOutOverTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.EndCheckOutOverTime)))
                ;
            CreateMap<WorkingShiftTimes, WorkingShiftTimesDetailDto>()
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                 .ForMember(dest => dest.FormatStartTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.StartTime)))
                .ForMember(dest => dest.FormatEndTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.EndTime)))
                .ForMember(dest => dest.FormatStartBreak, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.StartBreak)))
                .ForMember(dest => dest.FormatEndBreak, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.EndBreak)))
                .ForMember(dest => dest.FormatStartCheckIn, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.StartCheckIn)))
                 .ForMember(dest => dest.FormatEndCheckIn, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.EndCheckIn)))
                 .ForMember(dest => dest.FormatStartCheckOut, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.StartCheckOut)))
                  .ForMember(dest => dest.FormatEndCheckOut, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.EndCheckOut)))
                  .ForMember(dest => dest.FormatStartCheckInOverTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.StartCheckInOverTime)))
                   .ForMember(dest => dest.FormatEndCheckOutOverTime, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.EndCheckOutOverTime)))
                ;
        }
        private static string FormatTimeSpanToStr(TimeSpan? timeVl)
        {
            if (timeVl.HasValue)
            {
                return string.Format("{0:hh\\:mm}", timeVl);
            }
            return "";
        }

        private static string FormatMinute(int? timeVl)
        {
            if (timeVl.HasValue)
            {
                return timeVl + " ";
            }
            return "";
        }
    }


}
