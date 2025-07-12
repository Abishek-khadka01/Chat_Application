using Chat_Application.Models;
using System.Security.Cryptography;


namespace Chat_Application.Interface 
{
    public interface IAuthService
    {

        public string HashPasswordAsync(string password);

        public string GenerateRefreshToken(User user);

        public bool VerifyPasswordAsync(string password, string hashedPassword);

        public string GenerateAccessToken(User user);
    }
}
