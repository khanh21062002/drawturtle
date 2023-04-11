using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class UserPrivilege
    {
        public int UserId { get; set; }
        [StringLength(50)]
        public string PrivilegeId { get; set; }

        [ForeignKey("PrivilegeId")]
        [InverseProperty("UserPrivileges")]
        public virtual Privilege Privilege { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserPrivileges")]
        public virtual User User { get; set; }
    }
}
