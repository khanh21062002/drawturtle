using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class ImportDto
    {
        public string URL { get; set; }
        public int SumCount { get; set; }
        public int CountSuccess { get; set; }
    }
}
