using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.GroupAccess
{
    public class GroupAccessUpdateDto
    {
        public int Id { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> MachineId { get; set; }

        public Nullable<int> TimeSegId { get; set; }
        public bool IsDelete { get; set; }

        public Nullable<int> DeptId { get; set; }
        public List<Nullable<int>> LstMachineId { get; set; }
    }
}
