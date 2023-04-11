using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.PersonGroup
{
    public class PersonGroupGridDto
    {
        public Guid PersonId { get; set; }
        public int PersonGroupId { get; set; }

        public int Status { get; set; }


    }
}
