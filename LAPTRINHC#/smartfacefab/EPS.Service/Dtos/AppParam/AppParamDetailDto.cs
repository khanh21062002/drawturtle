using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.AppParam
{
    public class AppParamDetailDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string ParamName { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public Nullable<int> Position { get; set; }
        public string ErrorCode { get; set; }

    }
}
