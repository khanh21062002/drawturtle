using EPS.Utils.Common;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.DeviceFeatures
{
    public class DeviceFeaturesGridPagingDto : PagingParams<DeviceFeaturesGridDto>
    {
        public string FilterCode { get; set; }
        public Nullable<int> featuresId { get; set; }

        public string FilterName { get; set; }

        public override List<Expression<Func<DeviceFeaturesGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();
            predicates.Add(x => x.IsDelete == false);
      

            return predicates;
        }
    }
}
