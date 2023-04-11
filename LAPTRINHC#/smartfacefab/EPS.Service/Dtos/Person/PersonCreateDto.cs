using EPS.Service.Dtos.Face;
using EPS.Service.Dtos.PersonGroup;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class PersonCreateDto
    {
        public Guid PersonId { get; set; }
        //[Required]
        public string FullName { get; set; }
        [Required]
        public Nullable<int> CompId { get; set; }
        //[Required]
        public Nullable<int> DeptId { get; set; }
        [Required]
        public Nullable<int> Status { get; set; }
        public string Position { get; set; }
        public string JobDuties { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public Nullable<int> Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Card { get; set; }
        public string Email { get; set; }
        //[Required]
        public string PersonCode { get; set; }
        public string File1 { get; set; }

        public List<FaceCreateDto> ListFace { get; set; }
        //[Required]
        //public Nullable<int> GroupId { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> PersonType { get; set; }
        public Nullable<int> Vaccine { get; set; }
        public Nullable<int> CompType { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public string FileData { get; set; }
        public string Passport { get; set; }
        public string HomeAddress { get; set; }
        public string FaceFeature { get; set; }
        public string RegisterBy { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public Nullable<int> WearMask { get; set; }
        public string ImageBase64 { get; set; }
    }
}
