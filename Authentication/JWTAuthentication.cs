using Microsoft.IdentityModel.Tokens;
using CMS_appBackend.DTOs.ResponseModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS_appBackend.Authentication
{
    public class JWTAuthentication : IJWTAuthentication
    {
        public string _key;
        public JWTAuthentication(string key)
        {
            _key = key;
        }
        public string GenerateToken(UserResponseModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Data.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, model.Data.Username));
            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
