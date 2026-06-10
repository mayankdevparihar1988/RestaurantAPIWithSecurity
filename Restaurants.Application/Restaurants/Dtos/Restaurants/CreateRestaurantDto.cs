using Restaurants.Application.Restaurants.Dtos.Dishes;

namespace Restaurants.Application.Restaurants.Dtos.Restaurants;

public class CreateRestaurantDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    //By default true
    public bool HasDelivery { get; set; } = true;
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? LogoSasUrl { get; set; }
    public string? ContactNumber { get; set; }
    public string? ContactEmail { get; set; }
}