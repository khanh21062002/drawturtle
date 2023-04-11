using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class v_Customer
    {
        [Key]
        public Guid PersonId { get; set; }
        public string FullName { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string File1 { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> PersonType { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> NumberOfTimes { get; set; }
        public string HomeAddress { get; set; }
        public Nullable<int> CompType { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
    }
}
