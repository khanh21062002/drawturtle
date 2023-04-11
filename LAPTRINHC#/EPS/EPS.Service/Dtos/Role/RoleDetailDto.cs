using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Role
{
    public class RoleDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
        public string StatusName { get; set; }
    }
}
