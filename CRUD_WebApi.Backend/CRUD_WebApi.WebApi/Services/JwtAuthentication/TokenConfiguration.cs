using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CRUD_WebApi.WebApi.Services.JwtAuthentication
{
    public class TokenConfiguration
    {
        public const string Issuer = "CRUD_WebApi";
        public const string Audience = "CRUD_WebApiClient";
        public const string SecretKey = "123131232131223213213131";

        public static SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfiguration.SecretKey));
        }
    }
}
