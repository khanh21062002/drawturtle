using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class TimeKeeping
    {
        public int ID { get; set; }
        public Nullable<int> COMP_ID { get; set; }
        public Nullable<int> DEPT_ID { get; set; }
        public Nullable<Guid> PERSON_ID { get; set; }
        public Nullable<System.DateTime> DATE_WORKING { get; set; }
        public Nullable<int> SHIFT_ID { get; set; }
        public Nullable<double> WORK_DAY { get; set; }
        public string NOTE { get; set; }

        [ForeignKey("PERSON_ID")]
        public virtual Person Person { get; set; }

        [ForeignKey("SHIFT_ID")]
        public virtual WorkingShiftTimes WorkingShiftTimes { get; set; }

        [ForeignKey("DEPT_ID")]
        public virtual Department Department { get; set; }

        [ForeignKey("COMP_ID")]
        public virtual Company Company { get; set; }
    }
}
