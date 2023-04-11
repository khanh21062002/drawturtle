using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Machine
{
    public class MachineGridPagingDto : PagingParams<MachineGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> department_ID { get; set; }
        public Nullable<int> compId { get; set; }
        public Nullable<int> areaId { get; set; }
        public bool IsDelete { get; set; }
        public override List<Expression<Func<MachineGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Code.Contains(FilterText.Trim()) || x.DeviceName.Contains(FilterText.Trim()));
            }

            if (areaId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.AreaId == areaId);
            }
            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);
            }
            return predicates;
        }
    }
}
