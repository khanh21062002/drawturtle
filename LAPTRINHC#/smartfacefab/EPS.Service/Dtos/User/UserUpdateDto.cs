using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.User
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> UnitId { get; set; }
        public List<int> RoleIds { get; set; }
        public string NewPassword { get; set; }
        public string exPass { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CompId { get; set; }
        public bool IsDelete { get; set; }
        public string Username { get; set; }

    }
}
