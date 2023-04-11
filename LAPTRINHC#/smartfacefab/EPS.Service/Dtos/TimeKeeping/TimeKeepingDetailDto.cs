using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.TimeKeeping
{
    public class TimeKeepingDetailDto
    {
        public int ID { get; set; }
        public Nullable<int> COMP_ID { get; set; }
        public string COMP_NAME { get; set; }
        public Nullable<int> DEPT_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public Nullable<Guid> PERSON_ID { get; set; }
        public string PERSON_NAME { get; set; }
        public string PERSON_CODE { get; set; }
        public Nullable<System.DateTime> DATE_WORKING { get; set; }
        public string FORMAT_DATE_WORKING { get; set; }
        public Nullable<int> SHIFT_ID { get; set; }
        public string SHIFT_NAME { get; set; }
        public Nullable<double> WORK_DAY { get; set; }
        public string NOTE { get; set; }
    }
}
