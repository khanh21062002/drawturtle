using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.Company;
using EPS.Service.Helpers;
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
            CreateMap<Company, CompanyUpdateDto>();

        } 

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<Company, CompanyCreateDto>().IgnoreAllNonExisting()
                     .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto

                CreateMap<Company, CompanyGridDto>().ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.CompanyTypeStr, mo => mo.MapFrom(src => ConverterCompanyType(src.CompanyType)));
                CreateMap<Company, CompanyDetailDto>().ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.SupName, mo => mo.MapFrom(src => Converter1(src.SupId)))
                    .ForMember(dest => dest.ParentName, mo => mo.MapFrom(src => src.Parent.Name))
                      .ForMember(dest => dest.CompanyTypeStr, mo => mo.MapFrom(src => ConverterCompanyType(src.CompanyType)));
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

            private static string ConverterCompanyType(int? value)
            {
                if (value.HasValue)
                    switch (value.Value)
                    {
                        case 1:
                            return "System.Company.Detail.Label.CompanyTypeItem2";
                            break;
                        case 0:
                            return "System.Company.Detail.Label.CompanyTypeItem1";
                            break;
                        default:
                            return "System.Company.Detail.Label.CompanyTypeItem1";
                            break;
                    }
                else
                    return "System.Company.Detail.Label.CompanyTypeItem1";
            }
            private static string Converter1(int? value)
            {
                if (value.HasValue)
                    switch (value.Value)
                    {
                        case 1:
                            return "Guess.Detail.Form.Yes";
                            break;
                        case 0:
                            return "Guess.Detail.Form.No";
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
