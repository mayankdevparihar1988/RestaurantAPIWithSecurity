
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Dtos.Restaurants;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurantQuery;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain;
namespace Restaurant.API.Controllers;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantServices restaurantServices, IMediator mediator)
    : ControllerBase
{
    // GET All Restaurants
   
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GetRestaurants");
        var results = await mediator.Send(new GetAllRestaurantQuery(),cancellationToken);
        return Ok(results);
    }

    [HttpGet("{restaurantId}")]
    public async Task<IActionResult> GetRestaurantByIdAsync(int restaurantId, CancellationToken cancellationToken)
    {
        logger.LogInformation("getRestaurantById");
        var query = new GetRestaurantByIdQuery(restaurantId);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRestaurantAsync([FromBody]CreateRestaurantCommand createRestaurantCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateRestaurant");
        // call the correct handler using mediator 
       var result =  await mediator.Send(createRestaurantCommand, cancellationToken);
       
        return Ok(result);
    }
    
    [HttpPatch("{restaurantId}")]
    public async Task<IActionResult> UpdateRestaurantAsync(int restaurantId,[FromBody] UpdateRestaurantCommand?  updateRestaurantCommand, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateRestaurant");

        if (updateRestaurantCommand == null || restaurantId == null)
        {
            return BadRequest();
        }
        
        updateRestaurantCommand.Id = restaurantId;
        
        // call the correct handler using mediator 
        var result =  await mediator.Send(updateRestaurantCommand, cancellationToken);
       
        return Ok(result);
    }
    
   
}