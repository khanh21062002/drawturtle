using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class MenuDtoToEntity : Profile
    {
        public MenuDtoToEntity()
        {
            CreateMap<MenuGridDto, Menu>();
            CreateMap<MenuCreateDto, Menu>();
            CreateMap<MenuDetailDto, Menu>();
            CreateMap<MenuUpdateDto, Menu>();
        }
    }

    public class MenuEntityToDto : Profile
    {
        public MenuEntityToDto()
        {
            CreateMap<Menu, MenuGridDto>()
                .ForMember(dest => dest.PriceStr, mo => mo.MapFrom(src => String.Format("{0:n0}", src.Price)))
                .ForMember(dest => dest.TypeStr, mo => mo.MapFrom(src => ConvertTypeFood(src.Type)));

            CreateMap<Menu, MenuCreateDto>();

            CreateMap<Menu, MenuDetailDto>()
                .ForMember(dest => dest.PriceStr, mo => mo.MapFrom(src => String.Format("{0:n0}", src.Price)))
                .ForMember(dest => dest.TypeStr, mo => mo.MapFrom(src => ConvertTypeFood(src.Type)));

            CreateMap<Menu, MenuUpdateDto>();
        }

        private static string ConvertTypeFood(int? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case 1:
                        return "Categories.Menu.Crud.Type1";
                        break;
                    case 2:
                        return "Categories.Menu.Crud.Type2";
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
