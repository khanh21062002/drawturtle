using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.DayOff
{
    public class DayOffUpdateDto
    {
        public int ID { get; set; }
        public Guid PersonId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Nullable<Double> TotalDay { get; set; }
        public Nullable<bool> IsHalfDay { get; set; }
        public string Reason { get; set; }
        public Nullable<bool> Salary { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        public Nullable<int> DeptId { get; set; }
        public Nullable<int> CompId { get; set; }
    }
}
