using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class DeviceDtoToEntity : Profile
    {
        public DeviceDtoToEntity()
        {
            CreateMap<DeviceCreateDto, Device>();
            CreateMap<PolygonCreateDto, Polygon>();
            CreateMap<DeviceDetailDto, Device>();
            CreateMap<DeviceUpdateDto, Device>();

        }
    }

    public class DeviceEntityToDto : Profile
    {
        public DeviceEntityToDto()
        {
            CreateMap<Device, DeviceGridDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.DirectionStr, mo => mo.MapFrom(src => ConvertDirection(src.Direction)))
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => StatusStr(src.Status)))
                .ForMember(dest => dest.CurrentStatusName, mo => mo.MapFrom(src => CurrentStatusStr(src.CurrentStatus)))
                .ForMember(dest => dest.IsDelete, mo => mo.MapFrom(src => src.IsDelete));

            CreateMap<ViewDeviceFeatures, DeviceGridDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => StatusStr(src.Status)))
                .ForMember(dest => dest.FeaturesName, mo => mo.MapFrom(src => src.FeaturesName))
                .ForMember(dest => dest.DirectionStr, mo => mo.MapFrom(src => ConvertDirection(src.Direction)))
                .ForMember(dest => dest.CurrentStatusName, mo => mo.MapFrom(src => CurrentStatusStr(src.CurrentStatus)))
                .ForMember(dest => dest.IsDelete, mo => mo.MapFrom(src => src.IsDelete))
                ;

            CreateMap<Device, DeviceDetailDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.AreaName, mo => mo.MapFrom(src => src.Areas.Name))
                .ForMember(dest => dest.CurrentStatusName, mo => mo.MapFrom(src => CurrentStatusStr(src.CurrentStatus)))
                .ForMember(dest => dest.DirectionStr, mo => mo.MapFrom(src => ConvertDirection(src.Direction)))
                ;

            CreateMap<Polygon, PolygonCreateDto>()
                .ForMember(dest => dest.CamId, mo => mo.MapFrom(src => src.CamId));

            CreateMap<ViewDeviceFeatures, DeviceDetailDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.AreaName, mo => mo.MapFrom(src => src.AreaName))
                .ForMember(dest => dest.AreaCode, mo => mo.MapFrom(src => src.AreaCode))
                .ForMember(dest => dest.FeaturesName, mo => mo.MapFrom(src => src.FeaturesName))
                .ForMember(dest => dest.MjpegPort, mo => mo.MapFrom(src => 10000 + src.Id))
                .ForMember(dest => dest.DirectionStr, mo => mo.MapFrom(src => ConvertDirection(src.Direction)))
                .ForMember(dest => dest.CurrentStatusName, mo => mo.MapFrom(src => CurrentStatusStr(src.CurrentStatus)))
                ;

            CreateMap<ViewDeviceFeatures, DeviceDetailForApiDto>()
                .ForMember(dest => dest.FeaturesName, mo => mo.MapFrom(src => src.FeaturesName));

            CreateMap<Device, DeviceUpdateDto>();
        }

        public static string StatusStr(int? status)
        {
            string result = "";
            if (status.HasValue && status == 1)
            {
                result = "Đang hoạt động";
            }
            else
            {
                result = "Tạm ngưng";
            }
            return result;
        }

        public static string CurrentStatusStr(int? currentStatus)
        {
            string result = "";
            
            if (currentStatus.HasValue && currentStatus == 1)
            {
                result = "ON";
            }
            else
            {
                result = "OFF";
            }
            return result;
        }

        public static List<int> ConvertDependentDeviceArrayApi(int? id)
        {
            List<int> rs = new List<int>();
            if (id.HasValue)
            {
                rs.Add(id.Value);
            }
            return rs;
        }

        public static string ConvertDirection(int? direction)
        {
            string directionStr = "";
            if (direction.HasValue)
            {
                switch (direction.Value)
                {
                    case 1:
                        directionStr = "Categories.Device.Common.ListDirection.Entry";
                        break;

                    case 2:
                        directionStr = "Categories.Device.Common.ListDirection.Exit";
                        break;
                }
            }
            return directionStr;
        }

        public static string ConvertTime(DateTime? timestart)
        {
            string time = "";
            if (timestart.ToString().Contains("AM"))
            {
                time = timestart.Value.Hour.ToString() + ":" + timestart.Value.Minute.ToString() + timestart.Value.Millisecond.ToString();
                return time;
            }
            if (timestart.ToString().Contains("PM"))
            {
                time = timestart.Value.Hour.ToString() + ":" + timestart.Value.Minute.ToString() + timestart.Value.Millisecond.ToString();
                return time;
            }
            return time;
        }
    }
}
