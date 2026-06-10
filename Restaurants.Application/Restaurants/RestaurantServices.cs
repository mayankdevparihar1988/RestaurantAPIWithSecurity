using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Dtos.Restaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

public class RestaurantServices(ILogger<RestaurantServices> logger, IRestaurantRepository restaurantRepository, IMapper iMapper) : IRestaurantServices
{
    public  async Task<IEnumerable<RestaurantDto>> GetRestaurantsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRestaurants");
        var restaurants = await restaurantRepository.GetAllAsync(cancellationToken);
        var enumerable = restaurants as Restaurant[] ?? restaurants.ToArray();
        var result = iMapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantDto>>(enumerable);
        return result; // enumerable.Select(RestaurantDto.FromRestaurant);

    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRestaurantById");
        var result = await restaurantRepository.GetRestaurantByIdAsync(restaurantId, cancellationToken);
        if (result != null)
        {
            var mappedResult = iMapper.Map<Restaurant, RestaurantDto>(result);
            return mappedResult; //RestaurantDto.FromRestaurant(result);
        }
        return null;
    }

    public async Task<int?> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateRestaurant");
        var restaurant =  iMapper.Map<CreateRestaurantDto, Restaurant>(createRestaurantDto);
        var result = await restaurantRepository.CreateRestaurantAsync(restaurant, cancellationToken);
        return result;
    }
}