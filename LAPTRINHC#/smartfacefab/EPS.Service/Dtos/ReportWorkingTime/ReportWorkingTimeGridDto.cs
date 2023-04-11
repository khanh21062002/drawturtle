using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.ReportWorkingTime
{
    public class ReportWorkingTimeGridDto
    {
        public int ID { get; set; }
        public System.Guid PersonId { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> Day { get; set; }
        public string DayFormat { get; set; }
        public Nullable<System.DateTime> StartAccessTime { get; set; }
        public string StartAccessTimeFormat { get; set; }
        public Nullable<System.DateTime> EndAccessTime { get; set; }
        public string EndAccessTimeFormat { get; set; }
        public Nullable<int> DiffStartTime { get; set; }
        public Nullable<int> DiffEndTime { get; set; }
        public string EarlyTime { get; set; }
        public string LateTime { get; set; }
        public Nullable<double> TotalTime { get; set; }
        public string TotalTimeFormat { get; set; }
        public string DeptName { get; set; }
        public string OverTime { get; set; }
        public string DayOfWeekInVietnamese { get; set; }


    }
}
