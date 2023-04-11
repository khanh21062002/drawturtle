using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int SupComId { get; set; }
        public Nullable<int> SupId { get; set; }
        public Nullable<int> ComLevel { get; set; }
        public string Position { get; set; }
        public string Code { get; set; }
        public Nullable<int> ComIdMap { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Tel { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Company Parent { get; set; }
        public string HiddenParentField { get; set; }
        public Nullable<int> TreeLevel { get; set; }
        public Nullable<int> CompanyType { get; set; }

    }
}
