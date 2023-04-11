using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    [Table("CompanAncestors")]
    public partial class CompanyAncestor
    {
        public CompanyAncestor()
        {

        }

        public long Id { get; set; }
        public int CompanyId { get; set; }
        public int CompanyAncestorId { get; set; }
        [Required]
        [StringLength(250)]
        public string Code { get; set; }
        [StringLength(500)]
        public string Name { get; set; }

        [InverseProperty("Ancestors")]
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [InverseProperty("Descendants")]
        [ForeignKey("CompanyAncestorId")]
        public virtual Company Ancestor { get; set; }
    }
}
