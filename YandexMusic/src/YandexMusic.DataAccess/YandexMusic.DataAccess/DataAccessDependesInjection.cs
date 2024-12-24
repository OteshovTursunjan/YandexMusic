using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using YandexMusic.DataAccess.Persistance;
using YandexMusic.DataAccess.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;
namespace YandexMusic.DataAccess;


public static class DataAccessDependesInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddIdentity();

        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        //services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        //services.AddScoped<ITodoListRepository, TodoListRepository>();
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfig = configuration.GetSection("Database").Get<DatabaseConfiguration>();

        if (databaseConfig.UseInMemoryDatabase)
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("YandexDatabase");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        else
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(databaseConfig.ConnectionString,
                    opt => opt.MigrationsAssembly("YandexMusic.DataAccess")));  // Убедитесь, что здесь указана правильная сборка миграций
    }


    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DatabaseContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });
    }
}

// TODO move outside?
public class DatabaseConfiguration
{
    public bool UseInMemoryDatabase { get; set; }

    public string ConnectionString { get; set; }
}