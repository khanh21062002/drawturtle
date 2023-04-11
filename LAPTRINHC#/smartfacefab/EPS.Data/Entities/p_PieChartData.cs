using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class p_PieChartData
    {
        public int ID { get; set; }
        public int TotalLate { get; set; }
        public int TotalNotLate { get; set; }
        public int TotalNotCheck { get; set; }
    }
}
