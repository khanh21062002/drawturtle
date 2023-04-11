using EPS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Helpers
{
    public interface IUnitTraversal<TEntity>
    {
        void Traversing(IQueryable<UnitAncestor> unitAncestors, ref IQueryable<TEntity> query);
    }
}
