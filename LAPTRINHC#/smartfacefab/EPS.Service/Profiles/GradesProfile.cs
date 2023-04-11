using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Grades;
using EPS.Service.Dtos.Group;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class GradesDtoToEntity : Profile
    {
        public GradesDtoToEntity()
        {
            CreateMap<GradesCreateDto, Grades>();
            CreateMap<GradesUpdateDto, Grades>();
            CreateMap<Grades, GradesUpdateDto>();
        }

        public class GradesEntityToDto : Profile
        {
            public GradesEntityToDto()
            {

                CreateMap<Grades, GradesCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto

                CreateMap<Grades, GradesGridDto>()
                      .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name));

                CreateMap<Grades, GradesDetailDto>();

                CreateMap<Grades, GradesUpdateDto>();
            }
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
