using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Group
{
    public class GroupGridPagingDto : PagingParams<GroupGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> compId { get; set; }
        public Nullable<int> deptId { get; set; }
        public bool IsDelete { get; set; }

        public override List<Expression<Func<GroupGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.GroupCode.Contains(FilterText.Trim()) || x.GroupName.Contains(FilterText.Trim()));
            }

            //if (department_ID.HasValue)
            //{
            //    //Tìm kiếm theo đơn vị Id
            //    predicates.Add(x => x.DEPARTMENT_ID == department_ID);
            //}
            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);
            }
            if (deptId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.DeptId == deptId);
            }
            return predicates;
        }
    }
}
