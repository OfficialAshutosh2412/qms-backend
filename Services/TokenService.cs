using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using QualityWebSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QualityWebSystem.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> GenerateJWTToken(AppUser user)
        {
            //fetching current user roles
            var userRoles = await _userManager.GetRolesAsync(user);

            //cretaing user claims.
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            //adding user role in user claims.
            foreach (var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //securing key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            //making of credential key
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //making token body or token itself.
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}