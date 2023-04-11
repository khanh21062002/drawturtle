using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class StudentParentCreateDto
    {
        public int Id { get; set; }
        public Nullable<Guid> StudentId { get; set; }
        public Nullable<Guid> ParentId { get; set; }
        public string Note { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public Nullable<Boolean> IsDelete { get; set; }

    }
}
