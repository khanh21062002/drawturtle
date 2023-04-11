using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class ImportDto
    {
        public string URL { get; set; }
        public Nullable<int> COUNT { get; set; }
        public Nullable<int> SUMCOUNT { get; set; }
    }
}
