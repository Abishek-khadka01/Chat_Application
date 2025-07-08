using Chat_Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Chat_Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string HashPasswordAsync(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hashbuilder = new StringBuilder();
                foreach(var x in bytes){
                    hashbuilder.Append(x.ToString("x2"));
                }

                return hashbuilder.ToString();
            }
        }

        public string GenerateRefreshToken(User user)
        {
            var secretkey = _configuration["RefreshTokenSecret"];
            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var claims = new[]
            {
               new Claim("username", user.Username),
               new Claim("email", user.Email), 
               new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],   
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256)
            );


            return new JwtSecurityTokenHandler().WriteToken(token); 


        }

        public bool VerifyPasswordAsync(string password, string hashedPassword)
        {
            string hashpassword = HashPasswordAsync(password);

            return hashpassword == hashedPassword;
        }

        public string GenerateAccessToken(User user)
        {
            var secretkey = _configuration["AcessTokenSecret"];
            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var claims = new[]
            {
               new Claim("username", user.Username),
               new Claim("email", user.Email),
               new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256)
            );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
