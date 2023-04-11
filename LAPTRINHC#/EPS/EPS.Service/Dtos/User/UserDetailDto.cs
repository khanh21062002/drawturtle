using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.User
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CompanyId { get; set; }
        public string NewPassword { get; set; }
        public string PasswordConfirmation { get; set; }
        public string CompanyName { get; set; }
        public List<int> RoleIds { get; set; }
        public List<string> RoleNames { get; set; }
        public bool isAdminByCompany { get; set; }

        public Nullable<int> Status { get; set; }
        public string StatusName { get; set; }
        public bool? IsDelete { get; set; }
    }
}
