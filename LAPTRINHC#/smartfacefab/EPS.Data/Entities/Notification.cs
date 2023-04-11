using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class Notification
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<double> ScoreMatch { get; set; }
        public Nullable<int> TimeSchedule { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; }
    }
}
