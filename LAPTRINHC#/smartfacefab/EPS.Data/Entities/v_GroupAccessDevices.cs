using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class v_GroupAccessDevices
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string TimeSegName { get; set; }
        public string GroupName { get; set; }
        public string Devices { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> TimeSegId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> CompId { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string StrDeviceId { get; set; }
    }
}
