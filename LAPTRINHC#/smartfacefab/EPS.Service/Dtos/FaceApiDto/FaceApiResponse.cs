using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiResponse
    {
        public int person_id { get; set; }
        public string person_code { get; set; }
        public string person_name { get; set; }
    }
}
