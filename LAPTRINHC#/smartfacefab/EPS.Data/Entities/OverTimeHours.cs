using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class OverTimeHours
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public Nullable<DateTime> Day { get; set; }
        public Nullable<DateTime> TimeFrom { get; set; }
        public Nullable<DateTime> TimeTo { get; set; }
        public Nullable<double> BreakTime { get; set; }
        public Nullable<double> TotalReal { get; set; }
        public Nullable<double> TotalRegister { get; set; }
        public Nullable<int> WorkingShiftId { get; set; }
        public Nullable<double> Number { get; set; }
        public string NumberPartion { get; set; }
        public Nullable<double> Total { get; set; }
        public string Note { get; set; }
        public Nullable<int> Type { get; set; }
        public string NoteDev { get; set; }
        public Nullable<bool> IsDelete { get; set; }
       

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }
        [ForeignKey("WorkingShiftId")]
        public virtual WorkingShiftTimes WorkingShiftTimes { get; set; }

    }
}
