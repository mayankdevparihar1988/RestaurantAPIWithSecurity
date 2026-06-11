using MediatR;
using Restaurants.Application.Restaurants.Dtos.Dishes;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurantQuery;

public class GetDishByIdForRestaurantQuery(int restaurantId, int dishId)  : IRequest<DishDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}