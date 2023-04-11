using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Department
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ComId { get; set; }
        public Nullable<int> Status { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }
        public Nullable<int> ParentId { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> GradesId { get; set; }
        public string HiddenParentField { get; set; }
        public string TreeLevel { get; set; }
    }
}
