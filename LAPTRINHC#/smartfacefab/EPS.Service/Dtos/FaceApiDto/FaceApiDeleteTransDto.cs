using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiDeleteTransDto
    {
        //public Nullable<int> status { get; set; }
        //public string message { get; set; }
        public Guid person_id { get; set; }
        public string person_code { get; set; }
        public string person_name { get; set; }
    }
}
