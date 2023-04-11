using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.GroupAccess;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class GroupAccessDtoToEntity : Profile
    {
        public GroupAccessDtoToEntity()
        {
            CreateMap<GroupAccessCreateDto, GroupAccess>();
            CreateMap<GroupAccessUpdateDto, GroupAccess>();
            CreateMap<GroupAccess, GroupAccessUpdateDto>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<GroupAccess, GroupAccessCreateDto>().IgnoreAllNonExisting()
                     .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                    ; // Get newly created identity Id back to Dto

                CreateMap<v_GroupAccessDevices, GroupAccessGridDto>()
                    .ForMember(dest => dest.Machine, mo => mo.MapFrom(src => src.Devices))
                    .ForMember(dest => dest.Group, mo => mo.MapFrom(src => src.GroupName))
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.CompanyName))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.AccessTimeSeg, mo => mo.MapFrom(src => src.TimeSegName));

                CreateMap<GroupAccess, GroupAccessDetailDto>()
                    .ForMember(dest => dest.Machine, mo => mo.MapFrom(src => src.Machine.DeviceName))
                    .ForMember(dest => dest.Group, mo => mo.MapFrom(src => src.Group.GroupName))
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.AccessTimeSeg, mo => mo.MapFrom(src => src.AccessTimeSeg.TimeSegName));


                CreateMap<v_GroupAccessDevices, GroupAccessDetailDto>()
                    .ForMember(dest => dest.Machine, mo => mo.MapFrom(src => src.Devices))
                    .ForMember(dest => dest.Group, mo => mo.MapFrom(src => src.GroupName))
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.CompanyName))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.AccessTimeSeg, mo => mo.MapFrom(src => src.TimeSegName));
            }
        }
        private static string Converter(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 1:
                        return "Categories.Person.Detail.Label.Activate";
                        break;
                    case 0:
                        return "Categories.Person.Detail.Label.Stop";
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
