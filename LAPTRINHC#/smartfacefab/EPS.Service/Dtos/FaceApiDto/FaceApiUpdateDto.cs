using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiUpdateDto
    {
        public Nullable<int> collection_id { get; set; }
        public Nullable<Guid> person_id { get; set; }
        public string person_code { get; set; }
        public string person_name { get; set; }
        public string img_url { get; set; }
        public string img_base64 { get; set; }
    }
}
