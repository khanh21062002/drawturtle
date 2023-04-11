using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Common;
using EPS.Service.Dtos.FAQuestions;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class FAQuestionsDtoToEntity : Profile
    {
        public FAQuestionsDtoToEntity()
        {
            CreateMap<FAQuestionsCreateDto, FAQuestions>();
            CreateMap<FAQuestionsUpdateDto, FAQuestions>();
        }

        public class FAQuestionsEntityToDto : Profile
        {
            public FAQuestionsEntityToDto()
            {
                CreateMap<FAQuestions, FAQuestionsGridDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.Typestr, mo => mo.MapFrom(src => Converter(src.Type)));
                CreateMap<FAQuestions, FAQuestionsDetailDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.Typestr, mo => mo.MapFrom(src => Converter(src.Type)));
                CreateMap<FAQuestions, SelectItem>();
            }
        }
        private static string Converter(string value)
        {
            switch(value)
            {
                case "en":
                    return "ManagerFAQ.Detail.Label.TypeEn";
                    break;
                case "vi":
                    return "ManagerFAQ.Detail.Label.TypeVi";
                    break;
                default:
                    return "";
                    break;
            }
                
            return "";
        }
    }
}
