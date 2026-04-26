using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantServices
{
    Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync(CancellationToken cancellationToken);
    Task<RestaurantDto?> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken);
}