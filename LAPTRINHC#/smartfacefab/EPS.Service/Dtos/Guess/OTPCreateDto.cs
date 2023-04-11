using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Service.Dtos.Guess
{
    public class OTPCreateDto
    {
        public int ID { get; set; }

        public string PhoneNumber { get; set; }

        public Nullable<DateTime> RequestTime { get; set; }

        public Nullable<DateTime> ExpirationTime { get; set; }

        public string OTPCode { get; set; }
    }
}
