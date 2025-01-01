using YandexMusic;
using YandexMusic.Filter;
using YandexMusic.Middlewear;
using YandexMusic.Application;
using YandexMusic.DataAccess;
using YandexMusic.DataAccess.Authentication;
using YandexMusic.DataAccess.Persistance;
using Yandex.Shared.Service;
using Yandex.Shared.Service.lmpl;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidMethodAtributecs))
);

builder.Services.AddSwaggerGen();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

using var scope = app.Services.CreateScope();
await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

// Middleware setup
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

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<TransactionMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddlewear>();

app.MapControllers();

app.Run();

namespace MusicApp
{
    public partial class Program { }
}
