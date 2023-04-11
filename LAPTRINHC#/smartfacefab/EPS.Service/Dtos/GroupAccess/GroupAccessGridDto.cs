using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.GroupAccess
{
    public class GroupAccessGridDto
    {
        public string Id { get; set; }
        public Nullable<int> TimeSegId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<int> GroupId { get; set; }

        public string Machine { get; set; }
        public string Group { get; set; }
        public string AccessTimeSeg { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Company { get; set; }
        public Nullable<int> Status { get; set; }
        public string StatusName { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string Department { get; set; }
        public string StrDeviceId { get; set; }
    }
}
