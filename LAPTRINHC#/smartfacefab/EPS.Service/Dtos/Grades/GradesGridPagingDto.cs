using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Grades
{
    public class GradesGridPagingDto : PagingParams<GradesGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> compId { get; set; }
        
        public bool IsDelete { get; set; }

        public override List<Expression<Func<GradesGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Name.Contains(FilterText.Trim()) || x.Name.Contains(FilterText.Trim()));
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
           
            return predicates;
        }
    }
}
