using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using CRUD_WebApi.Application.Interface;

namespace CRUD_WebApi.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var conectionString = configuration["DbConnection"];

            services.AddDbContext<WebApiDbContext>(options =>
            {
                options.UseSqlServer(conectionString);
            });

            services.AddScoped<IWebApiDbContext>(provider =>
                provider.GetService<WebApiDbContext>());

            return services;
        }
    }
}
