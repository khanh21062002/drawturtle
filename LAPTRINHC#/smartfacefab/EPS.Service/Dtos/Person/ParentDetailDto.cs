using EPS.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class ParentDetailDto
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
        public string AvatarPath { get; set; }
        public byte[] AvatarPath1 { get; set; }
        public string PersonCode { get; set; }
        public IFormFile File { get; set; }
        public string GenderName { get; set; }
        public string file1 { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> PersonType { get; set; }
        public List<int> ListPersonGroup { get; set; }

        public Nullable<int> Vaccine { get; set; }

        public string VaccineStr { get; set; }

        public Nullable<int> CompType { get; set; }

        public string GradeName { get; set; }


        public List<StudentParents> Students { get; set; }

        public List<StudentParentsDetailDto> ListStudentDetails { get; set; }

    }
}
