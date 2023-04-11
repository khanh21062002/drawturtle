using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class DayOff
    {
        public int ID { get; set; }
        public Guid PersonId { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<Double> TotalDay { get; set; }
        public Nullable<bool> IsHalfDay { get; set; }
        public string Reason { get; set; }
        public Nullable<bool> Salary { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> CompId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
    }
}
