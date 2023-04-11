﻿using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.Group;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class GroupDtoToEntity : Profile
    {
        public GroupDtoToEntity()
        {
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();
            CreateMap<Group, GroupUpdateDto>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<Group, GroupCreateDto>().IgnoreAllNonExisting().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name));
                     /*.ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))*/; // Get newly created identity Id back to Dto

                CreateMap<Group, GroupGridDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                      .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    //.ForMember(dest => dest.Group, mo => mo.MapFrom(src => src.Group1.Name))
                    ;
                CreateMap<Group, GroupDetailDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                      .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    ;
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
