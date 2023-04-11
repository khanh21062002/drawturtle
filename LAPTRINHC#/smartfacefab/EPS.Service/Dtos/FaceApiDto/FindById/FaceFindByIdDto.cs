using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceFindByIdDto
    {
        public Nullable<int> collection_id { get; set; }
        public Nullable<Guid> person_id { get; set; }
    }
}
