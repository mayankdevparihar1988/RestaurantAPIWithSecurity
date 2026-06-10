using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken cancellationToken);
    Task<Restaurant?> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken);
    Task<int?> CreateRestaurantAsync(Restaurant? restaurant, CancellationToken cancellationToken);
    
    Task<bool> UpdateRestaurant(Restaurant? restaurant, CancellationToken cancellationToken);
    
    Task<int> SaveAsync(CancellationToken cancellationToken);
}