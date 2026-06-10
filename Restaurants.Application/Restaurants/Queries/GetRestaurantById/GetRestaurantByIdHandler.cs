using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdHandler(ILogger<GetRestaurantByIdHandler> logger, IRestaurantRepository restaurantRepository, IMapper iMapper): IRequestHandler<GetRestaurantByIdQuery,RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRestaurantById");
        var restaurantId = request.Id;
        var result = await restaurantRepository.GetRestaurantByIdAsync(restaurantId, cancellationToken);
        if (result != null)
        {
            var mappedResult = iMapper.Map<Restaurant, RestaurantDto>(result);
            return mappedResult; //RestaurantDto.FromRestaurant(result);
        }
        return null;
    }
}