using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.Guess
{
    public class GuessGridPagingDto : PagingParams<GuessGridDto>
    {
        public Nullable<int> compId { get; set; }

        public string fullName { get; set; }

        public string passport { get; set; }

        public override List<Expression<Func<GuessGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.ComId == compId);
            }

            if (!string.IsNullOrEmpty(fullName))
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.FullName.Contains(fullName.Trim()));
            }

            if (!string.IsNullOrEmpty(passport))
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.Passport.Contains(passport.Trim()));
            }

            return predicates;
        }
    }
}
