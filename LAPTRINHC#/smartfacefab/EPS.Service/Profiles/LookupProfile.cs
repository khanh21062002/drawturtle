using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using System.Linq;
using EPS.Service.Dtos.Common;

namespace EPS.Service.Profiles
{
    public class LookupProfileDtoToEntity : Profile
    {
        public LookupProfileDtoToEntity()
        {
        }
    }

    public class LookupProfileEntityToDto : Profile
    {
        public LookupProfileEntityToDto()
        {
            CreateMap<Privilege, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<Role, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<Department, DepartmentTreeDto>();

            CreateMap<Unit, UnitTreeDto>();

            CreateMap<Company, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<Department, SelectItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.ComId))
                .ForMember(dest => dest.CompId, mo => mo.MapFrom(src => src.ComId))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name))
                .ForMember(dest => dest.gradesId, mo => mo.MapFrom(src => src.GradesId))
                .ForMember(dest => dest.DepartmentType, mo => mo.MapFrom(src => src.Type));

            CreateMap<Group, SelectItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.GroupName));

            CreateMap<Machine, SelectItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                .ForMember(dest => dest.IsDelete, mo => mo.MapFrom(src => src.IsDelete))
                .ForMember(dest => dest.AreaId, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.DeviceName));

            CreateMap<GroupAccess, SelectItem>()
                //.ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name))
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId));

            CreateMap<Areas, UserNameItem>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));
            CreateMap<Menu, SelectItem>()
              .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                            .ForMember(dest => dest.Type, mo => mo.MapFrom(src => src.Type))
              .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));
            CreateMap<Device, UserNameItem>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.CompId, mo => mo.MapFrom(src => src.CompId));

            CreateMap<User, UserNameItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                //.ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.UserName));

            CreateMap<AccessTimeSeg, SelectItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.TimeSegName));

            CreateMap<Person, SelectItem>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.PersonId.ToString().ToLower()))
                .ForMember(dest => dest.PersonType, mo => mo.MapFrom(src => src.PersonType))
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Value, mo => mo.MapFrom(src => src.PersonId.ToString().ToLower()));

            CreateMap<Company, DepartmentTreeDto>();

            CreateMap<Areas, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name))
                .ForMember(dest => dest.AreaId, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsDelete, mo => mo.MapFrom(src => src.IsDelete))
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId));

            CreateMap<WorkingShiftTimes, SelectItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.ID))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<FAQuestions, SelectItem>();

            CreateMap<Features, SelectItem>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.Disabled, mo => mo.MapFrom(src => src.Status == 0 ? true : false))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<Grades, SelectItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<ViewDeviceFeatures, SelectItem>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.FeatureId, mo => mo.MapFrom(src => src.FeatureId))
                .ForMember(dest => dest.AreaId, mo => mo.MapFrom(src => Int32.Parse(src.AreaId)));

            CreateMap<Device, SelectItem>()
                .ForMember(dest => dest.filterId, mo => mo.MapFrom(src => src.CompId))
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.DeviceName));
        }
    }
}
