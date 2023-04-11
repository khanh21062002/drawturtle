using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.PreOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class PreOrderDtoToEntity : Profile
    {
        public PreOrderDtoToEntity()
        {
            CreateMap<PreOrderGridDto, PreOrder>();
            CreateMap<PreOrderCreateDto, PreOrder>();
            CreateMap<PreOrderDetailDto, PreOrder>();
            CreateMap<PreOrderUpdateDto, PreOrder>();
        }
    }

    public class PreOrderEntityToDto : Profile
    {
        public PreOrderEntityToDto()
        {
            CreateMap<PreOrder, PreOrderGridDto>()
                .ForMember(dest => dest.NameGuest, mo => mo.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.DownPaymentStr, mo => mo.MapFrom(src => String.Format("{0:n0}", src.DownPayment)))
                .ForMember(dest => dest.TimeOrderStr, mo => mo.MapFrom(src => src.TimeOrder.HasValue ? src.TimeOrder.Value.ToString("dd/MM/yyyy HH:mm") : ""));

            CreateMap<PreOrder, PreOrderCreateDto>();

            CreateMap<PreOrder, PreOrderDetailDto>()
                .ForMember(dest => dest.DownPaymentStr, mo => mo.MapFrom(src => String.Format("{0:n0}", src.DownPayment)))
                .ForMember(dest => dest.NameGuest, mo => mo.MapFrom(src => src.Person.FullName));

            CreateMap<PreOrder, PreOrderUpdateDto>();
        }
    }
}
