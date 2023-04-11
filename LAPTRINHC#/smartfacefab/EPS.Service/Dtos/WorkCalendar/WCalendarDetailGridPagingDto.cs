using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.WorkCalendar
{
    public class WCalendarDetailGridPagingDto : PagingParams<WCalendarDetailGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> CompId { get; set; }

        public override List<Expression<Func<WCalendarDetailGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

          

            return predicates;
        }
    }

}
