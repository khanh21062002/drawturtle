using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Models
{
    public class ApiResponseDto
    {
        public string HttpStatus { get; set; }

        public string ApiStatus { get; set; }
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }
}
