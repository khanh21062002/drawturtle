using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Models
{
    public class WSMessageModel
    {
        public int EVENT_ID { get; set; }
        public int DEP_ID { get; set; }
        public string DEVICE_CODE { get; set; }
    }
}
