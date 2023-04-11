using EPS.Data;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.OverTime
{
    public class OverTimeGridPagingDto : PagingParams<OverTimeGridDto>
    {
        public string FilterCodePerson { get; set; }
        public string FilterNamePerson { get; set; }
        public Nullable<int> CompId { get; set; }
        public string FilterDateFrom { get; set; }
        public string FilterDateTo { get; set; }
        public string HiddenParentField { get; set; }

        public string DeptId { get; set; }

        public override List<Expression<Func<OverTimeGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            //Tìm kiếm theo đơn vị Id
            if (CompId.HasValue)
            {
                predicates.Add(x => x.COMP_ID == CompId);
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
            if (!string.IsNullOrEmpty(DeptId))
            {
                List<int> lstperId = DeptId.Split(',').Select(Int32.Parse).ToList();
                predicates.Add(x => lstperId.Contains(x.DEPARTMENT_ID.Value));
            }
            //Tìm kiếm theo phòng ban
            if (!string.IsNullOrEmpty(HiddenParentField))
            {
                predicates.Add(x => x.HiddenParentField.StartsWith(HiddenParentField.Trim()));
            }
            //Tìm kiếm theo thời gian
            DateTime dateFrom, dateTo;

            if (string.IsNullOrEmpty(FilterDateFrom) || string.IsNullOrEmpty(FilterDateTo))
            {
                throw new EPSException("Contant.Error7");
            }


            try
            {
                dateFrom = DateTime.Parse(FilterDateFrom);
                predicates.Add(x => x.DATE_OT >= dateFrom);
                dateTo = DateTime.Parse(FilterDateTo);
                dateTo = dateTo.AddDays(1);
                double diffrenceDays = (dateTo - dateFrom).TotalDays;
                if (diffrenceDays > 31)
                {
                    throw new EPSException("CalculateWorkingHourService.Message.TimeMonth");
                }

                predicates.Add(x => x.DATE_OT < dateTo);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            

            return predicates;
        }
    }
}
