using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Areas
{
    public class AreasCreateDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? CompId { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public bool? IsDelete { get; set; }
    }
}
