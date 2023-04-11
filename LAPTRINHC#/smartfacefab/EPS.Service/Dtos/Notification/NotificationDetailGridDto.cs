using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Notification
{
    public class NotificationDetailGridDto
    {
        public int ID { get; set; }
        public Nullable<int> NotiId { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
