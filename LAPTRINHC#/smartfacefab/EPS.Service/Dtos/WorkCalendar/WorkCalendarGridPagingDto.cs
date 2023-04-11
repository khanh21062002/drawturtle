using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.WorkCalendar
{
    public class WorkCalendarGridPagingDto : PagingParams<WorkCalendarGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> CompId { get; set; }

        public override List<Expression<Func<WorkCalendarGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

            if (CompId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == CompId);
            }

            if (!String.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Name.Contains(FilterText));
            }

            return predicates;
        }
    }
}
