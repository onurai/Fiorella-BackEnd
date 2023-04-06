using Fiorella.Data.Context;
using Fiorella.Data.Entity;
using Fiorella.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiorella.Repository.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        

        public AuthRepository(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            
        }

        public Task<string> GenerateJWTToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:SigningKey"));
            //_configuration.GetSection("Appsettings:Token").Value
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Firstname),
            };

            //if (user.roles.count > 0)
            //{
            //    claims.addrange(user.roles.select(rolemanager => new claim(claimtypes.role, role)));
            //}

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "example.com",
                Audience = "example.com",
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));

        }
    }
}
