using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken);
    Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken);
}