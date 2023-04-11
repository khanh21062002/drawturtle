using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Common
{
    public class UserNameItem
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public Nullable<int> filterId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CompId { get; set; }
        public string GroupName { get; set; }
        public string Language { get; set; }
    }
}
