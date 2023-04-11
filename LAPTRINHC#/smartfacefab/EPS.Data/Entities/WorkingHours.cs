using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class WorkingHours
    {
        public int ID { get; set;}
        public Nullable<int> CompId { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public Nullable<int> WorkingShiftId { get; set; }
        public Nullable<DateTime> Day { get; set; }
        public Nullable<DateTime> CheckIn { get; set; }
        public Nullable<DateTime> MinCheckIn { get; set; }
        public Nullable<DateTime> CheckOut { get; set; }
        public Nullable<DateTime> MaxCheckOut { get; set; }

        public Nullable<int> Early { get; set; }
        public Nullable<int> Late { get; set; }
        public string Note { get; set; }
        public Nullable<Double> Total { get; set; }
        public Nullable<int> TypeDay { get; set; }
        public string NoteDay { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        [ForeignKey("WorkingShiftId")]
        public virtual WorkingShiftTimes WorkingShiftTimes { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
    }
}
