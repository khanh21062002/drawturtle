using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class ChartDataItem
    {
        public long Id { get; set; }
        public Nullable<long> timeInMilis { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<int> unknown { get; set; }
        public Nullable<int> employee { get; set; }
    }
}
