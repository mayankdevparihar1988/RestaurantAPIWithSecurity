using MediatR;
using Restaurants.Application.Restaurants.Dtos.Dishes;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurantQuery;

public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
}
