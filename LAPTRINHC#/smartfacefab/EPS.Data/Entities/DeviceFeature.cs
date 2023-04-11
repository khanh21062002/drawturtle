using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class DeviceFeature
    {
        public int Id { get; set; }
        public Nullable<int> DeviceId { get; set; }
        public Nullable<int> FeatureId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
