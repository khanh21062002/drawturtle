using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto.FindById
{
    public class ResultDto
    {
        public Nullable<Guid> person_id { get; set; }
        public string person_code { get; set; }
        public string person_name { get; set; }
        public string face_data { get; set; }
    }
}
