using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomeChartDay
    {
        public int Id { get; set; }
        public string Rang { get; set; }
        public Nullable<int> CountFirst { get; set; }
        public Nullable<int> CountReturn { get; set; }
    }
}
