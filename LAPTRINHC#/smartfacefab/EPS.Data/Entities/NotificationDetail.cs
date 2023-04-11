using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class NotificationDetail
    {
        public int ID { get; set; }
        public Nullable<int> NotiId { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        [ForeignKey("NotiId")]
        public virtual Notification Notification { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

    }
}
