using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Machine
{
    public class MachineSearchDto
    {
        public DateTime timestamp { get; set; }
        public string status { get; set; }
        public MachineInformationDto data { get; set; }
    }
}
