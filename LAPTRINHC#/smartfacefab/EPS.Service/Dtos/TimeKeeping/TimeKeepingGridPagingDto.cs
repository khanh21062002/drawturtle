using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.TimeKeeping
{
    public class TimeKeepingGridPagingDto : PagingParams<TimeKeepingGridDto>
    {
        public string FilterCodePerson { get; set; }
        public string FilterNamePerson { get; set; }
        public Nullable<int> CompId { get; set; }
        public Nullable<int> DeptId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string FilterDateFrom { get; set; }
        public string FilterDateTo { get; set; }

        public override List<Expression<Func<TimeKeepingGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            //Tìm kiếm theo đơn vị Id
            if (CompId.HasValue)
            {
                predicates.Add(x => x.COMP_ID == CompId);
            }
            if (GroupId.HasValue)
            {
                predicates.Add(x => x.GROUP_ID == GroupId);
            }

            //Tìm kiếm theo mã nhân viên
            if (!string.IsNullOrEmpty(FilterCodePerson))
            {
                predicates.Add(x => x.PERSON_CODE.Contains(FilterCodePerson.Trim()));
            }

            //Tìm kiếm theo tên nhân viên
            if (!string.IsNullOrEmpty(FilterNamePerson))
            {
                predicates.Add(x => x.PERSON_NAME.Contains(FilterNamePerson.Trim()));
            }

            //Tìm kiếm theo phòng ban
            if (DeptId.HasValue)
            {
                predicates.Add(x => x.DEPT_ID == DeptId);
            }
           

            //Tìm kiếm theo thời gian
            DateTime dateFrom, dateTo;
            if (!string.IsNullOrEmpty(FilterDateFrom))
            {
                try
                {
                    dateFrom = DateTime.Parse(FilterDateFrom);
                    predicates.Add(x => x.DATE_WORKING >= dateFrom);
                }
                catch (Exception ex) { }
            }
            if (!string.IsNullOrEmpty(FilterDateTo))
            {
                try
                {
                    dateTo = DateTime.Parse(FilterDateTo);
                    dateTo = dateTo.AddDays(1);
                    predicates.Add(x => x.DATE_WORKING < dateTo);
                }
                catch (Exception ex) { }
            }

            return predicates;
        }
    }
}