using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class IdentityClient
    {
        [Key]
        [StringLength(900)]
        public string IdentityClientId { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(4000)]
        public string SecretKey { get; set; }
        public int ClientType { get; set; }
        public bool IsActive { get; set; }
        public int RefreshTokenLifetime { get; set; }
        [StringLength(1000)]
        public string AllowedOrigin { get; set; }
    }
}
