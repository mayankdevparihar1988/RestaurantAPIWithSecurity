using System.Diagnostics.Contracts;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos.Dishes;

public class DishDto
{
  
    public string id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    
    public int RestaurantId { get; set; }
    
    public int? KiloCalories { get; set; }

    public static DishDto FromEntity(Dish entity)
    {
        return new DishDto
        {
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            KiloCalories = entity.KiloCalories
        };
    }
}