using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomePersonInfor
    {
        public int Id { get; set; }
        public int? EmpWorking { get; set; }
        public int? EmpInactivity { get; set; }
        public int? Blacklist { get; set; }
        public int? Unregistered { get; set; }
    }
}
