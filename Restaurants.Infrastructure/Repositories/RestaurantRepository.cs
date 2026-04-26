using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantRepository(RestaurantDbContext dbContext, ILogger<RestaurantRepository> logger)
    : IRestaurantRepository
{
    private RestaurantDbContext DbContext { get; } = dbContext;
    private ILogger<RestaurantRepository> Logger { get; } = logger;

    public async Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Retrieving all Restaurants");
        var result =  await DbContext.Restaurants.ToListAsync(cancellationToken);
        return result;
        
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken)
    {
        if (restaurantId == 0)
        {
            return null;
        }
        
        var result = await DbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId, cancellationToken);
        return result;
    }
}