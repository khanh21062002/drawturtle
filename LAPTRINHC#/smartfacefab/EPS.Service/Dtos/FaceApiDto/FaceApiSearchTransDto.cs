using EPS.Service.Dtos.Persons;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.FaceApiDto
{
    public class FaceApiSearchTransDto
    {
        //public Nullable<int> status { get; set; }
        //public string message { get; set; }
        public FaceApiSearchTransDataDto input_data { get; set; }
        public List<FaceApiSearchResultDto> result { get; set; }
    }
}
