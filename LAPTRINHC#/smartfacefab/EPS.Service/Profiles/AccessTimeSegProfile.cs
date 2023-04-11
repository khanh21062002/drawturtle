using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.AccessTimeSeg;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class AccessTimeSegDtoToEntity : Profile
    {
        public AccessTimeSegDtoToEntity()
        {
            CreateMap<AccessTimeSegCreateDto, AccessTimeSeg>();
            CreateMap<AccessTimeSegUpdateDto, AccessTimeSeg>();
            CreateMap<AccessTimeSeg, AccessTimeSegUpdateDto>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<AccessTimeSeg, AccessTimeSegCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto

                CreateMap<AccessTimeSeg, AccessTimeSegGridDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    ;

                CreateMap<AccessTimeSeg, AccessTimeSegDetailDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)));
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
}