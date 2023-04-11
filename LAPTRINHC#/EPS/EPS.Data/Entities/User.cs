using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class User : IdentityUser<int>, IDeleteInfo<User, int>, ICascadeDelete
    {
        public User()
        {
            UserPrivileges = new HashSet<UserPrivilege>();
            UserRoles = new HashSet<UserRole>();
        }

        [StringLength(250)]
        public string FullName { get; set; }

        public bool IsAdministrator { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Users")]
        public virtual Company Company { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public DateTime? DeletedDate { get; set; }
        public int? DeletedUserId { get; set; }

        public virtual User DeletedUser { get; set; }
        public bool? IsDelete { get; set; }

        public void OnDelete()
        {
            if (UserPrivileges.Count > 0)
            {
                throw new EPSException(EPSExceptionCode.DeleteRecordWithRelatedData, "quyền hạn");
            }
            if (UserRoles.Count > 0)
            {
                throw new EPSException(EPSExceptionCode.DeleteRecordWithRelatedData, "nhóm người dùng");
            }
        }
    }
}
