using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class PersonDetailDto
    {
        public Guid PersonId { get; set; }
        public string FullName { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> Status { get; set; }
        public string StatusName { get; set; }
        public string Position { get; set; }
        public string JobDuties { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public Nullable<int> Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AvartarPath { get; set; }
        public string AvartarPath1 { get; set; }
        public string PersonCode { get; set; }
        public IFormFile File { get; set; }
        public string GenderName { get; set; }
        public string file1 { get; set; }
        public string Card { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> PersonType { get; set; }
        public string PersonTypeName { get; set; }
        //public List<int> ListPersonGroup { get; set; }
        public Nullable<DateTime> RegisterTime { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> Vaccine { get; set; }
        public string VaccineStr { get; set; }
        public Nullable<int> CompType { get; set; }
        public string GradeName { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public string FromDateStr { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public string ToDateStr { get; set; }
        public string HomeAddress { get; set; }
        public string DobStr { get; set; }
    }
}
