using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class View_ListPerson
    {
        [Key]
        public Guid PersonId { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string PersonCode { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string JobDuties { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public Nullable<int> Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Education { get; set; }
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

        public string ComName { get; set; }
        public string DepName { get; set; }
        public string FeaturePath { get; set; }
        public Nullable<int> NumberOfTimes { get; set; }
        //[ForeignKey("PersonId")]
        //public virtual View_Person_NumberOfTimes View_Person_NumberOfTimes { get; set; }
    }
}
