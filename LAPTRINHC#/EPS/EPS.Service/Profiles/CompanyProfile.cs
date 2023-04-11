using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class CompanyDtoToEntity : Profile
    {
        public CompanyDtoToEntity()
        {
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();
        }
    }

    public class CompanyEntityToDto : Profile
    {
        public CompanyEntityToDto()
        {
            CreateMap<Company, CompanyDetailDto>()
                .ForMember(dest => dest.ParentName, mo => mo.MapFrom(src => src.Parent.Name));
        }
    }
}
