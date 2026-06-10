using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateResturantCommandValidator: AbstractValidator<CreateRestaurantCommand>
{
    public CreateResturantCommandValidator()
    {
        RuleFor(restaurant => restaurant.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Restaurant name is required");
        
        RuleFor(restaurant => restaurant.Description)
            .NotEmpty()
            .WithMessage("Restaurant description is required");

        RuleFor(restaurant => restaurant.ContactEmail)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Restaurant contact email is required");
    }
}