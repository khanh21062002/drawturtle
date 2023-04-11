using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace EPS.API.Models
{
    public class WSConnectionModel
    {
        public WebSocket Connection { get; set; }
        public int DEP_ID { get; set; }
        public string DEVICE_CODE { get; set; }
    }
}
