//using EPS.API.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using System.Linq.Dynamic.Core;
//using Microsoft.EntityFrameworkCore;

//namespace EPS.API.Helpers
//{
//    public static class Extensions
//    {
//        public static GridResult<TModel> ToGrid<TEntity, TModel>(this IQueryable<TEntity> source, GridPagination gridParams, Expression<Func<TEntity, TModel>> transformer)
//        {
//            var result = new GridResult<TModel>();

//            result.TotalRows = source.Count();

//            var page = gridParams.Page;
//            var pageSize = gridParams.ItemsPerPage;
//            var sortExpression = gridParams.SortExpression;

//            if (page < 1) page = 1;
//            if (pageSize <= 0) pageSize = 10;

//            var destination = source.Select(transformer);

//            if (!string.IsNullOrEmpty(sortExpression))
//            {
//                destination = destination.OrderBy(sortExpression);
//            }

//            destination = destination.Skip((page - 1) * pageSize);
//            destination = destination.Take(pageSize);

//            result.Data = destination.ToList();

//            return result;
//        }

//        public async static Task<GridResult<TModel>> ToGridAsync<TEntity, TModel>(this IQueryable<TEntity> source, GridPagination gridParams, Expression<Func<TEntity, TModel>> transformer)
//        {
//            var result = new GridResult<TModel>();

//            result.TotalRows = await source.CountAsync();

//            var page = gridParams.Page;
//            var pageSize = gridParams.ItemsPerPage;
//            var sortExpression = gridParams.SortExpression;

//            if (page < 1) page = 1;
//            if (pageSize <= 0) pageSize = 10;

//            var destination = source.Select(transformer);

//            if (!string.IsNullOrEmpty(sortExpression))
//            {
//                destination = destination.OrderBy(sortExpression);
//            }

//            destination = destination.Skip((page - 1) * pageSize);
//            destination = destination.Take(pageSize);

//            result.Data = await destination.ToListAsync();

//            return result;
//        }
//    }
//}
