using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Utils.Common
{
    public static class ExpressionExtension
    {
        ///// <summary>
        ///// Extension method to support dynamic sorting.
        ///// </summary>
        ///// <typeparam name="T">The entity type.</typeparam>
        ///// <param name="source">The entity list.</param>
        ///// <param name="statement">The order by statement.</param>
        ///// <returns></returns>
        //public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string statement)
        //{
        //    // NOTE: Currently only supports sorting of 1 column.
        //    string method = string.Empty;
        //    string[] parts = statement.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //    // Determine sort order.
        //    method = (parts.Length > 1 && parts[1].ToUpper() == "DESC") ?
        //                "OrderByDescending" : "OrderBy";

        //    // Create dynamic expression.
        //    var type = typeof(T);
        //    var property = type.GetProperty(parts[0]);
        //    var parameter = Expression.Parameter(type, "param");
        //    var member = Expression.MakeMemberAccess(parameter, property);
        //    var lambda = Expression.Lambda(member, parameter);

        //    var finalExpression = Expression.Call(typeof(Queryable), method,
        //                            new Type[] { type, property.PropertyType },
        //                            source.Expression, Expression.Quote(lambda));

        //    return source.Provider.CreateQuery<T>(finalExpression);
        //}

        public static IQueryable<T> WhereMany<T>(this IQueryable<T> source, params Expression<Func<T, bool>>[] predicates)
        {
            if (predicates != null && predicates.Any())
            {
                foreach (var predicate in predicates)
                {
                    source = source.Where(predicate);
                }
            }
            return source;
        }

        public static IQueryable<T> WhereMany<T>(this IQueryable<T> source, IEnumerable<Expression<Func<T, bool>>> predicates)
        {
            return source.WhereMany(predicates.ToArray());
        }

        public static IList<T> CastToList<T>(this IEnumerable source)
        {
            return new List<T>(source.Cast<T>());
        }

        public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector)
        {

            return from left in leftItems
                   join right in rightItems on leftKeySelector(left) equals rightKeySelector(right) into temp
                   from right in temp.DefaultIfEmpty()
                   select resultSelector(left, right);
        }

        public static IEnumerable<TResult> RightOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector)
        {

            return from right in rightItems
                   join left in leftItems on rightKeySelector(right) equals leftKeySelector(left) into temp
                   from left in temp.DefaultIfEmpty()
                   select resultSelector(left, right);
        }

        public static IEnumerable<TResult> FullOuterJoinDistinct<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector)
        {

            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Union(leftItems.RightOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }

        public static IEnumerable<TResult> RightAntiSemiJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector) where TLeft : class
        {

            var hashLK = new HashSet<TKey>(from l in leftItems select leftKeySelector(l));
            return rightItems.Where(r => !hashLK.Contains(rightKeySelector(r))).Select(r => resultSelector((TLeft)null, r));
        }

        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> leftItems,
            IEnumerable<TRight> rightItems,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector) where TLeft : class
        {

            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Concat(leftItems.RightAntiSemiJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }

        private static Expression<Func<TP, TC, TResult>> CastSMBody<TP, TC, TResult>(LambdaExpression ex, TP unusedP, TC unusedC, TResult unusedRes)
        {
            return (Expression<Func<TP, TC, TResult>>)ex;
        }

        public static IQueryable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector)
            where TLeft : class
            where TRight : class
            where TResult : class
        {

            var sampleAnonLR = new { left = (TLeft)null, rightg = (IEnumerable<TRight>)null };
            var parmP = Expression.Parameter(sampleAnonLR.GetType(), "p");
            var parmC = Expression.Parameter(typeof(TRight), "c");
            var argLeft = Expression.PropertyOrField(parmP, "left");
            var newleftrs = CastSMBody(Expression.Lambda(Expression.Invoke(resultSelector, argLeft, parmC), new[] { parmP, parmC }), sampleAnonLR, (TRight)null, (TResult)null);

            return leftItems.AsQueryable().GroupJoin(rightItems, leftKeySelector, rightKeySelector, (left, rightg) => new { left, rightg }).SelectMany(r => r.rightg.DefaultIfEmpty(), newleftrs);
        }

        public static IQueryable<TResult> RightOuterJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector)
            where TLeft : class
            where TRight : class
            where TResult : class
        {

            var sampleAnonLR = new { leftg = (IEnumerable<TLeft>)null, right = (TRight)null };
            var parmP = Expression.Parameter(sampleAnonLR.GetType(), "p");
            var parmC = Expression.Parameter(typeof(TLeft), "c");
            var argRight = Expression.PropertyOrField(parmP, "right");
            var newrightrs = CastSMBody(Expression.Lambda(Expression.Invoke(resultSelector, parmC, argRight), new[] { parmP, parmC }), sampleAnonLR, (TLeft)null, (TResult)null);

            return rightItems.GroupJoin(leftItems, rightKeySelector, leftKeySelector, (right, leftg) => new { leftg, right }).SelectMany(l => l.leftg.DefaultIfEmpty(), newrightrs);
        }

        public static IQueryable<TResult> FullOuterJoinDistinct<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector)
            where TLeft : class
            where TRight : class
            where TResult : class
        {

            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Union(leftItems.RightOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }

        private static Expression<Func<TP, TResult>> CastSBody<TP, TResult>(LambdaExpression ex, TP unusedP, TResult unusedRes)
        {
            return (Expression<Func<TP, TResult>>)ex;
        }

        public static IQueryable<TResult> RightAntiSemiJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector)
            where TLeft : class
            where TRight : class
            where TResult : class
        {

            var sampleAnonLgR = new { leftg = (IEnumerable<TLeft>)null, right = (TRight)null };
            var parmLgR = Expression.Parameter(sampleAnonLgR.GetType(), "lgr");
            var argLeft = Expression.Constant(null, typeof(TLeft));
            var argRight = Expression.PropertyOrField(parmLgR, "right");
            var newrightrs = CastSBody(Expression.Lambda(Expression.Invoke(resultSelector, argLeft, argRight), new[] { parmLgR }), sampleAnonLgR, (TResult)null);

            return rightItems.GroupJoin(leftItems, rightKeySelector, leftKeySelector, (right, leftg) => new { leftg, right }).Where(lgr => !lgr.leftg.Any()).Select(newrightrs);
        }

        public static IQueryable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            this IQueryable<TLeft> leftItems,
            IQueryable<TRight> rightItems,
            Expression<Func<TLeft, TKey>> leftKeySelector,
            Expression<Func<TRight, TKey>> rightKeySelector,
            Expression<Func<TLeft, TRight, TResult>> resultSelector)
            where TLeft : class
            where TRight : class
            where TResult : class
        {

            return leftItems.LeftOuterJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector).Concat(leftItems.RightAntiSemiJoin(rightItems, leftKeySelector, rightKeySelector, resultSelector));
        }

        public static IQueryable<T> SelectGroup<T>(this IQueryable source) where T : class
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var resultType = typeof(T);

            Type enumerableType = typeof(IEnumerable<>).MakeGenericType(resultType);

            // Parse the lambda
            LambdaExpression lambda =
                System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(source.ElementType, enumerableType, "it", null);

            // Fix lambda by recreating to be of correct Func<> type in case 
            // the expression parsed to something other than IEnumerable<T>.
            // For instance, a expression evaluating to List<T> would result 
            // in a lambda of type Func<T, List<T>> when we need one of type
            // an Func<T, IEnumerable<T> in order to call SelectMany().
            Type inputType = source.Expression.Type.GetGenericArguments()[0];
            //Type resultType = lambda.Body.Type.GetGenericArguments()[0];
            Type delegateType = typeof(Func<,>).MakeGenericType(inputType, enumerableType);
            lambda = Expression.Lambda(delegateType, lambda.Body, lambda.Parameters);

            // Create the new query
            return source.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable), "SelectMany",
                    new Type[] { source.ElementType, resultType },
                    source.Expression, Expression.Quote(lambda))).OfType<T>();
        }
    }
}
