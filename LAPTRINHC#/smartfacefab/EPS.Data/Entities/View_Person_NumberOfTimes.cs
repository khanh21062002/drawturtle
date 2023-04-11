using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class View_Person_NumberOfTimes
    {
        [Key]
        public Guid PersonId { get; set; }
        public Nullable<int> NumberOfTimes { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}
