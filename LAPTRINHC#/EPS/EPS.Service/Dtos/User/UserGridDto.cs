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
        public string[] Roles { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? DeletedUserId { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsDelete { get; set; }
    }
}
