using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Machine
{
    public class MachineDetailDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public Nullable<int> CompId { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public string Ipaddress { get; set; }
        public string Mac { get; set; }
        public string Imei { get; set; }
        public string ServerIp { get; set; }
        public Nullable<bool> AutoStart { get; set; }
        public string AutoStartName { get; set; }

        public Nullable<int> DeviceFunction { get; set; }

        public Nullable<bool> AutoSaveVisitor { get; set; }
        public string AutoSaveVisitorName { get; set; }
        public Nullable<bool> FraudProof { get; set; }
        public string FraudProofName { get; set; }
        public Nullable<int> ServerPort { get; set; }

        public Nullable<bool> Temperature { get; set; }
        public string TemperatureName { get; set; }
        public Nullable<int> AngleDetect { get; set; }

        public Nullable<int> DistanceDetect { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Logo { get; set; }
        public Nullable<float> Storage { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<int> Brightness { get; set; }
        public Nullable<int> Delay { get; set; }
        public Nullable<bool> Led { get; set; }
        public string LedName { get; set; }
        public Nullable<float> FaceThreshold { get; set; }
        public Nullable<float> TemperatureThreshold { get; set; }
        public string Company { get; set; }

        public string StatusName { get; set; }
        public bool IsDelete { get; set; }
        public string UsernameServer { get; set; }
        public string PasswordServer { get; set; }
        public Nullable<bool> UseTemperature { get; set; }
        public Nullable<bool> UseMask { get; set; }
        public string UseTemperatureName { get; set; }

        public string UseMaskName { get; set; }

        public Nullable<bool> UseVaccine { get; set; }
        public string UseVaccineName { get; set; }
        public Nullable<bool> UsePCCovid { get; set; }
        public string UsePCCovidName { get; set; }

        public string PCCovidPhone { get; set; }

        public string PCCovidLocation { get; set; }

        public string PCCovidToken { get; set; }
        public bool DailyReboot { get; set; }
        public Nullable<TimeSpan> RestartTime { get; set; }
        public string RestartTimeStr { get; set; }
        public bool AutoSleep { get; set; }
        public string DailyRebootStr { get; set; }
        public string AutoSleepStr { get; set; }
        public string LanguageStr { get; set; }
        public string Language { get; set; }
        public Nullable<int> AreaId { get; set; }
        public string AreaName { get; set; }

    }
}
