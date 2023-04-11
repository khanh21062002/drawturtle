using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class CustomerEvent
    {
        [Key]
        public int Id { get; set; }
        public Nullable<System.Guid> EventId { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public Nullable<int> CompId { get; set; }
        
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }

    }
}
