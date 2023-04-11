using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class PersonsAccess
    {
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
