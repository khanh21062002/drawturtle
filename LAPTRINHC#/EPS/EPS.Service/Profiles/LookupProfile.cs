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

            CreateMap<Company, CompanyTreeDto>();

            CreateMap<User, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.FullName));
            CreateMap<Company, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));
        }
    }
}
