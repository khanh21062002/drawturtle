using ArcFaceService.Entity;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Persons;

namespace EPS.Service.Profiles
{
    public class PersonsDtoToEntity : Profile
    {
        public PersonsDtoToEntity()
        {
            CreateMap<PersonsCreateDto, Persons>()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));

            CreateMap<PersonsUpdateDto, Persons>()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));

            CreateMap<PersonsDeleteDto, Persons>()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));
            CreateMap<PersonDetailDtos, Persons>();
        }

        public class PersonsEntityToDto : Profile
        {
            public PersonsEntityToDto()
            {
                CreateMap<Persons, PersonsCreateDto>()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));

                CreateMap<Persons, PersonsUpdateDto>()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));

                CreateMap<Persons, PersonsDeleteDto>()
                    .ForMember(dest => dest.PersonId, mo => mo.MapFrom(src => src.PersonId));

                CreateMap<Persons, PersonDetailDtos>()
                    .ForMember(dest => dest.GroupName, mo => mo.MapFrom(src => src.Groups.GroupName))
                    .ForMember(dest => dest.GroupId, mo => mo.MapFrom(src => src.Groups.GroupId))
                    .ForMember(dest => dest.person_id, mo => mo.MapFrom(src => src.PersonId))
                    .ForMember(dest => dest.person_code, mo => mo.MapFrom(src => src.PersonCode))
                    .ForMember(dest => dest.RegisterTime, mo => mo.MapFrom(src => src.RegisterTime))
                    .ForMember(dest => dest.person_name, mo => mo.MapFrom(src => src.PersonName));

                CreateMap<Persons, PersonUnit>();
            }
        }

        public class PersonDtoToUnit : Profile
        {
            public PersonDtoToUnit()
            {
                CreateMap<PersonsCreateDto, PersonUnit>();
                CreateMap<PersonsUpdateDto, PersonUnit>();
                CreateMap<PersonsDeleteDto, PersonUnit>();
            }
        }
    }
}