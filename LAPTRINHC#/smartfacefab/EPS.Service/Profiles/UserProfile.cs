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
            CreateMap<User, UserUpdateDto>();
        }
    }

    public class UserEntityToDto : Profile
    {
        public UserEntityToDto()
        {
            CreateMap<User, UserGridDto>().IgnoreAllNonExisting()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto
            
            CreateMap<User, UserGridDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                .ForMember(dest => dest.HiddenParentCompanyField, mo => mo.MapFrom(src => src.Company.HiddenParentField))
                .ForMember(dest => dest.Roles, mo => mo.MapFrom(src => src.UserRoles.Where(x => x.Role.IsDelete == false).Select(x => x.Role.Name)));
            
            CreateMap<User, UserDetailDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.RoleIds, mo => mo.MapFrom(src => src.UserRoles.Select(x => x.RoleId)))
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                .ForMember(dest => dest.isAdminByCompany, mo => mo.MapFrom(src => checkUserIsAdminByCompId(src.Company.SupId)))
                .ForMember(dest => dest.RoleNames, mo => mo.MapFrom(src => src.UserRoles.Where(x => x.Role.IsDelete == false).Select(x => x.Role.Name)));
            
            CreateMap<UserPrivilege, UserPrivilegeDto>();

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


        private static bool checkUserIsAdminByCompId(int? supId)
        {
            if (supId.HasValue)
            {
                switch (supId.Value)
                {
                    case 0:
                        return true;
                        break;
                    case 1:
                        return false;
                        break;
                    default:
                        return false;
                        break;
                }
            }

            return false;
        }
    }
}
