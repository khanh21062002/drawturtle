using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.PersonQRUpdate
{
    public class PersonQRUpdateGridPagingDto : PagingParams<PersonQRUpdateGridDto>
    {
        public string filterPerson { get; set; }
        public string filterDeptId { get; set; }
        public string filterDateFrom { get; set; }
        public string filterDateTo { get; set; }
        public Nullable<int> filterStatus { get; set; }
        public Nullable<int> compId { get; set; }

        public override List<Expression<Func<PersonQRUpdateGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (compId.HasValue)
            {
                predicates.Add(x => x.CompId == compId);
            }

            if (!string.IsNullOrEmpty(filterPerson))
            {
                predicates.Add(x => x.PersonCode.Contains(filterPerson.Trim()) || x.PersonName.Contains(filterPerson.Trim()));
            }
            //Tìm kiếm theo phòng ban
            if (!string.IsNullOrEmpty(filterDeptId))
            {
                var deptIdArr = filterDeptId.Split(',');
                predicates.Add(x => deptIdArr.Contains(x.DeptId.ToString()));
            }

            //Tìm kiếm theo thời gian
            DateTime dateFrom, dateTo;
            if (!string.IsNullOrEmpty(filterDateFrom))
            {
                try
                {
                    dateFrom = DateTime.Parse(filterDateFrom);
                    predicates.Add(x => x.DateUpdate >= dateFrom);
                }
                catch (Exception ex) { }
            }
            if (!string.IsNullOrEmpty(filterDateTo))
            {
                try
                {
                    dateTo = DateTime.Parse(filterDateTo);
                    dateTo = dateTo.AddDays(1);
                    predicates.Add(x => x.DateUpdate < dateTo);
                }
                catch (Exception ex) { }
            }

            if (filterStatus.HasValue)
            {
                if (filterStatus.Value == 1)
                {
                    predicates.Add(x => x.Status == 1);
                }
                if (filterStatus.Value == 2)
                {
                    predicates.Add(x => x.Status == 2);
                }
                if (filterStatus.Value == 0)
                {
                    predicates.Add(x => x.Status == 0);
                }
            }
            return predicates;
        }
    }
}
