using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class ImageSearch2Dto
    {
        public Guid personId { get; set; }
        public string personCode { get; set; }
        public string personName { get; set; }
        //public string face_data { get; set; }
        public double scoreMatching { get; set; }
    }
}
