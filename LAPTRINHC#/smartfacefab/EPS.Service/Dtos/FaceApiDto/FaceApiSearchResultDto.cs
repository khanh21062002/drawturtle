using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiSearchResultDto
    {
        public Guid person_id { get; set; }
        public string person_code { get; set; }
        public string person_name { get; set; }
        public string passpord { get; set; }
        public string phonenumber { get; set; }
        public Nullable<DateTime> birthday { get; set; }
        public Nullable<float> similary { get; set; }
        public string face_data { get; set; }
    }
}
