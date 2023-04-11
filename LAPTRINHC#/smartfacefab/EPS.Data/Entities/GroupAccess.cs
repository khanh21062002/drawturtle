using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class GroupAccess
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<int> GroupId  { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> TimeSegId { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> Status { get; set; }
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [ForeignKey("TimeSegId")]
        public virtual AccessTimeSeg AccessTimeSeg { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
       
        public Nullable<bool> IsDelete { get; set; }
    }
}
