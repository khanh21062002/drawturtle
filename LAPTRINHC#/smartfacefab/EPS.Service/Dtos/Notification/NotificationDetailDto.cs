using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Notification
{
    public class NotificationDetailDto
    {
        public int ID { get; set; }
        public Nullable<int> CompId { get; set; }
        public string CompName { get; set; }
        public Nullable<int> Type { get; set; }
        public string TypeName { get; set; }
        public Nullable<int> MachineId { get; set; }
        public string MachineName { get; set; }
        public Nullable<double> ScoreMatch { get; set; }
        public Nullable<int> TimeSchedule { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<NotificationDetailCreateDto> NotificationDetails { get; set; }
    }
}
