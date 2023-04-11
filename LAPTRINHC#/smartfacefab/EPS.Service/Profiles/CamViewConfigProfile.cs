using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.CamViewConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class CamViewConfigDtoToEntity : Profile
    {
        public CamViewConfigDtoToEntity()
        {
            CreateMap<CamViewConfigUpdateDto, CamViewConfig>();
        }
    }

    public class CamViewConfigEntityToDto : Profile
    {
        public CamViewConfigEntityToDto()
        {
            CreateMap<CamViewConfig, CamViewConfigDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.CompId, mo => mo.MapFrom(src => src.User.CompId))
                .ForMember(dest => dest.DeviceCode, mo => mo.MapFrom(src => src.Device.DeviceCode))
                .ForMember(dest => dest.AreaCode, mo => mo.MapFrom(src => src.Device.Areas.Code))
                .ForMember(dest => dest.RstpLink, mo => mo.MapFrom(src => src.Device.RstpLink))
                ;

            CreateMap<CamViewConfig, CamViewConfigUpdateDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id));
        }
    }
}
