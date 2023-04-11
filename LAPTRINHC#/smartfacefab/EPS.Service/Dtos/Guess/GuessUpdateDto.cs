using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Guess
{
    public class GuessUpdateDto
    {
        public Nullable<Guid> PersonId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<int> LstMachineId { get; set; }
        public Nullable<int> Approve { get; set; }
        //public DateTime StartTime { get; set; }
        //public DateTime EndTime { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
    }
}
