using EPS.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EPS.API.Helpers;
using EPS.Data.Entities;

namespace EPS.API.Controllers
{
    [Route("api/token")]
    public class TokenController : BaseController
    {
        private Data.EPSContext _context;

        //some config in the appsettings.json
        private IOptions<Audience> _settings;
        //repository to handler the sqlite database

        public TokenController(IOptions<Audience> settings, Data.EPSContext context)
        {
            this._settings = settings;
            _context = context;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth(TokenRequestParams parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify client's identification
            var client = await _context.IdentityClients.SingleOrDefaultAsync(x => x.IdentityClientId == parameters.client_id && x.SecretKey == parameters.client_secret);

            if (client == null)
            {
                return BadRequest("Unauthorized client.");
            }

            if (parameters.grant_type == "password")
            {
                return await DoPassword(parameters, client);
            }
            else if (parameters.grant_type == "refresh_token")
            {
                return await DoRefreshToken(parameters, client);
            }
            else if (parameters.grant_type == "invalidate_token")
            {
                return await DoInvalidateToken(parameters);
            }
            else
            {
                return BadRequest("Invalid grant type.");
            }
        }

        private async Task<IActionResult> DoInvalidateToken(TokenRequestParams parameters)
        {
            var token = await _context.IdentityRefreshTokens.FirstOrDefaultAsync(x => x.RefreshToken == parameters.refresh_token);

            if (token == null)
            {
                return Ok();
            }

            _context.Remove(token);

            await _context.SaveChangesAsync();

            return Ok();
        }

        //scenario 1 ： get the access-token by username and password
        private async Task<IActionResult> DoPassword(TokenRequestParams parameters, IdentityClient client)
        {
            //validate the client_id/client_secret/username/password                                          
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == parameters.username && x.IsDelete == false);

            if (user == null)
            {
                return BadRequest("Invalid user infomation.");
            }

            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, parameters.password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return BadRequest("Invalid user infomation.");
            }

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var rToken = new IdentityRefreshToken
            {
                ClientId = parameters.client_id,
                RefreshToken = refresh_token,
                IdentityRefreshTokenId = Guid.NewGuid().ToString(),
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(client.RefreshTokenLifetime),
                Identity = parameters.username
            };

            //store the refresh_token
            _context.IdentityRefreshTokens.Add(rToken);

            await _context.SaveChangesAsync();

            return Ok(GetJwt(parameters.client_id, refresh_token, user));
        }

        //scenario 2 ： get the access_token by refresh_token
        private async Task<IActionResult> DoRefreshToken(TokenRequestParams parameters, IdentityClient client)
        {
            var token = await _context.IdentityRefreshTokens.FirstOrDefaultAsync(x => x.RefreshToken == parameters.refresh_token);

            if (token == null)
            {
                return BadRequest("Token not found.");
            }

            if (token.IsExpired)
            {
                // Remove refresh token if expired
                _context.IdentityRefreshTokens.Remove(token);
                await _context.SaveChangesAsync();

                return BadRequest("Token has expired.");
            }

            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            //remove the old refresh_token and add a new refresh_token
            _context.IdentityRefreshTokens.Remove(token);

            _context.IdentityRefreshTokens.Add(new IdentityRefreshToken
            {
                ClientId = parameters.client_id,
                RefreshToken = refresh_token,
                IdentityRefreshTokenId = Guid.NewGuid().ToString(),
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(client.RefreshTokenLifetime),
                Identity = token.Identity
            });

            await _context.SaveChangesAsync();

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == token.Identity);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            return Ok(GetJwt(parameters.client_id, refresh_token, user));
        }

        private string GetJwt(string client_id, string refresh_token, User user)
        {
            var now = DateTime.UtcNow;

            var privileges = user.UserPrivileges.Select(x => x.PrivilegeId)
                .Union(user.UserRoles.SelectMany(x => x.Role.RolePrivileges.Select(y => y.PrivilegeId)))
                .Distinct()
                .ToList();

            var claims = new Claim[]
            {
                new Claim(CustomClaimTypes.ClientId, client_id),
                new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FullName, ClaimValueTypes.String),
                new Claim(ClaimTypes.NameIdentifier, user.UserName, ClaimValueTypes.String),
                new Claim(CustomClaimTypes.Privileges, string.Join(",", privileges)),
                new Claim(CustomClaimTypes.CompanyId, user.CompanyId.ToString())
            };

            var symmetricKeyAsBase64 = _settings.Value.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var expires = now.Add(TimeSpan.FromMinutes(1));

            var jwt = new JwtSecurityToken(
                issuer: _settings.Value.Iss,
                audience: _settings.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires,
                refresh_token,
                fullName = user.FullName,
                username = user.UserName,
                companyId = user.CompanyId,
                userId = user.Id,
                company = user.Company.Name,
                privileges
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
