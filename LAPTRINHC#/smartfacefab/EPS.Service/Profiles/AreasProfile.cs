using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Areas;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class AreasDtoToEntity : Profile
    {
        public AreasDtoToEntity()
        {
            CreateMap<AreasCreateDto, Areas>();

            CreateMap<AreasUpdateDto, Areas>();
        }
    }

    public class AreasEntityToDto : Profile
    {
        public AreasEntityToDto()
        {
            CreateMap<Areas, AreasDetailDto>();
            CreateMap<Areas, AreasUpdateDto>();
            CreateMap<Areas, AreasGridDto>()
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => ConvertStatus(src.Status)));
        }
        
        public static string ConvertStatus(int? status)
        {
            if (status.HasValue)
            {
                switch (status)
                {
                    case 1:
                        return "Guess.Detail.Form.Active";
                    case 0:
                        return "Guess.Detail.Form.Stop";
                }
            }
            return "";
        }
    }
}
