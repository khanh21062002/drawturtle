using EPS.Service.Dtos.FAQuestions;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.FAQuestions
{
    public class FAQuestionsGirdPagingDto
    {
        public class FAQuestionsGridPagingDto : PagingParams<FAQuestionsGridDto>
        {
            public string FilterText { get; set; }
            public override List<Expression<Func<FAQuestionsGridDto, bool>>> GetPredicates()
            {
                var predicates = base.GetPredicates();
                if (!string.IsNullOrEmpty(FilterText))
                {
                    FilterText = FilterText.Trim();
                    predicates.Add(x => x.Name.Contains(FilterText));
                }
                
                return predicates;
            }
        }
     }
}
