using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    [Table("UnitAncestors")]
    public partial class UnitAncestor
    {
        public UnitAncestor()
        {

        }

        public int Id { get; set; }
        public int UnitId { get; set; }
        public int UnitAncestorId { get; set; }
        [Required]
        [StringLength(250)]
        public string Code { get; set; }
        [StringLength(500)]
        public string Name { get; set; }

        [InverseProperty("Ancestors")]
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }
        [InverseProperty("Descendants")]
        [ForeignKey("UnitAncestorId")]
        public virtual Unit Ancestor { get; set; }
    }
}
