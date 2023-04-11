using EPS.Data;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace EPS.Service.Dtos.DayOff
{
    public class DayOffGridPagingDto : PagingParams<DayOffGridDto>
    {
        public Nullable<int> compId { get; set; }

        public Nullable<DateTime> dateFrom { get; set; }
        public Nullable<DateTime> dateTo { get; set; }
        public string HiddenParentField { get; set; }
        public string DeptId { get; set; }

        public string personCode { get; set; }
        public string personName { get; set; }
        public override List<Expression<Func<DayOffGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);
            }
            if (!string.IsNullOrEmpty(HiddenParentField))
            {
                predicates.Add(x => x.HiddenParentField.StartsWith(HiddenParentField.Trim()));
            }
            if (dateFrom.HasValue)
            {
                if(dateTo.HasValue && dateTo.Value < dateFrom.Value)
                {
                    // Khoang thoi gian tim kiem invalid
                    throw new EPSException("Lỗi tìm kiếm! Ngày bắt đầu lớn hơn ngày kết thúc");
                } else
                {
                    //Tìm kiếm theo đơn vị Id
                    predicates.Add(x => x.DateTo >= dateFrom);
                }
               
            }
            //Tìm kiếm theo phòng ban
            if (!string.IsNullOrEmpty(DeptId))
            {
                List<int> lstperId = DeptId.Split(',').Select(Int32.Parse).ToList();
                predicates.Add(x => lstperId.Contains(x.DeptId.Value));
            }
            if (dateTo.HasValue)
            {
                if (dateFrom.HasValue && dateFrom.Value > dateTo.Value)
                {
                    // Khoang thoi gian tim kiem invalid
                    throw new EPSException("Lỗi tìm kiếm! Ngày bắt đầu lớn hơn ngày kết thúc");
                }
                else
                {
                    //Tìm kiếm theo đơn vị Id
                    predicates.Add(x => x.DateFrom <= dateTo);
                }
            }
            if (!string.IsNullOrEmpty(personCode))
            {
                predicates.Add(x => x.PersonCode.Contains(personCode.Trim()));
            }
            if (!string.IsNullOrEmpty(personName))
            {
                predicates.Add(x => x.PersonName.Contains(personName.Trim()));
            }
            return predicates;
        }

    }
}
