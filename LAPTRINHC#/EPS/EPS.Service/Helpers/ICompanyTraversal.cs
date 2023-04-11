using EPS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Helpers
{
    public interface ICompanyTraversal<TEntity>
    {
        void Traversing(IQueryable<CompanyAncestor> companyAncestors, ref IQueryable<TEntity> query);
    }
}
