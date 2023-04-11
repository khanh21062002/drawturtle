using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class RolePrivilege
    {
        public int RoleId { get; set; }
        [StringLength(50)]
        public string PrivilegeId { get; set; }

        [ForeignKey("PrivilegeId")]
        [InverseProperty("RolePrivileges")]
        public virtual Privilege Privilege { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("RolePrivileges")]
        public virtual Role Role { get; set; }
    }
}
