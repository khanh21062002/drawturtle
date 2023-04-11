using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Device;
using EPS.Service.Dtos.DeviceFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class DeviceFeaturesDtoToEntity : Profile
    {
        public DeviceFeaturesDtoToEntity()
        {
            CreateMap<DeviceFeaturesCreateDto, DeviceFeature>();
            CreateMap<DeviceFeaturesDetailDto, DeviceFeature>();
            CreateMap<DeviceFeaturesUpdateDto, DeviceFeature>();
        }
    }

    public class DeviceFeaturesEntityToDto : Profile
    {
        public DeviceFeaturesEntityToDto()
        {
            CreateMap<DeviceFeature, DeviceFeaturesGridDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsDelete, mo => mo.MapFrom(src => src.IsDelete))
                ;

            CreateMap<DeviceFeature, DeviceFeaturesDetailDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                ;

            CreateMap<DeviceFeature, DeviceFeaturesCreateDto>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id))
                ;
        }

        public static string StatusStr(int? status)
        {
            string result = "";
            if (status == 1)
            {
                result = "Đang hoạt động";
            }
            else
            {
                result = "Tạm ngưng";
            }
            return result;
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
