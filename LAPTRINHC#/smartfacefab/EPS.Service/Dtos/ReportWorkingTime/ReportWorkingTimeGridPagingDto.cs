using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.ReportWorkingTime
{
    public class ReportWorkingTimeGridPagingDto : PagingParams<ReportWorkingTimeGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> FilterCompId { get; set; }
        public Nullable<int> FilterDeptId { get; set; }
        public Nullable<DateTime> FilterDateFrom { get; set; }
        public Nullable<DateTime> FilterDateTo { get; set; }

       
    }
}
