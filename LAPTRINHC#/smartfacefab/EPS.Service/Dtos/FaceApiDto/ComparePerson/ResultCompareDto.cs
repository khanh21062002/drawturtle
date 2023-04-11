using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.ComparePerson
{
    public class ResultCompareDto
    {
        public Nullable<Guid> person_id { get; set; }
        public string person_code { get; set; }
        public string person_name { get; set; }
        public string face_data { get; set; }
        public Nullable<float> similary { get; set; }
    }
}
