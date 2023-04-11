using EPS.Data.Entities;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPS.Service.Dtos.Department
{
    public class DepartmentGridDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ComId { get; set; }
        public Nullable<int> Status { get; set; }
        public string StatusName { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public Nullable<int> ParentId { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> GradesId { get; set; }
        public string HiddenParentField { get; set; }
        public string TreeLevel { get; set; }
        public int? TreeLevelInt { get; set; }

        public string GradeName { get; set; }
        //public DepartmentTraversalOption? DepartmentTraversalOption { get; set; }
        //public void Traversing(IQueryable<DepartmentAncestor> departmentAncestors, ref IQueryable<Data.Entities.Department> query)
        //{
        //    if (Id.HasValue)
        //    {
        //        switch (DepartmentTraversalOption.GetValueOrDefault())
        //        {
        //            case Service.Helpers.DepartmentTraversalOption.IncludeChildren: query = query.Where(x => x.Id == Id.Value || x.Parent.ParentId == Id.Value); break;
        //            case Service.Helpers.DepartmentTraversalOption.IncludeDescendants:
        //                //query = query.Where(x => x.Department.Ancestors.Select(y => y.DepartmentAncestorId).Contains(Id.Value));
        //                // using join instead of where in to improve performance
        //                query = query.Join(departmentAncestors.Where(x => x.DepartmentAncestorId == Id.Value), x => x.Id, x => x.Id, (x, y) => x);
        //                break;
        //            default: query = query.Where(x => x.Id == Id.Value); break;
        //        }
        //    }
        //}
    }
}
