using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.NotificationSystem
{
    public class NotificationSystemPagingDto : PagingParams<NotificationSystemGridDto>
    {
        public int userId { get; set; }

        public override List<Expression<Func<NotificationSystemGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            predicates.Add(x => x.UserId == userId);

            return predicates;
        }
    }
}
