
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yandex.Shared.Service;
using YandexMusic.DataAccess.Identity;
using YandexMusics.Core.Common;
using YandexMusics.Core.Entities.Music;

using YandexMusics.Core.Entities.Musics;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Reflection;

namespace YandexMusic.DataAccess.Persistance;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{

    private readonly IClaimService _claimService;

    public DatabaseContext(DbContextOptions options, IClaimService claimService) : base(options)
    {
        _claimService = claimService;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    public DbSet<Author> Author { get; set; }
    public DbSet<Dowloands> Dowloands { get; set; }
    public DbSet<Favourities> Favorities { get; set; }
    public DbSet<Genres> Genres { get; set; }
    public DbSet<Musics> Musics { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<Card_Type> Card_Types { get; set; }
    public DbSet<Cards> Cards { get; set; }
    public DbSet<Payment_History> Payment_History { get; set; }
    public DbSet<Tarrif_Type> Tarrif_Types { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatBy = _claimService.GetUserId();
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateBY = _claimService.GetUserId();
                    entry.Entity.UpdatedOn = DateTime.Now;
                    break;
            }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
