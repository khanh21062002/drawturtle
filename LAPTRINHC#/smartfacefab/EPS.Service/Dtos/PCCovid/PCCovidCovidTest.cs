using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Service.Dtos
{
    public class PCCovidCovidTest
    {
        public string state { get; set; }
        public Nullable<int> order { get; set; }
        public Nullable<int> lastTime { get; set; }
        public Nullable<int> expiredTime { get; set; }
        public Nullable<int> result { get; set; }
        public Nullable<int> techTest { get; set; }
        public string locationTest { get; set; }
    }
}
