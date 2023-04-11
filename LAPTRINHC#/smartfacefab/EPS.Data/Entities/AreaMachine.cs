using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class AreaMachine
    {
        public int Id { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; }
        [ForeignKey("AreaId")]
        public virtual Areas Areas { get; set; }

    }
}
