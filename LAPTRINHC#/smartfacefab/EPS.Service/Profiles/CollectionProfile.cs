using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Collection;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class CollectionDtoToEntity : Profile
    {
        public CollectionDtoToEntity()
        {
            CreateMap<CollectionRequest, Groups>().IgnoreAllNonExisting()
                .ForMember(dest => dest.GroupCode, mo => mo.MapFrom(src => src.collection_code))
                .ForMember(dest => dest.GroupName, mo => mo.MapFrom(src => src.collection_name))
                .ForMember(dest => dest.IsDelete, mo => mo.MapFrom(src => src.is_delete))
                .ForMember(dest => dest.Status, mo => mo.MapFrom(src => src.status))
                .ForMember(dest => dest.UserId, mo => mo.MapFrom(src => src.userId));

            CreateMap<CollectionRequestCreateDto, Groups>()
                .ForMember(dest => dest.GroupCode, mo => mo.MapFrom(src => src.collection_code))
                .ForMember(dest => dest.GroupName, mo => mo.MapFrom(src => src.collection_name))
                .ForMember(dest => dest.IsDelete, mo => mo.MapFrom(src => src.is_delete))
                .ForMember(dest => dest.Status, mo => mo.MapFrom(src => src.status))
                .ForMember(dest => dest.UserId, mo => mo.MapFrom(src => src.userId));
            CreateMap<CollectionDetailDto, Groups>().IgnoreAllNonExisting();
        }
        
    }
    public class CollectionEntityToDto : Profile
    {
        public CollectionEntityToDto()
        {
            CreateMap<Groups, CollectionGridDto>().IgnoreAllNonExisting()
                .ForMember(dest => dest.collection_id, mo => mo.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.collection_code, mo => mo.MapFrom(src => src.GroupCode))
                .ForMember(dest => dest.collection_name, mo => mo.MapFrom(src => src.GroupName))
                .ForMember(dest => dest.UserId, mo => mo.MapFrom(src => src.UserId));

            CreateMap<Groups, CollectionRequestCreateDto>().IgnoreAllNonExisting()
                .ForMember(dest => dest.collection_id, mo => mo.MapFrom(src => src.GroupId))
                .ForMember(dest => dest.collection_code, mo => mo.MapFrom(src => src.GroupCode))
                .ForMember(dest => dest.collection_name, mo => mo.MapFrom(src => src.GroupName))
                .ForMember(dest => dest.userId, mo => mo.MapFrom(src => src.UserId));
            CreateMap<Groups, CollectionDetailDto>().IgnoreAllNonExisting();
        }

    }
}
