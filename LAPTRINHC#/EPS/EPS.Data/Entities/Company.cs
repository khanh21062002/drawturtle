using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class Company : ICascadeDelete
    {
        public Company()
        {
            Users = new HashSet<User>();
            Roles = new HashSet<Role>();
            Ancestors = new HashSet<CompanyAncestor>();
            Descendants = new HashSet<CompanyAncestor>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Code { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [StringLength(250)]
        public string Level { get; set; }

        [ForeignKey("ParentId")]
        public virtual Company Parent { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<CompanyAncestor> Ancestors { get; set; }
        [InverseProperty("Ancestor")]
        public virtual ICollection<CompanyAncestor> Descendants { get; set; }
        public void OnDelete()
        {
            
        }
    }
}
