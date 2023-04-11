using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.RegisterWorkingShift
{
    public class RegisterWorkingShiftUpdateDto
    {
        public int ID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> TimeCycle { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public string Note { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        public List<RegisterWorkingShiftDetailCreateAndUpdateDto> listDetails { get; set; }
    }
}
