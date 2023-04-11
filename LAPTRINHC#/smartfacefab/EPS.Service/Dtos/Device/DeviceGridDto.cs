using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class DeviceGridDto
    {
        public int Id { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string RstpLink { get; set; }
        public Nullable<int> Status { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public Nullable<DateTime> RegisterTime { get; set; }
        public string RegisterBy { get; set; }
        public Nullable<DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public string DeleteBy { get; set; }
        public int? Direction { get; set; }
        public string DirectionStr { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string FeaturesName { get; set; }
        public string FeaturesCode { get; set; }
        public string StatusName { get; set; }
        public string FeatureId { get; set; }

        public int? CurrentStatus { get; set; }
        public Nullable<DateTime> SyncDate { get; set; }

        public string CurrentStatusName { get; set; }
    }
}
