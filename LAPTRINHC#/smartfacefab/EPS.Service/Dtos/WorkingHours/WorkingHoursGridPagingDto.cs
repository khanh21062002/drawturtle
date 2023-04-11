using EPS.Data;
using EPS.Service.Dtos.WorkingHours;
using EPS.Service.Models;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.RegisterWorkingShift
{
    public class WorkingHoursGridPagingDto : PagingParams<WorkingHoursGridDto>
    {
        public string FilterCodePerson { get; set; }
        public string FilterNamePerson { get; set; }
        public string Lang { get; set; }

        public Nullable<int> CompId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string FilterDateFrom { get; set; }
        public string FilterDateTo { get; set; }
        public List<Guid> ListPersonId { get; set; }
        public Nullable<int> IsExportRequest { get; set; }
        public string DeptId { get; set; }



        public override List<Expression<Func<WorkingHoursGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => !string.IsNullOrEmpty(x.PersonCode));

            //check if status is available or unavailable
            predicates.Add(x => (x.PersonStatus == AppConstants.PERSON_STATUS.AVAILABLE) || (x.PersonStatus == AppConstants.PERSON_STATUS.UNAVAILABLE && x.Total > 0));
            if (CompId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == CompId);
            }

            //Tìm kiếm theo mã nhân viên
            if (!string.IsNullOrEmpty(FilterCodePerson))
            {
                predicates.Add(x => x.PersonCode.Contains(FilterCodePerson.Trim()));
            }
            if (!string.IsNullOrEmpty(FilterNamePerson))
            {
                predicates.Add(x => x.PersonName.Contains(FilterNamePerson.Trim()));
            }

            if (!string.IsNullOrEmpty(DeptId))
            {
                List<int> lstperId = DeptId.Split(',').Select(Int32.Parse).ToList();
                predicates.Add(x => lstperId.Contains(x.DeptId.Value));
            }
            ////Tìm kiếm theo phòng ban
            //if (DeptId != null && DeptId.Count() > 0)
            //{
            //    predicates.Add(x => DeptId.Contains(x.DeptId));
            //}

            //Tìm kiếm theo thời gian
            DateTime dateFrom, dateTo;

            if (string.IsNullOrEmpty(FilterDateFrom) || string.IsNullOrEmpty(FilterDateTo))
            {
                throw new EPSException("Vui lòng chọn khoảng thời gian cần tìm kiếm!");
            }



            //Tìm kiếm theo list person id 
            if (ListPersonId != null && ListPersonId.Count > 0)
            {
                predicates.Add(x => x.PersonId.HasValue && ListPersonId.Contains(x.PersonId.Value));
            }

            try
            {
                dateFrom = DateTime.Parse(FilterDateFrom);
                predicates.Add(x => x.Day >= dateFrom);
                dateTo = DateTime.Parse(FilterDateTo);
                dateTo = dateTo.AddDays(1);
                double diffrenceDays = (dateTo - dateFrom).TotalDays;
                if (diffrenceDays > 45)
                {
                    throw new EPSException("Vui lòng chọn khoảng thời gian trong vòng 45 ngày!");
                }

                predicates.Add(x => x.Day < dateTo);

            }
            catch (Exception ex)
            {
                throw ex;
            }



            return predicates;
        }

    }
}
