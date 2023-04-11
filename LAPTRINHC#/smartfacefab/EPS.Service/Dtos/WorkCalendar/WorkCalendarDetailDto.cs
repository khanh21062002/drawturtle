using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.WorkCalendar
{
    public class WorkCalendarDetailDto
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public string CompName { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public string DateFromFormat { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public string DateToFormat { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
