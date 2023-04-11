using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.WorkCalendar
{
    public class WCalendarDetailDetailDto
    {
        public int ID { get; set; }
        public Nullable<int> WorkCalendarId { get; set; }
        public string Name { get; set; }
        public Nullable<int> IsWorkDay { get; set; }

        public Nullable<double> Number { get; set; }

        public Nullable<bool> IsDelete { get; set; }

        public Nullable<int> DayOfWeek { get; set; }

    }
}
