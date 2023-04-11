using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.OverTimeHours
{
    public class OverTimeHoursUpdateDto
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public Nullable<DateTime> Day { get; set; }
        public Nullable<DateTime> TimeFrom { get; set; }
        public Nullable<DateTime> TimeTo { get; set; }
        public Nullable<double> BreakTime { get; set; }
        public Nullable<double> TotalReal { get; set; }
        public Nullable<double> TotalRegister { get; set; }
        public Nullable<int> WorkingShiftId { get; set; }
        public Nullable<double> Number { get; set; }
        public Nullable<double> Total { get; set; }
        public string Note { get; set; }
        public Nullable<int> Type { get; set; }
        public string NoteDev { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string NumberPartion { get; set; }
    }
}
