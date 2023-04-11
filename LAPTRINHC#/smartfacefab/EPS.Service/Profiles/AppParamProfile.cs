using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.AppParam;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class AppParamDtoToEntity : Profile
    {
        public AppParamDtoToEntity()
        {
            CreateMap<AppParamCreateDto, AppParam>();
            CreateMap<AppParamUpdateDto, AppParam>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<AppParam, AppParamCreateDto>().IgnoreAllNonExisting()
                     /*.ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))*/; // Get newly created identity Id back to Dto
                CreateMap<AppParam, AppParamUpdateDto>().IgnoreAllNonExisting()
               /*.ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))*/; // Get newly created identity Id back to Dto
                CreateMap<AppParam, AppParamGridDto>()
                    //.ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    //.ForMember(dest => dest.AppParam, mo => mo.MapFrom(src => src.AppParam1.Name))
                    ;
                CreateMap<AppParam, AppParamDetailDto>()
                    ;
            }
        }
    }
}
