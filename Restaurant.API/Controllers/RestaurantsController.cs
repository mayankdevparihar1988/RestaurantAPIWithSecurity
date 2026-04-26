
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Domain;
namespace Restaurant.API.Controllers;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantServices restaurantServices)
    : ControllerBase
{
    // GET All Restaurants
   
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRestaurants");
        var results = await restaurantServices.GetRestaurantsAsync(cancellationToken);
        return Ok(results);
    }

    [HttpGet("{restaurantId}")]
    public async Task<IActionResult> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken)
    {
        logger.LogInformation("getRestaurantById");
        var result = await restaurantServices.GetRestaurantByIdAsync(restaurantId, cancellationToken);
        return Ok(result);
    }
   
}