using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PersonGroup
{
    public class PersonGroupCreateDto
    {
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public Nullable<int> GroupId { get; set; }       
        public Nullable<int> Status { get; set; }
        public bool IsDelete { get; set; }

    }
}
