using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chat_application.Models;
using Microsoft.IdentityModel.Tokens;

namespace Chat_Application.src.common
{
    public class GenerateJwtToken
    {
        private readonly IConfiguration _configuration;

        public GenerateJwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, string SecretKey, int minutesValid)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtKeys:Issuer"],
                audience: _configuration["JwtKeys:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutesValid),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
