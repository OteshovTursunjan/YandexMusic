using Microsoft.Extensions.Configuration;
using YandexMusic.DataAccess;
using YandexMusic.DataAccess.Persistance;
using Yandex.Shared.Service.lmpl;
using Yandex.Shared.Service;
using YandexMusic.Application;
using YandexMusic.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDataAccess(builder.Configuration).AddApplication(builder.Environment);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IClaimService, ClaimService>();



var app = builder.Build();

using var scope = app.Services.CreateScope();


await AutomatedMigration.MigrateAsync(scope.ServiceProvider);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
namespace MusicApp
{
    public partial class Program { }
}