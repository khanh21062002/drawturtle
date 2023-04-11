using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class ShiftTime
    {
        public int Id { get; set; }
        public string ShiftName { get; set; }
        public Nullable<int> CompId { get; set; }
        public string BeginShiftTime { get; set; }
        public string EndShiftTime { get; set; }
        public string BeginFreeShiftTime { get; set; }
        public string EndFreeShiftTime { get; set; }
        public string CreateBy { get; set; }
        public Nullable<DateTime> CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateTime { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }

    }
}
