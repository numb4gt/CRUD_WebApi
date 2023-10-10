using CRUD_WebApi.WebApi.Services.JwtAuthentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CRUD_WebApi.WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureJWT(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(config =>
               {
                   config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidIssuer = TokenConfiguration.Issuer,
                       ValidAudience = TokenConfiguration.Audience,
                       IssuerSigningKey = TokenConfiguration.GetKey()
                   };
               });
        }
    }
}
