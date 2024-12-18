using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using YandexMusic.DataAccess.Identity;

namespace YandexMusic.DataAccess.Persistance;

public class AutomatedMigration
{

    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<DatabaseContext>();

        if (context.Database.IsNpgsql()) await context.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        await DatabaseContextSeed.SeedDatabaseAsync(context, userManager);
    }
}
