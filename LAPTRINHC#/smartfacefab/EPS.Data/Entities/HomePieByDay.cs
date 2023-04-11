using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public class HomePieByDay
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}
