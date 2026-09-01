using Acelera.Core.Abstractions;
using Acelera.Core.Enums;
using Acelera.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Acelera.Infrastructure.Persistence;

public sealed class AceleraDbContext : DbContext
{
    public DbSet<City> Cities => Set<City>();

    public DbSet<District> Districts => Set<District>();

    public DbSet<Car> Cars => Set<Car>();
    
    public DbSet<User> Users => Set<User>();

    public DbSet<UserAccount> Accounts => Set<UserAccount>();

    public DbSet<Review> Reviews => Set<Review>();

    public AceleraDbContext(DbContextOptions<AceleraDbContext> options) : base(options) { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var licenseCategoryConverter = new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<List<LicenseCategory>, string[]>(
            v => v.Select(c => c.ToString()).ToArray(),
            v => v.Select(s => Enum.Parse<LicenseCategory>(s)).ToList()
        );

        modelBuilder.Entity<City>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<District>()
            .HasIndex(d => new { d.CityId, d.Name })
            .IsUnique();

        modelBuilder.Entity<UserAccount>()
            .HasIndex(a => new { a.UserId, a.Provider })
            .IsUnique();

        modelBuilder.Entity<UserAccount>()
            .HasIndex(a => new { a.Provider, a.AccountId })
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Profile)
            .HasMethod("gin");

        modelBuilder.Entity<UserAccount>()
            .Property(s => s.Provider)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();        

        modelBuilder.Entity<User>()
            .Property(u => u.Categories)
            .HasConversion(licenseCategoryConverter)
            .HasColumnType("text[]");

        modelBuilder.Entity<Review>()
            .HasIndex(r => new { r.ReviewerId, r.InstructorId })
            .IsUnique();

        modelBuilder.Entity<Review>()
            .HasIndex(r => new { r.InstructorId, r.CreatedAt });

        modelBuilder.Entity<District>()
            .HasOne(d => d.City)
            .WithMany(c => c.Districts)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserAccount>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Districts)
            .WithMany(d => d.Users)
            .UsingEntity(
                "district_users",
                l => l.HasOne(typeof(District)).WithMany().HasForeignKey("district_id"),
                r => r.HasOne(typeof(User)).WithMany().HasForeignKey("user_id"),
                j => j.HasKey("user_id", "district_id"));
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries<ITimeTrackable>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
