using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Machine
{
    public class MachineInformationDto
    {
        public string imei { get; set; }
        public string serial { get; set; }
        public string macAddress { get; set; }
        public string model { get; set; }
        public string versionName { get; set; }
        public string versionCode { get; set; }
        public decimal cpu { get; set; }
        public decimal ram { get; set; }
        public decimal temperature { get; set; }
        public string storageAvl { get; set; }
        public string storageSize { get; set; }
        public string osTime { get; set; }
    }
}
