using System.Reflection;
using Backend.Database.Interceptors;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    AppDbInterceptor? interceptor = null
) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (interceptor != null)
            optionsBuilder.AddInterceptors(interceptor);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ResetPassword> ResetPasswords { get; set; } = null!;
    public DbSet<Recipient> Recipients { get; set; } = null!;
    public DbSet<Delivery> Deliveries { get; set; } = null!;
    public DbSet<DeliveryHistory> DeliveryHistories { get; set; } = null!;
    public DbSet<PackageType> PackageTypes { get; set; } = null!;
    public DbSet<SizeType> SizeTypes { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
}
