using Acelera.Core.Abstractions;
using Acelera.Core.Enums;
using Acelera.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Acelera.Infrastructure.Persistence;

/// <summary>
/// Database context
/// </summary>
public sealed class AceleraDbContext : DbContext
{
    /// <summary>
    /// City repository
    /// </summary>
    public DbSet<City> Cities => Set<City>();

    /// <summary>
    /// District repository
    /// </summary>
    public DbSet<District> Districts => Set<District>();

    /// <summary>
    /// Car repository
    /// </summary>
    public DbSet<Car> Cars => Set<Car>();
    
    /// <summary>
    /// User repository
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Account repository
    /// </summary>
    public DbSet<UserAccount> Accounts => Set<UserAccount>();

    /// <summary>
    /// Review repository
    /// </summary>
    public DbSet<Review> Reviews => Set<Review>();

    /// <summary>
    /// Provides a new instance of <see cref="AceleraDbContext"/>
    /// </summary>
    /// <param name="options">
    /// The options of the context
    /// </param>
    public AceleraDbContext(DbContextOptions<AceleraDbContext> options) : base(options) { }

    /// <summary>
    /// Saves all changes at the database
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token
    /// </param>
    /// <returns>
    /// The number of records modified 
    /// within the current transaction.
    /// </returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Handles the creation of a new table.
    /// </summary>
    /// <param name="modelBuilder">
    /// Table building controller
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var licenseCategoryConverter = new ValueConverter<List<LicenseCategory>, string[]>(
            v => v.Select(c => c.ToString()).ToArray(),
            v => v.Select(s => Enum.Parse<LicenseCategory>(s)).ToList()
        );

        var licenseCategoryComparer = new ValueComparer<List<LicenseCategory>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
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
            .Metadata.SetValueComparer(licenseCategoryComparer);

        modelBuilder.Entity<User>()
            .Property(u => u.Categories)
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

    /// <summary>
    /// Processes the data before saving the changes.
    /// </summary>
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
