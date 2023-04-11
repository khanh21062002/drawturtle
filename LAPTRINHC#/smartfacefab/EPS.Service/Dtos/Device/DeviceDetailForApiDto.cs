using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class DeviceDetailForApiDto
    {
        public int Id { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string RstpLink { get; set; }
        public string FeaturesName { get; set; }
        public string FeaturesCode { get; set; }

        public Nullable<bool> IsDelete { get; set; }

        //public List<int> DependentDeviceId { get; set; }
    }
}
