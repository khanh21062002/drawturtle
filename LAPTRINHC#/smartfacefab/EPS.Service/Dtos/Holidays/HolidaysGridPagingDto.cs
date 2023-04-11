using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.Holidays
{
    public class HolidaysGridPagingDto : PagingParams<HolidaysGridDto>
    {
        public string year { get; set; }
        public Nullable<int> compId { get; set; }
        public override List<Expression<Func<HolidaysGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(year))
            {
                predicates.Add(x => x.Year.Equals(year));
            }
            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);
            }
            return predicates;
        }

    }
}
