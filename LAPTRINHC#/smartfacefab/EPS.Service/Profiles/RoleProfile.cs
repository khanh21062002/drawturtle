using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Role;
using System.Linq;
using EPS.Service.Helpers;

namespace EPS.Service.Profiles
{
    public class RoleDtoToEntity : Profile
    {
        public RoleDtoToEntity()
        {
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
            CreateMap<Role, RoleUpdateDto>();
        }
    }

    public class RoleEntityToDto : Profile
    {
        public RoleEntityToDto()
        {
            CreateMap<Role, RoleCreateDto>().IgnoreAllNonExisting()
                 .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto
            CreateMap<Role, RoleGridDto>()
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)));
            CreateMap<Role, RoleDetailDto>()
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)));
            CreateMap<RolePrivilege, RolePrivilegeDto>();
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
