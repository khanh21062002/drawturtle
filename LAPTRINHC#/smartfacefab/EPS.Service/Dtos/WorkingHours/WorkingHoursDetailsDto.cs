﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.WorkingHours
{
    public class WorkingHoursDetailsDto
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string CompName { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public string DeptName { get; set; }
        public string PersonName { get; set; }
        public string PersonCode { get; set; }
        public Nullable<int> WorkingShiftId { get; set; }
        public string WorkShiftName { get; set; }
        public Nullable<DateTime> Day { get; set; }
        public string DayFormat { get; set; }
        public string DayOfWeekInVietnamese { get; set; }
        public Nullable<DateTime> CheckIn { get; set; }
        public string CheckInFormat { get; set; }
        public Nullable<DateTime> MinCheckIn { get; set; }
        public Nullable<DateTime> CheckOut { get; set; }
        public string CheckOutFormat { get; set; }
        public Nullable<DateTime> MaxCheckOut { get; set; }
        public Nullable<int> Early { get; set; }
        public string EarlyStr { get; set; }
        public Nullable<int> Late { get; set; }
        public string LateStr { get; set; }
        public string Note { get; set; }
        public Nullable<Double> Total { get; set; }
        public string TotalFormat { get; set; }
        public Nullable<int> TypeDay { get; set; }
        public string NoteDay { get; set; }
    }
}
