using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Event.ImageSearch
{
    public class ImageSearchEventDto<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ImageSearchResultDto> data { get; set; }
    }
}
