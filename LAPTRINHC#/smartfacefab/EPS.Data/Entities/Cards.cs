using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Cards
    {
        [Key]
        public Guid CardId { get; set; }
        public Guid PersonId { get; set; }
        public string CardNo { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string RegisterBy { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public Nullable<System.DateTime> DeleteBy { get; set; }
        public Nullable<System.Int32> Status { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}
