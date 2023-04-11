using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.WorkCalendar
{
    public class WorkCalendarCreateDto
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<WCalendarDetailCreateDto> ListDetails { get; set; }
    }
}
