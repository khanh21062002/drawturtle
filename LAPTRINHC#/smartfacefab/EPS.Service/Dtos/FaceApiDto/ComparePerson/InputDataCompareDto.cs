using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.ComparePerson
{
    public class InputDataCompareDto
    {
        public Nullable<int> collection_id { get; set; }
        public Nullable<Guid> person_id { get; set; }
    }
}
