using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Notification;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class NotificationDtoToEntity : Profile
    {
        public NotificationDtoToEntity()
        {
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<NotificationUpdateDto, Notification>();
            CreateMap<NotificationDetailCreateDto, NotificationDetail>();
        }


    }
    public class NotificationEntityToDto : Profile
    {
        public NotificationEntityToDto()
        {
            CreateMap<Notification, NotificationCreateDto>().IgnoreAllNonExisting()
                 .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID)); // Get newly created identity Id back to Dto
            CreateMap<Notification, NotificationUpdateDto>();
            CreateMap<Notification, NotificationGridDto>()
               .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
               .ForMember(dest => dest.MachineName, mo => mo.MapFrom(src => src.Machine.DeviceName))
               .ForMember(dest => dest.TypeName, mo => mo.MapFrom(src => ConvertType(src.Type)))
               ;
            CreateMap<Notification, NotificationDetailDto>()
               .ForMember(dest => dest.CompName, mo => mo.MapFrom(src => src.Company.Name))
               .ForMember(dest => dest.MachineName, mo => mo.MapFrom(src => src.Machine.DeviceName))
               .ForMember(dest => dest.TypeName, mo => mo.MapFrom(src => ConvertType(src.Type)))
               ;
            CreateMap<NotificationDetail, NotificationDetailCreateDto>().IgnoreAllNonExisting()
                .ForMember(dest => dest.ID, mo => mo.MapFrom(src => src.ID)); // Get newly created identity Id back to Dto
            CreateMap<NotificationDetail, NotificationDetailGridDto>()
                   .ForMember(dest => dest.FullName, mo => mo.MapFrom(src => src.Person.FullName)); // Get newly created identity Id back to Dto
        }


        private static string ConvertType(int? type)
        {
            if (type.HasValue)
            {
                var rs = "";
                switch (type.Value)
                {
                    case 1:
                        // code block
                        rs = "Warning.Create.Type.Stranger";
                        break;
                    case 2:
                        // code block4
                        rs = "Warning.Create.Type.Temperature";
                        break;
                    default:
                        // code block
                        break;
                }
                return rs;
            }
            return "";
        }

    }
}
