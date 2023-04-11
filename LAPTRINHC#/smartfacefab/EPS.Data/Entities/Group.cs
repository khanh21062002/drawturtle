using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Group 
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        //public int ComId { get; set; }
        public int Status { get; set; }
        public string Position { get; set; }
        public string GroupCode { get; set; }
        public Nullable<int> DeptId { get; set; }
        //[ForeignKey("ComId")]
        //public virtual Company Company { get; set; }
        public Nullable<int> CompId { get; set; }
        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }
        public Nullable<bool>  IsDelete { get; set; }

    }
}
