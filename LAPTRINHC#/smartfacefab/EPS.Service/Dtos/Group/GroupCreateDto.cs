using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Group
{
    public class GroupCreateDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> Status { get; set; }
        public string Position { get; set; }
        public string GroupCode { get; set; }
        public Nullable<int> DeptId { get; set; }
        public string Department { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Company { get; set; }

        public bool IsDelete { get; set; }

        ///*public*/ string GroupCodeMap { get; set; }
    }
}
