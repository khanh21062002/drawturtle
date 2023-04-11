using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.GroupAccess
{
    public class GroupAccessGridPagingDto : PagingParams<GroupAccessGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> compId { get; set; }
        public string machineId { get; set; }
        public Nullable<int> groupId { get; set; }
        public Nullable<int> deptId { get; set; }
        public Nullable<int> timesegId { get; set; }
        public bool IsDelete { get; set; }
        public override List<Expression<Func<GroupAccessGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);

            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);
            }
            if (!string.IsNullOrEmpty(machineId))
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.StrDeviceId.Contains("(" + machineId + ")"));
            }
            if (groupId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.GroupId == groupId);
            }
            if (timesegId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.TimeSegId == timesegId);
            }
            if (deptId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.DeptId == deptId);
            }


            return predicates;
        }
    }
}
