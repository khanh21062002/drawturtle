using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.Department;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class DepartmentDtoToEntity : Profile
    {
        public DepartmentDtoToEntity()
        {
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
            CreateMap<Department, DepartmentUpdateDto>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<Department, DepartmentCreateDto>().IgnoreAllNonExisting()
                     .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto

                CreateMap<Department, DepartmentGridDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                     .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.Department, mo => mo.MapFrom(src => src.Parent.Name))
                    .ForMember(dest => dest.GradeName, mo => mo.MapFrom(src => src.Grades.Name))
                     .ForMember(dest => dest.TreeLevelInt, mo => mo.MapFrom(src => ConverterStringToInt(src.TreeLevel)));
                CreateMap<Department, DepartmentDetailDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                     .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.Department, mo => mo.MapFrom(src => src.Parent.Name))
                      .ForMember(dest => dest.GradeName, mo => mo.MapFrom(src => src.Grades.Name))
                      .ForMember(dest => dest.TreeLevelInt, mo => mo.MapFrom(src => ConverterStringToInt(src.TreeLevel)));
            }

            private static int? ConverterStringToInt(string treeVl)
            {
                if (!string.IsNullOrEmpty(treeVl))
                {
                    int x = 0;

                    if (Int32.TryParse(treeVl, out x))
                    {
                        // you know that the parsing attempt
                        // was successful
                        return x;
                    }
                }
                return null;
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
