using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.PersonGroup;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class PersonGroupDtoToEntity : Profile
    {
        public PersonGroupDtoToEntity()
        {
            CreateMap<PersonGroupCreateDto, PersonGroup>();
            CreateMap<PersonGroupUpdateDto, PersonGroup>();
            CreateMap<PersonGroup, PersonGroupUpdateDto>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<PersonGroup, PersonGroupCreateDto>().IgnoreAllNonExisting()
                     .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); 

                CreateMap<PersonGroup, PersonGroupGridDto>()
                    //.ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    //.ForMember(dest => dest.PersonGroup, mo => mo.MapFrom(src => src.PersonGroup1.Name))
                    ;
                CreateMap<PersonGroup, PersonGroupDetailDto>()
                    ;
            }
        }
    }
}
