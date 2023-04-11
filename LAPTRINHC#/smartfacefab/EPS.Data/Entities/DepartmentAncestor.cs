//using EPS.Utils.Repository.Audit;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace EPS.Data.Entities
//{
//    [Table("DepartmentAncestors")]
//    public partial class DepartmentAncestor
//    {
//        public DepartmentAncestor()
//        {

//        }

//        public int Id { get; set; }
//        //public Nullable<int>  Id { get; set; }
//        public int DepartmentAncestorId { get; set; }
//        [Required]
//        [StringLength(250)]
//        public string Code { get; set; }
//        [StringLength(500)]
//        public string Name { get; set; }

//        [InverseProperty("Ancestors")]
//        [ForeignKey("Id")]
//        public virtual Department Department { get; set; }
//        [InverseProperty("Descendants")]
//        [ForeignKey("DepartmentAncestorId")]
//        public virtual Department Ancestor { get; set; }

//    }
//}
