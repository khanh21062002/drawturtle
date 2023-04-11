using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.OverTime
{
    public class OverTimeGridDto
    {
        public int ID { get; set; }
        public Nullable<int> DEPARTMENT_ID { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public Nullable<Guid> PERSON_ID { get; set; }
        public string PERSON_NAME { get; set; }
        public string HiddenParentField { get; set; }

        public string PERSON_CODE { get; set; }
        public Nullable<System.DateTime> DATE_OT { get; set; }
        public string FORMAT_DATE_OT { get; set; }
        public Nullable<TimeSpan> OT_IN { get; set; }
        public string FORMAT_OT_IN { get; set; }
        public Nullable<TimeSpan> OT_OUT { get; set; }
        public string FORMAT_OT_OUT { get; set; }
        public Nullable<double> SUM_OT { get; set; }
        public Nullable<double> BREAK_TIME { get; set; }
        public string FORMAT_SUM_OT { get; set; }
        public Nullable<int> SHIFT_ID { get; set; }
        public string SHIFT_NAME { get; set; }
        public Nullable<double> TIME_OT { get; set; }
        public string TOTAL_TIME_OT { get; set; }
        public string NOTE { get; set; }
        public Nullable<int> COMP_ID { get; set; }
        public double COEFFICIENT { get; set; }
        public double COEFFICIENT_DATE { get; set; }
        public string COEFFICIENT_VIEW { get; set; }
    }
}
