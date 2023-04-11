using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class IdentityRefreshToken
    {
        [Key]
        [StringLength(900)]
        public string IdentityRefreshTokenId { get; set; }
        [Required]
        [StringLength(50)]
        public string Identity { get; set; }
        [Required]
        [StringLength(4000)]
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [Required]
        [StringLength(4000)]
        public string RefreshToken { get; set; }

        public bool IsExpired { get { return DateTime.UtcNow >= ExpiresUtc; } }
    }
}
