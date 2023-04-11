using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Origin { get; set; }
        [StringLength(50)]
        public string LogLevel { get; set; }
        [StringLength(4000)]
        public string Message { get; set; }
        public string Exception { get; set; }
        [StringLength(250)]
        public string Actor { get; set; }
        [StringLength(250)]
        public string Action { get; set; }
        public string Data { get; set; }
    }
}
