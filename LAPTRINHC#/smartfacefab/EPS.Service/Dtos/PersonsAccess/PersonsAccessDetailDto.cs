using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PersonsAccess
{
    public class PersonsAccessDetailDto
    {
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
