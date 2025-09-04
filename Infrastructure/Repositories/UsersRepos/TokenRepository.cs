using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clean_E_Commerce_Project.Infrastructure.Repositories.UsersRepos
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id),
                 new Claim(ClaimTypes.Name, user.UserName), 
                 new Claim(JwtRegisteredClaimNames.Iss, "https://localhost:7264/"),
                  new Claim(JwtRegisteredClaimNames.Aud, "https://localhost:7264/"), 
                
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],     
                audience: configuration["Jwt:Audience"],        
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),   // 30 minute expiration
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
