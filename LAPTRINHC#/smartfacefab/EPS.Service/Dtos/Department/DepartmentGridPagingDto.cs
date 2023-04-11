using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Department
{
    public class DepartmentGridPagingDto : PagingParams<DepartmentGridDto>
    {
        public string FilterText { get; set; }
        public Nullable<int> department_ID { get; set; }
        public Nullable<int> compId { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<int> grade_id { get; set; }
        public Nullable<int> Type { get; set; }
        public string HiddenParentField { get; set; }
        public override List<Expression<Func<DepartmentGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Code.Contains(FilterText.Trim()) || x.Name.Contains(FilterText.Trim()));
            }

            //if (department_ID.HasValue)
            //{
            //    //Tìm kiếm theo đơn vị Id
            //    predicates.Add(x => x.DEPARTMENT_ID == department_ID);
            //}
            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.ComId == compId);
            }
            if (Type.HasValue && Type.Value == 1)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.Type == Type);
            }
            else
            {
                predicates.Add(x => x.Type != 1);
            }


            if (grade_id.HasValue)
            {
                //Tìm kiếm theo khối
                predicates.Add(x => x.GradesId == grade_id);
            }
            return predicates;
        }
    }
}
