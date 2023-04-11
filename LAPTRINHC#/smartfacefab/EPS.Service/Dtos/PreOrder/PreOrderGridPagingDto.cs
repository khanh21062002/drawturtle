using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.PreOrder
{
    public class PreOrderGridPagingDto : PagingParams<PreOrderGridDto>
    {
        public int? CompId { get; set; }
        public string FilterName { get; set; }
        public int? FilterPhoneNumber { get; set; }
        public DateTime? FilterDateFrom { get; set; }
        public DateTime? FilterDateTo { get; set; }

        public override List<Expression<Func<PreOrderGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

            if (CompId.HasValue)
            {
                predicates.Add(x => x.CompId == CompId);
            }

            //Tìm kiếm theo họ tên khách hàng
            if (!string.IsNullOrEmpty(FilterName))
            {
                predicates.Add(x => x.NameGuest.Contains(FilterName.Trim()));
            }

            //Tìm kiếm theo số điện thoại
            if (FilterPhoneNumber.HasValue)
            {
                predicates.Add(x => x.PhoneNumber.Contains(FilterPhoneNumber.ToString().Trim()));
            }

            //Tìm kiếm theo thời gian
            if (FilterDateFrom.HasValue)
            {
                predicates.Add(x => x.TimeOrder >= FilterDateFrom);
            }
            if (FilterDateTo.HasValue)
            {
                predicates.Add(x => x.TimeOrder <= FilterDateTo);
            }

            return predicates;
        }
    }
}
