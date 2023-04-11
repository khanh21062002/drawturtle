using EPS.Data;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.RegisterWorkingShift
{
    public class RegisterWorkingShiftGridPagingDto : PagingParams<RegisterWorkingShiftGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeptId { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public Nullable<DateTime> dateFrom { get; set; }
        public Nullable<DateTime> dateTo { get; set; }

        public override List<Expression<Func<RegisterWorkingShiftGridDto, bool>>> GetPredicates()
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
                predicates.Add(x => x.Note.Contains(FilterText));
            }

            //Tìm kiếm theo phòng ban
            if (!string.IsNullOrEmpty(DeptId))
            {
                var deptIdArr = DeptId.Split(',');
                predicates.Add(x => deptIdArr.Contains(x.DeptId.ToString()));
            }

            if (!String.IsNullOrEmpty(PersonCode))
            {
                predicates.Add(x => x.PersonCode.Contains(PersonCode.Trim()));
            }

            if (!String.IsNullOrEmpty(PersonName))
            {
                predicates.Add(x => x.PersonName.Contains(PersonName.Trim()));
            }

            if (dateFrom.HasValue)
            {
                if (dateTo.HasValue && dateTo.Value < dateFrom.Value)
                {
                    // Khoang thoi gian tim kiem invalid
                    throw new EPSException("Lỗi tìm kiếm! Ngày bắt đầu lớn hơn ngày kết thúc");
                }
                else
                {
                    //Tìm kiếm theo đơn vị Id
                    predicates.Add(x => x.DateTo >= dateFrom);
                }
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

            return predicates;
        }
    }
}
