using Microsoft.AspNetCore.Http.HttpResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurantQuery;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurantQuery;
using Restaurants.Application.Restaurants.Dtos.Dishes;

namespace Restaurant.API.Controllers;
[ApiController]
[Route("api/restaurant/{restaurantId}/dishes")]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDishes([FromRoute]int restaurantId, CreateDishCommand command,CancellationToken cancellationToken)
    {
        command.RestaurantId = restaurantId;
       var dishId= await mediator.Send(command, cancellationToken);
       StatusCode(StatusCodes.Status201Created);
        return Ok(dishId);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute]int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }
}