using EPS.Service.Dtos.Persons;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Collection
{
    public class CollectionSearchResponse
    {
        public int collection_id { get; set; }
        public string collection_code { get; set; }
        public string collection_name { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int count { get; set; }
        public List<PersonDetailDtos> face_data { get; set; }
        
    }
}
