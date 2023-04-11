using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.RegisterWorkingShift
{
    public class RegisterWorkingShiftDetailCreateAndUpdateDto
    {
        public int ID { get; set; }
        public Nullable<int> RegisterWorkingShiftId { get; set; }
        public Nullable<int> WorkingShiftId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Monday { get; set; }
        public Nullable<int> TueDay { get; set; }
        public Nullable<int> WedDay { get; set; }
        public Nullable<int> ThuDay { get; set; }
        public Nullable<int> FriDay { get; set; }
        public Nullable<int> SatDay { get; set; }
        public Nullable<int> SunDay { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
