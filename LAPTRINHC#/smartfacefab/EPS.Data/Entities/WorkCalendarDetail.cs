using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class WorkCalendarDetail
    {
        public int ID { get; set; }
        public Nullable<int> WorkCalendarId { get; set; }
        public string Name { get; set; }
        public Nullable<int> IsWorkDay { get; set; }

        public Nullable<double> Number { get; set; }

        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> DayOfWeek { get; set; }

        [ForeignKey("WorkCalendarId")]
        public virtual WorkCalendar WorkCalendar { get; set; }

    }
}
