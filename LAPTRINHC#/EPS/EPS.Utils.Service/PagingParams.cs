using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Utils.Service
{
    public class PagingParams<TDto>
    {
        public static int DefaultPageSize = 10;

        public string SortExpression
        {
            get
            {
                return (string.IsNullOrEmpty(SortBy) ? "Id" : SortBy) + " " + (SortDesc ? "desc" : "asc");
            }
        }
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public int StartingIndex { get { return ItemsPerPage * (Page - 1); } }

        public PagingParams()
        {
            SortBy = "Id";
            SortDesc = true;
            ItemsPerPage = DefaultPageSize;
            Page = 0;
        }

        public virtual List<Expression<Func<TDto, bool>>> GetPredicates()
        {
            return new List<Expression<Func<TDto, bool>>>();
        }
    }

}
