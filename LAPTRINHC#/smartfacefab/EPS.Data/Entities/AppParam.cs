using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace EPS.Data.Entities
{
    
    public partial class AppParam
    {
    
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string ParamName { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public string ErrorCode { get; set; }
        public Nullable<int> Position { get; set; }



    }
}
