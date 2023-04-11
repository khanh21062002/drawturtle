using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class Role : ICascadeDelete
    {
        public Role()
        {
            RolePrivileges = new HashSet<RolePrivilege>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Roles")]
        public virtual Company COMPANY { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public int? Status { get; set; }
        public void OnDelete()
        {
            if (UserRoles.Count > 0)
            {
                throw new EPSException(EPSExceptionCode.DeleteRecordWithRelatedData, "nhóm người dùng");
            }

            RolePrivileges.Clear();
        }
    }
}
