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

using Quartz;
using Microsoft.Extensions.DependencyInjection;
using YandexMusics.Core.Entities.Music;
using YandexMusic.Application.Quartz;
using YandexMusic.Application.Features.Author.Commands;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddQuartz(q =>
//{
//    var jobKey = new JobKey("MonthlyPaymnet");
//    q.AddJob<MonthlyPaymnet>(opts => opts.WithIdentity(jobKey));
//    q.AddTrigger(opts => opts
//        .ForJob(jobKey)
//        .WithIdentity("MonthlyPaymentTrigger")
//        .WithCronSchedule("0 0 0 1 * ?"));

//});


builder.Services.AddQuartz(q =>
{
    q.AddJob<DailyLogging>(opts => opts.WithIdentity("DailyLogging", "group1"));

    q.AddTrigger(opts => opts
        .ForJob("DailyLogging", "group1") // ��� ��������� � AddJob
        .WithIdentity("DailyTrigger", "group1")
        .StartNow()
        .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever()));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


//builder.Services.AddQuartzHostedService(z => z.WaitForJobsToComplete = true);

builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidMethodAtributecs))
);

builder.Services.AddSwager();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddJwt(builder.Configuration); 
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

app.UseAuthentication(); // ������������� ��������������
app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<TransactionMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddlewear>();
app.UseMiddleware<UserIdMiddleware>();  // �������� middleware �����
app.UseMiddleware<LogginMiddleware>();
app.MapControllers();

app.Run();

namespace MusicApp
{
    public partial class Program { }
}
