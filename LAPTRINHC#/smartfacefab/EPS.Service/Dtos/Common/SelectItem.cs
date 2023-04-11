using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Common
{
    public class SelectItem
    {
        public string Id { get; set; }
        public string Value { get { return Id; } set { Id = value; } }
        public string Text { get; set; }
        public Nullable<int> filterId { get; set; }
        public Nullable<int> gradesId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> Type { get; set; }

        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> Disabled { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<Guid> PersonId { get; set; }
        public Nullable<int> PersonType { get; set; }
        public Nullable<int> DepartmentType { get; set; }
        public string HiddenParentField { get; set; }
        public string GroupName { get; set; }
        public string Language { get; set; }

        public Nullable<int> FeatureId { get; set; }

    }
}
