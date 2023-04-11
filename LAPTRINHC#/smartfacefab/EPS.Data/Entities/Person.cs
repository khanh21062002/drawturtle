using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Person
    {
        [Key]
        public Guid PersonId { get; set; }
        public int CompId { get; set; }

        public Nullable<int> DeptId { get; set; }
        public string PersonCode { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string JobDuties { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public Nullable<int> Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Religion { get; set; }
        public string Ethenic { get; set; }
        public string Nation { get; set; }
        public Nullable<int> Marital { get; set; }
        public string HomeAddress { get; set; }
        public string RegisterBy { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string AvartarPath { get; set; }
        public string File1 { get; set; }
        public string AvartarPath1 { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> PersonType { get; set; }
        public Nullable<int> Vaccine { get; set; }
        public Nullable<int> CompType { get; set; }
        public string Passport { get; set; }

        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }
    }
}
