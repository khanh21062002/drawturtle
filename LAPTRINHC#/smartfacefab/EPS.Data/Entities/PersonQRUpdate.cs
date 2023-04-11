using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class PersonQRUpdate
    {
        [Key]
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public Nullable<DateTime> DateUpdate { get; set; }
        public string ImageUpdate { get; set; }
        public Nullable<int> Status { get; set; }
        public string Note { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}
