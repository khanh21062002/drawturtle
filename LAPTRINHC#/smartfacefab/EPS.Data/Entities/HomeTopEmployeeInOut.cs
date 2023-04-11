using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomeTopEmployeeInOut
    {
        [Key]
        public Guid PersonId { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public int? NumberInOut { get; set; }
        public string DeptName { get; set; }
    }
}
