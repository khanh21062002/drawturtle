using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EPS.Utils.Repository
{
    public static class Extension
    {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> source, Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    source = source.Include(includeProperty);
                }
            }
            return source;
        }
    }
}
