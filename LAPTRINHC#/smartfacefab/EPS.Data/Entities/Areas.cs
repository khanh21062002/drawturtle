using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public class Areas
    {
        [Key]
        public int? Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public int? CompId { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public bool? IsDelete { get; set; }
    }
}
