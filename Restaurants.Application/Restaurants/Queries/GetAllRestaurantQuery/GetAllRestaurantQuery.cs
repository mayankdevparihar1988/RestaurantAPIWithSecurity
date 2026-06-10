using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurantQuery;

public class GetAllRestaurantQuery: IRequest<IEnumerable<RestaurantDto>>
{
    
}