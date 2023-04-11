using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.PersonGroup
{
    public class PersonGroupGridPagingDto : PagingParams<PersonGroupGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> department_ID { get; set; }
        public override List<Expression<Func<PersonGroupGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (!string.IsNullOrEmpty(FilterText))
            {
                //predicates.Add(x => x.Code.Contains(FilterText) || x.Name.Contains(FilterText));
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
