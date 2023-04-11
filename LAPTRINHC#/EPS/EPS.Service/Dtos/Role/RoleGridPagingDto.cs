using EPS.Service.Dtos.Role;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Service.Dtos.Role
{
    public class RoleGridPagingDto : PagingParams<RoleGridDto>
    {
        public string FilterText { get; set; }
        public override List<Expression<Func<RoleGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Name.Contains(FilterText));
            }
            return predicates;
        }
    }
}
