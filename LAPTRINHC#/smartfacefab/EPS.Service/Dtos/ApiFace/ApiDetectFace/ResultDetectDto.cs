using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.ApiFace.ApiDetectFace
{
    public class ResultDetectDto
    {
        public string status { get; set; }
        public string message { get; set; }
        public ResultDetailDetectDto data { get; set; }
    }
}
