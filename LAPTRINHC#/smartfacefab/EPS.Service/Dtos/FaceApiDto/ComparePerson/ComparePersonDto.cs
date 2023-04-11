using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.ComparePerson
{
    public class ComparePersonDto
    {
        public Nullable<Guid> person_id { get; set; }
        public string img_url { get; set; }
        public string img_base64 { get; set; }
    }
}
