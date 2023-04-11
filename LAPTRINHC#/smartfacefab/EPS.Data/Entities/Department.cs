using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ComId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int Status { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }

        public Nullable<int> Type { get; set; }

        public Nullable<int> GradesId { get; set; }
        public string HiddenParentField { get; set; }
        public string TreeLevel { get; set; }

        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        //[ForeignKey("ParentId")]
        //public virtual Department Department1 { get; set; }

        [ForeignKey("ParentId")]
        public virtual Department Parent { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        //public virtual ICollection<User> Users { get; set; } ????
        //public virtual ICollection<Role> Roles { get; set; } ????
        //[InverseProperty("Department")]
        //public virtual ICollection<DepartmentAncestor> Ancestors { get; set; }
        //[InverseProperty("Ancestor")]
        //public virtual ICollection<DepartmentAncestor> Descendants { get; set; }
        [ForeignKey("GradesId")]
        public virtual Grades Grades { get; set; }

    }
}
