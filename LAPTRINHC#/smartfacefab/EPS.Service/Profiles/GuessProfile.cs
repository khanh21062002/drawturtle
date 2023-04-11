using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Guess;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class OTPDtoToEntity : Profile
    {
        public OTPDtoToEntity()
        {
            CreateMap<OTPCreateDto, OTP>();
            CreateMap<GuessRegisterCreateDto, GuessRegister>();
            CreateMap<GuessUpdateDto, GuessRegister>();
        }

        public class OTPEntityToDto : Profile
        {
            public OTPEntityToDto()
            {
                CreateMap<OTP, OTPCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID)); // Get newly created identity Id back to Dto

                CreateMap<GuessRegister, GuessRegisterCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID)); // Get newly created identity Id back to Dto

                CreateMap<GuessRegister, GuessGridDto>()
                    .ForMember(dest => dest.StrStartTime, mo => mo.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(dest => dest.StrEndTime, mo => mo.MapFrom(src => src.EndTime.HasValue ? src.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""));

                CreateMap<View_ListGuess, GuessGridDto>()
                    .ForMember(dest => dest.StrStartTime, mo => mo.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.ToString("dd/MM/yyyy HH:mm") : ""))
                    .ForMember(dest => dest.StrEndTime, mo => mo.MapFrom(src => src.EndTime.HasValue ? src.EndTime.Value.ToString("dd/MM/yyyy HH:mm") : ""));

                CreateMap<GuessRegister, GuessDetailDto>();

                CreateMap<GuessRegister, GuessUpdateDto>();
            }
        }
    }
}
