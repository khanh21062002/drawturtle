using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class v_CustomerEvent
    {
        [Key]
        public int Id { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public Nullable<int> CompId { get; set; }
        public string FullName { get; set; }
        public string FacePath { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> Gender { get; set; }
        public string HomeAddress { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }

    }
}
