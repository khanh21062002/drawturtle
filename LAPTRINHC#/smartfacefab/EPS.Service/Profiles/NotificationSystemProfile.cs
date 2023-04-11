using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.NotificationSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class NotificationSystemEntityToDto : Profile
    {
        public NotificationSystemEntityToDto()
        {
            CreateMap<NotificationSystem, NotificationSystemGridDto>().ForMember(x => x.NoContent, opt => opt.Ignore());

            CreateMap<NotificationSystem, NotificationSystemDetailDto>();
        }
    }
}
