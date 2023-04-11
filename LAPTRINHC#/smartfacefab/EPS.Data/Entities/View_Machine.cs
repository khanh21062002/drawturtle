using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class View_Machine
    {
        public int Id { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeviceName { get; set; }
        public Nullable<int> DeviceType { get; set; }
        public Nullable<int> DeviceFunction { get; set; }
        public string Ipaddress { get; set; }
        public string Imei { get; set; }
        public string Mac { get; set; }
        public string ServerIp { get; set; }
        public Nullable<int> ServerPort { get; set; }
        public Nullable<bool> FraudProof { get; set; }
        public Nullable<bool> AutoStart { get; set; }
        public Nullable<int> AngleDetect { get; set; }
        public Nullable<bool> AutoSaveVisitor { get; set; }
        public Nullable<int> DistanceDetect { get; set; }
        public Nullable<double> TemperatureThreshold { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Logo { get; set; }
        public Nullable<double> Storage { get; set; }
        public Nullable<double> UsageStorage { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public Nullable<System.DateTime> RegKey { get; set; }
        public string RegIp { get; set; }
        public Nullable<short> LockControl { get; set; }
        public Nullable<int> MaxUserCount { get; set; }
        public Nullable<int> MaxFaceCount { get; set; }
        public Nullable<int> MaxFingerCount { get; set; }
        public Nullable<int> MaxCardCount { get; set; }
        public Nullable<int> MaxAttlogCount { get; set; }
        public string Language { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<int> Brightness { get; set; }
        public Nullable<int> Delay { get; set; }
        public Nullable<bool> Led { get; set; }
        public Nullable<double> FingerThreshold { get; set; }
        public Nullable<double> FaceThreshold { get; set; }
        public Nullable<int> UserCount { get; set; }
        public Nullable<int> CardCount { get; set; }
        public Nullable<int> FaceCount { get; set; }
        public Nullable<int> FingerCount { get; set; }
        public string FirmwareVersion { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string RegisterBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> AreaId { get; set; }
        public string Code { get; set; }
        public string File1 { get; set; }
        public string FileData { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> Temperature { get; set; }
        public string UsernameServer { get; set; }
        public string PasswordServer { get; set; }
        public Nullable<bool> UseTemperature { get; set; }
        public Nullable<bool> UseMask { get; set; }

        public Nullable<bool> UseVaccine { get; set; }

        public Nullable<bool> UsePCCovid { get; set; }

        public string PCCovidPhone { get; set; }

        public string PCCovidLocation { get; set; }

        public string PCCovidToken { get; set; }
        public string AreaName { get; set; }
        public bool DailyReboot { get; set; }
        public Nullable<TimeSpan> RestartTime { get; set; }
        public bool AutoSleep { get; set; }
    }
}
