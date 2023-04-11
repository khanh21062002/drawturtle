using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EPS.Data.Entities
{
    public class Groups
    {
        [Key]
        public int? GroupId { get; set; }
        public int UserId { get; set; }
        [StringLength(50)]
        public string GroupCode { get; set; }
        [StringLength(250)]
        public string GroupName { get; set; }
        public int Status { get; set; }
        public bool IsDelete { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
