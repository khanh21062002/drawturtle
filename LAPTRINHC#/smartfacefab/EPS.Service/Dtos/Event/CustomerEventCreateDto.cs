using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Event
{
    public class CustomerEventCreateDto
    {
        public int Id { get; set; }
        public Nullable<System.Guid> EventId { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public Nullable<int> CompId { get; set; }
    }
}
