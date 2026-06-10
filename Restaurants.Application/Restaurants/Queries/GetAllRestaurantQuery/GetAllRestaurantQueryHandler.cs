using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurantQuery;

public class GetAllRestaurantQueryHandler(ILogger<GetAllRestaurantQueryHandler>logger, IMapper iMapper, IRestaurantRepository restaurantRepository): IRequestHandler<GetAllRestaurantQuery,IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    { 
        logger.LogInformation("GetRestaurants");
        var restaurants = await restaurantRepository.GetAllAsync(cancellationToken);
        var enumerable = restaurants as Restaurant[] ?? restaurants.ToArray();
        var result = iMapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantDto>>(enumerable);
        return result; // enumerable.Select(RestaurantDto.FromRestaurant);
    }
}