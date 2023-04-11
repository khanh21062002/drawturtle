using AutoMapper;
using EPS.Data.Entities;

using EPS.Service.Dtos.Machine;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Profiles
{
    public class MachineDtoToEntity : Profile
    {
        public MachineDtoToEntity()
        {
            CreateMap<MachineCreateDto, Machine>()
                 .ForMember(dest => dest.UsePCCovid, mo => mo.MapFrom(src => src.UsePCCovid.HasValue ? src.UsePCCovid.Value : false));
            CreateMap<AreaMachineCreateDto, AreaMachine>();
            CreateMap<AreaMachineUpdateDto, AreaMachine>();
            CreateMap<AreaMachine, AreaMachineUpdateDto >();
            CreateMap<AreaMachine , AreaMachineCreateDto>();
            CreateMap<MachineUpdateDto, Machine>();
            CreateMap<Machine, MachineUpdateDto>();
            CreateMap<Machine, MachineUpdateDto>();
            CreateMap<MachineSynchronized, MachineGridDto>();
        }

        public class SupplierEntityToDto : Profile
        {
            public SupplierEntityToDto()
            {
                CreateMap<Machine, MachineCreateDto>().IgnoreAllNonExisting()
                    .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.Id)); // Get newly created identity Id back to Dto

                CreateMap<Machine, MachineGridDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)));
                CreateMap<AreaMachine, AreaMachineGridDto>();

                CreateMap<View_Machine, MachineGridDto>().ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)));

                CreateMap<Machine, MachineDetailDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.AutoSaveVisitorName, mo => mo.MapFrom(src => Converter1(src.AutoSaveVisitor)))
                    .ForMember(dest => dest.AutoStartName, mo => mo.MapFrom(src => Converter1(src.AutoStart)))
                    .ForMember(dest => dest.UseMaskName, mo => mo.MapFrom(src => Converter1(src.UseMask)))
                    .ForMember(dest => dest.UseTemperatureName, mo => mo.MapFrom(src => Converter1(src.UseTemperature)))
                    .ForMember(dest => dest.UseVaccineName, mo => mo.MapFrom(src => Converter1(src.UseVaccine)))
                    .ForMember(dest => dest.UsePCCovidName, mo => mo.MapFrom(src => Converter1(src.UsePCCovid)))
                    .ForMember(dest => dest.FraudProofName, mo => mo.MapFrom(src => Converter1(src.FraudProof)))
                    .ForMember(dest => dest.TemperatureName, mo => mo.MapFrom(src => Converter1(src.Temperature)))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.AutoSleepStr, mo => mo.MapFrom(src => Converter2(src.AutoSleep)))
                    .ForMember(dest => dest.DailyRebootStr, mo => mo.MapFrom(src => Converter2(src.DailyReboot)))
                    .ForMember(dest => dest.LanguageStr, mo => mo.MapFrom(src => Converter3(src.Language)))
                    .ForMember(dest => dest.RestartTimeStr, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.RestartTime)))
                    .ForMember(dest => dest.DailyReboot, mo => mo.MapFrom(src => src.DailyReboot))
                    .ForMember(dest => dest.LedName, mo => mo.MapFrom(src => Converter1(src.Led)));
                CreateMap<View_Machine, MachineDetailDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.AutoSaveVisitorName, mo => mo.MapFrom(src => Converter1(src.AutoSaveVisitor)))
                    .ForMember(dest => dest.AutoStartName, mo => mo.MapFrom(src => Converter1(src.AutoStart)))
                    .ForMember(dest => dest.UseMaskName, mo => mo.MapFrom(src => Converter1(src.UseMask)))
                    .ForMember(dest => dest.UseTemperatureName, mo => mo.MapFrom(src => Converter1(src.UseTemperature)))
                    .ForMember(dest => dest.UseVaccineName, mo => mo.MapFrom(src => Converter1(src.UseVaccine)))
                    .ForMember(dest => dest.UsePCCovidName, mo => mo.MapFrom(src => Converter1(src.UsePCCovid)))
                    .ForMember(dest => dest.FraudProofName, mo => mo.MapFrom(src => Converter1(src.FraudProof)))
                    .ForMember(dest => dest.TemperatureName, mo => mo.MapFrom(src => Converter1(src.Temperature)))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)))
                    .ForMember(dest => dest.AutoSleepStr, mo => mo.MapFrom(src => Converter2(src.AutoSleep)))
                    .ForMember(dest => dest.DailyRebootStr, mo => mo.MapFrom(src => Converter2(src.DailyReboot)))
                    .ForMember(dest => dest.LanguageStr, mo => mo.MapFrom(src => Converter3(src.Language)))
                    .ForMember(dest => dest.RestartTimeStr, mo => mo.MapFrom(src => FormatTimeSpanToStr(src.RestartTime)))
                    .ForMember(dest => dest.DailyReboot, mo => mo.MapFrom(src => src.DailyReboot))
                    .ForMember(dest => dest.LedName, mo => mo.MapFrom(src => Converter1(src.Led)))
                    .ForMember(dest => dest.AreaName, mo => mo.MapFrom(src => src.AreaName))
                    .ForMember(dest => dest.AreaId, mo => mo.MapFrom(src => src.AreaId));

                CreateMap<MachineSynchronized, MachineGridDto>()
                    .ForMember(dest => dest.Company, mo => mo.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.LastTimeSynchronized, mo => mo.MapFrom(src => src.LastTimeLogin.HasValue ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", src.LastTimeLogin.Value) : ""))
                    .ForMember(dest => dest.StatusOnOff, mo => mo.MapFrom(src => CheckStatusOnOff(src.LastTimeLogin)))
                    .ForMember(dest => dest.StatusName, mo => mo.MapFrom(src => Converter(src.Status)));

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

        private static int CheckStatusOnOff(DateTime? lastTimeLogin)
        {
            if (lastTimeLogin.HasValue)
            {
                DateTime timeTemp = DateTime.Now.AddMinutes(-5);
                if (lastTimeLogin.Value >= timeTemp) return 1;
            }

            return 0;
        }

        private static string Converter1(bool? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case true:
                        return "Guess.Detail.Form.Yes";
                        break;
                    case false:
                        return "Guess.Detail.Form.No";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "Guess.Detail.Form.No";
        }
        private static string Converter2(bool? value)
        {
            if (value.HasValue)
                switch (value.Value)
                {
                    case true:
                        return "Categories.Machine.List.SearchForm.StatusOn";
                        break;
                    case false:
                        return "Categories.Machine.List.SearchForm.StatusOff";
                        break;
                    default:
                        return "";
                        break;
                }
            else
                return "Categories.Machine.List.SearchForm.StatusOff";
        }
        private static string Converter3(string value)
        {
            switch (value)
            {
                case "EN":
                    return "ManagerFAQ.Detail.Label.TypeEn";
                    break;
                case "VI":
                    return "ManagerFAQ.Detail.Label.TypeVi";
                    break;
                default:
                    return "";
                    break;
            }

            return "";
        }
        private static string FormatTimeSpanToStr(TimeSpan? timeOT)
        {
            if (timeOT.HasValue)
            {
                return string.Format("{0:hh\\:mm\\:ss}", timeOT);
            }
            return "";
        }
    }
}
