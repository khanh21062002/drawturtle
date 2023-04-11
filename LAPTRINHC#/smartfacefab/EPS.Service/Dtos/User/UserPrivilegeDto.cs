using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.User
{
    public class UserPrivilegeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PrivilegeId { get; set; }
        public string PrivilegeName { get; set; }
    }
}
