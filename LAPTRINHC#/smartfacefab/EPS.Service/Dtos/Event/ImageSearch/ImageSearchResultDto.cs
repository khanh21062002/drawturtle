using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Event.ImageSearch
{
    public class ImageSearchResultDto
    {
        public Guid eventId { get; set; }
        public double? score { get; set; }
    }
}
