using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.User;
using System.Linq;
using EPS.Service.Helpers;

namespace EPS.Service.Profiles
{
    public class UserDtoToEntity : Profile
    {
        public UserDtoToEntity()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }

    public class UserEntityToDto : Profile
    {
        public UserEntityToDto()
        {
            CreateMap<User, UserGridDto>().IgnoreAllNonExisting()
                 .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto
            CreateMap<User, UserGridDto>()
                .ForMember(dest => dest.Roles, mo => mo.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));
            CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.RoleIds, mo => mo.MapFrom(src => src.UserRoles.Select(x => x.RoleId)))
                .ForMember(dest => dest.RoleNames, mo => mo.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));
            CreateMap<UserPrivilege, UserPrivilegeDto>();

        }
    }
}
