using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class Holidays
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Year { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string Description { get; set; }
        public Nullable<double> CoEf { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }

    }
}
