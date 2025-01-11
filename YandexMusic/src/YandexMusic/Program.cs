using YandexMusic;
using YandexMusic.Filter;
using YandexMusic.Middlewear;
using YandexMusic.Application;
using YandexMusic.DataAccess;
using YandexMusic.DataAccess.Authentication;
using YandexMusic.DataAccess.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
    config.Filters.Add(typeof(ValidMethodAtributecs))
);

builder.Services.AddSwager();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);



builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOptions"));




builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy =>
        policy.RequireClaim(ClaimTypes.Role, "User"));
    options.AddPolicy("Admin", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

var jwtOption = builder.Configuration.GetSection("JwtOptions").Get<JwtOption>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOption.Issuer,
            ValidAudience = jwtOption.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yandex"); });

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserIdMiddleware>();

app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<TransactionMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddlewear>();
app.MapControllers();

app.Run();

namespace MusicApp
{
    public partial class Program { }
}
