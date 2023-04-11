using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class RegisterWorkingShift
    {
        public int ID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> TimeCycle { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public string Note { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }


    }
}
