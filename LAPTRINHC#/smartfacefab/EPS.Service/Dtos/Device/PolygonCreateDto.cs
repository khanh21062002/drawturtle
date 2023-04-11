using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class PolygonCreateDto
    {
        public int? Id { get; set; }
        public string CanvasPoitArray { get; set; }
        public int? CamId { get; set; }
        public Double? CanvasPoitArrayWieght { get; set; }
        public Double? CanvasPoitArrayHight { get; set; }
        public int? Type { get; set; }



    }
}
