using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.FindById
{
    public class InputDataDto
    {
        public Nullable<int> collection_id { get; set; }
        public Nullable<Guid> person_id { get; set; }
    }
}
