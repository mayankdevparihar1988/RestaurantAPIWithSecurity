using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos.Restaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantHandler(ILogger<CreateRestaurantHandler> logger, IMapper iMapper, IRestaurantRepository restaurantRepository): IRequestHandler<CreateRestaurantCommand, int?>
{
    public async Task<int?> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateRestaurant");
        var restaurant =  iMapper.Map<CreateRestaurantCommand, Restaurant>(request);
        var result = await restaurantRepository.CreateRestaurantAsync(restaurant, cancellationToken);
        return result;
    }
}