using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Event
{
    public class CustomerEventGridDto
    {
        public int Id { get; set; }
        public Nullable<System.Guid> EventId { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.DateTime> AccessDate { get; set; }
        public Nullable<System.DateTime> AccessTime { get; set; }
        public Nullable<int> CompId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string FacePath { get; set; }
        public Nullable<int> Gender { get; set; }
        public string HomeAddress { get; set; }
        public string GenderStr { get; set; }
        public string CompanyName { get; set; }
        public string AccessDateDayFormat { get; set; }
    }
}
