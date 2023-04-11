using EPS.Utils.Common;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.Device
{
    public class DeviceGridPagingDto : PagingParams<DeviceGridDto>
    {
        public string filterText { get; set; }
        public Nullable<int> featuresId { get; set; }
        public Nullable<int> areaId { get; set; }
        public Nullable<int> compId { get; set; }
        public string FilterName { get; set; }
        public string areaName { get; set; }

        public override List<Expression<Func<DeviceGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(filterText)) 
            {
                predicates.Add(x => x.DeviceCode.Contains(filterText.Trim()) || x.DeviceName.Contains(filterText.Trim()));

            }
       
            if (featuresId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.FeatureId.Contains(featuresId.ToString()));

            }
            if (!string.IsNullOrEmpty(areaName))
            {
                predicates.Add(x => x.AreaName.Contains(areaName));

            }
            if (areaId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.AreaId == areaId.ToString());

            }
            if (compId.HasValue)
            {
                //Tìm kiếm theo đơn vị Id
                predicates.Add(x => x.CompId == compId);

            }
            return predicates;
        }
    }
}
