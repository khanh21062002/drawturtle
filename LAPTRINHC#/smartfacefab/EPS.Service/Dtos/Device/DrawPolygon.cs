using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class DrawPolygon
    {
        public int? id { get; set; }
        //public int? compId { get; set; }
        //public string rstplink { get; set; }

        //public string featuresName { get; set; }

        //public string featuresCode { get; set; }
        //public int isdelete { get; set; }
        public  List<active_area> active_area { get; set; }
        //public string deviceCode { get; set; }
        //public string deviceName { get; set; }



    }
}
