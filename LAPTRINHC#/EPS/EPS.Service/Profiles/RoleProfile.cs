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
        }
    }

    public class RoleEntityToDto : Profile
    {
        public RoleEntityToDto()
        {
            CreateMap<Role, RoleCreateDto>().IgnoreAllNonExisting()
                 .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto
            CreateMap<Role, RoleGridDto>().IgnoreAllNonExisting()
                 .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => src.Status == 1 ? "StatusList.Active":"StatusList.Inactive"));
            CreateMap<Role, RoleDetailDto>().IgnoreAllNonExisting()
                 .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => src.Status == 1 ? "StatusList.Active" : "StatusList.Inactive"));
            CreateMap<RolePrivilege, RolePrivilegeDto>();
        }
    }
}
