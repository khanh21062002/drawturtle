using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.CheckData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class CheckDataDtoToEntity : Profile
    {
        public CheckDataDtoToEntity()
        {
        }
    }

    public class CheckDataEntityToDto : Profile
    {
        public CheckDataEntityToDto()
        {
            CreateMap<Grades, ExitsIntDto>()
         .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.Id))
         .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<Department, ExitsIntDto>()
           .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.Id))
           .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.ComId));
            CreateMap<AccessTimeSeg, ExitsIntDto>()
           .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.Id))
           .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<DayOff, ExitsIntDto>()
          .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
          .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<Event, ExitsIntDto>()
          .ForMember(dest => dest.EventId, mo => mo.MapFrom(src => src.EventId))
          .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<GroupAccess, ExitsIntDto>()
       .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.Id))
       .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<Group, ExitsIntDto>()
  .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.GroupId))
  .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<GuessRegister, ExitsIntDto>()
         .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
         .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.ComId));
            CreateMap<Holidays, ExitsIntDto>()
        .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
        .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<Machine, ExitsIntDto>()
       .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.Id))
       .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<Notification, ExitsIntDto>()
       .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
       .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<OverTime, ExitsIntDto>()
       .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
       .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.COMP_ID));
            CreateMap<OverTimeHours, ExitsIntDto>()
      .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
      .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<Person, ExitsIntDto>()
      .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId))
      .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));

            CreateMap<RegisterWorkingShift, ExitsIntDto>()
     .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
     .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<ShiftTime, ExitsIntDto>()
   .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.Id))
   .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<TimeKeeping, ExitsIntDto>()
  .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
  .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.COMP_ID));
            CreateMap<User, ExitsIntDto>()
 .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.Id))
 .ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<WorkCalendar, ExitsIntDto>()
.ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
.ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<WorkingHours, ExitsIntDto>()
.ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
.ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));
            CreateMap<WorkingShiftTimes, ExitsIntDto>()
.ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
.ForMember(dest => dest.ComId, mo => mo.MapFrom(src => src.CompId));

        }
    }
}
