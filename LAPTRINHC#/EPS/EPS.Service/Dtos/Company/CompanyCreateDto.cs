using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Company
{
    public class CompanyCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Level { get; set; }
    }
}
