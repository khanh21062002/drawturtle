using EPS.Data.Entities;
using EPS.Service.Dtos.User;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPS.Service.Dtos.User
{
    public class UserGridPagingDto : PagingParams<UserGridDto>, IUnitTraversal<Data.Entities.User>
    {
        public string Username { get; set; }
        public int? UnitId { get; set; }
        public Nullable<int> compId { get; set; }
        public bool IsDelete { get; set; }
        public UnitTraversalOption? UnitTraversalOption { get; set; }

        public string HiddenParentField { get; set; }

        public override List<Expression<Func<UserGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(Username))
            {
                predicates.Add(x => x.Username.Contains(Username));
            }

            //if (UnitId.HasValue)
            //{
            //    predicates.Add(x => x.UnitId == UnitId.Value);
            //}

            if (!string.IsNullOrEmpty(HiddenParentField))
            {
                predicates.Add(x => x.HiddenParentCompanyField.StartsWith(HiddenParentField));
            }

            predicates.Add(x => x.DeletedUserId == null);

            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);
            }
            return predicates;
        }

        public void Traversing(IQueryable<UnitAncestor> unitAncestors, ref IQueryable<Data.Entities.User> query)
        {
            if (UnitId.HasValue)
            {
                switch (UnitTraversalOption.GetValueOrDefault())
                {
                    case Service.Helpers.UnitTraversalOption.IncludeChildren: query = query.Where(x => x.UnitId == UnitId.Value || x.Unit.ParentId == UnitId.Value); break;
                    case Service.Helpers.UnitTraversalOption.IncludeDescendants:
                        //query = query.Where(x => x.Unit.Ancestors.Select(y => y.UnitAncestorId).Contains(UnitId.Value));
                        // using join instead of where in to improve performance
                        query = query.Join(unitAncestors.Where(x => x.UnitAncestorId == UnitId.Value), x => x.UnitId, x => x.UnitId, (x, y) => x);
                        break;
                    default: query = query.Where(x => x.UnitId == UnitId.Value); break;
                }
            }
        }
    }
}
