using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Company
{
    public class CompanyDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Level { get; set; }
        public string ParentName { get; set; }
    }
}
