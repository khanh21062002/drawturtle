using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.TimeKeeping;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class TimeKeepingDtoToEntity : Profile
    {
        public TimeKeepingDtoToEntity()
        {
            CreateMap<TimeKeepingCreateDto, TimeKeeping>();
            CreateMap<TimeKeepingUpdateDto, TimeKeeping>();
        }

        public class TimeKeepingEntityToDto : Profile
        {
            public TimeKeepingEntityToDto()
            {
                CreateMap<TimeKeeping, TimeKeepingCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));

                CreateMap<TimeKeeping, TimeKeepingGridDto>()
                    .ForMember(dest => dest.PERSON_NAME, mo => mo.MapFrom(src => src.Person.FullName))
                     .ForMember(dest => dest.HiddenParentField, mo => mo.MapFrom(src => src.Department.HiddenParentField))
                    .ForMember(dest => dest.PERSON_CODE, mo => mo.MapFrom(src => src.Person.PersonCode))
                    .ForMember(dest => dest.SHIFT_NAME, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name))
                    .ForMember(dest => dest.FORMAT_DATE_WORKING, mo => mo.MapFrom(src => src.DATE_WORKING.HasValue ? String.Format("{0:dd/MM/yyyy}", src.DATE_WORKING.Value) : ""))
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID))
                      .ForMember(dest => dest.PERSON_CODE, mo => mo.MapFrom(src => src.Person.PersonCode));

                CreateMap<TimeKeeping, TimeKeepingDetailDto>()
                    .ForMember(dest => dest.COMP_NAME, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.DEPARTMENT_NAME, mo => mo.MapFrom(src => src.Department.Name))
                    .ForMember(dest => dest.PERSON_NAME, mo => mo.MapFrom(src => src.Person.FullName))
                    .ForMember(dest => dest.FORMAT_DATE_WORKING, mo => mo.MapFrom(src => src.DATE_WORKING.HasValue ? String.Format("{0:dd/MM/yyyy}", src.DATE_WORKING.Value) : ""))
                    .ForMember(dest => dest.SHIFT_NAME, mo => mo.MapFrom(src => src.WorkingShiftTimes.Name))
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));

                CreateMap<TimeKeeping, TimeKeepingUpdateDto>()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID));
            }
        }

        public static int GetDayOfWeek(DateTime? date)
        {
            if (date.HasValue) {
                
                return ((int)date.Value.DayOfWeek);
            }
            return -1;
        }
    }
}
