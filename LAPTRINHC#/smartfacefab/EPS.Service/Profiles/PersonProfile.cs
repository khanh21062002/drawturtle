using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Card;
using EPS.Service.Dtos.Event;
using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.Person;
using EPS.Service.Dtos.PersonsAccess;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class PersonDtoToEntity : Profile
    {
        public PersonDtoToEntity()
        {
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<Person, PersonUpdateDto>();
            CreateMap<Cards, CardUpdateDto>();
            CreateMap<Cards, CardCreateDto>();

            //school
            CreateMap<StudentCreateDto, Person>();
            CreateMap<ShowImfomationDto, ShowImfomation>();
            CreateMap<StudentUpdateDto, Person>();
            CreateMap<ParentCreateDto, Person>();
            CreateMap<ParentUpdateDto, Person>();
            CreateMap<CardCreateDto, Cards>();
            CreateMap<PersonsAccessCreateDto, PersonsAccess>();
        }

        public class PersonEntityToDto : Profile
        {
            public PersonEntityToDto()
            {
                CreateMap<Face, FaceCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.FaceId, mo => mo.MapFrom(src => src.FaceId));

                CreateMap<Person, PersonCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId)); // Get newly created identity Id back to Dto

                CreateMap<Person, PersonGridDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.Department, mo => mo.MapFrom(src => src.Department.Name))
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.HiddenParentField, mo => mo.MapFrom(src => src.Department.HiddenParentField))
                    .ForMember(dest => dest.DobStr, mo => mo.MapFrom(src => FormatTime(src.Birthday)));

                CreateMap<v_Customer, PersonGridDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.PersonTypeStr, mo => mo.MapFrom(src => ConverterPersonType(src.PersonType)))
                    .ForMember(dest => dest.DobStr, mo => mo.MapFrom(src => FormatTime(src.Birthday)));

                CreateMap<View_ListPerson, PersonGridDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.ComName))
                    .ForMember(dest => dest.PersonTypeStr, mo => mo.MapFrom(src => ConverterPersonType(src.PersonType)))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    //.ForMember(dest => dest.NumberOfTimes, mo => mo.MapFrom(src => src.View_Person_NumberOfTimes.NumberOfTimes))
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.DobStr, mo => mo.MapFrom(src => FormatTime(src.Birthday)));

                CreateMap<View_ListPerson, PersonDetailDto>()
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.PersonTypeName, mo => mo.MapFrom(src => ConverterPersonType(src.PersonType)))
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.DobStr, mo => mo.MapFrom(src => FormatTime(src.Birthday)));
                CreateMap<Person, PersonDetailDto>()
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.PersonTypeName, mo => mo.MapFrom(src => ConverterPersonType(src.PersonType)))
                    .ForMember(dest => dest.DobStr, mo => mo.MapFrom(src => FormatTime(src.Birthday)));

                CreateMap<Person, StudentCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));

                CreateMap<Cards, CardCreateDto>().IgnoreAllNonExisting();

                CreateMap<Cards, CardUpdateDto>().IgnoreAllNonExisting();

                CreateMap<PersonsAccess, PersonsAccessCreateDto>().IgnoreAllNonExisting();

                CreateMap<Person, ParentCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));

                CreateMap<Person, ParentDetailDto>()
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.VaccineStr, mo => mo.MapFrom(src => ConverterVaccineStr(src.Vaccine)))
                    .ForMember(dest => dest.GradeName, mo => mo.MapFrom(src => src.Department.Grades.Name));

                CreateMap<Person, StudentParentsDetailDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.Department, mo => mo.MapFrom(src => src.Department.Name))
                    .ForMember(dest => dest.GenderName, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.VaccineStr, mo => mo.MapFrom(src => ConverterVaccineStr(src.Vaccine)))
                    .ForMember(dest => dest.GradeName, mo => mo.MapFrom(src => src.Department.Grades.Name))
                    .ForMember(dest => dest.GradeId, mo => mo.MapFrom(src => src.Department.Grades.Id))
                    .ForMember(dest => dest.DobStr, mo => mo.MapFrom(src => FormatTime(src.Birthday)));

                CreateMap<ShowImfomation, ShowImfomationDto>();

                CreateMap<v_CustomerEvent, CustomerEventGridDto>()
                    .ForMember(dest => dest.CompanyName, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.GenderStr, mo => mo.MapFrom(src => Converter1(src.Gender)))
                    .ForMember(dest => dest.AccessDateDayFormat, mo => mo.MapFrom(src => src.AccessTime.HasValue ? src.AccessTime.Value.ToString("dd/MM/yyyy HH:mm") : ""));
            }

            private static string Converter1(int? value)
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

            private static string FormatTime(DateTime? src)
            {
                if (src.HasValue)
                {
                    return src.Value.ToString("dd/MM/yyyy");
                }
                return "";
            }

            private static string ConverterVaccineStr(int? value)
            {
                if (value.HasValue)
                    switch (value.Value)
                    {
                        case -1:
                            return "Categories.Person.Detail.Label.StatusVaccine1";
                            break;
                        case 0:
                            return "Categories.Person.Detail.Label.StatusVaccine2";
                            break;
                        case 1:
                            return "Categories.Person.Detail.Label.StatusVaccine3";
                            break;
                        case 2:
                            return "Categories.Person.Detail.Label.StatusVaccine4";
                            break;
                        case 3:
                            return "Categories.Person.Detail.Label.StatusVaccine5";
                            break;
                        case 4:
                            return "Categories.Person.Detail.Label.StatusVaccine6";
                            break;
                        default:
                            return "";
                            break;
                    }
                else
                    return "Categories.Person.Detail.Label.StatusVaccine2";
            }

            private static string ConverterPersonType(int? value)
            {
                if (value.HasValue)
                    switch (value.Value)
                    {
                        case 0:
                            return "Reporting.Event.List.Table.CustomerVip";
                            break;
                        case 1:
                            return "Reporting.Event.List.Table.Employee";
                            break;
                        case 2:
                            return "Reporting.Event.List.Table.Customer";
                            break;
                        case 4:
                        case 7: //Nhân viên hết hạn tạm thời coi là đối tượng theo dõi
                            return "Reporting.Event.List.Table.Suspect";
                            break;
                        case 5:
                            return "Reporting.Event.List.Table.Thief";
                            break;
                        case 6:
                            return "Reporting.Event.List.Table.Vandalize";
                            break;
                        default:
                            return "";
                            break;
                    }
                else
                    return "Reporting.Event.List.Table.Employee";
            }
        }
    }
}
