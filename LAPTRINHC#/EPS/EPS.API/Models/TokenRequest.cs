using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Models
{
    public class TokenRequestParams
    {
        [Required]
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
        [Required]
        public string client_id { get; set; }
        [Required]
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
