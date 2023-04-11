using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Common
{
    public class SelectItem
    {
        public string Id { get; set; }
        public string Value { get { return Id; } set { Id = value; } }
        public string Text { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CompId { get; set; }
        public int? Status { get; set; }
    }
}
