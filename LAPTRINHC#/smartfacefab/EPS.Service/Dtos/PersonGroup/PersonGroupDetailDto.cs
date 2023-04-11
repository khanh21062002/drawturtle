using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PersonGroup
{
    public class PersonGroupDetailDto
    {
        public Guid PersonId { get; set; }
        public List<int> GroupId { get; set; }
        public Nullable<int> Status { get; set; }

    }
}
