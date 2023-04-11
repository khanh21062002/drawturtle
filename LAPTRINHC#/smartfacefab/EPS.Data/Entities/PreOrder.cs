using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class PreOrder
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DateOrder { get; set; }
        public DateTime? TimeOrder { get; set; }
        public Guid? IdGuest { get; set; }
        public string PhoneNumber { get; set; }
        public int? AmountPeople { get; set; }
        public string RequestOrder { get; set; }
        public string AnotherOrder { get; set; }
        public decimal? DownPayment { get; set; }
        public int? CompId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DateCreate { get; set; }

        [ForeignKey("IdGuest")]
        public virtual Person Person { get; set; }
    }
}
