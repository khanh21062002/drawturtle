using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public class Polygon
    {
        [Key]
        public int? Id { get; set; }
        public string CanvasPoitArray { get; set; }
        public int? CamId { get; set; }
        public int? Type { get; set; }

    }
}
