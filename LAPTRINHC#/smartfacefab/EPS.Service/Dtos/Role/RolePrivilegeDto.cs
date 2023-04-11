using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Role
{
    public class RolePrivilegeDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string PrivilegeId { get; set; }
        public string PrivilegeName { get; set; }
    }
}
