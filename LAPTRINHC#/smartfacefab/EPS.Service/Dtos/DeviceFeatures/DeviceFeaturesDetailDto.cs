using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.DeviceFeatures
{
    public class DeviceFeaturesDetailDto
    {
        public int Id { get; set; }
        public Nullable<int> DeviceId { get; set; }

        public Nullable<int> FeatureId { get; set; }

        public Nullable<bool> IsDelete { get; set; }

    }
}
