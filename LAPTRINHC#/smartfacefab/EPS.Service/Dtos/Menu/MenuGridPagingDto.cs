using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.Menu
{
    public class MenuGridPagingDto : PagingParams<MenuGridDto>
    {
        public Nullable<int> CompId { get; set; }
        public string FilterName { get; set; }
        public Nullable<decimal> FilterPrice { get; set; }

        public override List<Expression<Func<MenuGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

            if (CompId.HasValue)
            {
                predicates.Add(x => x.CompId == CompId);
            }
            if (!string.IsNullOrEmpty(FilterName))
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.Name.Contains(FilterName.Trim()));

            }
            if (FilterPrice.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.Price == FilterPrice);

            }
            return predicates;
        }
    }
}
