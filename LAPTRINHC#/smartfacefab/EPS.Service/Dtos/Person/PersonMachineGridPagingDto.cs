using EPS.Service.Dtos.Event;
using EPS.Service.Models;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Person
{
    public class PersonMachineGridPagingDto : PagingParams<CustomerEventGridDto>
    {
        public Nullable<Guid> personId { get; set; }

        public override List<Expression<Func<CustomerEventGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            //Tìm kiếm theo đơn vị Id
            if (personId.HasValue)
            {
                predicates.Add(x => x.PersonId == personId);
            }
            return predicates;
        }
    }
}
