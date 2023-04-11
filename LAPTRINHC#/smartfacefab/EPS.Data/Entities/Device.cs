using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string RstpLink { get; set; }
        public Nullable<int> Status { get; set; }
        public int? AreaId { get; set; }
        public Nullable<DateTime> RegisterTime { get; set; }
        public string RegisterBy { get; set; }
        public Nullable<DateTime> UpdateTime { get; set; }
        public int? Direction { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public string DeleteBy { get; set; }

     
        //public int? DependentDeviceId { get; set; }

        public int? CurrentStatus { get; set; }
        public Nullable<DateTime> SyncDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
        [ForeignKey("AreaId")]
        public virtual Areas Areas { get; set; }

        //[ForeignKey("DependentDeviceId")]
        //public virtual Device DependentDevice { get; set; }

    }
}
