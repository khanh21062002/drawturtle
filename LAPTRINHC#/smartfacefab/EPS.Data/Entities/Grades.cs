using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public class Grades
    {
        public int Id { get; set; }
        public Nullable<int> CompId { get; set; }
        public string Name { get; set; }
        public Nullable<int> GradeNumber { get; set; }
        public string Descriptions { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        [ForeignKey("CompId")]
        public virtual Company Company { get; set; }

    }
}
