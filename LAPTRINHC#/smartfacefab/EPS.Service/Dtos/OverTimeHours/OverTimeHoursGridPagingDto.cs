using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.OverTimeHours
{
    public class OverTimeHoursGridPagingDto : PagingParams<OverTimeHoursGridDto>
    {
        public string FilterCodePerson { get; set; }
        public string FilterNamePerson { get; set; }
        public Nullable<int> CompId { get; set; }
        public string DeptId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string FilterDateFrom { get; set; }
        public string FilterDateTo { get; set; }
        public string Lang { get; set; }
        public string HiddenParentField { get; set; }

        
        public List<Guid> ListPersonId { get; set; }

        public Nullable<int> IsExportRequest { get; set; }

        public override List<Expression<Func<OverTimeHoursGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            //Tìm kiếm theo đơn vị Id
            if (CompId.HasValue)
            {
                predicates.Add(x => x.CompId == CompId);
            }

            //Tìm kiếm theo mã nhân viên
            if (!string.IsNullOrEmpty(FilterCodePerson))
            {
                predicates.Add(x => x.PersonCode.Contains(FilterCodePerson.Trim()));
            }

            //Tìm kiếm theo tên nhân viên
            if (!string.IsNullOrEmpty(FilterNamePerson))
            {
                predicates.Add(x => x.PersonName.Contains(FilterNamePerson.Trim()));
            }
            //Tìm kiếm theo phòng ban
            if (!string.IsNullOrEmpty(DeptId))
            {
                List<int> lstperId = DeptId.Split(',').Select(Int32.Parse).ToList();
                predicates.Add(x => lstperId.Contains(x.DeptId.Value));
            }
            //Tìm kiếm theo phòng ban
            if (!string.IsNullOrEmpty(HiddenParentField))
            {
                predicates.Add(x => x.HiddenParentField.StartsWith(HiddenParentField.Trim()));
            }
            //Tìm kiếm theo list person id 
            if (ListPersonId != null && ListPersonId.Count > 0)
            {
                predicates.Add(x => x.PersonId.HasValue && ListPersonId.Contains(x.PersonId.Value));
            }

            //Tìm kiếm theo thời gian
            DateTime dateFrom, dateTo;
            if (!string.IsNullOrEmpty(FilterDateFrom))
            {
                try
                {
                    dateFrom = DateTime.Parse(FilterDateFrom);
                    predicates.Add(x => x.Day >= dateFrom);
                }
                catch (Exception) { }
            }
            if (!string.IsNullOrEmpty(FilterDateTo))
            {
                try
                {
                    dateTo = DateTime.Parse(FilterDateTo);
                    dateTo = dateTo.AddDays(1);
                    predicates.Add(x => x.Day < dateTo);
                }
                catch (Exception) { }
            }

            return predicates;
        }
    }
}
