using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.WorkingShiftTimes
{
    public class WorkingShiftTimesDetailDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> CompId { get; set; }
        public string CompName { get; set; }
        public Nullable<TimeSpan> StartBreak { get; set; }
        public string FormatStartBreak { get; set; }
        public Nullable<TimeSpan> EndBreak { get; set; }
        public string FormatEndBreak { get; set; }
        public Nullable<TimeSpan> StartTime { get; set; }
        public string FormatStartTime { get; set; }
        public Nullable<TimeSpan> EndTime { get; set; }
        public string FormatEndTime { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<double> WorkingShift { get; set; }
        public Nullable<double> OT { get; set; }
        public Nullable<int> LateAccept { get; set; }
        public Nullable<int> EarlyAccept { get; set; }
        public Nullable<TimeSpan> StartCheckIn { get; set; }
        public string FormatStartCheckIn { get; set; }
        public Nullable<TimeSpan> EndCheckIn { get; set; }
        public string FormatEndCheckIn { get; set; }
        public Nullable<TimeSpan> StartCheckOut { get; set; }
        public string FormatStartCheckOut { get; set; }
        public Nullable<TimeSpan> EndCheckOut { get; set; }
        public string FormatEndCheckOut { get; set; }
        public Nullable<int> NotCheckIn { get; set; }
        public Nullable<int> NotCheckOut { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<TimeSpan> StartCheckInOverTime { get; set; }
        public string FormatStartCheckInOverTime { get; set; }
        public Nullable<TimeSpan> EndCheckOutOverTime { get; set; }
        public string FormatEndCheckOutOverTime { get; set; }
    }
}
