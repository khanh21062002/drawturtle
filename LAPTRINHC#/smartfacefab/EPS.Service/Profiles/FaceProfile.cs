using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.Face;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class FaceDtoToEntity : Profile
    {
        public FaceDtoToEntity()
        {
            CreateMap<FaceCreateDto, Face>();
            CreateMap<FaceUpdateDto, Face>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<Face, FaceCreateDto>().IgnoreAllNonExisting()
                     /*.ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))*/; // Get newly created identity Id back to Dto
                CreateMap<Face, FaceUpdateDto>().IgnoreAllNonExisting()
               /*.ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))*/; // Get newly created identity Id back to Dto
                CreateMap<Face, FaceGridDto>()
                    //.ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    //.ForMember(dest => dest.Face, mo => mo.MapFrom(src => src.Face1.Name))
                    ;
                CreateMap<Face, FaceDetailDto>()
                    ;
            }
        }
    }
}
