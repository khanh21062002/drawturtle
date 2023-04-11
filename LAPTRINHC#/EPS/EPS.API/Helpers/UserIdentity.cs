using EPS.Utils.Repository.Audit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EPS.API.Helpers
{
    public class UserIdentity : IUserIdentity<int>
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public UserIdentity(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        private string GetClaimValue(string claimType)
        {
            var claim = _claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim == null ? null : claim.Value;
        }

        public int UserId { get => Convert.ToInt32(GetClaimValue(CustomClaimTypes.UserId)); }
        public string Username { get => GetClaimValue(ClaimTypes.NameIdentifier); }
        public List<string> Privileges { get => GetClaimValue(CustomClaimTypes.Privileges).Split(",").ToList(); }
        public bool IsAdministrator { get => Convert.ToBoolean(GetClaimValue(CustomClaimTypes.Privileges)); }
        public int CompanyId { get => Convert.ToInt32(GetClaimValue(CustomClaimTypes.CompanyId)); }
    }
}
