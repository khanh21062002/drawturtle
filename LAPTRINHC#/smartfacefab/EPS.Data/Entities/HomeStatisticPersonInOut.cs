using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomeStatisticPersonInOut
    {
        public int Id { get; set; }
        public DateTime? DayInOut { get; set; }
        public int? NmbEmployee { get; set; }
        public int? NmbAll { get; set; }
    }
}
