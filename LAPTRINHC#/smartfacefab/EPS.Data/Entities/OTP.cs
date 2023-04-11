using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class OTP
    {
        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public Nullable<DateTime> RequestTime { get; set; }

        public Nullable<DateTime> ExpirationTime { get; set; }

        [StringLength(6)]
        public string OTPCode { get; set; }
    }
}
