using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class StudentParents
    {
        public int Id { get; set; }
        public Nullable<Guid> StudentId { get; set; }
        public Nullable<Guid> ParentId { get; set; }
        public string Note { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public Nullable<Boolean> IsDelete { get; set; }

        [ForeignKey("StudentId")]
        public virtual Person Student { get; set; }

        [ForeignKey("ParentId")]
        public virtual Person Parent { get; set; }
    }
}
