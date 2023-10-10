using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRUD_WebApi.WebApi.Services.JwtAuthentication
{
    public class JwtTokenService : ITokenService
    {
        public string CreateToken(int id)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = CreateClaims(id);

            var token = new JwtSecurityToken(
                TokenConfiguration.Issuer,
                TokenConfiguration.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = TokenConfiguration.GetKey();
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return signingCredentials;
        }

        private List<Claim> CreateClaims(int id)
        {
            var claims = new List<Claim>
            {
                new Claim("userId",id.ToString()),
            };
            return claims;
        }
    }
}
