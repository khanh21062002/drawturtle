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
        public Nullable<int> Status { get; set; }
        //public Nullable<int> Id { get; set; }
       
        public Nullable<bool> IsDelete { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public int? UnitId { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }

        [ForeignKey("UnitId")]
        [InverseProperty("Roles")]
        public virtual Unit UNIT { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

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
