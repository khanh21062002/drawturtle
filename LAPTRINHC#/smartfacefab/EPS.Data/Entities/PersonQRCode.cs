using System;
using System.ComponentModel.DataAnnotations;

namespace EPS.Data.Entities
{
    public class PersonQRCode
    {
        [Key]
        public Guid Id { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CompId { get; set; }
    }
}
