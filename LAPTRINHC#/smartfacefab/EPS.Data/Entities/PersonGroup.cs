using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class PersonGroup 
    {
        public int Id { get; set; }
        public Guid PersonId { get; set; }
        public int GroupId { get; set; }
        public int Status { get; set; }
        public bool IsDelete { get; set; }


        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

    }
}
