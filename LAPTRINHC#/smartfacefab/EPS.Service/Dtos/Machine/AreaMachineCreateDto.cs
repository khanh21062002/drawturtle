using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Machine
{
    public class AreaMachineCreateDto
    {
        public int Id { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
