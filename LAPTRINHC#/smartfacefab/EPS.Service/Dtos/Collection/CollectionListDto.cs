using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Collection
{
    public class CollectionListDto
    {
        //public int status { get; set; }
        //public string message { get; set; }
        public List<CollectionGridDto> collection { get; set; }
    }
}
