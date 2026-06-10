using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Dtos.Restaurants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantServices
{
    Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync(CancellationToken cancellationToken);
    Task<RestaurantDto?> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken);
    Task<int?> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto, CancellationToken cancellationToken);
}