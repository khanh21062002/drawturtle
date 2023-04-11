using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Persons
{
    public class PersonsDeleteDto
    {
        public Guid PersonId { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
