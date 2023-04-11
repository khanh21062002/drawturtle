using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.OverTime
{
    public class OverTimeUpdateDto
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
    }
}
