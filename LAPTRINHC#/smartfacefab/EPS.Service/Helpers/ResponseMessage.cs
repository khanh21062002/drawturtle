using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Helpers
{
    public class ResponseMessage
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
