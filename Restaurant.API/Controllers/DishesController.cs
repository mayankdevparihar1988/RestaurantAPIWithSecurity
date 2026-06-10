using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands;

namespace Restaurant.API.Controllers;
[ApiController]
[Route("api/restaurant/{restaurantId}/dishes")]
public class DishesController : ControllerBase
{
    [HttpPost]
    public  Task<IActionResult> CreateDishes([FromRoute]int restaurantId, CreateDishCommand command,CancellationToken cancellationToken)
    {
         return Task.FromResult<IActionResult>(Ok("Thanks"));
    }
    
}