using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomeStatisticUnregistered
    {
        public int Id { get; set; }
        public DateTime? DayInOut { get; set; }
        public int? NmbUnregistered { get; set; }
    }
}
