using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.Notification
{
    public class NotificationGridPagingDto : PagingParams<NotificationGridDto>
    {
        public Nullable<int> CompId { get; set; }
        public Nullable<int> MachineId { get; set; }
        public override List<Expression<Func<NotificationGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (CompId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == CompId);
            }
            if (MachineId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.MachineId == MachineId);
            }

            return predicates;
        }

    }
}
