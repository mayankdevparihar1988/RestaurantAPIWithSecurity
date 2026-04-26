using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

public class RestaurantServices(ILogger<RestaurantServices> logger, IRestaurantRepository restaurantRepository) : IRestaurantServices
{
    public  async Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRestaurants");
        var restaurants = await restaurantRepository.GetAllAsync(cancellationToken);
        var enumerable = restaurants as Restaurant[] ?? restaurants.ToArray();
        return enumerable.Select(RestaurantDto.FromRestaurant);
        
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRestaurantById");
        var result = await restaurantRepository.GetRestaurantByIdAsync(restaurantId, cancellationToken);
        return RestaurantDto.FromRestaurant(result);
    }
}