using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string SetFoodId { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }
        public int? CompId { get; set; }
        public bool? IsDelete { get; set; }

        //[ForeignKey("SetFoodId")]
        //public virtual SetFood SetFood { get; set; }
    }
}
