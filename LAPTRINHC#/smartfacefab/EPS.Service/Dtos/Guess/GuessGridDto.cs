using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Guess
{
    public class GuessGridDto
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime StartTime { get; set; }

        public string StrStartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string StrEndTime { get; set; }

        public string Email { get; set; }

        public string JobDuties { get; set; }

        public Nullable<int> Gender { get; set; }

        public string ImageUrl { get; set; }

        public Nullable<bool> IsTravel { get; set; }

        public string TravelDec { get; set; }

        public Nullable<bool> IsFever { get; set; }

        public string FeverDec { get; set; }

        public Nullable<bool> NCovid_NN { get; set; }

        public Nullable<bool> NCovid_NCB { get; set; }

        public Nullable<bool> NCovid_BH { get; set; }

        public string Passport { get; set; }

        public Nullable<int> ComId { get; set; }

        public Nullable<int> Approve { get; set; }
        public string FeaturePath { get; set; }
    }
}
