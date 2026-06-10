using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos.Restaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantHandler(ILogger<UpdateRestaurantHandler> logger, IMapper iMapper, IRestaurantRepository restaurantRepository): IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateRestaurant");
        var id = request.Id;
        
        
        var result = await restaurantRepository.GetRestaurantByIdAsync(id,cancellationToken);
            //.CreateRestaurantAsync(restaurant, cancellationToken);
            if (result == null)
            {
                logger.LogError("Restaurant not found");
                return false;
            }
            
            result.Name = request.Name;
            result.HasDelivery = request.HasDelivery;
            result.Category = request.Category;
            result.ContactEmail = request.ContactEmail;
            result.Description = request.Description;
            
        await restaurantRepository.SaveAsync(cancellationToken);
            
        return true;
    }
}