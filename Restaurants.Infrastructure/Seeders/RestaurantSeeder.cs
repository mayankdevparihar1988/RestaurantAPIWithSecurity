using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

public class RestaurantSeeder(RestaurantDbContext restaurantsDbContext, ILogger<RestaurantSeeder> logger) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (restaurantsDbContext.Database.GetPendingMigrations().Any())
        {
            await restaurantsDbContext.Database.MigrateAsync();
        }

        if (await restaurantsDbContext.Database.CanConnectAsync())
        {
            if (!restaurantsDbContext.Restaurants.Any())
            {
                // Console log that seeder is initializing the db
                logger.LogInformation("Seeding restaurants Started...");
                var restaurants = GetRestaurants();
                restaurantsDbContext.Restaurants.AddRange(restaurants);
                await restaurantsDbContext.SaveChangesAsync();
                logger.LogInformation("Seeding restaurants Finished...");
            }

            /*
             // Adding default roles 
            if(!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
            */
        }
    }
/*
    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
            ];

        return roles;
    }
*/
    private IEnumerable<Restaurant> GetRestaurants()
    {
        /*
        User owner = new User()
        {
            Email = "seed-user@test.com"
        };
      */
        List<Restaurant> restaurants = [
            new()
            {
                // Owner = owner,
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                },
                
            },
            new ()
            {
               // Owner = owner,
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];

        return restaurants;
    }
}