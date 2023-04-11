using System;
using System.ComponentModel.DataAnnotations;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiCreateDto
    {
        public Nullable<int> collection_id { get; set; }
        public string collection_code { get; set; }
        [Required]
        [MaxLength(50)]
        public string person_code { get; set; }
        [Required]
        [MaxLength(150)]
        public string person_name { get; set; }
        public string img_url { get; set; }
        public string img_base64 { get; set; }
    }
}
