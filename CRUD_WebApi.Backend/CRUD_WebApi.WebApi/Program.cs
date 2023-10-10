using CRUD_WebApi.Persistence;
using CRUD_WebApi.Application.Common.Mappings;
using CRUD_WebApi.Application.Interface;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CRUD_WebApi.Application;
using CRUD_WebApi.WebApi.Middleware;
using CRUD_WebApi.WebApi.Extensions;
using CRUD_WebApi.WebApi.Services.JwtAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.SwaggerGen;
using CRUD_WebApi.WebApi.Swagger;
using Serilog;
using Serilog.Events;
using Serilog.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("CRUDWebApi-.txt", rollingInterval:
        RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddAuthorization(opt => opt.DefaultPolicy =
    new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());

var services = builder.Services;

services.ConfigureJWT();

services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IWebApiDbContext).Assembly));
});

services.AddSingleton<ITokenService, JwtTokenService>();

services.AddApplication();
services.AddPersistence(builder.Configuration);
services.AddControllers();
services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
    ConfigureSwaggerOptions>();
services.AddSwaggerGen(config =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "CRUD_WebApi");
});

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var scope = builder.Services.BuildServiceProvider().CreateScope()) // invoke method of Db initialization
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<WebApiDbContext>(); // for accessing dependencies
        DbInitializer.Initialize(context); // initialize database
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "An error occurded while app initialization");
    }
}

app.Run();
