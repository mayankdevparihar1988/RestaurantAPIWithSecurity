using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddRestaurantDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // getting connection string from RestaurantsDb in applicationsettings
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        // Adding it using dependency injection
        services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
    }
}