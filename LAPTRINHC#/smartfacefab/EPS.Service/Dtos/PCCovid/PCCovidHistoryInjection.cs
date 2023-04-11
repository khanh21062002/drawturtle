using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Service.Dtos
{
    public class PCCovidHistoryInjection
    {
        public string State { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<long> LastTime { get; set; }
        public Nullable<long> ExpiredTime { get; set; }
        public string InjectionName { get; set; }
        public string InjectionAddress { get; set; }
        public string LotNumber { get; set; }
    }
}
