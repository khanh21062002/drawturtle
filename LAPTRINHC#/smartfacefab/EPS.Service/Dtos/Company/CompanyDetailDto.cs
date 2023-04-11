using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Company
{
    public class CompanyDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> SupId { get; set; }
        public Nullable<int> ComLevel { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }
        public Nullable<int> ComIdMap { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Tel { get; set; }
        public Nullable<int> Status { get; set; }
        public string StatusName { get; set; }
        public bool IsDelete { get; set; }
        public string SupName { get; set; }

        public Nullable<int> ParentId { get; set; }
        public string ParentName { get; set; }
        public string HiddenParentField { get; set; }

        public Nullable<int> TreeLevel { get; set; }

        public Nullable<int> CompanyType { get; set; }

        public string CompanyTypeStr { get; set; }
    }

}
