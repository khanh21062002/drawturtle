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
    public class UserGridPagingDto : PagingParams<UserGridDto>, ICompanyTraversal<Data.Entities.User>
    {
        public string Username { get; set; }
        public int? CompanyId { get; set; }
        public CompanyTraversalOption? CompanyTraversalOption { get; set; }

        public override List<Expression<Func<UserGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(Username))
            {
                predicates.Add(x => x.Username.Contains(Username));
            }

            if (CompanyId.HasValue)
            {
                predicates.Add(x => x.CompanyId == CompanyId.Value);
            }

            predicates.Add(x => x.DeletedUserId == null);
            return predicates;
        }

        public void Traversing(IQueryable<CompanyAncestor> companyAncestors, ref IQueryable<Data.Entities.User> query)
        {
            if (CompanyId.HasValue)
            {
                switch (CompanyTraversalOption.GetValueOrDefault())
                {
                    case Service.Helpers.CompanyTraversalOption.IncludeChildren: query = query.Where(x => x.CompanyId == CompanyId.Value || x.Company.ParentId == CompanyId.Value); break;
                    case Service.Helpers.CompanyTraversalOption.IncludeDescendants:
                        //query = query.Where(x => x.Company.Ancestors.Select(y => y.CompanyAncestorId).Contains(CompanyId.Value));
                        // using join instead of where in to improve performance
                        query = query.Join(companyAncestors.Where(x => x.CompanyAncestorId == CompanyId.Value), x => x.CompanyId, x => x.CompanyId, (x, y) => x);
                        break;
                    default: query = query.Where(x => x.CompanyId == CompanyId.Value); break;
                }
            }
        }
    }
}
