using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Company
{
    public class CompanyGridPagingDto : PagingParams<CompanyGridDto>
    {
        public string FilterText { get; set; }
        public bool IsDelete { get; set; }
        public override List<Expression<Func<CompanyGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Code.Contains(FilterText) || x.Name.Contains(FilterText));
            }

            //if (department_ID.HasValue)
            //{
            //    //Tìm kiếm theo đơn vị Id
            //    predicates.Add(x => x.DEPARTMENT_ID == department_ID);
            //}

            return predicates;
        }
    }
}
