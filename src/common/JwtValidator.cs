

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Chat_Application.src.common
{

    public  class JwtValidator(IConfiguration configuration)
    {
        public bool ValidateToken(string token, string SecretKey, out JwtSecurityToken jwtToken)
        {
            var validationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = configuration["JwtKeys:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JwtKeys:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true

            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                jwtToken = (JwtSecurityToken)validatedToken;

                return true;  
            }
            catch (SecurityTokenValidationException error)
            {
                Log.Error($"Error in the validation of the token  {error.Message}");
                jwtToken = null;                
                return false;
            }


        }
    }

}