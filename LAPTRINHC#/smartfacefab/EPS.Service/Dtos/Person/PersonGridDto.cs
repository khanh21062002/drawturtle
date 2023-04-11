using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class PersonGridDto
    {
        public Guid PersonId { get; set; }
        public string FullName { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> PersonType { get; set; }
        public string StatusName { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<DateTime> BirthDay { get; set; }
        public string DobStr { get; set; }
        public string Address { get; set; }
        public string PersonCode { get; set; }
        public Nullable<int> NumberOfTimes { get; set; }
        public string HomeAddress { get; set; }
        public string Machine { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string PersonTypeStr { get; set; }
        public string GenderName { get; set; }
        public bool IsDelete { get; set; }
        public string File1 { get; set; }
        public Nullable<int> CompType { get; set; }
        public string ComStr { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string HiddenParentField { get; set; }
        public string FeaturePath { get; set; }
    }
}
