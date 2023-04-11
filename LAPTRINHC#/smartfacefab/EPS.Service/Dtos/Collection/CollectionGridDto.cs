using System;
using System.Collections.Generic;


namespace EPS.Service.Dtos.Collection
{

    public class CollectionGridDto
    {
        public int collection_id { get; set; }
        public string collection_code { get; set; }
        public string collection_name { get; set; }
        [NonSerialized]
        public int? UserId;
    }
}
