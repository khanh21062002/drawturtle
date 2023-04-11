using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Grades
{
    public class GradesDetailDto
    {
        public int Id { get; set; }
        public Nullable<int> CompId { get; set; }
        public string CompName { get; set; }
        public string Name { get; set; }
        public Nullable<int> GradeNumber { get; set; }
        public string Descriptions { get; set; }
        public Nullable<bool> IsDelete { get; set; }

    }
}
