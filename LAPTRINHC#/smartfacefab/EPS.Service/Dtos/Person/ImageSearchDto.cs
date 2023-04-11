using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class ImageSearchDto
    {
        public ImageSearch1Dto input_data { get; set; }
        public List<ImageSearch2Dto> result { get; set; }
    }
}
