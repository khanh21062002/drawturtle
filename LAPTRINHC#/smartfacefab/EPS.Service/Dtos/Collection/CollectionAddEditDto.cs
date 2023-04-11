using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Collection
{
    public class CollectionAddEditDto
    {
        public int collection_id { get; set; }
        //public int status { get; set; }
        //public string message { get; set; }
        public string collection_name { get; set; }
        public string collection_code { get; set; }
    }
}
