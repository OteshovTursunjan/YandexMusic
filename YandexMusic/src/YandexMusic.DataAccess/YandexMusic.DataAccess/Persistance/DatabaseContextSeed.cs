
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;
using YandexMusic.DataAccess.Identity;

namespace YandexMusic.DataAccess.Persistance;

public class DatabaseContextSeed
{
   public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        if(!userManager.Users.Any())
        {
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };
            await userManager.CreateAsync(user, "Admin123.?");
        }
        await context.SaveChangesAsync();
    } 
}
