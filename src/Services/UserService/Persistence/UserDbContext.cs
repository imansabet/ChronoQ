using Microsoft.EntityFrameworkCore;
using UserService.Models.Domain.Entities;

namespace UserService.Persistence;

public class UserDbContext(DbContextOptions<UserDbContext> options ) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<CustomerProfile> Customers => Set<CustomerProfile>();
    public DbSet<ProviderProfile> Providers => Set<ProviderProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(u => u.FullName).IsRequired();
            
            entity.HasOne(u => u.CustomerProfile)
                .WithOne()
                .HasForeignKey<CustomerProfile>(c => c.UserId);

            entity.HasOne(u => u.ProviderProfile)
                .WithOne()
                .HasForeignKey<ProviderProfile>(p => p.UserId);

        });
        
        modelBuilder.Entity<CustomerProfile>(entity =>
        {
            entity.HasKey(c => c.Id);
        });

        modelBuilder.Entity<ProviderProfile>(entity =>
        {
            entity.HasKey(p => p.Id);
        });
    }
}