using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class DeviceDetailDto
    {
        public int Id { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string RstpLink { get; set; }
        public string MjpegPort { get; set; }
        public Nullable<int> Status { get; set; }
        public int? AreaId { get; set; }
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        public string FeaturesName { get; set; }
        public Nullable<DateTime> RegisterTime { get; set; }
        public int? Direction { get; set; }
        public string DirectionStr { get; set; }
        public string RegisterBy { get; set; }
        public List<int?> FeaturesId { get; set; }
        public Nullable<DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public int? CurrentStatus { get; set; }
        public string CurrentStatusName { get; set; }
        public Nullable<DateTime> SyncDate { get; set; }
    }
}
