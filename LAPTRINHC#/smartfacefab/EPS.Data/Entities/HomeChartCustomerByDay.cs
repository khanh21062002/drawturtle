using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomeChartCustomerByDay
    {
        public int ID { get; set; }
        public Nullable<DateTime> DayEvent { get; set; }
        public Nullable<int> CountFirst { get; set; }
        public Nullable<int> CountReturn { get; set; }
    }
}
