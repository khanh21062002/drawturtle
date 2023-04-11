using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiSearchDto
    {
        public Nullable<int> collection_id { get; set; }
        public Nullable<int> dept_id { get; set; }
        public Nullable<float> threshold { get; set; }
        public Nullable<float> quality { get; set; }
        public Nullable<int> num_result { get; set; }
        public string img_url { get; set; }
        public string img_base64 { get; set; }
    }
}
