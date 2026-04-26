using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
{
    internal DbSet<Restaurant>  Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=RestaurantDb;User Id=sa;Password=ChangeMe_StrongP@ssw0rd;TrustServerCertificate=True;");
    }
    */
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);
        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne(d => d.Restaurant)
            .HasForeignKey(r => r.RestaurantId);
    }   
}