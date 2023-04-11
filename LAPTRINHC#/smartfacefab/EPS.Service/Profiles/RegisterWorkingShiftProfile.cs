using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.RegisterWorkingShift;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class RegisterWorkingShiftDtoToEntity : Profile
    {
        public RegisterWorkingShiftDtoToEntity()
        {
            CreateMap<RegisterWorkingShiftCreateDto, RegisterWorkingShift>();
            CreateMap<RegisterWorkingShiftUpdateDto, RegisterWorkingShift>();
            CreateMap<RegisterWorkingShiftDetailCreateAndUpdateDto, RegisterWorkingShiftDetail>();
        }
    }
    public class RegisterWorkingShiftEntityToDto : Profile
    {
        public RegisterWorkingShiftEntityToDto()
        {
            CreateMap<RegisterWorkingShift, RegisterWorkingShiftCreateDto>().IgnoreAllNonExisting()
                .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID)); // Get newly created identity Id back to Dto;

            CreateMap<RegisterWorkingShift, RegisterWorkingShiftUpdateDto>();

            CreateMap<RegisterWorkingShift, RegisterWorkingShiftDetailDto>()
                .ForMember(dest => dest.TypeName, mo => mo.MapFrom(src => GetTypeName(src.Type)))
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.DateFromFormat, mo => mo.MapFrom(src => FormatDate(src.DateFrom)))
                .ForMember(dest => dest.DateToFormat, mo => mo.MapFrom(src => FormatDate(src.DateTo)));

            CreateMap<RegisterWorkingShift, RegisterWorkingShiftGridDto>()
                .ForMember(dest => dest.HiddenParentField, mo => mo.MapFrom(src => src.Department.HiddenParentField))
                .ForMember(dest => dest.TypeName, mo => mo.MapFrom(src => GetTypeName(src.Type)))
                .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.DateFromFormat, mo => mo.MapFrom(src => FormatDate(src.DateFrom)))
                .ForMember(dest => dest.DateToFormat, mo => mo.MapFrom(src => FormatDate(src.DateTo)));

            CreateMap<RegisterWorkingShiftDetail, RegisterWorkingShiftDetailCreateAndUpdateDto>().IgnoreAllNonExisting()
                .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID)); // Get newly created identity Id back to Dto;
        }

        private static string FormatDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("dd/MM/yyyy");
            }
            return "";
        }

        private static string GetTypeName(int? type)
        {
            if (type.HasValue)
            {
                switch (type.Value)
                {
                    case 0:
                        return "Timesheet.RegisterWorkingShift.Detail.InitDetail.Company";
                        break;
                    case 1:
                        return "Timesheet.RegisterWorkingShift.Detail.InitDetail.Department";
                        break;
                    case 2:
                        return "Timesheet.RegisterWorkingShift.Detail.InitDetail.Employee";
                        break;
                    default:
                        return "";
                        break;
                }
            }
            return "";
        }
    }
}
