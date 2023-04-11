using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Service.Dtos
{
    public class PCCovidKBCN
    {
        public string identify { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string town { get; set; }
        public string address { get; set; }

    }
}
