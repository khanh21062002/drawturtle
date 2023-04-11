using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.PersonsAccess;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class PersonsAccessDtoToEntity : Profile
    {
        public PersonsAccessDtoToEntity()
        {
            CreateMap<PersonsAccessCreateDto, PersonsAccess>();
        }        
    }

    public class PersonsAccessEntityToDto : Profile
    {
        public PersonsAccessEntityToDto()
        {
            CreateMap<PersonsAccess, PersonsAccessCreateDto>().IgnoreAllNonExisting()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto

            CreateMap<PersonsAccess, PersonsAccessDetailDto>();
        }
    }
}
