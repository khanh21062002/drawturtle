using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.DayOff
{
    public class DayOffGridDto
    {
        public int ID { get; set; }
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonCode { get; set; }
        public string CompName { get; set; }
        public string DeptName { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public string FormatDateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public string FormatDateTo { get; set; }
        public Nullable<Double> TotalDay { get; set; }
        public Nullable<bool> IsHalfDay { get; set; }
        public string Reason { get; set; }
        public Nullable<bool> Salary { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<bool> IsOutDate { get; set; }
        public string HiddenParentField { get; set; }

    }
}
