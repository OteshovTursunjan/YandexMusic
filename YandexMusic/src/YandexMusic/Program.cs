using YandexMusic;
using YandexMusic.Filter;
using YandexMusic.Middlewear;
using YandexMusic.Application;
using YandexMusic.DataAccess;
using YandexMusic.DataAccess.Authentication;
using YandexMusic.DataAccess.Persistance;
using Yandex.Shared.Service;
using Yandex.Shared.Service.lmpl;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidMethodAtributecs))
);

builder.Services.AddSwager();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddJwt(builder.Configuration); // Регистрация Jwt, которая, вероятно, уже настраивает аутентификацию
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy =>
        policy.RequireClaim(ClaimTypes.Role, "User"));
    options.AddPolicy("Admin", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));
});

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

app.UseAuthentication(); // Использование аутентификации
app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<TransactionMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddlewear>();
app.UseMiddleware<UserIdMiddleware>();  // Добавьте middleware здесь

app.MapControllers();

app.Run();

namespace MusicApp
{
    public partial class Program { }
}
