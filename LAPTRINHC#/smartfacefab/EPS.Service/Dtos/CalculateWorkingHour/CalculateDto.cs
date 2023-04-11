using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.CalculateWorkingHour
{
    public class CalculateDto
    {
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public int Type { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<Guid> PersonId { get; set; }

    }
}
