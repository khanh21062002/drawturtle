using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.ShiftTime
{
    public class ShiftTimeGridPagingDto : PagingParams<ShiftTimeGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> CompId { get; set; }

        public override List<Expression<Func<ShiftTimeGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (CompId.HasValue)
            {
                predicates.Add(x => x.CompId == CompId.Value);
            }
            return predicates;
        }
    }
}
