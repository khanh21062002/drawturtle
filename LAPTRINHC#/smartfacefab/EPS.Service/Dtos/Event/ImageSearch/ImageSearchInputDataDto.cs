using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Event.ImageSearch
{
    public class ImageSearchInputDataDto
    {
        public Nullable<int> collection_id { get; set; }
        public double threshold { get; set; }
        public int num_result { get; set; }
        public string img_url { get; set; }
        public string img_base64 { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
