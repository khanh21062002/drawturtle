using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Person
{
    public class StudentParentsDetailDto
    {
        public int Id { get; set; }
        public Nullable<Guid> StudentId { get; set; }
        public Nullable<Guid> ParentId { get; set; }
        public string Note { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public Nullable<Boolean> IsDelete { get; set; }

        public Guid PersonId { get; set; }
        public string FullName { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> PersonType { get; set; }
        public string StatusName { get; set; }
        //public int GroupId { get; set; }

        public Nullable<int> Gender { get; set; }

        public Nullable<DateTime> BirthDay { get; set; }

        public string DobStr { get; set; }
        public string PersonCode { get; set; }
        public string Machine { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string GenderName { get; set; }
        public string File1 { get; set; }

        public Nullable<int> Vaccine { get; set; }
        public string VaccineStr { get; set; }

        public Nullable<int> CompType { get; set; }

        public string ComStr { get; set; }

        public Nullable<int> GradeId { get; set; }

        public string GradeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


    }
}
