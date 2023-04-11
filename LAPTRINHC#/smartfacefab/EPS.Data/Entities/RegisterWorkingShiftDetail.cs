using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class RegisterWorkingShiftDetail
    {
        public int ID { get; set; }
        public Nullable<int> RegisterWorkingShiftId { get; set; }
        public Nullable<int> WorkingShiftId { get; set; }
        public Nullable<int> Monday { get; set; }
        public Nullable<int> TueDay { get; set; }
        public Nullable<int> WedDay { get; set; }
        public Nullable<int> ThuDay { get; set; }
        public Nullable<int> FriDay { get; set; }
        public Nullable<int> SatDay { get; set; }
        public Nullable<int> SunDay { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        [ForeignKey("RegisterWorkingShiftId")]
        public virtual RegisterWorkingShift RegisterWorkingShift { get; set; }
        [ForeignKey("WorkingShiftId")]
        public virtual WorkingShiftTimes WorkingShiftTimes { get; set; }
    }
}
