using Microsoft.Extensions.Configuration;
using YandexMusic.DataAccess;
using YandexMusic.DataAccess.Persistance;
using Yandex.Shared.Service.lmpl;
using Yandex.Shared.Service;
using YandexMusic.Application;
using YandexMusic.DataAccess;
using YandexMusic.Application.Services;
using YandexMusic.DataAccess.Repository.lmpl;
using YandexMusic.DataAccess.Repository;
using YandexMusic.Application.Services.lmpl;
using YandexMusic.DataAccess.Authentication;

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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICard_TypeService, Card_TypeService>();
builder.Services.AddScoped<ICard_TypeRepository ,  Card_TypeRepository>();
builder.Services.AddScoped<ITarrifTypeService, TarrifTypeService>();
builder.Services.AddScoped<ITarrift_TypeRepository, Tarrif_TypeRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IJwtTokenHandler , JwtTokenHandler>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
builder.Services.AddScoped<IPayment_HistoryRepository, Payment_HistoryRepository>();
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