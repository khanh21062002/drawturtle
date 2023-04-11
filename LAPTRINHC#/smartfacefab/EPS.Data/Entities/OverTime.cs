using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class OverTime
    {
        public int ID { get; set; }
        public Nullable<int> DEPARTMENT_ID { get; set; }
        public Nullable<Guid> PERSON_ID { get; set; }
        public Nullable<System.DateTime> DATE_OT { get; set; }
        public Nullable<TimeSpan> OT_IN { get; set; }
        public Nullable<TimeSpan> OT_OUT { get; set; }
        public Nullable<double> SUM_OT { get; set; }
        public Nullable<double> BREAK_TIME { get; set; }
        public Nullable<int> SHIFT_ID { get; set; }
        public Nullable<double> TIME_OT { get; set; }
        public string NOTE { get; set; }
        public Nullable<int> COMP_ID { get; set; }

        [ForeignKey("PERSON_ID")]
        public virtual Person Person { get; set; }

        [ForeignKey("SHIFT_ID")]
        public virtual WorkingShiftTimes WorkingShiftTimes { get; set; }

        [ForeignKey("DEPARTMENT_ID")]
        public virtual Department Department { get; set; }

        [ForeignKey("COMP_ID")]
        public virtual Company Company { get; set; }
    }
}
