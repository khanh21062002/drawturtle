using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.Areas
{
    public class AreasGridPagingDto : PagingParams<AreasGridDto>
    {

        public Nullable<int> compId { get; set; }

        public string FilterText { get; set; }
        public int? FilterStatus { get; set; }
        public override List<Expression<Func<AreasGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);
            }
            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Code.Contains(FilterText.Trim()) || x.Name.Contains(FilterText.Trim()));
            }
            if (FilterStatus.HasValue)
            {
                if(FilterStatus == 2) { }
                else
                {
                    predicates.Add(x => x.Status == FilterStatus);
                }
            }

            return predicates;
        }
    }
}
