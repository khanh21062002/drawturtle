using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.RegisterWorkingShift
{
    public class RegisterWorkingShiftGridDto
    {
        public int ID { get; set; }
        public Nullable<int> Type { get; set; }
        public string TypeName { get; set; }
        public Nullable<int> CompId { get; set; }
        public string CompName { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string HiddenParentField { get; set; }
        public string DeptName { get; set; }
        public Nullable<int> TimeCycle { get; set; }
        public Nullable<Guid> PersonId { get; set; }

        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string Note { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public string DateFromFormat { get; set; }
        public Nullable<DateTime> DateTo { get; set; }

        public string DateToFormat { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
