using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.WorkingShiftTimes
{
    public class WorkingShiftTimesGridPagingDto : PagingParams<WorkingShiftTimesGridDto>
    {
        public string FilterCode { get; set; }

        public string FilterName { get; set; }
        public Nullable<int> CompId { get; set; }

        public override List<Expression<Func<WorkingShiftTimesGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(FilterCode))
            {
                predicates.Add(x => x.Code.Contains(FilterCode.Trim()));
            }
            if (!string.IsNullOrEmpty(FilterName))
            {
                predicates.Add(x => x.Name.Contains(FilterName.Trim()));
            }

            if (CompId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == CompId);
            }


            return predicates;
        }
    }
}
