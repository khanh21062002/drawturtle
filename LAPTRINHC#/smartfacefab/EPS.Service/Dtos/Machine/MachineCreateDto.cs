using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Machine
{
    public class MachineCreateDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> Status { get; set; }
        public string Code { get; set; }
        public string Ipaddress { get; set; }
        public string Mac { get; set; }
        public string Imei { get; set; }
        public string ServerIp { get; set; }
        public Nullable<int> ServerPort { get; set; }
        public Nullable<double> UsageStorage { get; set; }

        public Nullable<int> DeviceFunction { get; set; }
        public Nullable<short> LockControl { get; set; }
        public Nullable<int> MaxUserCount { get; set; }
        public Nullable<int> MaxFaceCount { get; set; }
        public Nullable<int> MaxFingerCount { get; set; }
        public Nullable<int> MaxCardCount { get; set; }
        public Nullable<int> MaxAttlogCount { get; set; }
        public Nullable<int> AngleDetect { get; set; }
        public Nullable<bool> AutoStart { get; set; }
        public Nullable<bool> AutoSaveVisitor { get; set; }
        public Nullable<bool> FraudProof { get; set; }
        public Nullable<int> DistanceDetect { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Logo { get; set; }
        public Nullable<float> Storage { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<int> Brightness { get; set; }
        public Nullable<int> Delay { get; set; }
        public Nullable<bool> Led { get; set; }
        public Nullable<float> FingerThreshold { get; set; }
        public Nullable<float> TemperatureThreshold { get; set; }
        public string Company { get; set; }
        public string File1 { get; set; }
        public string FileData { get; set; }
        public Nullable<float> FaceThreshold { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<bool> Temperature { get; set; }
        public string UsernameServer { get; set; }
        public string PasswordServer { get; set; }
        public Nullable<bool> UseTemperature { get; set; }
        public Nullable<bool> UseMask { get; set; }
        public Nullable<int> UserCount { get; set; }
        public Nullable<int> CardCount { get; set; }
        public Nullable<int> FaceCount { get; set; }
        public Nullable<int> FingerCount { get; set; }
        public Nullable<int> DeviceType { get; set; }
        public Nullable<bool> UseVaccine { get; set; }
        public Nullable<bool> UsePCCovid { get; set; }
        public string PCCovidPhone { get; set; }
        public string PCCovidLocation { get; set; }
        public string PCCovidToken { get; set; }
        public bool DailyReboot { get; set; }
        public string DailyRebootStr { get; set; }
        public string AutoSleepStr { get; set; }
        public Nullable<TimeSpan> RestartTime { get; set; }
        public bool AutoSleep { get; set; }
        public Nullable<int> AreaId { get; set; }
        public string AreaName { get; set; }
        public string Language { get; set; }
    }
}
