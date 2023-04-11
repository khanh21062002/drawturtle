using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.PersonQRUpdate;
using EPS.Service.Dtos.PersonQRUpdate.QRCodePerson;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class PersonQRUpdateDtoToEntity : Profile
    {
        public PersonQRUpdateDtoToEntity()
        {
            CreateMap<PersonQRUpdateGridDto, PersonQRUpdate>();
            CreateMap<UpdateImageDto, PersonQRUpdate>();

            CreateMap<QRCodePersonDetailDto, PersonQRCode>();
            CreateMap<QRCodePersonUpdateDto, PersonQRCode>();
        }

        public class TimeKeepingEntityToDto : Profile
        {
            public TimeKeepingEntityToDto()
            {
                CreateMap<PersonQRUpdate, PersonQRUpdateGridDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                    .ForMember(dest => dest.DateUpdateStr, mo => mo.MapFrom(src => src.DateUpdate.HasValue ? String.Format("{0:dd/MM/yyyy}", src.DateUpdate.Value) : ""))
                    .ForMember(dest => dest.DeptName, mo => mo.MapFrom(src => src.Person.Department.Name))
                    .ForMember(dest => dest.Gender, mo => mo.MapFrom(src => ConverterGender(src.Person.Gender)))
                    .ForMember(dest => dest.StatusStr, mo => mo.MapFrom(src => ConverterStatus(src.Status)))
                    .ForMember(dest => dest.DeptId, mo => mo.MapFrom(src => src.Person.DeptId))
                    .ForMember(dest => dest.CompId, mo => mo.MapFrom(src => src.Person.CompId))
                    .ForMember(dest => dest.PersonCode, mo => mo.MapFrom(src => src.Person.PersonCode))
                    .ForMember(dest => dest.PersonName, mo => mo.MapFrom(src => src.Person.FullName))
                    ;

                CreateMap<PersonQRCode, QRCodePersonDetailDto>()
                    .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                    .ForMember(dest => dest.DateFromStr, mo => mo.MapFrom(src => src.DateFrom.HasValue ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", src.DateFrom.Value) : ""))
                    .ForMember(dest => dest.DateToStr, mo => mo.MapFrom(src => src.DateTo.HasValue ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", src.DateTo.Value) : ""))
                    ;

                CreateMap<PersonQRCode, QRCodePersonUpdateDto>();
                CreateMap<PersonQRUpdate, UpdateImageDto>();
            }
        }

        private static string ConverterGender(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 0:
                        return "Guess.Detail.Form.Male";
                        break;
                    case 1:
                        return "Guess.Detail.Form.Female";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "";
        }

        private static string ConverterStatus(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 1:
                        return "QRCodePerson.BackEnd.Profile.ListStatus.1";
                        break;
                    case 0:
                        return "QRCodePerson.BackEnd.Profile.ListStatus.0";
                        break;
                    case 2:
                        return "QRCodePerson.BackEnd.Profile.ListStatus.2";
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
