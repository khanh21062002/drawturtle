using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class NotificationSystem
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string NoContent { get; set; }
        public Nullable<bool> Readed { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
    }
}
