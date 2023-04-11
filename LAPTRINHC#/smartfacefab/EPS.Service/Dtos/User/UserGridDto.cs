using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.User
{
    public class UserGridDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string[] Roles { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int? DeletedUserId { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Company { get; set; }
        public Nullable<int> Status { get; set; }
        public string StatusName { get; set; }
        public bool IsDelete { get; set; }

        public string HiddenParentCompanyField { get; set; }
    }
}
